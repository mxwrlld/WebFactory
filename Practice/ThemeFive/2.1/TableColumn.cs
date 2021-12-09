using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1
{
    class TableColumn<T> where T : class
    {
        public int Width
        {
            get => MaxContentLength > Title.Length
                ? MaxContentLength + 2 * IndentLength : Title.Length + 2 * IndentLength;
        }
        public string Title { get; set; }
        public bool Hidden { get; set; }
        public int IndentLength { get; set; }
        public int MaxContentLength { get; set; }
        public bool IsMaxContentGreaterTitle => MaxContentLength > Title.Length;

        public Func<T, string> GetFormattedValue { get; }

        public TableColumn(string title, int indentLength, int maxContentLength, Func<T, string> getFormattedValue, bool hidden = false)
        {
            Title = title;
            IndentLength = indentLength;
            MaxContentLength = maxContentLength;
            GetFormattedValue = getFormattedValue;
            Hidden = hidden;
        }

        public void PrintContent(T obj, bool hiddenContent = false, bool lastContent = false)
        {
            var content = (obj == null ? null : GetFormattedValue(obj));
            ConsoleColor foregroundColor = Console.ForegroundColor;
            if (!Hidden)
            {
                if (content == null)
                {
                    Console.Write(new string(' ', Width));
                }
                else
                {
                    if (hiddenContent)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    string indent = new string(' ', IndentLength);
                    Console.Write(indent + content
                        + new string(' ', (IsMaxContentGreaterTitle
                                                ? MaxContentLength - content.Length
                                                : Title.Length - content.Length))
                        + indent
                        );
                }
                if (hiddenContent)
                {
                    Console.ForegroundColor = foregroundColor;
                }
                if (lastContent)
                {
                    Console.ResetColor();
                }
                Console.Write("│");
            }

        }
    }
}