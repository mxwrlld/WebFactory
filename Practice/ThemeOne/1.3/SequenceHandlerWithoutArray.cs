using System;
using System.Collections.Generic;
using System.Text;

namespace _1._3
{
    class SequenceHandlerWithoutArray
    {

        public static void ProcessingSequenceOfRationalNumbersSingleInput(out int amountOfNumbers,
            out double min, out double max, out double sum, out double arithmeticalMean)
        {
            string input = Console.ReadLine();
            if (input == "")
                throw new Exception("Последовательность не может не содержать ни одного члена");
            if (!CheckingEntereValue(input))
                throw new Exception("Invalid value");
            double rationalNumber = double.Parse(input);
            if (!CheckingDiapazon(rationalNumber))
                throw new Exception("Нарушен допустимый диапазон: [-1000, 1000]");
            min = max = sum = rationalNumber;
            int sequenceLength = 1;

            while ((input = Console.ReadLine()) != "")
            {
                if (!CheckingEntereValue(input))
                    throw new Exception("Invalid value");
                else
                    rationalNumber = double.Parse(input);
                if (!CheckingDiapazon(rationalNumber))
                    throw new Exception("Нарушен допустимый диапазон: [-1000, 1000]");

                if (min > rationalNumber)
                    min = rationalNumber;
                else
                {
                    if (max < rationalNumber)
                        max = rationalNumber; 
                }
                sum += rationalNumber;

                ++sequenceLength;
            }

            amountOfNumbers = sequenceLength;
            arithmeticalMean = Math.Round(sum / amountOfNumbers);
        }

        public static void ProcessingSequenceOfRationalNumbersSequenceInput(out int amountOfNumbers,
            out double min, out double max, out double sum, out double arithmeticalMean)
        {
            string enteredSequence = Console.ReadLine(),
                reducedSequence,
                ratNumberStringForm;
            int endNumberIndex,
                sequenceLength = 0;
            double rationalNumber;

            endNumberIndex = enteredSequence.IndexOf(' ');
            if (endNumberIndex != -1)
                ratNumberStringForm = enteredSequence.Substring(0, endNumberIndex);
            else
                ratNumberStringForm = enteredSequence;
            if (!CheckingEntereValue(ratNumberStringForm))
                throw new Exception("Invalid value");
            rationalNumber = double.Parse(ratNumberStringForm);
            if (!CheckingDiapazon(rationalNumber))
                throw new Exception("Нарушен допустимый диапазон: [-1000, 1000]");

            if (endNumberIndex != -1)
                reducedSequence = enteredSequence.Substring(endNumberIndex + 1);
            else
                reducedSequence = "";
            enteredSequence = reducedSequence;
            sequenceLength += 1;

            min = max = sum = rationalNumber;
            

            while (true)
            {
                endNumberIndex = enteredSequence.IndexOf(' ');
                if (enteredSequence == "")
                    break;
                if (endNumberIndex != -1)
                    ratNumberStringForm = enteredSequence.Substring(0, endNumberIndex);
                else
                    ratNumberStringForm = enteredSequence; 

                if (!CheckingEntereValue(ratNumberStringForm))
                    throw new Exception("Invalid input");
                rationalNumber = double.Parse(ratNumberStringForm);
                if (!CheckingDiapazon(rationalNumber))
                    throw new Exception("Нарушен допустимый диапазон: [-1000, 1000]");

                min = (min > rationalNumber) ? rationalNumber : min;
                max = (max < rationalNumber) ? rationalNumber : max;
                sum += rationalNumber;
                ++sequenceLength;

                if (endNumberIndex != -1)
                    reducedSequence = enteredSequence.Substring(endNumberIndex + 1);
                else
                    reducedSequence = "";     
                enteredSequence = reducedSequence; 
            }

            amountOfNumbers = sequenceLength;
            arithmeticalMean = Math.Round(sum / amountOfNumbers);
        }

        private static bool CheckingEntereValue(string rationalNumber)
        {
            if (!double.TryParse(rationalNumber, out _))
                    return false;
            return true;
        }

        private static bool CheckingDiapazon(double rationalNumber)
        {
            if (rationalNumber >= -1000 && rationalNumber <= 1000)
                return true;
            return false;
        }
    }
}
