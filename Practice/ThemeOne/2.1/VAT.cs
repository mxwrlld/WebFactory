using System;

namespace _2._1
{
    class VAT
    {
        static void Main()
        {
            decimal cost, 
                costPrice,
                vatAmount = 0m;

            try
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.Write("Введите стоимость товара (с НДС): ");

                    // Контроль ввода
                    string input = Console.ReadLine();
                    if (InputControl(input))
                        cost = decimal.Parse(input);
                    else
                        throw new Exception("Invalid value");

                    // Вычисления
                    CalculateCostPrice(cost, out costPrice);
                    vatAmount += CalculateVAT(cost, costPrice);
                    //

                    PrintCostWithoutVAT(RoundForCheck(costPrice));

                    // Продолжить ли ввод? 
                    bool choice = IsInputContinue();
                    if (!choice)
                    {
                        break;
                    }
                }

                // Вывод чека и накладной 
                PrintVATReceiptAndTaxReturn(RoundForCheck(vatAmount), RoundForTaxReturn(vatAmount));
            }
            catch(Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
         }

        private static bool IsInputContinue()
        {
            string choice; 
            Console.Write("Желаете продолжить ввод товара? [Y / N ]:  ");
            do
            {
                choice = Console.ReadLine().ToLower();
                if (choice != "y" && choice != "n")
                {
                    Console.Write("Да или нет? [Y / N ]:  ");
                }
                else
                {
                    break;
                }
            } while (true);

            if (choice == "y")
                return true;
            return false;
        }

        static bool InputControl(string input)
        {
            if (decimal.TryParse(input, out _))
            {
                return true;
            }
            return false;
        }

        static decimal RoundForCheck(decimal vatAmount)
        {
            return Math.Round(vatAmount, 2);
        }
                
        static decimal RoundForTaxReturn(decimal vatAmount)
        {
            decimal fractionalPart = vatAmount - (int)vatAmount;
            if (fractionalPart >= 0.5m)
                return Math.Ceiling(vatAmount);
            return Math.Floor(vatAmount);
        }


        static void CalculateCostPrice(decimal cost, out decimal costPrice)
        {
            costPrice = (cost / 1.2m); // Неверное округление при выполнении деления? 
        }
        
        static decimal CalculateVAT(decimal cost, decimal costPrice)
        {
            return (cost - costPrice);
        }


        static void PrintCostWithoutVAT(decimal costPrice)
        {
            Console.WriteLine($"Cтоимость товара без НДС: {costPrice}");
        }        
        
        static void PrintVATReceiptAndTaxReturn (decimal vatAmountCheck, decimal vatAmountTaxReturn)
        {
            Console.WriteLine($"--------------------------------------------------- ");
            Console.WriteLine($"Cумма НДС в чеке: {vatAmountCheck}");
            Console.WriteLine($"Cумма НДС для налоговой декларации: {vatAmountTaxReturn}");
            Console.WriteLine();
        }
    }
}
