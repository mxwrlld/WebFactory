using System;

namespace _1._3
{
    class SequenceHandlerWithArray
    {
        static void NeMain(string[] args)
        {
            Console.WriteLine("\tПодсчёт и вывед количества введенных чисел, минимальное, максимальное числа, сумму и среднее арифметическое. ");
            Console.Write("Введите последовательность рац.чисел: ");
            string enteredSequence = Console.ReadLine();
            int amountOfNumbers = 0;
            double min = 0,
                max = 0,
                sum = 0,
                arithmeticalMean = 0;

            try
            {
                string[] rationalNumberSequence = enteredSequence.Split(" ");
                if (!CheckingEnteredSequence(rationalNumberSequence))
                    throw new Exception("Последовательность должна состоять только из рациональных чисел!");

                double[] rationalNumberArray = stringArr2doubleArr(rationalNumberSequence);
                ProcessingSequenceOfRationalNumbers(rationalNumberArray, out amountOfNumbers, out min, out max, out sum, out arithmeticalMean);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Ошибка: {ex.Message}"); 
            }
        }

        private static double[] stringArr2doubleArr(string[] stringArray)
        {
            double[] doubleArray = new double[stringArray.Length];
            for(int i = 0; i < stringArray.Length; ++i)
            {
                doubleArray[i] = double.Parse(stringArray[i]);
            }
            return doubleArray;
        }

        private static bool CheckingEnteredSequence(string[] wordArray)
        {
            foreach (var word in wordArray)
            {
                if (!double.TryParse(word, out _))
                    return false;
            }
            return true;
        }

        private static void ProcessingSequenceOfRationalNumbers(double[] rationalNumberArray, out int amountOfNumbers, 
            out double min, out double max, out double sum, out double arithmeticalMean)
        {
            amountOfNumbers = rationalNumberArray.Length;

            Array.Sort(rationalNumberArray);
            min = rationalNumberArray[0];
            max = rationalNumberArray[amountOfNumbers - 1];

            double preSum = 0;
            foreach(var ratNumber in rationalNumberArray)
            {
                preSum += ratNumber;
            }
            sum = preSum;

            arithmeticalMean = Math.Round(sum / amountOfNumbers, 1);
            
        }

    }
}
