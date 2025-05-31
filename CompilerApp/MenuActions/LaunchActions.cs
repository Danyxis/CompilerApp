using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CompilerApp.PolishReverseNotation;
using CompilerApp.RegexSubstringSearch;
using CompilerApp.ArithmeticExpression;
using CompilerApp.AutomatonSubstringSearch;

namespace CompilerApp
{
    public static class LaunchActions // Класс для реализации пункта меню "Пуск"
    {
        // Запуск для объявления прототипа функции на языке C/C++
        public static void LaunchFunctionPrototypeAnalyzer(MainMenuForm form)
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputTable = form.GetOutputTable();
            var errorsTable = form.GetErrorsTable();
            var outputArea = form.GetOutputArea();

            string[] codeLines = inputArea.Lines; // Получаем текст из поля ввода

            // Создаем и запускаем лексический анализатор (сканер)
            LexicalAnalyzer scanner = new LexicalAnalyzer(codeLines);
            List<Token> tokens = scanner.Analyze();

            // Создаем и запускаем синтаксический анализатор (парсер)
            ParserFromText parser = new ParserFromText(codeLines);
            parser.Parse();

            // Очищаем таблицы перед выводом новых данных
            outputTable.Rows.Clear();
            errorsTable.Rows.Clear();
            outputArea.Clear();

            // Заполняем таблицы результатами сканера
            int index = 1;
            foreach (var token in tokens)
            {
                outputTable.Rows.Add(index++, token.TypeCode, token.Name, token.Value, token.Position);
            }

            // Заполняем таблицу результатами парсера (синтаксические ошибки)
            index = 1;
            foreach (var error in parser.Errors)
            {
                errorsTable.Rows.Add(index++, error.Message, error.ErrorValue, error.Position);
            }

            // Подсветка синтаксических и лексических ошибок в редакторе
            form.HighlightErrors(parser.Errors);

            // Переключаемся на вкладку с ошибками, если они есть
            if (errorsTable.Rows.Count > 0)
            {
                form.SelectErrorsTab();
                form.UpdateStatus($"Обнаружено ошибок: {errorsTable.Rows.Count}"); // Обновляем статус
            }
            else
            {
                form.SelectTokensTab();
                form.UpdateStatus("Ошибок не обнаружено"); // Обновляем статус
            }
        }

        // Запуск анализа польской инверсной записи (ПОЛИЗ)
        public static void LaunchPolishReverseNotationAnalyzer(MainMenuForm form)
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputTable = form.GetOutputTable();
            var errorsTable = form.GetErrorsTable();
            var outputArea = form.GetOutputArea();

            // Очищаем старые результаты
            outputTable.Rows.Clear();
            errorsTable.Rows.Clear();
            outputArea.Clear();

