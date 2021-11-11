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
            HomePage();
        }

        private static void HomePage()
        {
            bool ignore = false;
            do
            {
                Console.Clear();
                PrintCountryAndCapitalsBar();

                Console.WriteLine("[1.] Поиск");
                Console.WriteLine("[2.] Добавление столицы");
                Console.WriteLine("[3.] Удаление страны");
                Console.WriteLine("[Q.] Выход");
                Console.Write("Осуществите выбор: ");

                var userChoice = Console.ReadKey().Key;

                switch (userChoice)
                {
                    case ConsoleKey.D1:
                        SubPagePrint(FindController);
                        break;
                    case ConsoleKey.D2:
                        SubPagePrint(AddCapitalController);
                        break;
                    case ConsoleKey.D3:
                        SubPagePrint(RemoveCountryController);
                        break;
                    case ConsoleKey.Q:
                        return;
                    default:
                        ignore = true;
                        break;
                }

                if (ignore)
                {
                    ignore = false;
                    continue;
                }
            } while (true);
        }

        private static void SubPagePrint(Action action)
        {
            Console.Clear();
            PrintCountryAndCapitalsBar();

            action();

            Console.WriteLine();
            Console.Write("Продолжить - любая клавиша: ");
            Console.ReadKey();
        }

        private static void PrintCountryAndCapitalsBar()
        {
            PrintCountryAndCapitals();
            Console.WriteLine();
            Console.WriteLine("Число заполненных стран: {0}\n", GetAmountOfFilledCountries());
        }

        private static bool RemoveCountry(string country)
        {
            return countryCapitals.Remove(country);
        }

        private static void RemoveCountryController()
        {
            Console.WriteLine("\nУдаление страны".ToUpper());
            Console.Write("Введите страну для удаления: ");
            string country = Console.ReadLine().Trim();
            if (RemoveCountry(country))
            {
                Console.Write("\tСтрана удалена");
            }
            else
            {
                Console.Write("\tВведённая страна не найдена");
            }
        }

        private static void AddCapitalController()
        {
            Console.WriteLine("\nДобавление столицы".ToUpper());
            Console.Write("Введите страну: ");
            string country = Console.ReadLine().Trim();
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

        }

        static void PrintCountryAndCapitals()
        {
            int indentLength = "Люксенбург".Length,
                i = 1;

            foreach (var countryCapital in countryCapitals)
            {
                Console.Write($"{countryCapital.Key}");
                Console.Write(new string(' ', indentLength - countryCapital.Key.Length + 3));
                Console.Write($"{countryCapital.Value}");
                Console.Write(new string(' ', indentLength - countryCapital.Value.Length + 3));
                Console.Write("| ");


                if (i++ % 3 == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        static int GetAmountOfFilledCountries()
        {
            return countryCapitals.Where(countryCapital => countryCapital.Value != String.Empty).ToArray().Length;
        }

        static void FindController()
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
                if (capital == String.Empty)
                    capital = "не задано";
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
                .Where(countryCapital => countryCapital.Key.Contains(country))
                .FirstOrDefault().Value;
        }

        static List<string> FindByCapital(string capital)
        {
            return countryCapitals
                .Where(countryCapital => countryCapital.Value.ToLower() == capital.ToLower())
                .Select(countryCapital => countryCapital.Key)
                .ToList();
        }

        static void ReplaceCapital(string country, string newCapital)
        {
            countryCapitals[country] = newCapital;
        }

    }
}
