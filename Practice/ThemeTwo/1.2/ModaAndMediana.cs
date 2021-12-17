using System;
using System.Linq;
// https://studopedia.ru/19_82511_pravila-nahozhdeniya-modi.html


namespace _1._2
{
    class ModaAndMediana
    {
        static void Main()
        {
            do
            {
                StartMenu();
                int size = InputControl();
                int[] randArray = CreateRandIntArray(size);
                PrintArray("Сгенерированный массив: ", randArray);

                double modaVers1 = CalculateModaVers1(randArray);
                double modaVers2 = CalculateModaVers2(randArray);
                double mediana = CalculateMediana(randArray);

                PrintResult(mediana, modaVers1, modaVers2);
                
                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static void PrintResult(double mediana, params double[] mods)
        {
            int count = 1; 
            foreach (var moda in mods)
            {
                if (moda != -1)
                {
                    Console.WriteLine("Moda (version {0}): {1}",count++, moda);
                }
                else
                {
                    Console.WriteLine("Данный набор не имеет моды");
                }
            }

            Console.WriteLine("Медиана: {0}", mediana);
            Console.WriteLine();
        }

        private static void PrintArray(string outputString, int[] array)
        {
            Console.WriteLine(outputString + string.Join("; ", array));
        }

        private static void StartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("\tМОДА И МЕДИНА выборки случайных чисел");
            Console.Write("Введите длину массива (целое число): ");
        }

        static int InputControl()
        {
            int input;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Ошибка ввода. Попробуйте ещё раз: ");
                }
                else
                    break;
            } while (true);

            return input;
        }

        static int[] CreateRandIntArray(int size)
        {
            int[] randNumbers = new int[size];
            int seed = 2;
            Random random = new Random(seed);

            for (int i = 0; i < randNumbers.Length; ++i)
            {
                randNumbers[i] = random.Next(0, 10);
            }

            return randNumbers;
        }

        static double CalculateModaVers1(int[] array)
        {
            var set = array.Distinct().ToArray();
            var amountOfEachMember = new int[set.Length];
            int count; 

            for (int i = 0; i < set.Length; ++i)
            {
                count = 0;
                for (int j = 0; j < array.Length; ++j)
                {
                    if (set[i] == array[j])
                        ++count;
                }
                amountOfEachMember[i] = count;
            }

            PrintArray("The amount of each unique element of Array: ", amountOfEachMember);


            double moda;
            if (amountOfEachMember.All(x => x == 1))
            {
                moda = -1;
            }
            else
            {
                int index = Array.IndexOf(amountOfEachMember, amountOfEachMember.Max());
                var indexesOfRepeatitions = new int[5];
                int amountOfRepeatitions = 0;
                for (int i = index + 1, j = 0; i < amountOfEachMember.Length; ++i)
                {
                    if (amountOfEachMember[index] == amountOfEachMember[i])
                    {
                        amountOfRepeatitions++;
                        indexesOfRepeatitions[j] = i;
                    }
                }

                if (amountOfRepeatitions == 0)
                {
                    moda = set[index];
                }
                else
                {
                    double sum = set[index];
                    for (int i = 0; i < amountOfRepeatitions; ++i)
                    {
                        sum += set[indexesOfRepeatitions[i]];
                    }
                    double average = sum / (amountOfRepeatitions + 1);

                    moda = average;
                }
            }

            return moda;
        }

        static double CalculateMediana(int[] intArray)
        {
            int index; 
            double mediana;
            if (intArray.Length % 2 != 0)
            {
                index = (int)Math.Ceiling((double)(intArray.Length / 2));
                mediana = intArray[index];
            }
            else
            {
                index = (intArray.Length / 2);
                mediana = ((double)(intArray[index - 1] + intArray[index])) / 2;
            }
            return mediana;
        }

        static double CalculateModaVers2(int[] intArray)
        {
            Array.Sort(intArray);
            string mods = String.Empty;
            int maxAmountOfRepetitions = 0,
                currentAmountOfRepetitions = 0,
                indexOfNextElement = 0,
                currentElement = intArray[indexOfNextElement];


            for(int i = 0; i < intArray.Length; ++i){
                if(currentElement == intArray[i])
                {
                    currentAmountOfRepetitions++;
                }
                else
                {
                    indexOfNextElement = i;
                    currentAmountOfRepetitions = 0;
                    currentElement = intArray[indexOfNextElement];
                    --i;  
                }


                if(currentAmountOfRepetitions > maxAmountOfRepetitions)
                {
                    maxAmountOfRepetitions = currentAmountOfRepetitions;
                    
                    if(mods != "")
                        mods = "";
                    mods += (currentElement + " ");
                    continue;
                }
                if(currentAmountOfRepetitions == maxAmountOfRepetitions)
                {
                    mods += (currentElement + " ");
                }
            }

            double moda;


            if ((mods.Length / 2) == intArray.Length)
            {
                moda = -1;
            }
            else  if (mods.Length == 1)
            {
                moda = double.Parse(mods.Trim());
            }
            else
            {
                int[] modsIntArray = mods.Trim().Split(" ")
                    .Select(modaValue => int.Parse(modaValue))
                    .ToArray();
                moda = ((double)modsIntArray.Sum() / modsIntArray.Length);
            }


            return moda; 
        }


    }
}
