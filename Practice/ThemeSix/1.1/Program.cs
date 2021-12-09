using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "Капуста малина яблоко Аспирин";
            List<string> words = input.Split(" ").Select(x => x.ToUpper())
                .OrderBy(x => x.ToString()).ToList();
            Console.WriteLine("Результат: " + string.Join(" ", words));
        }
    }
}
