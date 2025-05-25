using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.ArithmeticExpression
{
    // Класс, реализующий синтаксический анализ (парсер) арифметического выражения
    internal class Parser
    {
        private int pos = 0; // Текущая позиция токена при анализе

        public List<Token> Tokens { get; private set; } = new List<Token>(); // Список токенов, полученных от лексера
        public List<Error> Errors { get; private set; } = new List<Error>(); // Список синтаксических ошибок
        public LexicalAnalyzer Scanner { get; private set; }                 // Лексический анализатор (сканер)

        private StringBuilder callTrace = new StringBuilder(); // Строка трассировки вызовов правил (для дерева разбора)
        private bool isFirstCall = true; // Флаг первого вызова для красивого форматирования трассировки

        public Parser(string codeText)
        {
            Scanner = new LexicalAnalyzer(codeText);
            Tokens = Scanner.Analyze();            // Выполняем лексический анализ
            Errors.AddRange(Scanner.Errors);       // Добавляем ошибки лексического анализа в список ошибок парсера
        }

        // Получение строки с трассировкой вызовов правил грамматики
        public string GetCallTrace()
        {
            return callTrace.ToString().TrimEnd();
        }

        // Добавление записи о вызове правила в трассировку
        private void AddCall(string rule)
        {
            if (isFirstCall)
            {
                callTrace.Append(rule + " ");
                isFirstCall = false;
            }
            else
            {
                callTrace.Append($"→ {rule} ");
            }
        }

        // Обработка ошибки: добавление в список ошибок
        private void HandleError(string message, Token token)
        {
            Errors.Add(new Error(message, token.Value, token.Position));
        }

        // Безопасное получение токена с заданным смещением от текущей позиции
        private Token SafeToken(int offset = 0)
        {
            return pos + offset < Tokens.Count ? Tokens[pos + offset] :
                (Tokens.LastOrDefault() ?? new Token(TokenType.End, "", (0, 0)));
        }

        // Проверка: совпадает ли текущий токен с ожидаемым типом
        private bool Match(TokenType type)
        {
            return pos < Tokens.Count && Tokens[pos].Type == type;
        }

        // Точка входа: запускает синтаксический анализ с правила <Выражение>
        public void Parse()
        {
            pos = 0; // Сброс позиции на начало
            Expression(); // Запуск главного правила грамматики

            // Проверка остатка токенов — должны быть разобраны все
            while (pos < Tokens.Count && Tokens[pos].Type != TokenType.End)
            {
                var token = Tokens[pos];

                if (token.Type == TokenType.CloseParenthesis)
                {
                    HandleError("Лишняя закрывающая скобка ')'", token);
                }
                else
                {
                    HandleError("Неожиданный токен после конца выражения", token);
                }
                pos++;
            }
        }

        // Правило: <Выражение> = <Слагаемое> { (+|-) <Слагаемое> }
        private void Expression()
        {
            AddCall("<Выражение>");
            Term(); // Сначала разбираем первое слагаемое

            // Пока встречаются + или -, продолжаем разбор новых слагаемых
            while (Match(TokenType.Plus) || Match(TokenType.Minus))
            {
                var token = Tokens[pos];
                AddCall($"'{token.Value}'");
                pos++;
                Term();
            }
        }

        // Правило: <Слагаемое> = <Множитель> { (*|/) <Множитель> }
        private void Term()
        {
            AddCall("<Слагаемое>");
            Factor(); // Сначала разбираем множитель

            // Обрабатываем цепочку умножений/делений
            while (Match(TokenType.Multiplication) || Match(TokenType.Division))
            {
                var token = Tokens[pos];
                AddCall($"'{token.Value}'");
                pos++;
                Factor();
            }
        }

        // Правило: <Множитель> = [+|-] <ДробноеЧисло> | <Функция> | (<Выражение>)
        private void Factor()
        {
            AddCall("<Множитель>");

            // Унарный + или -
            if (Match(TokenType.Plus) || Match(TokenType.Minus))
            {
                AddCall($"'унарный {Tokens[pos].Value}'");
                pos++;
            }

            // Проверка на конец входа — если ничего не осталось
            if (pos >= Tokens.Count)
            {
                HandleError("Ожидался множитель, но достигнут конец ввода", SafeToken());
                return;
            }

            var token = Tokens[pos];

            if (token.Type == TokenType.Number)
            {
                // Обработка числа
                ParseFractionalNumber(token);
                pos++;
            }
            else if (token.Type == TokenType.FunctionName)
            {
                // Обработка функции
                ParseFunction();
            }
            else if (token.Type == TokenType.OpenParenthesis)
            {
                // Обработка выражения в скобках
                AddCall("'('");
                pos++;
                Expression(); // Рекурсивный вызов выражения

                if (Match(TokenType.CloseParenthesis))
                {
                    AddCall("')'");
                    pos++;
                }
                else
                {
                    HandleError("Ожидалась закрывающая скобка ')'", SafeToken(-1));
                }
            }
            else
            {
                HandleError("Ожидался множитель (число, функция или выражение в скобках)", token);
                pos++;
            }
        }

        // Правило: <ДробноеЧисло> = <ЦелаяЧасть> | <ЦелаяЧасть>.<ДробнаяЧасть>
        private void ParseFractionalNumber(Token token)
        {
            AddCall("<ДробноеЧисло>");
            var value = token.Value;
            var parts = value.Split('.'); // Делим число на целую и дробную часть

            ParseIntegerPart(parts[0]); // Разбираем целую часть

            if (parts.Length == 2)
            {
                AddCall("'.'"); // Уточняем наличие точки
                ParseFractionalPart(parts[1]); // Разбираем дробную часть
            }
        }

        // Правило: <ЦелаяЧасть> = <Цифра>{<Цифра>}
        private void ParseIntegerPart(string digits)
        {
            AddCall("<ЦелаяЧасть>");
            foreach (char digit in digits)
            {
                ParseDigit(digit); // Разбираем каждую цифру
            }
        }

        // Правило: <ДробнаяЧасть> = <Цифра>{<Цифра>}
        private void ParseFractionalPart(string digits)
        {
            AddCall("<ДробнаяЧасть>");
            foreach (char digit in digits)
            {
                ParseDigit(digit); // Разбираем каждую цифру
            }
        }

        // Правило: <Цифра> = 0|1|2|3|4|5|6|7|8|9
        private void ParseDigit(char digit)
        {
            AddCall($"<Цифра> = {digit}");
        }

        // Правило: <Функция> = <ИмяФункции>(<Выражение>)
        private void ParseFunction()
        {
            AddCall("<Функция>");

            if (pos >= Tokens.Count)
            {
                HandleError("Ожидалось имя функции", SafeToken());
                return;
            }

            // Разбор имени функции
            AddCall($"<ИмяФункции> = {Tokens[pos].Value}");
            pos++;

            // Ожидаем открывающую скобку
            if (Match(TokenType.OpenParenthesis))
            {
                AddCall("'('");
                pos++;
                Expression(); // Разбираем аргумент функции — это выражение

                if (Match(TokenType.CloseParenthesis))
                {
                    AddCall("')'");
                    pos++;
                }
                else
                {
                    HandleError("Ожидалась закрывающая скобка ')'", SafeToken(-1));
                }
            }
            else
            {
                HandleError("Ожидалась открывающая скобка '(' после имени функции", SafeToken());
            }
        }
    }
}
