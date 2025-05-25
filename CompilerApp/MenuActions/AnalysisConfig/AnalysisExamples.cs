using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    /// <summary>
    /// Содержит тестовые строки для каждого режима анализа,
    /// которые автоматически подставляются в поле ввода при выборе режима.
    /// </summary>
    internal static class AnalysisExamples
    {
        /// <summary>
        /// Сопоставление режимов анализа с соответствующими примерами ввода.
        /// </summary>
        internal static readonly Dictionary<AnalysisMode, string> TestExamples = new()
        {
            { AnalysisMode.FunctionPrototype, "float calculateRectangleArea(float length, float width);" },
            { AnalysisMode.PolishReverseNotation, "((100 + 25) - 5) * (10 / 2)" },
            { AnalysisMode.HexColor, "HEX-код цвета: #FFF, #abc" },
            { AnalysisMode.HexNumber, "Шестнадцатеричное число: 0xABC, 0X1F4" },
            { AnalysisMode.RealNumber, "Действительное число: 3.14, -0.5, 2e10, +1.2E-4" },
            { AnalysisMode.ArithmeticExpression, "-ln(8.9 - 4.5) / 3.0" }
        };
    }
}
