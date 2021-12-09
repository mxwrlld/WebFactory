using System;
using System.Collections.Generic;

namespace _2._2
{
    class Program
    {
        static void Main()
        {
            var inventories = GetMockData();
            PrintInventoryNumbers(inventories);
        }

        static void PrintInventoryNumbers(List<IInventory> inventories)
        {
            foreach (var unit in inventories)
            {
                Console.WriteLine(unit.Number);
            }
        }

        static List<IInventory> GetMockData()
        {
            List<IInventory> inventories = new List<IInventory>();

            inventories.Add(new Building("ул. Городская"));
            inventories.Add(new Building("ул. Деревенская"));
            inventories.Add(new Building("ул. Поселочная"));

            inventories.Add(new Device());
            inventories.Add(new Device());
            inventories.Add(new Device());

            inventories.Add(new Computer("i3", 500, 4));
            inventories.Add(new Computer("i5", 1000, 8));
            inventories.Add(new Computer("i7", 2000, 16));

            inventories.Add(new Book("Бесконечныq голод"
                , new string[] { "Студентов А.В.", "Общажный Т.Г." }, 0));
            inventories.Add(new Book("Суровая зима"
                , new string[] { "Снегов Д."}, 2003));
            inventories.Add(new Book("Солнечная сторона луны"
                , new string[] { "Майорова А.М." }, 2022));

            return inventories;
        }
    }
}
