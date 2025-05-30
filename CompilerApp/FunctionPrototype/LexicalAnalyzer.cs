using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    internal class LexicalAnalyzer // Класс лексического анализатора (сканера)
    {
        private bool lastTokenWasType = false; // Флаг для отслеживания ключевого слова (типа данных)
        private readonly string[] _lines; // Массив строк кода для анализа

        public LexicalAnalyzer(string[] lines)
        {
            _lines = lines;
        }

        public List<Token> Analyze()
        {
            List<Token> tokens = new List<Token>(); // Список найденных токенов
            int lineNumber = 1; // Номер текущей строки

            // Обход всех строк кода
            foreach (string line in _lines)
            {
                int position = 0;
                lastTokenWasType = false;

                // Обход символов в текущей строке
                while (position < line.Length)
                {
                    char currentChar = line[position];
                    string charAsString = currentChar.ToString(); // Преобразуем символ в строку для удобства работы

                    // Игнорируем пробелы, но добавляем их, если они значимые (разделитель после типа данных)
                    if (char.IsWhiteSpace(currentChar))
                    {
                        if (currentChar == ' ' && lastTokenWasType)
                        {
                            tokens.Add(new Token(TokenType.SpaceCode, "Разделитель (пробел)", "", lineNumber, position + 1, position + 1));
                            lastTokenWasType = false;
                        }
                        position++;
                        continue;
                    }

                    // Обработка операторов (специальные символы, такие как ( ) , ;)
                    if (IsOperator(currentChar))
                    {
                        string type = (charAsString == ";") ? "Конец оператора" : "Специальный символ";
                        tokens.Add(new Token(TokenType.Operators[charAsString], type, charAsString, lineNumber, position + 1, position + 1));
                        position++;
                        continue;
                    }

                    // Обнаружение идентификаторов и ключевых слов (начинаются с английской буквы)
                    if (char.IsLetter(currentChar) && IsEnglishLetter(currentChar))
                    {
                        ProcessIdentifierOrKeyword(line, ref position, lineNumber, tokens);
                        continue;
                    }

                    // Проверка на недопустимые идентификаторы (начинаются с недопустимого символа, например, русской буквы, цифры или _)
                    if (!IsEnglishLetter(currentChar))
                    {
                        ProcessInvalidIdentifier(line, ref position, lineNumber, tokens);
                        continue;
                    }

                    // Обработка всех оставшихся недопустимых символов
                    tokens.Add(new Token(TokenType.InvalidCode, "Недопустимый символ", charAsString, lineNumber, position + 1, position + 1));
                    position++;
                }

                lineNumber++; // Переход к следующей строке
            }

            return tokens; // Возвращаем список найденных токенов
        }

        /// <summary>
        /// Обрабатывает идентификаторы и ключевые слова
        /// </summary>
        private void ProcessIdentifierOrKeyword(string line, ref int position, int lineNumber, List<Token> tokens)
        {
            int start = position;
            string lexeme = "";
            bool hasRussianLetter = false; // Флаг, есть ли русские буквы в идентификаторе

            // Читаем символы, пока они соответствуют допустимым символам идентификатора (буквы, цифры, _)
            while (position < line.Length && (char.IsLetterOrDigit(line[position]) || line[position] == '_'))
            {
                lexeme += line[position];
                if (IsRussianLetter(line[position]))
                {
                    hasRussianLetter = true; // Если найдена русская буква, запоминаем
                }
                position++;
            }

            // Если внутри идентификатора есть русская буква — это ошибка
            if (hasRussianLetter)
            {
                tokens.Add(new Token(TokenType.InvalidCode, "Недопустимый идентификатор", lexeme, lineNumber, start + 1, position));
                return;
            }

            // Проверяем, является ли слово ключевым
            if (TokenType.Keywords.ContainsKey(lexeme))
            {
                tokens.Add(new Token(TokenType.Keywords[lexeme], "Ключевое слово", lexeme, lineNumber, start + 1, position));
                lastTokenWasType = true; // Устанавливаем флаг, что найдено ключевое слово
            }
            else
            {
                tokens.Add(new Token(TokenType.IdentifierCode, "Идентификатор", lexeme, lineNumber, start + 1, position));
            }
        }

        /// <summary>
        /// Обрабатывает недопустимые идентификаторы (если слово начинается с недопустимого символа)
        /// </summary>
        private void ProcessInvalidIdentifier(string line, ref int position, int lineNumber, List<Token> tokens)
        {
            int start = position;
            string lexeme = "";

            // Читаем символы, пока они соответствуют допустимым символам идентификатора (буквы, цифры, _)
            while (position < line.Length && (char.IsLetterOrDigit(line[position]) || line[position] == '_'))
            {
                lexeme += line[position];
                position++;
            }

            // Если удалось считать слово, но оно начинается с недопустимого символа - добавляем в список как недопустимый идентификатор
            if (lexeme.Length > 0)
            {
                tokens.Add(new Token(TokenType.InvalidCode, "Недопустимый идентификатор", lexeme, lineNumber, start + 1, position));
            }
            else
            {
                // Если слово не собрано, а символ недопустимый, добавляем его отдельно
                tokens.Add(new Token(TokenType.InvalidCode, "Недопустимый символ", line[position].ToString(), lineNumber, position + 1, position + 1));
                position++;
            }
        }

        /// <summary>
        /// Проверяет, является ли символ оператором (скобки, запятая, точка с запятой)
        /// </summary>
        private bool IsOperator(char c) => TokenType.Operators.ContainsKey(c.ToString());

        /// <summary>
        /// Проверяет, является ли символ английской буквой (A-Z, a-z)
        /// </summary>
        private bool IsEnglishLetter(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');

        /// <summary>
        /// Проверяет, является ли символ русской буквой (А-Я, а-я)
        /// </summary>
        private bool IsRussianLetter(char c) => (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я');
    }
}
