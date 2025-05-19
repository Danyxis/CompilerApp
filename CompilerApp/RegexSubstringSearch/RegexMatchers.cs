using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerApp.RegexSubstringSearch
{
    /// <summary>
    /// Класс, содержащий методы поиска подстрок с использованием регулярных выражений.
    /// Возвращает список совпадений (Match) с позициями и содержимым.
    /// </summary>
    internal static class RegexMatchers
    {
        // Ищет HEX-коды цвета длиной 3 символа (например: #abc, #FFF)
        public static List<Match> FindHexColors(string inputText)
        {
            string pattern = @"#(?:[0-9a-fA-F]{3})";
            return GetMatches(inputText, new Regex(pattern));
        }

        // Ищет шестнадцатеричные числа с префиксом 0x или 0X (например: 0x1A3F, 0Xabc)
        public static List<Match> FindHexNumbers(string inputText)
        {
            string pattern = @"0[xX][0-9a-fA-F]+";
            return GetMatches(inputText, new Regex(pattern));
        }

        // Ищет действительные числа, включая экспоненциальную запись (например: -3.14, 2e10, +1.2E-4)
        public static List<Match> FindRealNumbers(string inputText)
        {
            string pattern = @"[+-]?(\d+(\.\d*)?|\.\d+)([eE][+-]?\d+)?";
            return GetMatches(inputText, new Regex(pattern));
        }

        // Выполняет поиск совпадений с заданным регулярным выражением и возвращает их в виде списка
        private static List<Match> GetMatches(string inputText, Regex regex)
        {
            return regex.Matches(inputText).Cast<Match>().ToList();
        }
    }
}