            // Проверяем, что текст для анализа есть
            if (!string.IsNullOrWhiteSpace(inputArea.Text))
            {
                // Создаем синтаксический анализатор (парсер)
                var parser = new CompilerApp.PolishReverseNotation.Parser(inputArea.Text);

                if (parser.Tokens.Count != 0)
                {
                    parser.Parse(); // Запускаем синтаксический анализатор (парсер)

                    // Собираем все ошибки: сначала синтаксические, затем лексические
                    List<CompilerApp.PolishReverseNotation.Error> Errors = parser.Errors;
                    Errors.AddRange(parser.Scanner.Errors);

                    // Сортируем ошибки по начальной позиции
                    Errors = Errors.OrderBy(e => e.Position.start).ToList();

                    // Заполняем таблицу ошибок
                    int index = 1;
                    foreach (var error in Errors)
                    {
                        errorsTable.Rows.Add(index++, error.Message, error.Fragment,
                            $"символы {error.Position.start + 1}-{error.Position.end}");
                    }

                    // Сбрасываем выделение и очищаем фон
                    inputArea.SelectAll();
                    inputArea.SelectionBackColor = inputArea.BackColor;

                    // Подсветка синтаксических и лексических ошибок в редакторе
                    foreach (var error in Errors)
                    {
                        inputArea.Select(error.Position.start, error.Position.end - error.Position.start);
                        inputArea.SelectionBackColor = Color.Pink;
                    }

                    // Снимаем выделение
                    inputArea.Select(0, 0);
                    inputArea.SelectionBackColor = inputArea.BackColor;

                    if (Errors.Count == 0)
                    {
                        // Если ошибок нет — считаем ПОЛИЗ
                        PolishConverter polishConverter = new PolishConverter(parser.Tokens);
                        polishConverter.ConvertToPolishReverseNotation();
                        double result = polishConverter.CalculatePolishReverseNotation();

                        // Выводим результат в outputArea
                        outputArea.AppendText($"Исходное арифметическое выражение:\n{inputArea.Text}\n");

                        outputArea.AppendText($"\nАрифметическое выражение в ПОЛИЗ:\n");
                        foreach (var token in polishConverter.outToken)
                        {
                            outputArea.AppendText(token.Value);
                        }

                        outputArea.AppendText($"\n\nРезультат вычисления:\n{result}");

                        // Переключаемся на вкладку с результатом ПОЛИЗ
                        form.SelectResultsTab();
                        form.UpdateStatus("Ошибок не обнаружено"); // Обновляем статус
                    }
                    else
                    {
                        // Переключаемся на вкладку с ошибками
                        form.SelectErrorsTab();
                        form.UpdateStatus($"Обнаружено ошибок: {errorsTable.Rows.Count}"); // Обновляем статус
                    }
                }
            }
        }

        // Запуск поиска HEX-кодов цвета
        public static void LaunchHexColorSearch(MainMenuForm form)
        {
            RunSingleRegexSearch(form, "HEX-код цвета", RegexMatchers.FindHexColors);
        }

        // Запуск поиска шестнадцатеричных чисел
        public static void LaunchHexNumberSearch(MainMenuForm form)
        {
            RunSingleRegexSearch(form, "Шестнадцатеричное число", RegexMatchers.FindHexNumbers);
        }

        // Запуск поиска действительных чисел (включая экспоненциальную форму)
        public static void LaunchRealNumberSearch(MainMenuForm form)
        {
            // Через регулярное выражение
            RunSingleRegexSearch(form, "Действительное число", RegexMatchers.FindRealNumbers);

            // Через автомат
            RunAutomatonSearch(form);
        }

        /// <summary>
        /// Общий метод для запуска поиска выражений по регулярному выражению.
        /// Очищает старые результаты, выполняет поиск, подсвечивает совпадения и выводит результат.
        /// </summary>
        private static void RunSingleRegexSearch(MainMenuForm form, string label, Func<string, List<Match>> searchFunc)
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputTable = form.GetOutputTable();
            var errorsTable = form.GetErrorsTable();
            var outputArea = form.GetOutputArea();

            // Очищаем старые результаты
            outputTable.Rows.Clear();
            errorsTable.Rows.Clear();
            outputArea.Clear();

            // Получаем введенный текст
            string inputText = inputArea.Text;

            // Убираем старую подсветку
            inputArea.Select(0, inputArea.TextLength);
            inputArea.SelectionBackColor = inputArea.BackColor;

            // Выполняем поиск совпадений
            var matches = searchFunc(inputText);
            var results = new StringBuilder();

            // Заголовок результатов
            results.AppendLine("Найденные выражения через регулярное выражение:\n");

            // Заполняем результаты и подсвечиваем совпадения
            RegexUtils.AppendMatches(results, label, matches, inputArea);

            // Выводим результат в текстовую область
            outputArea.Text = results.ToString();

            // Сбрасываем выделение
            inputArea.Select(0, 0);
            inputArea.SelectionBackColor = inputArea.BackColor;

            // Переключаемся на вкладку с результатами
            form.SelectResultsTab();
        }

        // Метод для запуска поиска выражений (действительных чисел) через автомат
        private static void RunAutomatonSearch(MainMenuForm form)
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputArea = form.GetOutputArea();

            // Получаем введенный текст
            string inputText = inputArea.Text;

            // Выполняем поиск совпадений
            var matches = RealNumberAutomaton.FindRealNumbersByAutomaton(inputText);
            var results = new StringBuilder();
            results.AppendLine("\nНайденные выражения через автомат:\n");

            // Заполняем результаты и подсвечиваем совпадения
            foreach (var match in matches)
            {
                results.AppendLine($"Действительное число: {match.Value}, начальная позиция: символ {match.Index}");
                inputArea.Select(match.Index, match.Value.Length);
                inputArea.SelectionBackColor = System.Drawing.Color.LightGreen;
            }

            // Выводим результат в текстовую область
            outputArea.AppendText(results.ToString());

            // Сбрасываем выделение
            inputArea.Select(0, 0);
            inputArea.SelectionBackColor = inputArea.BackColor;
        }

        // Запуск анализа арифметического выражения
        public static void LaunchArithmeticExpressionAnalyzer(MainMenuForm form)
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputTable = form.GetOutputTable();
            var errorsTable = form.GetErrorsTable();
            var outputArea = form.GetOutputArea();

            // Очищаем старые результаты
            outputTable.Rows.Clear();
            errorsTable.Rows.Clear();
            outputArea.Clear();

            // Проверяем, что текст для анализа есть
            if (!string.IsNullOrWhiteSpace(inputArea.Text))
            {
                // Создаем и запускаем синтаксический анализатор (парсер)
                var parser = new CompilerApp.ArithmeticExpression.Parser(inputArea.Text);
                parser.Parse();

                // Выводим дерево разбора (вызовы правил грамматики)
                outputArea.AppendText("Последовательность вызова правил грамматики:\n\n");
                outputArea.AppendText(parser.GetCallTrace());

                // Выводим список лексем в таблицу
                int index = 1;
                foreach (var token in parser.Tokens)
                {
                    outputTable.Rows.Add(index++, token.Id, token.Description, token.Value,
                        $"символы {token.Position.start + 1}-{token.Position.end + 1}");
                }

                // Получаем и сортируем ошибки
                var errors = parser.Errors.OrderBy(e => e.Position.start).ToList();

                // Выводим ошибки в таблицу
                index = 1;
                foreach (var error in errors)
                {
                    errorsTable.Rows.Add(index++, error.Message, error.Fragment,
                        $"символы {error.Position.start + 1}-{error.Position.end}");
                }

                // Снимаем старую подсветку
                inputArea.SelectAll();
                inputArea.SelectionBackColor = inputArea.BackColor;

                // Подсвечиваем ошибки
                foreach (var error in errors)
                {
                    inputArea.Select(error.Position.start, error.Position.end - error.Position.start);
                    inputArea.SelectionBackColor = Color.Pink;
                }

                // Сбрасываем выделение
                inputArea.Select(0, 0);
                inputArea.SelectionBackColor = inputArea.BackColor;

                // Обновляем статус и переключаем вкладку
                if (errors.Count == 0)
                {
                    form.SelectResultsTab();
                    form.UpdateStatus("Ошибок не обнаружено");
                }
                else
                {
                    form.SelectErrorsTab();
                    form.UpdateStatus($"Обнаружено ошибок: {errors.Count}");
                }
            }
        }
    }
}
