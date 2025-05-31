using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.ArithmeticExpression
{
    // Класс, реализующий лексический анализ (сканер) арифметических выражений
    internal class LexicalAnalyzer
    {
        // Входной текст для анализа
        private string codeText;

        // Текущее состояние конечного автомата
        private int State;

        // Список распознанных токенов
        private List<Token> Tokens = new List<Token>();

        // Список ошибок лексического анализа
        public List<Error> Errors = new List<Error>();

        // Конструктор: принимает текст и удаляет управляющие символы
        public LexicalAnalyzer(string codeText)
        {
            this.codeText = codeText.Replace("\n", " ").Replace("\t", " ").Replace("\r", " ");
        }

        // Добавление токена в список
        private void AddToken(TokenType type, string value, (int start, int end) position)
        {
            var (id, desc) = GetTokenInfo(type, value);
            Tokens.Add(new Token(type, id, desc, value, position));
        }

        // Возвращает идентификатор токена и его название по типу и значению
        private (int, string) GetTokenInfo(TokenType type, string value)
        {
            return type switch
            {
                // Для числа
                TokenType.Number => (1, "Дробное число"),

                // Для имени функции
                TokenType.FunctionName => value switch
                {
                    "sin" => (2, "Математическая функция"),
                    "cos" => (3, "Математическая функция"),
                    "tg" => (4, "Математическая функция"),
                    "ctg" => (5, "Математическая функция"),
                    "log" => (6, "Математическая функция"),
                    "ln" => (7, "Математическая функция"),
                    _ => (666, "Неизвестная функция") // если имя функции неизвестно
                },

                // Для арифметических операторов
                TokenType.Plus => (8, "Оператор сложения"),
                TokenType.Minus => (9, "Оператор вычитания"),
                TokenType.Multiplication => (10, "Оператор умножения"),
                TokenType.Division => (11, "Оператор деления"),

                // Для специальных символов
                TokenType.Dot => (12, "Специальный символ"),
                TokenType.OpenParenthesis => (13, "Специальный символ"),
                TokenType.CloseParenthesis => (14, "Специальный символ"),

                // Для недопустимых символов
                TokenType.Error => (666, "Недопустимый символ"),

                // На случай появления неизвестного типа токена
                _ => (666, "Неизвестно")
            };
        }

        // Основной метод лексического анализа
        public List<Token> Analyze()
        {
            int position = 0;              // Позиция текущего символа
            int beginPosition = 0;         // Начало текущего токена
            int endPosition = 0;           // Конец текущего токена

            char currentChar = ' ';        // Текущий символ
            int errorStart = 0;            // Начало ошибочного фрагмента
            string buffer = "";            // Буфер для накопления символов токена
            string errorFragment = "";     // Буфер для ошибочного фрагмента

            bool endFound = false;         // Признак конца анализа

            while (!endFound)
            {
                // Чтение текущего символа, либо \0 — если достигнут конец текста
                currentChar = position < codeText.Length ? codeText[position] : '\0';

                switch (State)
                {
                    case 0:
                        // Начальное состояние: определяем тип символа
                        switch (currentChar)
                        {
                            // Начало числа
                            case char c when char.IsDigit(currentChar):
                                State = 1;
                                beginPosition = position;
                                break;

                            // Начало имени функции
                            case char c when char.IsLetter(currentChar):
                                State = 4;
                                beginPosition = position;
                                break;

                            // Арифметические операторы
                            case '+':
                                State = 5;
                                break;

                            case '-':
                                State = 6;
                                break;

                            case '*':
                                State = 7;
                                break;

                            case '/':
                                State = 8;
                                break;

                            // Отдельно стоящая точка
                            case '.':
                                State = 9;
                                break;

                            // Скобки
                            case '(':
                                State = 10;
                                break;

                            case ')':
                                State = 11;
                                break;

                            // Пробел — просто пропускаем
                            case ' ':
                                position++;
                                break;

                            // Конец строки — переход в состояние завершения
                            case '\0':
                                State = 12;
                                break;

                            // Неизвестный символ — ошибка
                            default:
                                State = 13;
                                errorStart = position;
                                AddToken(TokenType.Error, currentChar.ToString(), (position, position));
                                errorFragment += currentChar;
                                position++;
                                break;
                        }
                        break;

                    // Чтение целой части числа
                    case 1:
                        if (char.IsDigit(currentChar))
                        {
                            buffer += currentChar;
                            position++;
                        }
                        else if (currentChar == '.')
                        {
                            buffer += currentChar;
                            position++;
                            State = 2; // Переход к дробной части
                        }
                        else
                        {
                            // Завершение числа (целая часть)
                            endPosition = position - 1;
                            AddToken(TokenType.Number, buffer, (beginPosition, endPosition));
                            buffer = "";
                            State = 0;
                        }
                        break;

                    // После точки — ожидание дробной части
                    case 2:
                        if (char.IsDigit(currentChar))
                        {
                            buffer += currentChar;
                            position++;
                            State = 3; // Начинаем читать дробную часть
                        }
                        else
                        {
                            // Ошибка: точка без дробной части
                            Errors.Add(new Error("Ожидалась дробная часть после точки", buffer, (beginPosition, position - 1)));
                            buffer = "";
                            State = 0;
                        }
                        break;

                    // Чтение дробной части
                    case 3:
                        if (char.IsDigit(currentChar))
                        {
                            buffer += currentChar;
                            position++;
                        }
                        else
                        {
                            // Завершение дробного числа
                            endPosition = position - 1;
                            AddToken(TokenType.Number, buffer, (beginPosition, endPosition));
                            buffer = "";
                            State = 0;
                        }
                        break;

                    // Чтение имени функции
                    case 4:
                        if (char.IsLetter(currentChar))
                        {
                            buffer += currentChar;
                            position++;
                        }
                        else
                        {
                            endPosition = position - 1;
                            if (IsFunction(buffer))
                            {
                                // Добавление токена с именем функции
                                AddToken(TokenType.FunctionName, buffer, (beginPosition, endPosition));
                            }
                            else
                            {
                                // Неизвестное имя функции — ошибка
                                Errors.Add(new Error($"Неизвестная функция '{buffer}'", buffer, (beginPosition, endPosition)));
                            }
                            buffer = "";
                            State = 0;
                        }
                        break;

                    case 5: // '+'
                        AddToken(TokenType.Plus, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 6: // '-'
                        AddToken(TokenType.Minus, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 7: // '*'
                        AddToken(TokenType.Multiplication, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 8: // '/'
                        AddToken(TokenType.Division, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 9: // '.'
                        AddToken(TokenType.Dot, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 10: // '('
                        AddToken(TokenType.OpenParenthesis, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 11: // ')'
                        AddToken(TokenType.CloseParenthesis, currentChar.ToString(), (position, position));
                        position++;
                        State = 0;
                        break;

                    case 12: // Конец ввода
                        endFound = true; // Устанавливаем флаг завершения анализа
                        break;

                    case 13: // Ошибочный символ
                        // Продолжаем собирать ошибочный фрагмент, пока не встретим допустимый символ
                        if (position < codeText.Length && IsErrorChar(currentChar))
                        {
                            // Добавляем токен для каждого недопустимого символа
                            AddToken(TokenType.Error, currentChar.ToString(), (position, position));
                            errorFragment += currentChar;
                            position++;
                        }
                        else
                        {
                            // Завершаем ошибочный фрагмент и добавляем ошибку в список
                            Errors.Add(new Error("Ошибочный фрагмент", errorFragment, (errorStart, position - 1)));
                            errorFragment = "";
                            State = 0;
                        }
                        break;

                    default:
                        break;
                }

            }

            return Tokens; // Возвращаем список токенов
        }

        // Проверка: является ли строка допустимым именем функции
        private bool IsFunction(string value)
        {
            return value == "sin" || value == "cos" || value == "tg" ||
                   value == "ctg" || value == "log" || value == "ln";
        }

        // Проверка: является ли символ ошибочным (не распознается как допустимый токен)
        private bool IsErrorChar(char c)
        {
            return !char.IsLetterOrDigit(c) && c != '+' && c != '-' && c != '*' && c != '/' && c != '(' && c != ')' && c != '.';
        }
    }
}
