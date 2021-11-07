using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._1
{
    class CountryCapitals
    {
        static Dictionary<string, string> countryCapitals = new Dictionary<string, string>
        {
            ["Казахстан"] = "Нур-Султан",
            ["Греция"] = "Афины",
            ["Германия"] = "Берлин",
            ["Швейцария"] = "Берн",
            ["Швеция"] = "Стокгольм",
            ["Италия"] = "Рим",
            ["Чехия"] = "Прага",
            ["Монако"] = "Монако",
            ["Дания"] = "Копенгаген",
            ["Испания"] = "Мадрид",
            ["Венгрия"] = "Будапешт",
            ["Румыния"] = "Бухарест",
            ["Ирландия"] = "Дублин",
            ["Люксенбург"] = "Люксенбург",
            ["Болгария"] = "София",
            ["Албания"] = "Тирана",
            ["Финляндия"] = "Хельсинки",
            ["Индия"] = "Нью-Дели",
            ["Абхазия"] = "Сухум",
            ["Кувейт"] = "Эль-Кувейт",
            ["Куба"] = "Гавана",
            ["Россия"] = "",
            ["Белоруссия"] = "",
            ["Франция"] = ""
        };

        static void Main()
        {
            PrintCountryCapitals();

            Console.WriteLine("Число заполненных стран: {0}\n", GetAmountOfFilledCountries());
            Find();
            AddCapital();
            RemoveCountryController();

            PrintCountryCapitals();
        }

        private static bool RemoveCountry(string country)
        {
            return countryCapitals.Remove(country);
        }

        private static void RemoveCountryController()
        {
            Console.WriteLine("\nУдаление страны".ToUpper());
            Console.Write("Введите страну для удаления: ");
            string country = Console.ReadLine();
            if (RemoveCountry(country))
            {
                Console.Write("\tСтрана удалена");
            }
            else
            {
                Console.Write("\tВведённая страна не найдена");
            }
        }

        private static void AddCapital()
        {
            do
            {
                Console.WriteLine("\nДобавление столицы".ToUpper());
                Console.Write("Введите страну: ");
                string country = Console.ReadLine();
                string capital = FindByCountry(country);
                if (capital == null)
                    Console.WriteLine("\tСтрана не найдена");
                else if (capital != String.Empty)
                {
                    Console.WriteLine("\tCтолица уже задана. Нужно ли выполнить замену");
                    Console.Write("\tДа - любая клавиша, Нет - [N]: ");
                    if (Console.ReadKey().Key != ConsoleKey.N)
                    {
                        Console.WriteLine();
                        Console.Write("\t\tВведите столицу: ");
                        capital = Console.ReadLine();
                        ReplaceCapital(country, capital);
                        Console.WriteLine("\t\tЗамена произведена!");
                        Console.Write("\t\t\tРезультат: {0} - {1}", country, FindByCountry(country));
                    }
                }
                else
                {
                    Console.Write("Введите столицу: ");
                    capital = Console.ReadLine();
                    ReplaceCapital(country, capital);
                    Console.Write("\tСтолица добавлена!");
                    Console.Write("Результат: {0} - {1}", country, FindByCountry(country));
                }

                Console.WriteLine("\nПопробовать ещё раз?");
                Console.Write("Да - любая клавиша, Нет - [ESCAPE]: ");

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static void PrintCountryCapitals()
        {
            Console.WriteLine("  Страна   -   Столица");
            int indentLength = "Люксенбург".Length;
            foreach (var countryCapital in countryCapitals)
            {
                Console.Write($"{countryCapital.Key}");
                Console.Write(new string(' ', indentLength - countryCapital.Key.Length + 3));
                Console.Write($"{countryCapital.Value}");
                Console.WriteLine();
            }
        }

        static int GetAmountOfFilledCountries()
        {
            return countryCapitals.Where(countryCapital => countryCapital.Value != String.Empty).ToArray().Length;
        }

        static void Find()
        {
            Console.WriteLine("Поиск".ToUpper());
            FindByCountryController();
            FindByCapitalController();
        }

        static void FindByCountryController()
        {
            Console.WriteLine("Поиск по стране");
            Console.Write("\tВведите страну: ");
            string country = Console.ReadLine();
            string capital = FindByCountry(country);
            if (capital != null)
            {
                Console.WriteLine("\tСтолица: {0}", capital);
            }
            else
            {
                Console.WriteLine("\tСтолица введённой страны не найдена");
            }
        }

        static void FindByCapitalController()
        {
            Console.WriteLine("Поиск по столице");
            Console.Write("\tВведите столицу: ");
            string capital = Console.ReadLine();
            var countries = FindByCapital(capital);
            if (countries.Count != 0)
            {
                if (countries.Count == 1)
                {
                    Console.WriteLine("\tСтрана: {0}", countries[0]);
                }
                else
                {
                    Console.WriteLine("\tСтраны: ");
                    foreach (var country in countries)
                    {
                        Console.WriteLine("\t\t{0}", country);
                    }
                }
            }
            else
            {
                Console.WriteLine("\tСтрана введённой столицы не найдена");
            }
        }

        static string FindByCountry(string country)
        {
            return countryCapitals
                .Where(countryCapital => countryCapital.Key == country)
                .FirstOrDefault().Value;
        }

        static List<string> FindByCapital(string capital)
        {
            var pairList = countryCapitals
                .Where(countryCapital => countryCapital.Value.ToLower() == capital.ToLower())
                .ToList();
            var countries = new List<string>();
            foreach (var pair in pairList)
            {
                countries.Add(pair.Key);
            }
            return countries;
        }

        static void ReplaceCapital(string country, string newCapital)
        {
            countryCapitals[country] = newCapital;
        }


    }
}
