using System;

namespace _1._1
{
    class KeyCode2Hex
    {
        static void Main()
        {
            do
            {
                StartMenu();
                var key = Console.ReadKey().Key;
                PrintResult(key);
            } while (true);
        }

        private static void PrintResult(ConsoleKey key)
        {
            Console.WriteLine("\n\nКод клавиши: 0x{0:X4}", GetIntKeydordKey(key));
        }

        private static int GetIntKeydordKey(ConsoleKey key) => (int)key;

        private static void StartMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Бесконечный ввод, как ни печально ...");
            Console.Write("Нажмите любую клавишу: ");
        }
    }
}
