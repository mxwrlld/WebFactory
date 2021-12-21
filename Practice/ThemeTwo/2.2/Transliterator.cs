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

            string[] words = russianText.Split(" "); 

            foreach (var word in words)
            {
                for (int j = 0; j < word.Length; ++j)
                {
                    char currentChar = word[j];
                    string transilteratedWord = WordTransliterating(word: word, symbol: currentChar);
                    transliteratedText.Append(transilteratedWord);
                }
                transliteratedText.Append(" ");
            }

            return transliteratedText.ToString();
        }

        private static string WordTransliterating(string word, char symbol)
        {
            bool isWordContainsLCLetter = IsWordContainsLowercaseLetter(word);
            int index;
            if (symbol >= 'А' && symbol <= 'Я')
            {
                index = symbol - 'А';
                if (!isWordContainsLCLetter)
                {                    
                    return (latinTransliteration[index].ToUpper());
                }
                else
                {
                    return ((latinTransliteration[index].Length == 1) ?
                        latinTransliteration[index].ToUpper() :
                        (latinTransliteration[index][0].ToString().ToUpper() +
                        latinTransliteration[index].Substring(1)));
                }

            }
            if (symbol >= 'а' && symbol <= 'я')
            {
                index = symbol - 'а';
                return (latinTransliteration[index]);
            }
            if (symbol == 'Ё' || symbol == 'ё')
            {
                if (symbol == 'Ё')
                {
                    return ((isWordContainsLCLetter) ?
                        (latinTransliteration[latinTransliteration.Length - 1][0].ToString().ToUpper() +
                        latinTransliteration[latinTransliteration.Length - 1].Substring(1)) :
                        latinTransliteration[latinTransliteration.Length - 1].ToUpper());
                }
                else
                {
                    return (latinTransliteration[latinTransliteration.Length - 1]);
                }
            }

            return symbol.ToString();
        }

        private static bool IsWordContainsLowercaseLetter(string word)
        {
            foreach (var letter in word)
            {
                if (letter >= 'а' && letter <= 'я' || letter == 'ё')
                {
                    return true;
                }

            }
            return false;
        }
    }
}
