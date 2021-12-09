using System;
using System.Collections.Generic;

namespace _1._2
{
    class Program
    {
        static void Main()
        {
            List<string> values = new List<string>();
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Введите текст: ");
                    string value = Console.ReadLine();
                    int foundIndex = values.IndexOf(value);
                    if (foundIndex != -1)
                        throw new AlreadyExistsException("Значение было введено ранее", value, foundIndex);

                    values.Add(value);
                }
                catch(AlreadyExistsException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ошибка: " + ex.Message);
                    Console.WriteLine("Значение: " + ex.Value + " вводилось на " + ex.Position + " итерации");
                }
            }
        }
    }
}
