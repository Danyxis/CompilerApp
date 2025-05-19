using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    // Класс синтаксического анализатора (парсера)
    // (анализ по токенам, до первой ошибки, без нейтрализации ошибок)
    internal class Parser
    {
        private State state = State.START; // Текущее состояние конечного автомата
        private Token currentToken; // Текущий токен, обрабатываемый парсером
        public List<Token> Tokens { get; } // Все входные токены (после лексического анализа)
        public List<ParseError> Errors { get; } = new List<ParseError>(); // Список найденных синтаксических ошибок

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
        }

        // Основной метод синтаксического анализа
        public void Parse()
        {
            foreach (var token in Tokens)
            {
                if (state == State.ERROR) break; // Если уже зафиксирована ошибка — прекращаем анализ

                currentToken = token;

                // Переход к соответствующему методу по текущему состоянию
                switch (state)
                {
                    case State.START: StateSTART(); break;
                    case State.SPACE: StateSPACE(); break;
                    case State.IDFUNC: StateIDFUNC(); break;
                    case State.IDFUNCREM: StateIDFUNCREM(); break;
                    case State.EMPTY: StateEMPTY(); break;
                    case State.PARAMETERS: StatePARAMETERS(); break;
                    case State.SPACE2: StateSPACE2(); break;
                    case State.IDPARAM: StateIDPARAM(); break;
                    case State.IDPARAMREM: StateIDPARAMREM(); break;
                    case State.END: StateEND(); break;
                    case State.ERROR: break;
                    default: break;
                }
            }

            // Проверка на незавершенное выражение (если строка обрывается)
            CheckForUnfinishedExpression();
        }

        // Устанавливает новое состояние автомата
        private void SetState(State newState) => state = newState;

        // Добавляет ошибку и переходит в состояние ERROR
        private void HandleError(string message, Token token)
        {
            Errors.Add(new ParseError(message, token.Value, token.Position));
            state = State.ERROR; // Останавливаем анализ
        }

        /// <summary>
        /// Каждое состояние проверяет ожидаемый токен и устанавливает следующее состояние или фиксирует ошибку
        /// </summary>
        private void StateSTART()
        {
            // Ожидается тип данных (int, float, string и т.д.)
            if (TokenType.Keywords.ContainsKey(currentToken.Value))
                SetState(State.SPACE);
            else
                HandleError("Ожидался тип данных", currentToken);
        }

        private void StateSPACE()
        {
            // Ожидается пробел после типа
            if (currentToken.TypeCode == TokenType.SpaceCode)
                SetState(State.IDFUNC);
            else
                HandleError("Ожидался пробел после типа данных", currentToken);
        }

        private void StateIDFUNC()
        {
            // Ожидается идентификатор (имя функции)
            if (currentToken.TypeCode == TokenType.IdentifierCode)
                SetState(State.IDFUNCREM);
            else
                HandleError("Ожидался идентификатор функции", currentToken);
        }

        private void StateIDFUNCREM()
        {
            // Ожидается открывающая скобка
            if (currentToken.Value == "(")
            {
                SetState(State.EMPTY);
            }
            else
            {
                HandleError("Ожидалась открывающая скобка '('", currentToken);
            }
        }

        private void StateEMPTY()
        {
            // Если после ( сразу ) — параметры пустые
            if (currentToken.Value == ")")
            {
                SetState(State.END);
            }
            // Иначе должен быть тип данных параметра
            else if (TokenType.Keywords.ContainsKey(currentToken.Value))
            {
                SetState(State.SPACE2);
            }
            else
            {
                HandleError("Ожидался тип данных или закрывающая скобка ')'", currentToken);
            }
        }

        private void StatePARAMETERS()
        {
            // Ожидается тип данных следующего параметра
            if (TokenType.Keywords.ContainsKey(currentToken.Value))
                SetState(State.SPACE2);
            else
                HandleError("Ожидался тип данных параметра", currentToken);
        }

        private void StateSPACE2()
        {
            // После типа параметра — пробел
            if (currentToken.TypeCode == TokenType.SpaceCode)
                SetState(State.IDPARAM);
            else
                HandleError("Ожидался пробел после типа данных параметра", currentToken);
        }

        private void StateIDPARAM()
        {
            // Ожидается идентификатор (имя параметра)
            if (currentToken.TypeCode == TokenType.IdentifierCode)
                SetState(State.IDPARAMREM);
            else
                HandleError("Ожидался идентификатор параметра", currentToken);
        }

        private void StateIDPARAMREM()
        {
            // После имени параметра — либо запятая (следующий параметр), либо закрывающая скобка
            if (currentToken.Value == ",")
                SetState(State.PARAMETERS);
            else if (currentToken.Value == ")")
                SetState(State.END);
            else
                HandleError("Ожидалась запятая ',' или закрывающая скобка ')'", currentToken);
        }

        private void StateEND()
        {
            // В конце выражения ожидается точка с запятой
            if (currentToken.Value == ";")
                SetState(State.START); // Готов к следующему выражению
            else
                HandleError("Ожидался конец оператора ';'", currentToken);
        }

        // Если токены закончились, но выражение не завершено — сообщаем об ошибке
        private void CheckForUnfinishedExpression()
        {
            if (state == State.START || state == State.ERROR)
                return; // Всё хорошо или ошибка уже зафиксирована

            // Формируем ожидаемый элемент по текущему состоянию
            string expected = state switch
            {
                State.SPACE => "пробел после типа данных",
                State.IDFUNC => "идентификатор функции",
                State.IDFUNCREM => "открывающая скобка '('",
                State.EMPTY => "тип данных или закрывающая скобка ')'",
                State.PARAMETERS => "тип данных параметра",
                State.SPACE2 => "пробел после типа данных параметра",
                State.IDPARAM => "идентификатор параметра",
                State.IDPARAMREM => "запятая ',' или закрывающая скобка ')'",
                State.END => "конец оператора ';'",
                _ => "продолжение выражения"
            };

            // Добавляем ошибку об обрыве конструкции
            Errors.Add(new ParseError(
                $"Ожидалось: {expected}",
                currentToken?.Value ?? "EOF",
                currentToken?.Position ?? "конец строки"
            ));
        }
    }
}
