using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._2
{
    class LyubimkiProgram
    {
        class User
        {
            public string Name { get; }
            public HashSet<string> Lyubimki { get; }

            public User(string name, List<string> lyubimki)
            {
                Name = name;
                Lyubimki = new HashSet<string>();
                foreach (var lbmk in lyubimki)
                {
                    Lyubimki.Add(lbmk);
                }
            }
        }

        static void Main()
        {
            StartMenu();
            var users = GetMockData();
            //var users = GetInitialData();
            PrintInitialData(users);

            StatisticsController(users);
        }

        private static void StatisticsController(List<User> users)
        {
            /*
             * Вывести:
                что нравится всем пользователям;
                что нравится хотя бы одному из пользователей;
                для каждого пользователя - что ему нравится, а всем остальным нет;
                для каждой «любимки» - скольки пользователям она нравится.
             */

            Console.Write("\n\nТо, что нравится всем пользователям: ");
            var allUsersLike = AllUsersLike(users);
            if(allUsersLike.Count == 0)
            {
                Console.WriteLine("Совпадений не найдено");
            }
            else
            {
                Console.WriteLine(String.Join(", ", allUsersLike));
            }

            Console.Write("\n\nТо, что нравится хотя бы одному из пользователей: ");
            var anyUsersLike = AnyUsersLike(users);
            if (allUsersLike.Count == 0)
            {
                Console.WriteLine("Совпадений не найдено");
            }
            else
            {
                Console.WriteLine(String.Join(", ", anyUsersLike));
            }

            Console.Write("\n\nТо, что каждому пользователю нравится, а всем остальным нет: ");
            var forEachButNotForAll = ForEachButNotForAll(users);
            PrintInitialData(forEachButNotForAll);

            Console.Write("\n\nСкольким пользователям нравится «любимка»: ");
            var amountOfLikes = AmountOfLoveForLyubimka(users);
            foreach (var amount in amountOfLikes)
            {
                var lyubimkaPlusCount = amount.Split("-");
                Console.Write("\n\t\tЛюбимка: {0}", lyubimkaPlusCount[0]);
                Console.Write("\tКоличество - {0}", lyubimkaPlusCount[1]);
                
            }
        }

        private static List<string> AmountOfLoveForLyubimka(List<User> users)
        {
            List<string> lyubimki = new List<string>();
            foreach (var user in users)
            {
                foreach (var lyubimka in user.Lyubimki)
                {
                    string lyubimkaStr = lyubimka;
                    int amount = users.Where(user => user.Lyubimki.Contains(lyubimka)).Count();
                    string lyubimkaPlusCount = lyubimkaStr +"-" + amount;
                    if(!lyubimki.Contains(lyubimkaPlusCount))
                        lyubimki.Add(lyubimkaPlusCount);
                }
            }
            return lyubimki;
        }

        private static List<string> AllUsersLike(List<User> users)
        {
            var user = users[0];
            var allUsersLike = new List<string>();
            foreach (var lyubimka in user.Lyubimki)
            {
                if (users.All(user => user.Lyubimki.Contains(lyubimka)))
                    allUsersLike.Add(lyubimka);
            }
            return allUsersLike;
        }

        private static List<string> AnyUsersLike(List<User> users)
        {
            var anyUsersLike = new List<string>();
            foreach (var user in users)
            {
                foreach (var lyubimka in user.Lyubimki)
                {
                    if (!anyUsersLike.Contains(lyubimka))
                        anyUsersLike.Add(lyubimka);
                }
            }
            return anyUsersLike;
        }


        private static List<User> ForEachButNotForAll(List<User> users)
        {
            var forEachButNotForAll = new List<User>();
            foreach (var user in users)
            {
                var lyubimki = new List<string>();
                foreach (var lyubimka in user.Lyubimki)
                {
                    if (!users
                    .Where(usr => usr != user)
                    .Any(usr => usr.Lyubimki.Contains(lyubimka)))
                        lyubimki.Add(lyubimka);
                }
                forEachButNotForAll.Add(new User(user.Name, lyubimki));
            }
            return forEachButNotForAll;
        }

        static void StartMenu()
        {
            Console.WriteLine("\t\tСовпадение вкусов".ToUpper());
        }

        static List<User> GetInitialData()
        {
            List<User> users = new List<User>();
            do
            {
                Console.WriteLine();
                Console.Write("\nВведите пользователя: ");
                string userName = Console.ReadLine().Trim();
                Console.WriteLine("\nВведите «любимки»");
                Console.Write("\t(ввод через запятую): ");
                string input = Console.ReadLine().Trim();
                var lyubimki = input.Split(",")
                    .Select(lyubimka => lyubimka.Trim())
                    .ToList();
                users.Add(new User(userName, lyubimki));

                Console.WriteLine("\nПродолжение ввода - ENTER || Выход - Любая другая");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);

            return users;
        }

        static List<User> GetMockData()
        {
            var lyubimki = new List<List<string>>()
            {
                new List<string>(){"Шоколад"},
                new List<string>(){"Манная каша", "Рассвет в кроватке", "Шоколад"},
                new List<string>(){"Варенье сливовое", "Шоколад"},
                new List<string>(){"Запах напалма поутру"},
            };

            var users = new List<User>()
            {
                new User("Олег", lyubimki[1]),
                new User("Пашка", lyubimki[0]),
                new User("Саша", lyubimki[2]),
                new User("Валентин", lyubimki[3])
            };

            return users;
        }

        static void PrintInitialData(List<User> users)
        {
            Console.Write("\nПользователи:");
            foreach (var user in users)
            {
                Console.Write("\n\tПользователь: {0}", user.Name);
                if(user.Lyubimki.Count == 0)
                {
                    Console.Write("\n\t\tЛюбимок нет :(");
                }
                else
                {
                    foreach (var lyubimka in user.Lyubimki)
                    {
                        Console.Write("\n\t\tЛюбимка: {0}", lyubimka);
                    }
                }
            }
        }

    }
}
