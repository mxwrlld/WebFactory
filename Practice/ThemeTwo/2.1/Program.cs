using System;
using System.Linq;

namespace _2._1
{
    class Program
    {
        const int amountOfStations = 4;
        const int amountOfProviders = 6;

        static int[] maxPurchaseVolume = new int[]
        {
            600, 420, 360, 250, 700, 390
        };

        static double[] unitPurchasePrice = new double[]
        {
            5.2, 4.5, 6.1, 3.8, 6.4, 5.6
        };

        static int[][] deliveryPriceToTheGasStation = new int[][]
        {
            new int[]{ 803, 952, 997, 931 },
            new int[]{ 967, 1012, 848, 1200 },
            new int[]{ 825, 945, 777, 848 },
            new int[]{ 1024, 1800, 931, 999 },
            new int[]{ 754, 817, 531, 628 },
            new int[]{ 911, 668, 865, 1526 }
        };

        static void Main()
        {
            PrintOfStartTable();
            //int[] applicationsForGasStations = FillingTheApplicationsForGasStations();
            int[] applicationsForGasStations = new int[]
            {
                400, 550, 280, 310
            };

            var purchasePlan = CalculatePurchase(applicationsForGasStations);
            double[] purchaseCosts = CalculatePurchaseCosts(purchasePlan);
            double[] deliveryPrices = CalculateDeliveryPrices(purchasePlan);
            PrintResult(purchasePlan, purchaseCosts, deliveryPrices);
        }

        private static double[] CalculatePurchaseCosts(int[][] computedVelues)
        {
            double[] purchaseCosts = new double[unitPurchasePrice.Length];
            for(int i = 0; i < purchaseCosts.Length; ++i)
            {
                for(int j = 0; j < computedVelues[i].Length; ++j)
                {
                    if(computedVelues[i][j] != 0)
                    {
                        purchaseCosts[i] += computedVelues[i][j] * unitPurchasePrice[i];
                    }
                }
            }
            return purchaseCosts;
        }

        private static double[] CalculateDeliveryPrices(int[][] computedVelues)
        {
            double[] deliveryPrices = new double[amountOfProviders];
            for (int i = 0; i < deliveryPrices.Length; ++i)
            {
                for (int j = 0; j < computedVelues[i].Length; ++j)
                {
                    if (computedVelues[i][j] != 0)
                    {
                        deliveryPrices[i] += deliveryPriceToTheGasStation[i][j];
                    }
                }
            }

            return deliveryPrices;
        }

        static void PrintResult(int[][] purchasePlan, double[] purchaseCosts, double[] deliveryPrices)
        {
            Console.WriteLine("┌──────────┬───────┬───────┬───────┬───────┬───────────┬────────────┐");
            Console.WriteLine("│          │       │       │       │       │ Стоимость │  С учетом  │");
            Console.WriteLine("│Поставщики│ АЗС А │ АЗС Б │ АЗС В │ АЗС Г │  закупки  │  доставки  │");
            Console.WriteLine("├──────────┼───────┼───────┼───────┼───────┼───────────┼────────────┤");
            
            for (int i = 0; i < amountOfProviders; i++)
            {
                Console.WriteLine($"│{i + 1,10}│{purchasePlan[i][0],7}│{purchasePlan[i][1],7}│{purchasePlan[i][2],7}│{purchasePlan[i][3],7}│{purchaseCosts[i],11:F2}│{purchaseCosts[i] + deliveryPrices[i],12:F2}│");
            }
            Console.WriteLine("└──────────┴───────┴───────┴───────┴───────┴───────────┴────────────┘");
            Console.WriteLine($"Итого: {purchaseCosts.Sum() + deliveryPrices.Sum():## ###.00}");
        }

