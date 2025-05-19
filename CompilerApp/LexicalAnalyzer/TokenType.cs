using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    internal static class TokenType // Класс условных типов и кодов лексем
    {
        // Коды типов данных (ключевые слова)
        internal static readonly Dictionary<string, int> Keywords = new Dictionary<string, int>
        {
            {"int", 1},      // "int"
            {"float", 2},    // "float"
            {"char", 3},     // "char"
            {"string", 4},   // "string"
            {"bool", 5}      // "bool"
        };

        // Коды операторов и знаков
        internal static readonly Dictionary<string, int> Operators = new Dictionary<string, int>
        {
            {"(", 8}, {")", 9}, {",", 10}, {";", 11}
        };

        // Коды для других лексем
        internal const int IdentifierCode = 6;   // Идентификатор
        internal const int SpaceCode = 7;        // Разделитель (пробел)
        internal const int InvalidCode = 666;    // Недопустимый символ (ошибка)
    }
}
