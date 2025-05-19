using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.PolishReverseNotation
{
    // Класс, представляющий информацию об ошибке в коде
    internal class Error
    {
        public string Message { get; set; } // Сообщение об ошибке

        public string Fragment { get; set; } // Фрагмент текста, в котором обнаружена ошибка

        public (int start, int end) Position { get; set; } // Глобальная позиция ошибки в тексте

        public Error(string message, string fragment, (int start, int end) position)
        {
            Message = message;
            Fragment = fragment;
            Position = (position.start, position.end + 1); // end + 1 нужен для подсветки диапазона в RichTextBox
        }
    }
}
