using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.PolishReverseNotation
{
    // Класс, реализующий лексический анализ (сканер) арифметических выражений
    internal class LexicalAnalyzer
    {
        private string codeText; // Исходный текст для анализа

        private int State; // Текущее состояние конечного автомата
        private int prevStatus; // Предыдущее состояние (используется при выходе из состояния ошибки)

        private List<Token> Tokens = new List<Token>(); // Список корректно распознанных токенов
        public List<Error> Errors = new List<Error>(); // Список лексических ошибок, обнаруженных при анализе

        // Конструктор: принимает текст и удаляет управляющие символы
        public LexicalAnalyzer(string codeText)
        {
            this.codeText = codeText.Replace("\n", " ").Replace("\t", " ").Replace("\r", " ");
        }

        // Добавление токена в список
        private void AddToken(TokenType type, string value, (int start, int end) position)
        {
            Tokens.Add(new Token(type, value, position));
        }

        // Основной метод лексического анализа
        public List<Token> Analyze()
        {
            int position = 0;             // Глобальная позиция символа в тексте
            int beginPosition = 0;        // Начальная позиция токена
            int endPosition = 0;          // Конечная позиция токена

            char currentChar = ' ';
            int errorStart = 0;           // Начало ошибочного фрагмента
            string number = "";           // Сборка числа
            string errorFragment = "";    // Ошибочный фрагмент текста

            bool endFound = false;        // Признак конца текста

            while (!endFound)
            {
                // Получаем текущий символ или символ конца строки
                currentChar = position < codeText.Length ? codeText[position] : '\0';

                switch (State)
                {
                    case 0:
                        // Начальное состояние: определяем тип символа
                        switch (currentChar)
                        {
                            case char c when char.IsDigit(currentChar):
                                State = 1; // Переход в состояние обработки числа
                                beginPosition = position;
                                break;
                            case '+':
                                State = 2;
                                break;
                            case '-':
                                State = 3;
                                break;
                            case '*':
                                State = 4;
                                break;
                            case '/':
                                State = 5;
                                break;
                            case '(':
                                State = 6;
                                break;
                            case ')':
                                State = 7;
                                break;
                            case ' ':
                                position++; // Пропускаем пробел
                                break;
                            case '\0':
                                State = 8;
                                break;
                            default:
                                State = 9; // Переход в состояние ошибки
                                errorFragment += currentChar;
                                errorStart = position;
                                position++;
                                break;
                        }
                        break;

                    case 1: // Считывание числа
                        if (char.IsDigit(currentChar))
                        {
                            number += currentChar;
                            position++;
                        }
                        else
                        {
                            endPosition = position - 1;
                            AddToken(TokenType.Number, number, (beginPosition, endPosition));
                            State = 0;
                            number = "";
                        }
                        break;

                    case 2: // '+'
                        AddToken(TokenType.Plus, currentChar.ToString(), (position, position));
                        State = 0;
                        position++;
                        break;

                    case 3: // '-'
                        AddToken(TokenType.Minus, currentChar.ToString(), (position, position));
                        State = 0;
                        position++;
                        break;

                    case 4: // '*'
                        AddToken(TokenType.Multiplication, currentChar.ToString(), (position, position));
                        State = 0;
                        position++;
                        break;

                    case 5: // '/'
                        AddToken(TokenType.Division, currentChar.ToString(), (position, position));
                        State = 0;
                        position++;
                        break;

                    case 6: // '('
                        AddToken(TokenType.OpenParenthesis, currentChar.ToString(), (position, position));
                        State = 0;
                        position++;
                        break;

                    case 7: // ')'
                        AddToken(TokenType.CloseParenthesis, currentChar.ToString(), (position, position));
                        State = 0;
                        position++;
                        break;

                    case 8: // Конец ввода
                        endFound = true;
                        break;

                    case 9: // Ошибочный символ
                        if (position < codeText.Length && IsError(currentChar))
                        {
                            errorFragment += currentChar;
                            position++;
                        }
                        else
                        {
                            // Завершаем ошибочный фрагмент и добавляем ошибку
                            Errors.Add(new Error("Ошибочный фрагмент", errorFragment, (errorStart, position - 1)));
                            errorFragment = "";
                            State = prevStatus;
                        }
                        break;

                    default:
                        break;
                }

                // Запоминаем предыдущее состояние, если мы не в режиме ошибки
                if (State != 9)
                {
                    prevStatus = State;
                }
            }

            return Tokens;
        }

        // Проверка: является ли символ ошибочным (неизвестным)
        private bool IsError(char c)
        {
            return !(char.IsDigit(c) || c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')');
        }
    }
}
