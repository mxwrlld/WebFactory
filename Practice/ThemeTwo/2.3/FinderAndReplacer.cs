using System;
using System.Linq;
using System.Text;

namespace _2._3
{
    class FinderAndReplacer
    {
        static string srcText = @"
    В конце ноября, в оттепель, часов в девять утра, поезд Петербургско-Варшавской железной дороги на всех парах подходил к Петербургу. Было так сыро и туманно, что насилу рассвело; в десяти шагах, вправо и влево от дороги, трудно было разглядеть хоть что-нибудь из окон вагона. Из пассажиров были и возвращавшиеся из-за границы; но более были наполнены отделения для третьего класса, и всё людом мелким и деловым, не из очень далека. Все, как водится, устали, у всех отяжелели за ночь глаза, все назяблись, все лица были бледно-желтые, под цвет тумана.
    В одном из вагонов третьего класса, с рассвета, очутились друг против друга, у самого окна, два пассажира — оба люди молодые, оба почти налегке, оба не щегольски одетые, оба с довольно замечательными физиономиями и оба пожелавшие, наконец, войти друг с другом в разговор. Если б они оба знали один про другого, чем они особенно в эту минуту замечательны, то, конечно, подивились бы, что случай так странно посадил их друг против друга в третьеклассном вагоне петербургско-варшавского поезда. Один из них был небольшого роста, лет двадцати семи, курчавый и почти черноволосый, с серыми маленькими, но огненными глазами. Нос его был широк и сплюснут, лицо скулистое; тонкие губы беспрерывно складывались в какую-то наглую, насмешливую и даже злую улыбку; но лоб его был высок и хорошо сформирован и скрашивал неблагородно развитую нижнюю часть лица. Особенно приметна была в этом лице его мертвая бледность, придававшая всей физиономии молодого человека изможденный вид, несмотря на довольно крепкое сложение, и вместе с тем что-то страстное, до страдания, не гармонировавшее с нахальною и грубою улыбкой и с резким, самодовольным его взглядом. Он был тепло одет, в широкий мерлушечий черный крытый тулуп, и за ночь не зяб, тогда как сосед его принужден был вынести на своей издрогшей спине всю сладость сырой ноябрьской русской ночи, к которой, очевидно, был не приготовлен.";

        static void Main()
        {   
            
            while (true)
            {
                Console.Clear();
                StartMenu();
                Console.WriteLine(srcText);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape: return;
                    case ConsoleKey.F2:
                        {
                            Console.WriteLine("\nПОИСК");
                            Console.Write("Введите искомое: ");
                            string searchLine = Console.ReadLine();

                            int[] indexesOfStartHighlighting = FindIndexesOfSearchStatemet(srcText, searchLine);
                            if (indexesOfStartHighlighting.Length == 0)
                            {
                                Console.WriteLine("Совпадений не найдено!");
                            }
                            else
                            {
                                PrintWithHighlighting(srcText, searchLine, indexesOfStartHighlighting);
                            }

                            break;
                        }
                    case ConsoleKey.F3:
                        {
                            Console.WriteLine("\nЗАМЕНА");
                            Console.Write("Что заменить: ");
                            string oldLine = Console.ReadLine();
                            Console.Write("На что заменить: ");
                            string newLine = Console.ReadLine();

                            int[] indexesOfStartReplacement = FindIndexesOfSearchStatemet(srcText, oldLine);
                            string changedText = Replace(srcText, oldLine, newLine, indexesOfStartReplacement);

                            PrintWithHighlighting(changedText, newLine, FindIndexesOfSearchStatemet(changedText, newLine));
                            break;

                        }
                    default:
                        break;
                }
                Console.ReadKey();
            }
        }

        private static string Replace(string srcText, string oldLine, string newLine, int[] indexesOfStartReplacement)
        {
            string text = srcText.Substring(0);
            string changedText = String.Empty,
                before = String.Empty,
                after = String.Empty;
            int firstIndexOfChange,
                lastIndexOfChange,
                startIndexOfFilling = 0;

            for(int i = 0; i < indexesOfStartReplacement.Length; ++i)
            {
                firstIndexOfChange = indexesOfStartReplacement[i];
                lastIndexOfChange = firstIndexOfChange + oldLine.Length;


                before = text.Substring(startIndexOfFilling, firstIndexOfChange - startIndexOfFilling);

                changedText += (before + newLine);

                startIndexOfFilling = lastIndexOfChange;
            }
            changedText += text.Substring(startIndexOfFilling);

            return changedText;
        }

        private static void PrintWithHighlighting(string text, string highlightingStatement, int[] indexesOfStartHL)
        {
            string textCopy = text.Substring(0),
                beforeHL = String.Empty,
                afterHL = String.Empty;
            int firstIndexHL,
                lastIndexHL,
                firstIndexOfPrint = 0;

            for (int i = 0; i < indexesOfStartHL.Length; ++i)
            {
                firstIndexHL = indexesOfStartHL[i];
                lastIndexHL = firstIndexHL + highlightingStatement.Length;

                beforeHL = textCopy.Substring(firstIndexOfPrint, firstIndexHL - firstIndexOfPrint);
                Console.Write(beforeHL);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(highlightingStatement);
                Console.ResetColor();

                //afterHL = textCopy.Substring(lastIndexHL, textCopy.Length - lastIndexHL);
                //textCopy = afterHL;

                firstIndexOfPrint = lastIndexHL;

            }
            Console.Write(textCopy.Substring(firstIndexOfPrint));
        }


        // Нахождение индексов искомого выражения
        private static int[] FindIndexesOfSearchStatemet(string srcText, string searchLine)
        {
            string text = srcText.Substring(0),
                arrOfIndexesStringForm = String.Empty,
                afterFoundLine = String.Empty;
            int firstIndex,
                lastIndex,
                beginIndexOfNewPiece = 0;

            do
            {
                if (text.Contains(searchLine, StringComparison.CurrentCultureIgnoreCase))
                {
                    firstIndex = text.IndexOf(searchLine, StringComparison.CurrentCultureIgnoreCase);
                    lastIndex = firstIndex + searchLine.Length;

                    arrOfIndexesStringForm += ((beginIndexOfNewPiece + firstIndex) + " ");

                    afterFoundLine = text.Substring(lastIndex, text.Length - lastIndex);

                    text = afterFoundLine;
                    beginIndexOfNewPiece += lastIndex;
                }
                else
                {
                    break;
                }
            } while (true);
            

            if(arrOfIndexesStringForm.Length == 0)
            {
                arrOfIndexesStringForm += "-1";
            }


            int[] arrOfIndexes = arrOfIndexesStringForm.Trim()
                .Split(" ").Select(x => int.Parse(x)).ToArray();

            return arrOfIndexes;
        }

        private static void StartMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.CursorLeft = 0;
            PrintMenuCommand("ESC", "Выход");
            PrintMenuCommand("F2", "Поиск");
            PrintMenuCommand("F3", "Заменить");
            Console.WriteLine();
            Console.ResetColor();
        }

        static void PrintMenuCommand(string key, string action)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(key);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" " + action + " ");
        }

        static int InputControl(int leftBorder, int rightBorder)
        {
            int input;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.Write("Ошибка ввода. Попробуйте ещё раз: ");
                }
                else if(input >= leftBorder && input <= rightBorder)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Выход за диапазон возможных значений - [{leftBorder},{rightBorder}]");
                    Console.Write("Попробуйте ещё раз: ");
                }
                    
            } while (true);

            return input;
        }
    }
}
