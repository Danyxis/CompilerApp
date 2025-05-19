using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    // Класс описывает синтаксическую или лексическую ошибку, возникшую при разборе
    // Наследуется от Exception, чтобы при необходимости можно было использовать как исключение
    internal class ParseError : Exception // Класс синтаксической (лексической) ошибки
    {
        public string ErrorValue { get; } // Значение токена или символа, вызвавшего ошибку
        public string Position { get; } // Позиция ошибки в тексте в виде строки

        // Конструктор принимает сообщение об ошибке, значение и позицию
        public ParseError(string message, string value, string position) 
            : base(message) // Передаёт сообщение в базовый класс Exception
        {
            ErrorValue = value;
            Position = position;
        }
    }
}
