using System;
using System.Collections.Generic;
using System.Text;

namespace _1._3
{
    class _1_3
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tПодсчёт и вывед количества введенных чисел, минимальное, максимальное числа, сумму и среднее арифметическое. ");
            Console.Write("Введите последовательность рац.чисел: ");
            
            int amountOfNumbers = 0;
            double min = 0,
                max = 0,
                sum = 0,
                arithmeticalMean = 0;

            try
            {
                SequenceHandlerWithoutArray.ProcessingSequenceOfRationalNumbersSequenceInput(out amountOfNumbers, out min, out max, 
                    out sum, out arithmeticalMean);
                Console.WriteLine(GetResult(amountOfNumbers, min, max, arithmeticalMean));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static string GetResult(int amountOfNumbers, double min, double max, double arithmeticalMean)
        {
            return ("Результат: \t\nкол-во: " + amountOfNumbers + "\t\nMin / Max: " + min + " " + max 
                + "\t\nСреднее: " + arithmeticalMean);
        }

    }
}
