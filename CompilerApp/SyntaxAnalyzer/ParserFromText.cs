using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    // Класс синтаксического анализатора (парсера)
    // (анализ по символам, с выводом всех ошибок, с нейтрализацией ошибок)
    // Частично работает корректно
    internal class ParserFromText
    {
        // Входные строки (все строки из редактора, могут быть многострочными)
        private readonly string[] _lines;

        // Список ошибок, найденных в процессе синтаксического анализа
        private readonly List<ParseError> _errors = new();

        // Текущая строка (вся программа как одна строка после объединения)
        private string _line;

        // Позиция внутри строки _line
        private int _pos;

        // Номер строки — используется для сообщений об ошибках (фиксированный = 1)
        private int _lineNumber;

        // Текущий символ в позиции _pos или '\0', если позиция вышла за границу строки
        private char Current => _pos < _line.Length ? _line[_pos] : '\0';

        // Публичный доступ к списку ошибок
        public List<ParseError> Errors => _errors;

        // Конструктор — принимает строки из редактора
        public ParserFromText(string[] lines)
        {
            _lines = lines;
        }

        // Основной метод запуска синтаксического анализа
        public void Parse()
        {
            _lineNumber = 1;

            // Объединяем все строки из редактора в одну строку через пробел
            _line = string.Join(" ", _lines);
            _pos = 0;

            // Пока не достигнут конец строки — пытаемся разобрать объявления
            while (_pos < _line.Length)
            {
                ParseFunctionPrototype();
                SkipWhitespace(); // Убираем пробелы между объявлениями
            }
        }

        // Метод разбора одного объявления: тип имя(параметры);
        private void ParseFunctionPrototype()
        {
            SkipWhitespace(); // Пропускаем пробелы в начале

            // Попытка найти тип данных (с поддержкой нейтрализации мусора)
            if (!MatchKeywordWithNeutralization(out var typeStart, out var typeEnd))
            {
                return; // Если ошибка уже добавлена — выход
            }

            // Проверка наличия пробела после типа
            int wsStart = _pos;
            bool foundWhitespace = false;

            // Ищем пробел и собираем мусор до него
            while (_pos < _line.Length)
            {
                if (_line[_pos] == ' ')
                {
                    foundWhitespace = true;
                    break;
                }
                _pos++;
            }

            // Если нашли пробел
            if (foundWhitespace)
            {
                if (wsStart < _pos)
                {
                    string junk = _line.Substring(wsStart, _pos - wsStart);
                    string pos = $"строка {_lineNumber}, символы {wsStart + 1}-{_pos}";
                    _errors.Add(new ParseError("Ожидался пробел после типа данных", $"Отброшенный фрагмент: {junk}", pos));
                }
                _pos++; // Пропускаем найденный пробел
            }
            else
            {
                // Пробела нет вообще — это ошибка
                if (wsStart < _line.Length)
                {
                    string junk = _line.Substring(wsStart);
                    string pos = $"строка {_lineNumber}, символы {wsStart + 1}-{_line.Length}";
                    _errors.Add(new ParseError("Ожидался пробел после типа данных", $"Отброшенный фрагмент: {junk}", pos));
                }
                else
                {
                    string pos = $"строка {_lineNumber}, символ {_line.Length}";
                    _errors.Add(new ParseError("Ожидался пробел после типа данных", "", pos));
                }
                return;
            }

            // Поиск имени функции (идентификатор) с нейтрализацией
            if (!MatchIdentifierWithNeutralization(out var idStart, out var idEnd))
            {
                return; // Если ошибка уже добавлена — выход
            }

            // Ожидаем символ открывающей скобки
            if (!MatchChar('('))
            {
                CollectGarbage("Ожидалась открывающая скобка '('", _pos);
                return;
            }

            SkipWhitespace(); // Пропускаем пробелы перед параметрами

            // Обработка параметров или пустого списка
            if (Current == ')')
            {
                _pos++; // Пустой список параметров
            }
            else
            {
                if (!ParseParameterList())
                    return;

                // Закрывающая скобка после параметров
                if (!MatchChar(')'))
                {
                    CollectGarbage("Ожидалась запятая ',' или закрывающая скобка ')'", _pos);
                    return;
                }
            }

            // Ожидаем точку с запятой в конце объявления
            if (!MatchChar(';'))
            {
                CollectGarbage("Ожидался конец оператора ';'", _pos);
            }

            // После завершённого объявления — пробуем снова запустить парсинг "с нуля" на остатке строки
            SkipWhitespace();
            if (_pos < _line.Length)
            {
                ParseFunctionPrototype(); // Рекурсивно продолжаем разбор
            }
        }

        // Обрабатывает список параметров внутри скобок: (тип имя, тип имя, ...)
        private bool ParseParameterList()
        {
            bool afterComma = false;

            while (true)
            {
                SkipWhitespace(); // Убираем пробелы перед параметром

                // Тип данных параметра
                if (!MatchKeywordWithNeutralization(out var typeStart, out var typeEnd))
                {
                    string message = afterComma
                        ? "Ожидался тип данных параметра"
                        : "Ожидался тип данных параметра или закрывающая скобка ')'";
                    CollectGarbage(message, _pos);
                    return false;
                }

                // Пробел между типом и именем параметра
                if (!MatchWhitespace())
                {
                    CollectGarbage("Ожидался пробел после типа данных параметра", _pos);
                    return false;
                }

                // Имя параметра
                if (!MatchParameterIdentifierWithNeutralization(out var nameStart, out var nameEnd))
                {
                    return false; // Если ошибка уже добавлена — выход
                }

                SkipWhitespace(); // Убираем пробелы после имени

                // Проверка наличия запятой для следующего параметра
                if (Current == ',')
                {
                    _pos++; // Пропускаем запятую
                    afterComma = true; // Следом обязательно должен идти тип
                    continue;
                }

                // Если нет запятой — выходим (ожидаем закрытие скобки дальше)
                break;
            }

            return true;
        }

        // Пропускает все пробелы и табуляции
        private void SkipWhitespace()
        {
            while (char.IsWhiteSpace(Current)) _pos++;
        }

        // Проверяет наличие пробела и продвигает указатель
        private bool MatchWhitespace()
        {
            if (Current == ' ')
            {
                _pos++;
                return true;
            }
            return false;
        }

        // Проверяет, совпадает ли текущий символ с ожидаемым
        private bool MatchChar(char expected)
        {
            if (Current == expected)
            {
                _pos++;
                return true;
            }
            return false;
        }

        // Простой метод для разбора идентификатора (используется только в базовых местах)
        private bool MatchIdentifier(out int start, out int end)
        {
            start = _pos;

            // Идентификатор должен начинаться с английской буквы
            if (!(char.IsLetter(Current) && Current <= 127))
            {
                end = start;
                return false;
            }

            _pos++;

            // Остальная часть может содержать английские буквы, цифры и подчёркивания
            while ((char.IsLetterOrDigit(Current) && Current <= 127) || Current == '_')
            {
                _pos++;
            }

            end = _pos;
            return true;
        }

        // Метод для сбора мусорного фрагмента при неожиданном символе или структуре
        private void CollectGarbage(string message, int start)
        {
            int errorStart = start;

            // Двигаемся вперёд, пока не дойдём до символа-разделителя
            while (_pos < _line.Length &&
                !char.IsWhiteSpace(Current) &&
                Current != ',' && Current != ')' && Current != ';')
                _pos++;

            int errorEnd = Math.Max(errorStart, _pos - 1);

            // Если удалось собрать фрагмент мусора — добавляем его как ошибку
            if (errorEnd >= errorStart && errorEnd < _line.Length)
            {
                string value = _line.Substring(errorStart, errorEnd - errorStart + 1);
                string position = $"строка {_lineNumber}, символы {errorStart + 1}-{errorEnd + 1}";
                _errors.Add(new ParseError(message, value, position));
            }
            else
            {
                // Иначе просто добавим ошибку с текущей позицией
                string pos = $"строка {_lineNumber}, символ {_pos + 1}";
                _errors.Add(new ParseError(message, "", pos));
            }
        }

        // Разбирает тип данных (int, float, char, ...) с нейтрализацией мусора
        private bool MatchKeywordWithNeutralization(out int typeStart, out int typeEnd)
        {
            typeStart = _pos;

            // Смотрим до первого пробела или конца строки
            int lookahead = _pos;
            while (lookahead < _line.Length && _line[lookahead] != ' ')
                lookahead++;

            // Фрагмент, который мы хотим разобрать как тип
            string fragment = _line.Substring(_pos, lookahead - _pos);

            // Допустимые типы данных
            string[] keywords = { "int", "float", "char", "string", "bool" };

            // Проверим, можно ли из мусора собрать допустимый тип
            foreach (var keyword in keywords)
            {
                int keywordIndex = 0; // Позиция в ключевом слове
                List<int> accepted = new(); // Принятые символы
                List<(int index, char c)> garbage = new(); // Мусор между символами

                for (int i = 0; i < fragment.Length && keywordIndex < keyword.Length; i++)
                {
                    if (char.ToLower(fragment[i]) == keyword[keywordIndex])
                    {
                        accepted.Add(i);
                        keywordIndex++;
                    }
                    else
                    {
                        garbage.Add((i, fragment[i]));
                    }
                }

                // Если удалось собрать весь тип
                if (keywordIndex == keyword.Length)
                {
                    // Группируем мусорные последовательности
                    var groups = GroupGarbageSequences(garbage);

                    foreach (var g in groups)
                    {
                        string pos = $"строка {_lineNumber}, символы {_pos + g.start + 1}-{_pos + g.end + 1}";
                        _errors.Add(new ParseError($"Ожидался тип данных {keyword}", $"Отброшенный фрагмент: {g.value}", pos));
                    }

                    // Продвигаем позицию до конца последнего принятого символа
                    int lastAccepted = accepted.Last();
                    _pos += lastAccepted + 1;
                    typeEnd = _pos;
                    return true;
                }
            }

            // Ни один тип не подошёл — вся строка считается ошибкой
            string failed = _line.Substring(_pos, lookahead - _pos);

            if (!string.IsNullOrWhiteSpace(failed))
            {
                string pos = $"строка {_lineNumber}, символы {_pos + 1}-{lookahead}";
                _errors.Add(new ParseError("Ожидался тип данных", failed, pos));
            }

            _pos = lookahead;
            typeEnd = _pos;
            return false;
        }

        // Поиск идентификатора функции с нейтрализацией ошибок
        private bool MatchIdentifierWithNeutralization(out int start, out int end)
        {
            start = _pos;

            // Двигаемся до первой открывающей скобки (конец идентификатора)
            int lookahead = _pos;
            while (lookahead < _line.Length && _line[lookahead] != '(')
                lookahead++;

            // Если ничего нет между типом и скобкой — это ошибка
            if (lookahead == _pos)
            {
                end = _pos;
                return false;
            }

            // Извлекаем подстроку и анализируем
            string fragment = _line.Substring(_pos, lookahead - _pos);
            List<(int index, char c)> garbage = new();
            List<(int index, char c)> valid = new();

            bool foundFirstLetter = false;

            // Первый символ должен быть английской буквой
            for (int i = 0; i < fragment.Length; i++)
            {
                char c = fragment[i];

                if (!foundFirstLetter)
                {
                    if (char.IsLetter(c) && c <= 127)
                    {
                        valid.Add((i, c));
                        foundFirstLetter = true;
                    }
                    else
                    {
                        garbage.Add((i, c));
                    }
                }
                else
                {
                    // После первой буквы допустимы английские буквы, цифры и подчёркивание
                    if ((char.IsLetter(c) && c <= 127) || char.IsDigit(c) || c == '_')
                    {
                        valid.Add((i, c));
                    }
                    else
                    {
                        garbage.Add((i, c)); // всё остальное — мусор
                    }
                }
            }

            // Если не было ни одной англ. буквы — это ошибка
            if (!foundFirstLetter)
            {
                string pos = $"строка {_lineNumber}, символы {_pos + 1}-{lookahead}";
                _errors.Add(new ParseError("Ожидался идентификатор функции", fragment, pos));
                _pos = lookahead;
                end = _pos;
                return false;
            }

            // Добавляем в ошибки все группы мусора
            var groups = GroupGarbageSequences(garbage);

            foreach (var g in groups)
            {
                string pos = $"строка {_lineNumber}, символы {_pos + g.start + 1}-{_pos + g.end + 1}";
                _errors.Add(new ParseError("Ожидался идентификатор функции", $"Отброшенный фрагмент: {g.value}", pos));
            }

            _pos = lookahead;
            end = _pos;
            return true;
        }

        // Поиск идентификатора параметра с нейтрализацией ошибок
        private bool MatchParameterIdentifierWithNeutralization(out int start, out int end)
        {
            start = _pos;

            // Двигаемся до ближайшего ограничителя (запятая, скобка, пробел)
            int lookahead = _pos;
            while (lookahead < _line.Length &&
                   !char.IsWhiteSpace(_line[lookahead]) &&
                   _line[lookahead] != ',' &&
                   _line[lookahead] != ')' &&
                   _line[lookahead] != ';')
            {
                lookahead++;
            }

            // Пустой фрагмент — это ошибка
            if (lookahead == _pos)
            {
                end = _pos;
                return false;
            }

            // Извлекаем подстроку и анализируем
            string fragment = _line.Substring(_pos, lookahead - _pos);
            List<(int index, char c)> garbage = new();
            List<(int index, char c)> valid = new();

            bool foundFirstLetter = false;

            // Первый символ должен быть английской буквой
            for (int i = 0; i < fragment.Length; i++)
            {
                char c = fragment[i];

                if (!foundFirstLetter)
                {
                    if (char.IsLetter(c) && c <= 127)
                    {
                        valid.Add((i, c));
                        foundFirstLetter = true;
                    }
                    else
                    {
                        garbage.Add((i, c));
                    }
                }
                else
                {
                    // После первой буквы допустимы английские буквы, цифры и подчёркивание
                    if ((char.IsLetter(c) && c <= 127) || char.IsDigit(c) || c == '_')
                    {
                        valid.Add((i, c));
                    }
                    else
                    {
                        garbage.Add((i, c));
                    }
                }
            }

            // Если не было ни одной англ. буквы — это ошибка
            if (!foundFirstLetter)
            {
                string pos = $"строка {_lineNumber}, символы {_pos + 1}-{lookahead}";
                _errors.Add(new ParseError("Ожидался идентификатор параметра", fragment, pos));
                _pos = lookahead;
                end = _pos;
                return false;
            }

            // Добавляем в ошибки все группы мусора
            var groups = GroupGarbageSequences(garbage);

            foreach (var g in groups)
            {
                string pos = $"строка {_lineNumber}, символы {_pos + g.start + 1}-{_pos + g.end + 1}";
                _errors.Add(new ParseError("Ожидался идентификатор параметра", $"Отброшенный фрагмент: {g.value}", pos));
            }

            _pos = lookahead;
            end = _pos;
            return true;
        }

        // Группировка подряд идущих символов-мусора в одну ошибку
        private List<(int start, int end, string value)> GroupGarbageSequences(List<(int index, char c)> garbage)
        {
            var groups = new List<(int, int, string)>();

            if (garbage.Count == 0)
                return groups;

            // Начинаем с первого символа
            int start = garbage[0].index;
            int end = start;
            string current = garbage[0].c.ToString();

            for (int i = 1; i < garbage.Count; i++)
            {
                // Если следующий символ сразу за предыдущим — продолжаем текущую группу
                if (garbage[i].index == end + 1)
                {
                    end++;
                    current += garbage[i].c;
                }
                else
                {
                    // Завершаем текущую группу
                    groups.Add((start, end, current));

                    // Начинаем новую
                    start = garbage[i].index;
                    end = start;
                    current = garbage[i].c.ToString();
                }
            }

            // Добавляем последнюю группу
            groups.Add((start, end, current));
            return groups;
        }
    }

    //// Класс синтаксического анализатора (парсера)
    //// (анализ по символам, с выводом всех ошибок, без нейтрализации ошибок)
    //// Частично работает корректно, закомментированные части внутри алгоритма - не расскоментировать!
    //internal class ParserFromText
    //{
    //    private readonly string[] _lines;               // Все строки из редактора
    //    private string _line;                           // Объединённая строка
    //    private int _pos;                               // Текущая позиция в строке
    //    private int _lineNumber = 1;                    // Номер строки (всегда 1, если строки объединены)
    //    private readonly List<ParseError> _errors = new(); // Ошибки, найденные при анализе

    //    // Текущий символ в позиции _pos или '\0', если вышли за границу
    //    private char Current => _pos < _line.Length ? _line[_pos] : '\0';

    //    public List<ParseError> Errors => _errors;      // Публичный доступ к списку ошибок

    //    // Конструктор принимает все строки из редактора
    //    public ParserFromText(string[] lines)
    //    {
    //        _lines = lines;
    //    }

    //    // Главный метод запуска синтаксического анализа
    //    public void Parse()
    //    {
    //        _line = string.Join(" ", _lines); // Объединяем строки в одну
    //        _pos = 0;

    //        // Запускаем разбор до конца строки
    //        while (_pos < _line.Length)
    //        {
    //            ParseFunctionPrototype(); // Пытаемся разобрать одно объявление
    //            SkipWhitespace();         // Пропускаем пробелы между объявлениями
    //        }
    //    }

    //    // Пропускает все пробелы и табуляции в строке
    //    private void SkipWhitespace()
    //    {
    //        while (_pos < _line.Length && char.IsWhiteSpace(_line[_pos]))
    //        {
    //            _pos++;
    //        }
    //    }

    //    // Метод разбора одного объявления функции: тип имя(параметры);
    //    private void ParseFunctionPrototype()
    //    {
    //        SkipWhitespace();

    //        // 1. Ожидаем тип данных (int, float и т.д.)
    //        if (!MatchType(out var typeStart, out var typeEnd))
    //        {
    //            SkipToNextAfterSpace(); // Переход к следующей лексеме после пробела
    //        }

    //        // 2. Ожидаем пробел после типа
    //        if (!MatchWhitespace())
    //        {
    //            ReportError("Ожидался пробел после типа данных", _pos, _pos);
    //        }

    //        // 3. Ожидаем идентификатор (имя функции)
    //        if (!MatchIdentifier(out var idStart, out var idEnd))
    //        {
    //            /*SkipToNextChar('(');*/ // Переход к скобке
    //        }

    //        // 4. Ожидаем открывающую скобку (
    //        bool hasOpenedBracket = MatchChar('(');
    //        if (!hasOpenedBracket)
    //        {
    //            ReportError("Ожидалась открывающая скобка '('", _pos, _pos);
    //            /*SkipToNextChar('(');*/ // Пропускаем до скобки
    //            hasOpenedBracket = true;
    //        }

    //        if (hasOpenedBracket)
    //        {
    //            SkipWhitespace();

    //            // 5. Параметры: если скобка закрыта сразу — параметров нет
    //            if (Current == ')')
    //            {
    //                _pos++; // Просто закрываем скобку
    //            }
    //            else
    //            {
    //                ParseParameterList(); // Даже если есть ошибки — продолжаем

    //                // Закрывающая скобка
    //                if (!MatchChar(')'))
    //                {
    //                    ReportError("Ожидалась закрывающая скобка ')'", _pos, _pos);
    //                }
    //            }

    //            // 6. Завершающая точка с запятой
    //            if (!MatchChar(';'))
    //            {
    //                ReportError("Ожидался конец оператора ';'", _pos, _pos);
    //            }
    //        }
    //    }

    //    // Разбирает список параметров: тип имя, тип имя, ...
    //    private bool ParseParameterList()
    //    {
    //        while (true)
    //        {
    //            SkipWhitespace();

    //            // Тип параметра
    //            if (!MatchType(out var typeStart, out var typeEnd))
    //            {
    //                /*SkipToNextChar(',', ')');*/ // Пропускаем до следующего разделителя
    //                SkipToNextAfterSpace(); // Переход к следующей лексеме после пробела
    //                //return false;
    //            }

    //            // Обязательный пробел
    //            if (!MatchWhitespace())
    //            {
    //                ReportError("Ожидался пробел после типа данных параметра", _pos, _pos);
    //            }

    //            // Имя параметра
    //            if (!MatchParameterIdentifier(out var nameStart, out var nameEnd))
    //            {
    //                /*SkipToNextChar(',', ')');*/ // Пропускаем до разделителя
    //                return false;
    //            }

    //            SkipWhitespace();

    //            // Следующий символ — либо запятая, либо закрывающая скобка
    //            if (Current == ',')
    //            {
    //                _pos++; // Пропускаем запятую и идём за следующим параметром
    //                continue;
    //            }

    //            if (Current == ')')
    //            {
    //                return true; // Параметры завершены
    //            }

    //            // Неожиданный символ — ошибка
    //            ReportError("Ожидалась запятая ',' или закрывающая скобка ')'", _pos, _pos);
    //            SkipToNextChar(',', ')');
    //            return false;
    //        }
    //    }

    //    // Проверка и разбор типа данных (int, float и т.д.)
    //    // Если найден некорректный тип — выделяет всю подстроку до пробела как ошибку
    //    private bool MatchType(out int start, out int end)
    //    {
    //        start = _pos;

    //        // Двигаемся до пробела или конца строки — предполагаем, что это "тип"
    //        int lookahead = _pos;
    //        while (lookahead < _line.Length && !char.IsWhiteSpace(_line[lookahead]))
    //        {
    //            lookahead++;
    //        }

    //        string fragment = _line.Substring(_pos, lookahead - _pos);

    //        // Список допустимых типов
    //        string[] validTypes = { "int", "float", "char", "string", "bool" };

    //        if (validTypes.Contains(fragment))
    //        {
    //            // Тип корректный
    //            end = lookahead;
    //            _pos = lookahead; // Продвигаем указатель
    //            return true;
    //        }
    //        else
    //        {
    //            // Тип некорректный — фиксируем как ошибку
    //            ReportError("Ожидался тип данных", start, lookahead - 1, fragment);
    //            end = lookahead;
    //            _pos = lookahead;
    //            return false;
    //        }
    //    }

    //    // Проверяет, есть ли хотя бы один пробел. Если нет — сообщает об ошибке.
    //    private bool MatchWhitespace()
    //    {
    //        if (_pos < _line.Length && _line[_pos] == ' ')
    //        {
    //            _pos++; // Пропускаем только один пробел — этого достаточно
    //            return true;
    //        }

    //        return false;
    //    }

    //    // Проверяет идентификатор: должен начинаться с англ. буквы и содержать только буквы и цифры
    //    private bool MatchIdentifier(out int start, out int end)
    //    {
    //        start = _pos;
    //        int lookahead = _pos;

    //        // Двигаемся до ограничителя (например, скобки или пробела)
    //        while (lookahead < _line.Length && _line[lookahead] != '(' && !char.IsWhiteSpace(_line[lookahead]))
    //        {
    //            lookahead++;
    //        }

    //        string fragment = _line.Substring(_pos, lookahead - _pos);

    //        if (IsValidIdentifier(fragment))
    //        {
    //            end = lookahead;
    //            _pos = lookahead;
    //            return true;
    //        }
    //        else
    //        {
    //            ReportError("Ожидался идентификатор функции", _pos, lookahead - 1, fragment);
    //            end = lookahead;
    //            _pos = lookahead;
    //            return false;
    //        }
    //    }

    //    // Проверяет идентификатор параметра: заканчивается на запятую, пробел или скобку
    //    private bool MatchParameterIdentifier(out int start, out int end)
    //    {
    //        start = _pos;
    //        int lookahead = _pos;

    //        // Двигаемся до ближайшего ограничителя: запятая, пробел, закрывающая скобка
    //        while (lookahead < _line.Length &&
    //               _line[lookahead] != ',' &&
    //               _line[lookahead] != ')' &&
    //               !char.IsWhiteSpace(_line[lookahead]) &&
    //               _line[lookahead] != ';')
    //        {
    //            lookahead++;
    //        }

    //        string fragment = _line.Substring(_pos, lookahead - _pos);

    //        if (IsValidIdentifier(fragment))
    //        {
    //            end = lookahead;
    //            _pos = lookahead;
    //            return true;
    //        }
    //        else
    //        {
    //            ReportError("Ожидался идентификатор параметра", _pos, lookahead - 1, fragment);
    //            end = lookahead;
    //            _pos = lookahead;
    //            return false;
    //        }
    //    }

    //    // Проверяет, соответствует ли строка правилам идентификатора
    //    private bool IsValidIdentifier(string value)
    //    {
    //        if (string.IsNullOrEmpty(value)) return false;

    //        // Первый символ — только английская буква
    //        if (!char.IsLetter(value[0]) || value[0] > 127)
    //            return false;

    //        // Остальные — буквы или цифры
    //        for (int i = 1; i < value.Length; i++)
    //        {
    //            char c = value[i];
    //            if (!(char.IsLetterOrDigit(c) && c <= 127))
    //                return false;
    //        }

    //        return true;
    //    }

    //    // Проверяет, соответствует ли текущий символ ожидаемому, иначе сообщает об ошибке
    //    private bool MatchChar(char expected)
    //    {
    //        if (_pos < _line.Length && _line[_pos] == expected)
    //        {
    //            _pos++; // Пропускаем символ
    //            return true;
    //        }

    //        return false;
    //    }

    //    // Пропускает символы до ближайшего одного из указанных символов
    //    private void SkipToNextChar(params char[] stopChars)
    //    {
    //        while (_pos < _line.Length && !stopChars.Contains(_line[_pos]))
    //        {
    //            _pos++;
    //        }

    //        if (_pos < _line.Length)
    //        {
    //            _pos++; // Пропускаем найденный символ
    //        }
    //    }

    //    // Пропускает текущую подстроку до следующего пробела (используется после ошибки в типе)
    //    private void SkipToNextAfterSpace()
    //    {
    //        while (_pos < _line.Length && _line[_pos] != ' ')
    //        {
    //            _pos++;
    //        }

    //        // Пропускаем сам пробел, если он есть
    //        if (_pos < _line.Length && _line[_pos] == ' ')
    //        {
    //            //_pos++;
    //            return;
    //        }
    //    }

    //    // Добавляет ошибку с заданным сообщением и позициями символов
    //    private void ReportError(string message, int start, int end, string value = "")
    //    {
    //        string position = start == end
    //            ? $"строка {_lineNumber}, символ {start + 1}"
    //            : $"строка {_lineNumber}, символы {start + 1}-{end + 1}";

    //        _errors.Add(new ParseError(message, value, position));
    //    }
    //}
}
