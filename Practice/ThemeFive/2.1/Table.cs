using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1
{
    class Table<T> where T : class
    {
        public List<TableColumn<T>> Columns { get; }

        public Table(List<TableColumn<T>> columns)
        {
            Columns = columns;
        }

        public void PrintHeader()
        {
            Console.WriteLine();

            Console.Write("┌");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┬");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┐");


            Console.WriteLine();


            for (int i = 0; i < Columns.Count; i++)
            {
                if (i == 0)
                    Console.Write("│");
                string indent = new string(' ', Columns[i].IndentLength);
                Console.Write(indent + Columns[i].Title
                    + (Columns[i].IsMaxContentGreaterTitle
                        ? new string(' ', Columns[i].MaxContentLength - Columns[i].Title.Length)
                        : String.Empty)
                    + indent);
                Console.Write("│");
            }


            Console.WriteLine();


            Console.Write("├");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┼");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┤");
        }

        public void PrintFooter()
        {
            Console.Write("└");
            for (int i = 0; i < Columns.Count; i++)
            {
                if (i > 0)
                    Console.Write("┴");
                Console.Write(new string('─', Columns[i].Width));
            }
            Console.Write("┘");
        }

        public static int GetMaxContentLength(List<string> content)
        {
            int maxContentLength = 0;

            foreach (var unit in content)
            {
                if (maxContentLength < unit.Length)
                    maxContentLength = unit.Length;
            }

            return maxContentLength;
        }

        public void Print(IEnumerable<T> rows)
        {
            PrintHeader();
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.Write("│");
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (i == Columns.Count - 1)
                        Columns[i].PrintContent(row, false, true);
                    else
                        Columns[i].PrintContent(row);
                }
                Console.WriteLine();
            }
            PrintFooter();
        }

        public void Print(IEnumerable<T> rows, T selectedRow)
        {
            PrintHeader();
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.Write("│");
                if (row == selectedRow)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (i == Columns.Count - 1)
                        Columns[i].PrintContent(row, false, true);
                    else
                        Columns[i].PrintContent(row);
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            PrintFooter();
        }

        public void SetWindowSize()
        {
            int maxWindowWidth = 8; // Количество соединительных символов вроде "┌", "┐" и т.д.
            foreach (var column in Columns)
            {
                if (!column.Hidden)
                    maxWindowWidth += column.Width;
            }

            Console.SetWindowSize(maxWindowWidth > Console.LargestWindowWidth
                ? Console.LargestWindowWidth : maxWindowWidth, 20);
        }
    }
}
