using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerApp.RegexSubstringSearch
{
    /// <summary>
    /// Вспомогательный класс для обработки и отображения результатов поиска по регулярным выражениям.
    /// Отвечает за вывод найденных совпадений и подсветку их в поле ввода.
    /// </summary>
    internal static class RegexUtils
    {
        // Добавляет найденные совпадения в текстовый буфер и подсвечивает их в поле ввода
        public static void AppendMatches(StringBuilder builder, string label, List<Match> matches, RichTextBox inputBox)
        {
            foreach (var match in matches)
            {
                builder.AppendLine($"{label}: {match.Value}, начальная позиция: символ {match.Index}");
                HighlightMatch(inputBox, match.Index, match.Length);
            }
        }

        // Подсвечивает найденное совпадение в RichTextBox заданным цветом
        public static void HighlightMatch(RichTextBox box, int start, int length)
        {
            box.Select(start, length);
            box.SelectionBackColor = System.Drawing.Color.LightGreen;
        }
    }
}
