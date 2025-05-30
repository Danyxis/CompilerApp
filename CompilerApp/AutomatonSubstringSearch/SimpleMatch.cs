using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.AutomatonSubstringSearch
{
    /// <summary>
    /// Представляет результат поиска подстроки с помощью автомата.
    /// Хранит найденное значение и его начальную позицию в тексте.
    /// </summary>
    internal class SimpleMatch
    {
        public string Value { get; } // Найденная подстрока
        public int Index { get; } // Начальный индекс подстроки в исходном тексте

        public SimpleMatch(string value, int index)
        {
            Value = value;
            Index = index;
        }
    }
}
