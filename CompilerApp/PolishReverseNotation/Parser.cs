using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.PolishReverseNotation
{
    // Класс, реализующий синтаксический анализ (парсер) арифметического выражения
    internal class Parser
    {
        private int pos = 0; // Текущая позиция токена при анализе

        public List<Token> Tokens = new List<Token>();      // Список токенов, полученных от лексера
        public List<Error> Errors = new List<Error>();      // Список синтаксических ошибок
        public LexicalAnalyzer Scanner;                     // Лексический анализатор (сканер)

        public Parser(string codeText)
        {
            Scanner = new LexicalAnalyzer(codeText);
            Tokens = Scanner.Analyze(); // Получаем список токенов
        }

        // Метод для фиксации синтаксической ошибки
        private void HandleError(string message, string value, (int start, int end) pos)
        {
            Errors.Add(new Error(message, value, pos));
        }

        // Начало синтаксического анализа
        public void Parse()
        {
            E(); // Начинаем с правила E

            // Если после разбора остались токены — они считаются синтаксическими ошибками
            while (pos < Tokens.Count)
            {
                var token = Tokens[pos];

                if (token.Type == TokenType.CloseParenthesis)
                {
                    HandleError("Лишняя закрывающая скобка ')'", token.Value, token.Position);
                }
                else
                {
                    HandleError("Неожиданный токен после конца выражения", token.Value, token.Position);
                }
                pos++;
            }
        }

        public void E() // E → TA
        {
            T();
            A();
        }

        public void T() // T → ОВ
        {
            O();
            B();
        }

        public void A() // A → ε | + TA | - TA
        {
            if (pos < Tokens.Count &&
                (Tokens[pos].Type == TokenType.Plus || Tokens[pos].Type == TokenType.Minus))
            {
                pos++;
                T();
                A();
            }
        }

        public void B() // В → ε | *ОВ | /ОВ
        {
            if (pos < Tokens.Count &&
                (Tokens[pos].Type == TokenType.Multiplication || Tokens[pos].Type == TokenType.Division))
            {
                pos++;
                O();
                B();
            }
        }

        public void O() // О → num | (E)
        {
            if (pos >= Tokens.Count)
            {
                // Если достигнут конец ввода — ошибка
                var lastToken = Tokens.Last();
                HandleError("Ожидался операнд, но достигнут конец ввода", "", (lastToken.Position.end, lastToken.Position.end));
                return;
            }

            var currentToken = Tokens[pos];

            if (currentToken.Type == TokenType.Number)
            {
                pos++; // Число — допустимо
            }
            else if (currentToken.Type == TokenType.OpenParenthesis)
            {
                pos++;
                E(); // Рекурсивно разбираем выражение в скобках

                if (pos < Tokens.Count && Tokens[pos].Type == TokenType.CloseParenthesis)
                {
                    pos++; // Закрывающая скобка найдена
                }
                else
                {
                    // Нет закрывающей скобки
                    var prevToken = Tokens[Math.Max(pos - 1, 0)];
                    HandleError("Ожидалась закрывающая скобка ')'", prevToken.Value, prevToken.Position);
                }
            }
            else
            {
                // Ни число, ни открывающая скобка — ошибка
                HandleError("Ожидался операнд", currentToken.Value, currentToken.Position);
            }
        }
    }
}
