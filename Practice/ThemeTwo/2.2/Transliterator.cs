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
                if (charCodeCurrent >= 'А' && charCodeCurrent <= 'Я')
                {
                    if (!isWordContainsLCLetter)
                    {
                        index = charCodeCurrent - 'А';
                        transliteratedText.Append(latinTransliteration[index].ToUpper());
                        continue;
                    }
                    else
                    {
                        index = charCodeCurrent - 'А';
                        transliteratedText.Append((latinTransliteration[index].Length == 1) ?
                            latinTransliteration[index].ToUpper() :
                            (latinTransliteration[index][0].ToString().ToUpper() +
                            latinTransliteration[index].Substring(1)));
                        continue;
                    }

                }
                if (charCodeCurrent >= 'а' && charCodeCurrent <= 'я')
                {
                    index = charCodeCurrent - 'а';
                    transliteratedText.Append(latinTransliteration[index]);
                    continue;
                }
                if (charCodeCurrent == 'Ё' || charCodeCurrent == 'ё')
                {
                    if(charCodeCurrent == 'Ё')
                    {
                        transliteratedText.Append((isWordContainsLCLetter) ?
                            (latinTransliteration[latinTransliteration.Length - 1][0].ToString().ToUpper() +
                            latinTransliteration[latinTransliteration.Length - 1].Substring(1)) :
                            latinTransliteration[latinTransliteration.Length - 1].ToUpper());
                        continue;
                    }
                    else
                    {
                        transliteratedText.Append(latinTransliteration[latinTransliteration.Length - 1]);
                        continue;
                    }
                }
                if(charCodeCurrent == ' ')
                {
                    isEndOfWord = true; 
                }
                transliteratedText.Append(russianText[i]);
            }

            return transliteratedText.ToString();
        }

        private static bool IsWordContainsLowercaseLetter(string word)
        {
            foreach (var letter in word)
            {
                if(letter >= 'а' && letter <= 'я' || letter == 'ё')
                {
                    return true;
                }

            }
            return false;
        }
    }
}
