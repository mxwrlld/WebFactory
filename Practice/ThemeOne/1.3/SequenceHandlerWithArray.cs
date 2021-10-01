using System;

namespace _1._3
{
    class SequenceHandlerWithArray
    {
        public static void ProcessingSequence(out int amountOfNumbers,
            out double min, out double max, out double sum, out double arithmeticalMean)
        {
            string enteredSequence = Console.ReadLine();
                string[] rationalNumberSequence = enteredSequence.Split(" ");
                if (!CheckingEnteredSequence(rationalNumberSequence))
                    throw new Exception("Последовательность должна состоять только из рациональных чисел!");

                double[] rationalNumberArray = stringArr2doubleArr(rationalNumberSequence);

                amountOfNumbers = rationalNumberArray.Length;

                Array.Sort(rationalNumberArray);
                min = rationalNumberArray[0];
                max = rationalNumberArray[amountOfNumbers - 1];

                double preSum = 0;
                foreach (var ratNumber in rationalNumberArray)
                {
                    preSum += ratNumber;
                }
                sum = preSum;

                arithmeticalMean = Math.Round(sum / amountOfNumbers, 1);
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
    }
}
