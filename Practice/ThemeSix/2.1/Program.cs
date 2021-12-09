using System;
using System.Collections.Generic;
using _2._1.Exceptions;

namespace _2._1
{
    class Program
    {
        static List<string> logs = new List<string>();

        static void Main()
        {
            Tank tank = new Tank(500);
            tank.Notify += Logging;
            tank.Notify += ConsolePrinting;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\tУправление уровнем цистерны");
                TankManipulationMenu();

                //Вывод логов
                LogOutput();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.A:
                        ActionOnTheTank(tank.Add);
                        
                        break;
                    case ConsoleKey.S:
                        ActionOnTheTank(tank.Take);

                        break;
                    default:
                        Console.WriteLine();
                        break;
                }

                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
        }

        private static void ActionOnTheTank(Action<double> action)
        {
            try
            {
                Console.WriteLine();
                Console.Write("Обьём: ");
                var volume = double.Parse(Console.ReadLine());
                action(volume);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Ошибка: Объём - число" + "\n" + ex.Message);
            }
            catch (TankOverflowException ex)
            {
                logs[logs.Count - 1] += ex.Message;
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            catch (NotEnoughException ex)
            {
                logs[logs.Count - 1] += ex.Message;
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private static void TankManipulationMenu()
        {
            Console.WriteLine("[A] - Долить жидкость");
            Console.WriteLine("[S] - Слить жидкость");
            Console.Write("Действие: ");
        }

        private static void Logging(string log)
        {
            logs.Add(log);
        }

        private static void ConsolePrinting(string message)
        {
            Console.WriteLine(message);
        }

        private static void LogOutput()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            for (int i = 0; i < 6; ++i)
            {
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', Console.WindowWidth));
            foreach (var log in logs)
            {
                Console.WriteLine(log);
            }
            Console.SetCursorPosition(left, top);
        }
    }
}
