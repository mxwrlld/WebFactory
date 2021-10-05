using System;

namespace FinancialIndicatorsAnalizer
{
    class Analizer
    {
        static void Main()
        {
            CreateRecord();
        }

        static void CreateRecord()
        {
            string name,
                tIN,
                legalAddress,
                year;
            int quarter;
            decimal income,
                consumption,
                profit,
                rentability;


            GetEnterprise(out name, out tIN, out legalAddress);
            PrintInfAbout(name, tIN, legalAddress);
            do
            {
                GetFinancialResults(out year, out quarter, out income, out consumption);
                profit = CalculateProfit(income, consumption);
                rentability = CalculateRentability(profit, consumption);
                PrinResult(profit, rentability);


                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
            
        }

        static void PrinResult(decimal profit, decimal rentability)
        {
            Console.WriteLine();
            Console.WriteLine($"Прибыль: {profit}");
            Console.WriteLine($"Рентабельность: {rentability} %");
            Console.WriteLine("**********************************");
        }

        static void PrintInfAbout(string name, string tIN, string legalAddress)
        {
            Console.WriteLine();
            Console.WriteLine("Предприятие");
            Console.WriteLine($"Название: {name}");
            Console.WriteLine($"ИНН: {tIN}");
            Console.WriteLine($"Адрес: {legalAddress}");
            Console.WriteLine("----------------------------------");
        }

        private static decimal CalculateRentability(decimal profit, decimal consumption)
        {
            return Math.Round((profit / consumption) * 100, 2);
        }

        private static decimal CalculateProfit(decimal income, decimal consumption)
        {
            return (income - consumption);
        }

        static void GetEnterprise(out string name, out string tIN, out string legalAddress)
        {

            Console.WriteLine("Введите информацию о предприятии");
            Console.WriteLine();
            Input("Введите название предприятия: ", "NAME", out name);
            Input("Введите ИНН предприятия: ", "TIN", out tIN);
            Input("Введите адрес предприятия: ", "ADDRESS", out legalAddress);

        }

        static void GetFinancialResults(out string year, out int quarter, out decimal income,
                out decimal consumption)
        {
            string quarterStr,
                incomeStr,
                consumptionStr;

            Console.WriteLine();
            Input("Введите год (4 цифры, до текущего - 2015го): ", "YEAR", out year);
            Input("Введите номер квартала (1 - 4): ", "QUARTER", out quarterStr);
            quarter = int.Parse(quarterStr);
            Input("Введите доход за квартал (в рублях): ", "INCOME", out incomeStr);
            income = decimal.Parse(incomeStr);
            Input("Введите расход за квартал (в рублях): ", "CONSUMPTION", out consumptionStr);
            consumption = decimal.Parse(consumptionStr);
        }

        static bool InputControl(string input, out string checkedInput)
        {
            input = input.Trim();
            checkedInput = input;
            if (input.Length >= 1)
                return true;
            return false;
        }

        static bool TINValidator(string tIN)
        {
            if (tIN.Length != 3)
                return false;
            if (tIN.Substring(0, 2) == "00")
                return false;
            return true;
        }

        static bool YearValidator(string yearStr, DateTime currentTime)
        {
            
            int year;
            if (yearStr.Length != 4)
                return false;
            if (!int.TryParse(yearStr, out year))
                return false;
            if ( year  > currentTime.Year)
                return false;
            return true;
        }

        static bool QuaterValidator(string quarterStr)
        {
            int quarter;
            if (!int.TryParse(quarterStr, out quarter))
                return false;
            if (quarter < 1 || quarter > 4)
                return false;
            return true;
        }

        static bool IncomeValidator(string incomeStr)
        {
            decimal income;
            if (!decimal.TryParse(incomeStr, out income))
                return false;
            if (income < 0)
                return false;
            return true;
        }

        static bool ConsumptionValidator(string consumptionStr)
        {
            decimal consumption;
            if (!decimal.TryParse(consumptionStr, out consumption))
                return false;
            if (consumption < 0)
                return false;
            return true;
        }

        static void Input(string consoleRequest, string argumetType, out string argument)
        {
            do
            {
                Console.Write($"{consoleRequest}");
                if (InputControl(Console.ReadLine(), out argument))
                {
                    switch (argumetType)
                    {
                        case "TIN":
                            {
                                if (!TINValidator(argument))
                                {
                                    Console.WriteLine("Ошибка ввода. Будьте внимательнее!");
                                    continue;
                                }
                                break;
                            }
                        case "YEAR":
                            {
                                DateTime currentDate = DateTime.Parse("01/01/2015 00:00:00");
                                if (!YearValidator(argument, currentDate))
                                {
                                    Console.WriteLine("Ошибка ввода. Будьте внимательнее!");
                                    continue;
                                }
                                break;
                            }
                        case "QUARTER":
                            {
                                if (!QuaterValidator(argument))
                                {
                                    Console.WriteLine("Ошибка ввода. Будьте внимательнее!");
                                    continue;
                                }
                                break;
                            }
                        case "INCOME":
                            {
                                if (!IncomeValidator(argument))
                                {
                                    Console.WriteLine("Ошибка ввода. Будьте внимательнее!");
                                    continue;
                                }
                                break;
                            }
                        case "CONSUMPTION":
                            {
                                if (!ConsumptionValidator(argument))
                                {
                                    Console.WriteLine("Ошибка ввода. Будьте внимательнее!");
                                    continue;
                                }
                                break;
                            }
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода. Будьте внимательнее!");
                }
            } while (true);
        }
    }
}
