using System;

namespace _1._1
{
    public enum Range
    {
        Floor = - 1_000_000_000,
        Ceiling = 1_000_000_000
    }

    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("\tGetting the square of an integer");
                Console.Write("Enter the number: ");
                int inputNumber  = InputControl();
                double squareOEnteredNumber = Math.Pow(inputNumber, 2);
                Console.WriteLine($"Result: {squareOEnteredNumber}"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
              
        }

        static int InputControl()
        {
            int inputNumber;
            if (int.TryParse(Console.ReadLine(), out inputNumber) && 
                   (inputNumber >= (int)Range.Floor && inputNumber <= (int)Range.Ceiling))
            {
                return inputNumber;
            }
            else
            {
                throw new Exception("Out of range value entered");
            }
        }

    }
}
