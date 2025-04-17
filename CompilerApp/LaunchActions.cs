using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class LaunchActions // Класс для реализации пункта меню "Пуск"
    {
        public static void Launch(MainMenuForm form) // Запуск компилятора
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputTable = form.GetOutputTable();
            var errorsTable = form.GetErrorsTable();

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

            // Список для лексических ошибок (убрать потом)
            //List<ParseError> scannerErrors = new List<ParseError>();
            // Добавляем лексическую ошибку в таблицу (убрать весь foreach потом)
            //index = 1;
            //foreach (var token in tokens)
            //{
            //    if (token.TypeCode == TokenType.InvalidCode)
            //    {
            //        errorsTable.Rows.Add(index++, token.Name, token.Value, token.Position);

            //        // Добавляем лексическую ошибку в список ошибок для будущей подсветки
            //        scannerErrors.Add(new ParseError(token.Name, token.Value, token.Position));
            //    }
            //}

            // Подсветка синтаксических и лексических ошибок в редакторе
            form.HighlightErrors(parser.Errors);

            // Подсветка лексических ошибок в редакторе (убрать потом)
            //form.HighlightErrors(scannerErrors);

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
    }
}
