using System;
using System.Linq;

namespace _1._3
{
    class WordsSequence
    {
        static void Main()
        {
            do
            {
                StartMenu();
                string input = Console.ReadLine().Trim();
                string[] wordsSequence = GetSequenceOfUppercaseWords(input);
                PrintResult(wordsSequence);

                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static void PrintResult(string[] wordsSequence)
        {
            Console.WriteLine("Результат: " + string.Join(" ", wordsSequence));
            Console.WriteLine();
        }

        private static void StartMenu()
        {
            Console.WriteLine();
            Console.Write("Введите слова в произвольном регистре (разделитель - пробел): ");
        }

        private static string[] GetSequenceOfUppercaseWords(string input)
        {
            string[] wordsSequence = input.Split(" ");
            wordsSequence = wordsSequence.Select(word => word.ToUpper())
                                         .OrderBy(word=>word)
                                         .ToArray();
            return wordsSequence;
        }
    }
}
