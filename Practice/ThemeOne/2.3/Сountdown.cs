using System;

namespace _2._3
{
    class Сountdown
    {
        static void Main(string[] args)
        {
            int choice;
            bool cmdLineArgs = false;
            DateTime dateTime = new DateTime();


            if (args.Length > 0)
            {
                InputCotrol(args[0], out dateTime);
                cmdLineArgs = true;
                choice = 2;
            }
            else
            {
                StartMenu();
                InputCotrol(Console.ReadLine(), out choice);
            }

            switch (choice)
            {
                case 1:
                    {
                        try
                        {
                            int amountOfMinutes;
                            do
                            {
                                Console.Write("\n\nВведите число минут, оставшихся до события: ");
                                InputCotrol(Console.ReadLine(), out amountOfMinutes);

                                Console.WriteLine(GetСountdown(amountOfMinutes));

                                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
                            } while (Console.ReadKey().Key != ConsoleKey.Escape);
                        }
                        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                        break;
                    }
                case 2:
                    {
                        try
                        {       
                            do
                            {
                                if(!cmdLineArgs)
                                {
                                    Console.Write("\n\nВведите дату и время (в формате 16/02/2018 12:15:12):");
                                    InputCotrol(Console.ReadLine(), out dateTime);
                                }
                                var timeSpan = (DateTime.Now).Subtract(dateTime);
                                Console.WriteLine(GetСountdown(Convert.ToInt32(timeSpan.TotalSeconds)));
                                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
                            } while (Console.ReadKey().Key != ConsoleKey.Escape);
                        }
                        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Понял вас, приятного времяпрепровождения!");
                        break;
                    }
            }

        }

        private static void StartMenu()
        {
            Console.Write("\nДоброго времени суток! Что желаете ввести? ");
            Console.Write("\n1. Число минут, оставшихся до события ");
            Console.Write("\n2. Дату и время ");
            Console.Write("\nВаш выбор [1/2]: ");
        }

        private static string GetСountdown(int amountOfMinutes)
        {
            int days,
                hours,
                minutes;

            Calculate(amountOfMinutes, out days, out hours, out minutes);
            return GetResult(amountOfMinutes, days, hours, minutes);
        }

        private static string GetResult(int amountOfMinutes, int days, int hours, int minutes)
        {
            string amountOfMinutesWord = GetSuitableEnding(minutes, "минут", "минута", "минуты");
            string daysWord = GetSuitableEnding(days, "дней", "день", "дня");
            string hoursWord = GetSuitableEnding(hours, "часов", "час", "часа");
            string minutesWord = GetSuitableEnding(minutes, "минут", "минута", "минуты");

            return ($"\n\n {amountOfMinutes} {amountOfMinutesWord} - {days} {daysWord} " +
                $"{hours} {hoursWord} {minutes} {minutesWord} \n\n");
        }

        private static void InputCotrol(string input, out int variable)
        {
            if (!int.TryParse(input, out variable))
                throw new Exception("Invlaid Input");
            if(variable <= 0)
                Console.WriteLine("\tУже началось!");
        }        
        
        private static void InputCotrol(string input, out DateTime variable)
        {
            if(!DateTime.TryParse(input, out variable))
                throw new Exception("Invlaid Input");
            if(variable > DateTime.Now)
                throw new Exception("Invlaid Input");
        }



        private static void Calculate(int amountOfMinutes, out int days, out int hours, out int minutes)
        {
            int quotient,
                remainder,
                localDays;
            quotient = amountOfMinutes / 60;
            remainder = amountOfMinutes % 60;
            localDays = quotient / 24; 
            
            if(localDays == 0)
            {
                hours = quotient;
                minutes = remainder;
            }
            else
            {
                amountOfMinutes = amountOfMinutes % 1440; 
                quotient = amountOfMinutes / 60;
                remainder = amountOfMinutes % 60;
                hours = quotient; 
                minutes = remainder; 
            }

            days = localDays; 
        }

        private static string GetSuitableEnding(int value, string firstForm, string secondForm, string thirdForm)
        {
            value = (value > 20) ? value % 20 : value;  
            if (value >= 5)
            {
                return firstForm;
            }
            if (value < 2)
            {
                if (value == 1)
                    return secondForm;
                if (value == 0)
                    return firstForm;
            }
            return thirdForm;
        }   
    }
}
