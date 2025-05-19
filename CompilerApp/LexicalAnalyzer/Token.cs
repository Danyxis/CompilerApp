using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    internal class Token // Класс лексемы
    {
        public int TypeCode { get; }       // Код лексемы (числовое значение)
        public string Name { get; }        // Тип лексемы (ключевое слово, идентификатор и т.д.)
        public string Value { get; }       // Значение лексемы (сама строка)
        public string Position { get; }    // Позиция в исходном коде

        public Token(int typeCode, string name, string value, int line, int start, int end)
        {
            TypeCode = typeCode;
            Name = name;
            Value = value;
            Position = $"строка {line}, символы {start}-{end}";
        }
    }
}
