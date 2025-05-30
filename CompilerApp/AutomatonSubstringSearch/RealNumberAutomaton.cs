using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerApp.AutomatonSubstringSearch
{
    /// <summary>
    /// Класс, реализующий конечный автомат для поиска действительных чисел в тексте,
    /// включая экспоненциальную запись (например: -3.14, 2e10, +1.2E-4, .5, 2.).
    /// </summary>
    internal class RealNumberAutomaton
    {
        // Выполняет посимвольный анализ входного текста для поиска действительных чисел
        public static List<SimpleMatch> FindRealNumbersByAutomaton(string inputText)
        {
            var matches = new List<SimpleMatch>(); // Список найденных совпадений (действительных чисел)
            int i = 0;

            // Основной цикл: сканирует текст с начала до конца
            while (i < inputText.Length)
            {
                int start = i;                  // Начальная позиция потенциального совпадения
                int state = 1;                  // Начальное состояние автомата
                bool isAccepted = false;        // Флаг: является ли текущее состояние допустимым концом числа
                int lastAccepting = -1;         // Индекс последнего принятого символа

                // Внутренний цикл автомата — проверяет каждый символ
                while (i < inputText.Length)
                {
                    char c = inputText[i];
                    switch (state)
                    {
                        case 1: // Начало (может быть знак, цифра или точка)
                            if (c == '+' || c == '-')
                            {
                                state = 2; i++;
                            }
                            else if (char.IsDigit(c))
                            {
                                state = 3; i++; isAccepted = true; lastAccepting = i;
                            }
                            else if (c == '.')
                            {
                                state = 4; i++;
                            }
                            else
                            {
                                goto ExitLoop; // недопустимый символ — прерываем
                            }
                            break;

                        case 2: // После знака (требуется либо цифра, либо точка)
                            if (char.IsDigit(c))
                            {
                                state = 3; i++; isAccepted = true; lastAccepting = i;
                            }
                            else if (c == '.')
                            {
                                state = 4; i++;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;

                        case 3: // Целая часть числа
                            if (char.IsDigit(c))
                            {
                                i++; isAccepted = true; lastAccepting = i;
                            }
                            else if (c == '.')
                            {
                                state = 5; i++; isAccepted = true; lastAccepting = i; // перешли к дробной части
                            }
                            else if (c == 'e' || c == 'E')
                            {
                                state = 6; i++;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;

                        case 4: // Начинается с точки (например, .5)
                            if (char.IsDigit(c))
                            {
                                state = 5; i++; isAccepted = true; lastAccepting = i;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;

                        case 5: // Дробная часть
                            if (char.IsDigit(c))
                            {
                                i++; isAccepted = true; lastAccepting = i;
                            }
                            else if (c == 'e' || c == 'E')
                            {
                                state = 6; i++;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;

                        case 6: // После 'e' или 'E' (может быть знак или цифра)
                            if (c == '+' || c == '-')
                            {
                                state = 7; i++;
                            }
                            else if (char.IsDigit(c))
                            {
                                state = 8; i++; isAccepted = true; lastAccepting = i;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;

                        case 7: // Знак после 'e' или 'E'
                            if (char.IsDigit(c))
                            {
                                state = 8; i++; isAccepted = true; lastAccepting = i;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;

                        case 8: // Цифры после экспоненты
                            if (char.IsDigit(c))
                            {
                                i++; isAccepted = true; lastAccepting = i;
                            }
                            else
                            {
                                goto ExitLoop;
                            }
                            break;
                    }
                }

            ExitLoop:
                // Если достигли допустимого состояния — фиксируем найденное число
                if (isAccepted)
                {
                    string value = inputText.Substring(start, lastAccepting - start);
                    matches.Add(new SimpleMatch(value, start));
                    i = lastAccepting; // продолжаем после конца совпадения
                }
                else
                {
                    i = start + 1; // невалидно — переходим к следующему символу
                }
            }

            return matches; // Возвращаем список найденных совпадений (действительных чисел)
        }
    }
}
