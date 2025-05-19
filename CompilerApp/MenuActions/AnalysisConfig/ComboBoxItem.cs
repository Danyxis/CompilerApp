using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    /// <summary>
    /// Представляет элемент для отображения в ComboBox, содержащий режим анализа и его отображаемое имя.
    /// Используется для связывания enum и строки без потери типа.
    /// </summary>
    internal class ComboBoxItem
    {
        public AnalysisMode Mode { get; set; } // Режим анализа (enum), связанный с этим элементом
        public string Display { get; set; } // Отображаемое название режима (для пользователя)

        public override string ToString() => Display; // Определяет, какое значение будет отображаться в ComboBox
    }
}
