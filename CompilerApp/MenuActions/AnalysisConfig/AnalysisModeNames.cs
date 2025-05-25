using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    /// <summary>
    /// Содержит отображаемые (локализованные) названия для каждого режима анализа.
    /// Используется для отображения в ComboBox.
    /// </summary>
    internal static class AnalysisModeNames
    {
        /// <summary>
        /// Сопоставление режима анализа с его отображаемым русским названием.
        /// </summary>
        internal static readonly Dictionary<AnalysisMode, string> DisplayNames = new()
        {
            { AnalysisMode.FunctionPrototype, "Прототип функции" },
            { AnalysisMode.PolishReverseNotation, "ПОЛИЗ" },
            { AnalysisMode.HexColor, "HEX-код цвета" },
            { AnalysisMode.HexNumber, "Шестнадцатеричное число" },
            { AnalysisMode.RealNumber, "Действительное число" },
            { AnalysisMode.ArithmeticExpression, "Арифметическое выражение" }
        };
    }
}
