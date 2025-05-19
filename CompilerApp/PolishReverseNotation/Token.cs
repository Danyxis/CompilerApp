using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.PolishReverseNotation
{
    // Перечисление возможных типов токенов (лексем)
    internal enum TokenType
    {
        Number,             // Числовой литерал
        Plus,               // Знак сложения '+'
        Minus,              // Знак вычитания '-'
        Multiplication,     // Знак умножения '*'
        Division,           // Знак деления '/'
        OpenParenthesis,    // Открывающая скобка '('
        CloseParenthesis,   // Закрывающая скобка ')'
        End,                // Конец ввода
        Error               // Недопустимый символ / ошибка
    }

    // Класс, представляющий токен (лексему)
    internal class Token
    {
        public TokenType Type { get; set; } // Тип токена

        public string Value { get; set; } // Значение токена в виде строки

        public (int start, int end) Position { get; set; } // Глобальная позиция токена в тексте

        public Token(TokenType type, string value, (int start, int end) position)
        {
            Type = type;
            Value = value;
            Position = position;
        }
    }
}
