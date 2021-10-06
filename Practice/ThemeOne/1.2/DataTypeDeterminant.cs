using System;

namespace _1._2
{
    public class DataTypeDeterminant
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tDetermining data type by value ");
            Console.Write("Enter the value: ");
            string inputValue = Console.ReadLine();
            var dataType = DefenitionTypeOfValue(inputValue);
            Console.WriteLine($"Result: {dataType}");
        }

        public static string DefenitionTypeOfValue(string inputValue)
        {
            if (int.TryParse(inputValue, out _))
            {
                return "Integer";
            }
            if (double.TryParse(inputValue, out _))
            {
                return "Rational Number";
            }
            if (bool.TryParse(inputValue, out _))
            {
                return "Boolean";
            }
            return "Text";
        }
    }
}
    }
}
