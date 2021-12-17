using System;
using System.Text;

/* isWordContainsLCLetter, isEndOfWord, 
 * Массив words, wordsIterator
 * и метод IsWordContainsLowercaseLetter нужны исключительно 
 * для реализации пункта 6.3 ГОСТ'a 7.79-2000 (ИСО 9-95)
 */


namespace _2._2
{
    class Transliterator
    {
        static string[] latinTransliteration = new string[]
                {"a", "b", "v", "g", "d", "e", "zh", "z",
                "i", "j", "k", "l", "m", "n", "o", "p", "r", "s",
                "t", "u", "f", "x", "cz", "ch", "sh", "shh", "``",
                "у`", "`", "е`", "yu", "ya", "yo"};

        static void Main()
        {
            do
            {
                StartMenu();
                string russianText = Console.ReadLine().Trim();

                string transliteratedText = Transliterating(russianText);

                PrintText(transliteratedText);

                Console.WriteLine("ПРОДОЛЖИТЬ - любая кнопка |" + "|  ВЫХОД - ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static void PrintText(string transliteratedText)
        {
            Console.WriteLine();
            Console.WriteLine(transliteratedText);
        }

        private static void StartMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Транслитератор".ToUpper());
            Console.Write("Введите строку для транслитерации: ");
        }

        private static string Transliterating(string russianText)
        {
            StringBuilder transliteratedText = new StringBuilder();
            bool isWordContainsLCLetter = false,
                isEndOfWord = true;

            string[] words = russianText.Split(" ");
            int wordsIterator = 0; 

            for (int i = 0; i < russianText.Length; ++i)
            {
                int charCodeCurrent = (int)russianText[i];

                if (isEndOfWord)
                {
                    isWordContainsLCLetter = IsWordContainsLowercaseLetter(words[wordsIterator]);
                    wordsIterator++;
                    isEndOfWord = false;
                }

                int index;
                if (charCodeCurrent >= 1040 && charCodeCurrent <= 1071)
                {
                    if (!isWordContainsLCLetter)
                    {
                        index = charCodeCurrent - 1040;
                        transliteratedText.Append(latinTransliteration[index].ToString().ToUpper());
                        continue;
                    }
                    else
                    {
                        index = charCodeCurrent - 1040;
                        transliteratedText.Append((latinTransliteration[index].Length == 1) ?
                            latinTransliteration[index].ToString().ToUpper() :
                            (latinTransliteration[index][0].ToString().ToUpper() +
                            latinTransliteration[index].Substring(1)));
                        continue;
                    }

                }
                if (charCodeCurrent >= 1072 && charCodeCurrent <= 1103)
                {
                    index = charCodeCurrent - 1072;
                    transliteratedText.Append(latinTransliteration[index]);
                    continue;
                }
                if (charCodeCurrent == 1025 || charCodeCurrent == 1105)
                {
                    if(charCodeCurrent == 1025)
                    {
                        transliteratedText.Append((isWordContainsLCLetter) ?
                            (latinTransliteration[latinTransliteration.Length - 1][0].ToString().ToUpper() +
                            latinTransliteration[latinTransliteration.Length - 1].Substring(1)) :
                            latinTransliteration[latinTransliteration.Length - 1].ToString().ToUpper());
                        continue;
                    }
                    else
                    {
                        transliteratedText.Append(latinTransliteration[latinTransliteration.Length - 1]);
                        continue;
                    }
                }
                if(charCodeCurrent == 32)
                {
                    isEndOfWord = true; 
                }
                transliteratedText.Append(russianText[i]);
            }

            return transliteratedText.ToString();
        }

        private static bool IsWordContainsLowercaseLetter(string word)
        {
            bool isLCLetter = false;
            foreach (var letter in word)
            {
                if(letter >= 1072 && letter <= 1103 || letter == 1105)
                {
                    isLCLetter = true;
                }

            }
            return isLCLetter;
        }
    }
}
