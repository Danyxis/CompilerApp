using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    /// <summary>
    /// Перечисление режимов анализа, доступных пользователю.
    /// Используется для выбора действия в интерфейсе.
    /// </summary>
    internal enum AnalysisMode
    {
        FunctionPrototype,      // Разбор прототипа функции на языке C/C++
        PolishReverseNotation,  // Анализ польской инверсной записи (ПОЛИЗ)
        HexColor,               // Поиск HEX-кодов цвета, например: #abc, #FFF
        HexNumber,              // Поиск шестнадцатеричных чисел, например: 0x1A3F
        RealNumber,             // Поиск действительных чисел, включая экспоненциальную форму, например: 3.14, -2.5e3
        ArithmeticExpression    // Анализ арифметического выражения
    }
}
