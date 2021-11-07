using System;
using System.Collections.Generic;
using System.Text;

namespace _1._3
{
    class BracketsSequence
    {
        private static Stack<string> bracketsStack = new Stack<string>();
        private static string[] initialBrackets = new string[] { "(", "{", "[", "<" };
        private static string[] endBrackets = new string[] { ")", "}", "]", ">" };

        static void Main()
        {
            do
            {
                Console.WriteLine();
                Console.Write("Введите скобочную последовательность: ");
                string inpuString = Console.ReadLine();
                StringBuilder input = new StringBuilder(inpuString);
                int errorIndex;
                string errorMessage;

                if (IsBracketsSequenceTrue(input, out errorIndex, out errorMessage))
                {
                    Console.WriteLine("Скобочная последовательность верна или не найдена");
                }
                else
                {
                    Console.WriteLine("Ошибка!");
                    Console.Write((errorMessage != String.Empty) ? errorMessage
                        : "Скобочная последовательность нарушена: ");
                    if (errorMessage == String.Empty)
                    {
                        for (int i = 0; i < input.Length; i++)
                        {
                            if (i == errorIndex)
                                Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{input[i]}");
                            Console.ResetColor();
                        }
                    }
                }
                Console.WriteLine();

                Console.WriteLine("Escape - выход || Иное - продолжить");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static bool IsBracketsSequenceTrue(StringBuilder input, out int errorIndex, out string errorMessage)
        {
            bool isSequenceFailed = false,
                 isMatchFound;
            errorIndex = -1;
            errorMessage = String.Empty;
            List<int> entryIndexes = new List<int>();

            for (int i = 0; i < input.Length; ++i)
            {
                isMatchFound = false;
                foreach (var initialBracket in initialBrackets)
                {
                    if (input[i].ToString() == initialBracket)
                    {
                        bracketsStack.Push(input[i].ToString());
                        entryIndexes.Add(i);
                        isMatchFound = true;
                        break;
                    }
                }
                if (isMatchFound)
                    continue;
                for (int j = 0; j < endBrackets.Length; ++j)
                {
                    if (input[i].ToString() == endBrackets[j])
                    {
                        if (bracketsStack.Count != 0)
                        {
                            var bracket = bracketsStack.Pop();
                            if (bracket == initialBrackets[j])
                            {
                                entryIndexes.RemoveAt(entryIndexes.Count - 1);
                            }
                            else
                            {
                                isSequenceFailed = true;
                            }
                            break;
                        }
                        else
                        {
                            isSequenceFailed = true;
                            errorMessage = "Не хватает открывающих скобок";
                        }
                    }
                }

            }
            if (isSequenceFailed || bracketsStack.Count != 0)
            {
                if (entryIndexes.Count != 0)
                {
                    errorIndex = entryIndexes[entryIndexes.Count - 1];
                }
                return false;
            }
            return true;
        }


    }
}