        private static int[][] CalculatePurchase(int[] applicationsForGasStations)
        {
            int[][] deliveryPriceInProcess = new int[amountOfProviders][];
            for(int i = 0; i < deliveryPriceToTheGasStation.Length; ++i)
            {
                deliveryPriceInProcess[i] = new int[deliveryPriceToTheGasStation[i].Length];
                for (int j = 0; j < deliveryPriceToTheGasStation[i].Length; ++j)
                {
                    deliveryPriceInProcess[i][j] = deliveryPriceToTheGasStation[i][j];
                }
            }
            int[] applInProcess = new int[applicationsForGasStations.Length];
            Array.Copy(applicationsForGasStations, applInProcess,
                applicationsForGasStations.Length);
            int[] purchaseVolumeInProcess = new int[maxPurchaseVolume.Length];
            Array.Copy(maxPurchaseVolume, purchaseVolumeInProcess, maxPurchaseVolume.Length);
            double[] unitPurchasePriceInProcess = new double[unitPurchasePrice.Length];
            Array.Copy(unitPurchasePrice, unitPurchasePriceInProcess, unitPurchasePrice.Length);
            int[][] computedValues = new int[amountOfProviders][];
            for(int i = 0; i < amountOfProviders; ++i)
            {
                computedValues[i] = new int[amountOfStations];
            }

            while (!applInProcess.All(x => x == 0))
            {
                double minCostOfProvider = SpecificMinFunc(unitPurchasePriceInProcess);
                int minCostOfProviderIndex = Array.IndexOf(unitPurchasePriceInProcess
                    , minCostOfProvider);
                do
                {
                    int index = GetIndexOfCheapestShipping(deliveryPriceInProcess,minCostOfProviderIndex);
                    if (index == -1)
                        break;
                    if (applInProcess[index] <= purchaseVolumeInProcess[minCostOfProviderIndex])
                    {
                        purchaseVolumeInProcess[minCostOfProviderIndex] -= applInProcess[index];
                        computedValues[minCostOfProviderIndex][index] = applInProcess[index];
                        applInProcess[index] = 0;
                        deliveryPriceInProcess[minCostOfProviderIndex][index] = -1;
                    }
                    else
                    {
                        applInProcess[index] -= purchaseVolumeInProcess[minCostOfProviderIndex];
                        computedValues[minCostOfProviderIndex][index] = purchaseVolumeInProcess[minCostOfProviderIndex];
                        purchaseVolumeInProcess[minCostOfProviderIndex] = 0;
                        unitPurchasePriceInProcess[minCostOfProviderIndex] = -1;
                    }

                } while (purchaseVolumeInProcess[minCostOfProviderIndex] != 0
                && applInProcess.Any(x => x != 0));
            }
            return computedValues;
        }

        private static double SpecificMinFunc(double[] array)
        {
            double min = array.Max();
            for(int i = 0; i < array.Length; ++i)
            {
                if (array[i] != -1 && array[i] < min)
                    min = array[i];
            }
            return min;
        }

        private static int GetIndexOfCheapestShipping(int[][] deliveryPrice, int rowIndex)
        {
            int index = -1,
                min = deliveryPrice[rowIndex].Max();

            for(int i = 0; i < deliveryPrice[rowIndex].Length; ++i)
            {
                if (deliveryPrice[rowIndex][i] == -1)
                    continue;
                if (min > deliveryPrice[rowIndex][i])
                {
                    min = deliveryPrice[rowIndex][i];
                    index = i;
                }
                    
            }
            return index;
        }

        static int[] FillingTheApplicationsForGasStations()
        {
            int[] applications = new int[amountOfStations];
            Console.Write("------->");
            Console.ReadKey();
            Console.WriteLine("ЗАПОЛНЕНИЕ ЗАЯВОК НА ТОПЛИВО ДЛЯ АЗС");
            for (int i = 0; i < amountOfStations; ++i)
            {
                Console.Write($"Величина заявки для {(char)('А' + i)}: ");
                InputValidation(out applications[i]);
            }
            return applications;
        }

        private static void InputValidation(out int input)
        {
            do
            {
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Требуется целое число");
                    Console.Write("Повторите ввод: ");
                }
                else
                {
                    break;
                }

            } while (true);
        }

        static void PrintOfStartTable()
        {
            Console.WriteLine("┌────────────┬────────────┬─────────┬───────────────────────────────┐");
            Console.WriteLine("│            │  Максимум  │  Цена   │   Стоимость доставки на АЗС   │");
            Console.WriteLine("│ Поставщики │   объема   │ закупки ├───────┬───────┬───────┬───────┤");
            Console.WriteLine("│            │  закупки   │  за ед. │   А   │   Б   │   В   │   Г   │");
            Console.WriteLine("├────────────┼────────────┼─────────┼───────┼───────┼───────┼───────┤");

            for (int i = 0; i < amountOfProviders; i++)
            {
                Console.WriteLine($"│{i + 1,12}│{maxPurchaseVolume[i],12}│{unitPurchasePrice[i],9}│{deliveryPriceToTheGasStation[i][0],7}│{deliveryPriceToTheGasStation[i][1],7}│{deliveryPriceToTheGasStation[i][2],7}│{deliveryPriceToTheGasStation[i][3],7}│");
            }


            Console.WriteLine("└────────────┴────────────┴─────────┴───────┴───────┴───────┴───────┘");
        }
    }
}
