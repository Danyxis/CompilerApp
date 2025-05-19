using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp.PolishReverseNotation
{
    // Класс, преобразующий арифметическое выражение в ПОЛИЗ (обратную польскую нотацию)
    // и вычисляющий результат по ПОЛИЗ
    internal class PolishConverter
    {
        private List<Token> Tokens = new List<Token>();        // Список входных токенов (инфиксная форма)

        private Stack<Token> Stack = new Stack<Token>();       // Стек для операторов (в процессе преобразования)

        public List<Token> outToken = new List<Token>();       // Результат — токены в обратной польской нотации (ПОЛИЗ)

        private Stack<double> stackNum = new Stack<double>();  // Стек чисел для вычисления ПОЛИЗ

        public PolishConverter(List<Token> tokens)
        {
            Tokens = tokens; // Получаем список токенов
        }

        // Метод преобразования инфиксного выражения в ПОЛИЗ
        public void ConvertToPolishReverseNotation()
        {
            foreach (Token token in Tokens)
            {
                if (token.Type == TokenType.Number)
                {
                    // Операнды сразу идут в выходную очередь
                    outToken.Add(token);
                }
                else
                {
                    // Если оператор + - * /
                    if (token.Type == TokenType.Minus || token.Type == TokenType.Plus ||
                        token.Type == TokenType.Multiplication || token.Type == TokenType.Division)
                    {
                        // Если стек пуст — просто кладём оператор
                        if (Stack.Count == 0)
                        {
                            Stack.Push(token);
                        }
                        // Если приоритет на стеке ниже — кладём новый оператор
                        else if (GetPriority(Stack.Peek().Value) < GetPriority(token.Value))
                        {
                            Stack.Push(token);
                        }
                        // Иначе выталкиваем всё с большим или равным приоритетом
                        else
                        {
                            while (Stack.Count > 0 && (GetPriority(Stack.Peek().Value) >= GetPriority(token.Value)))
                            {
                                outToken.Add(Stack.Pop());
                            }
                            Stack.Push(token);
                        }
                    }

                    // Если открывающая скобка
                    if (GetPriority(token.Value) == 0)
                    {
                        Stack.Push(token);
                    }

                    // Если закрывающая скобка — выталкиваем всё до открывающей
                    if (GetPriority(token.Value) == 1)
                    {
                        Token op = Stack.Pop();

                        while (Stack.Count > 0 && GetPriority(op.Value) != 0)
                        {
                            outToken.Add(op);
                            op = Stack.Pop();
                        }
                    }
                }
            }

            // Выталкиваем оставшиеся операторы из стека
            while (Stack.Count > 0)
            {
                outToken.Add(Stack.Pop());
            }
        }

        // Метод вычисления двух операндов по типу оператора
        public double Calculate(TokenType tokenType, double a, double b)
        {
            double result = 0;

            switch (tokenType)
            {
                case TokenType.Minus:
                    result = b - a; // важно: сначала второй, потом первый (стек LIFO)
                    break;

                case TokenType.Plus:
                    result = a + b;
                    break;

                case TokenType.Multiplication:
                    result = a * b;
                    break;

                case TokenType.Division:
                    result = b / a; // снова порядок важен
                    break;
            }

            return result;
        }

        // Метод вычисления результата выражения в ПОЛИЗ
        public double CalculatePolishReverseNotation()
        {
            foreach (var token in outToken)
            {
                if (token.Type == TokenType.Number)
                {
                    // Преобразуем строку в число и кладём в стек
                    stackNum.Push(double.Parse(token.Value));
                }

                if (token.Type == TokenType.Minus || token.Type == TokenType.Plus ||
                    token.Type == TokenType.Multiplication || token.Type == TokenType.Division)
                {
                    // Забираем два числа и вычисляем результат
                    stackNum.Push(Calculate(token.Type, stackNum.Pop(), stackNum.Pop()));
                }
            }

            // Результат — последний оставшийся элемент в стеке
            return stackNum.Peek();
        }

        // Метод определения приоритета операций и скобок
        private int GetPriority(string value)
        {
            switch (value)
            {
                case "(":
                    return 0;

                case ")":
                    return 1;

                case "+":
                    return 7;

                case "-":
                    return 7;

                case "*":
                    return 8;

                case "/":
                    return 8;

                default:
                    return 0;
            }
        }
    }
}
