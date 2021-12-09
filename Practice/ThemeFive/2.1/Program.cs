using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._1
{
    class Program
    {
        private static Table<Person> table;
        static Program()
        {
            int indentLength = 2;
            table = new Table<Person>(new List<TableColumn<Person>>()
            {
                new TableColumn<Person>("ИМЯ", indentLength, 15, person => person.Name),
                new TableColumn<Person>("ФАМИЛИЯ", indentLength, 15, person => person.Surname),
                new TableColumn<Person>("ДАТА РОЖДЕНИЯ", indentLength, 20, person => person.BirthDate.ToString()),
            });
        }

        static void Main()
        {
            List<Person> people = GetMockPeople();
            ConsoleKey consoleKey;

            do
            {
                Console.Clear();
                table.SetWindowSize();
                table.Print(people);

                Console.Write("\n\nСОРТИРОВАТЬ: ");
                Console.WriteLine("\t\t F1 - Возрастание || F2 - Убывание");
                consoleKey = Console.ReadKey().Key;
            } while (consoleKey != ConsoleKey.F1 && consoleKey != ConsoleKey.F2) ;


            if (consoleKey == ConsoleKey.F1)
                people.Sort();
            else
            {
                people.Sort();
                people.Reverse();
            }

            table.Print(people);
        }

        private static List<Person> GetMockPeople()
        {
            int amountOfPeople = 11;
            List<Person> people = new List<Person>();
            List<string> almostFullName = new List<string>()
            {
                "Мирон Романов",
                "Антонина Яковлева",
                "Антонина Яковлева",
                "Никита Королев",
                "Георгий Чернов",
                "Елизавета Баранова",
                "Ульяна Смирнова",
                "Иван Жданов",
                "Фёдор Воронов",
                "Полина Козлова",
                "Кира Лебедева"
            };
            List<DateTime> birthDates = new List<DateTime>()
            {
                DateTime.Parse("04.01.2001"),
                DateTime.Parse("10.07.2005"),
                DateTime.Parse("10.08.2004"),
                DateTime.Parse("04.06.2007"),
                DateTime.Parse("23.12.2007"),
                DateTime.Parse("13.11.2008"),
                DateTime.Parse("25.12.2008"),
                DateTime.Parse("28.02.2010"),
                DateTime.Parse("06.01.2012"),
                DateTime.Parse("16.03.2012"),
                DateTime.Parse("05.05.2012"),
                DateTime.Parse("30.08.2013"),
                DateTime.Parse("10.11.2015"),
                DateTime.Parse("30.07.2018"),
                DateTime.Parse("16.08.2019")
            };

            for(int i = 0; i < amountOfPeople; ++i)
            {
                people.Add(new Person(almostFullName[i].Split()[0], almostFullName[i].Split()[1], birthDates[i]));
            }
            return people;
        }
    }
}
