using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.ArithmeticExpression
{
    // Типы лексем (токенов), используемые для разбора арифметических выражений
    internal enum TokenType
    {
        Number,             // Число: целое или дробное (например, 123, 45.67)

        FunctionName,       // Имя функции: sin, cos, tg, ctg, log, ln

        Plus,               // '+': бинарный или унарный плюс
        Minus,              // '-': бинарный или унарный минус
        Multiplication,     // '*': умножение
        Division,           // '/': деление

        Dot,                // '.': разделяет целую и дробную часть числа

        OpenParenthesis,    // '(': открывающая скобка
        CloseParenthesis,   // ')': закрывающая скобка

        End,                // Конец ввода

        Error               // Ошибка или недопустимый символ
    }

    // Представление лексемы (токена), полученной после лексического анализа
    internal class Token
    {
        public TokenType Type { get; set; } // Тип токена

        public string Value { get; set; } // Значение токена в исходном тексте

        public (int start, int end) Position { get; set; } // Позиция токена в исходной строке: начало и конец (включительно)

        public Token(TokenType type, string value, (int start, int end) position)
        {
            Type = type;
            Value = value;
            Position = position;
        }
    }
}
