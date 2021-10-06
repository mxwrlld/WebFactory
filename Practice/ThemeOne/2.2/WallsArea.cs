using System;

namespace _2._2
{
    class WallsArea
    {
        static void Main()
        {
            double wallsArea,
                length,
                width,
                height;

            try
            {
                GetRoomParameters(out length, out width, out height);
                wallsArea = CalculateWallsArea(length, width, height);

                wallsArea = AccountingAreaOfApertures("окон", "окна", wallsArea);
                wallsArea = AccountingAreaOfApertures("дверей", "двери", wallsArea);

                PrintResultWallsArea(wallsArea);
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }

        }

        static double AccountingAreaOfApertures(string apertureTypePlural, string apertureTypeSingular, double wallsArea)
        {
            int apertureCount;
            double length,
                width;

            Console.WriteLine();
            Console.WriteLine();
            Console.Write($"Введите количество {apertureTypePlural}: ");
            InputCotrol(Console.ReadLine(), out apertureCount);

            for (int i = 0; i < apertureCount; ++i)
            {
                GetApertureParameters(apertureTypeSingular, out length, out width);
                wallsArea -= CalculateApertureArea(length, width);
                // Какой-никакой учёт невозможных вариантов
                if (wallsArea <= 0)
                    throw new Exception("Невозможная ситуация!");
            }

            return wallsArea;
        }

        static void GetApertureParameters(string apertureType, out double length, out double width)
        {
            Console.WriteLine();
            Console.WriteLine($"Введите размеры {apertureType} в метрах! ");
            GetParametr("длину", apertureType, out length);
            GetParametr("ширину", apertureType, out width);
        }

        static void GetRoomParameters(out double length, out double width, out double height)
        {
            Console.WriteLine();
            Console.WriteLine("Введите размеры помещения (в метрах) !!!");
            GetParametr("длину", "помещения", out length);
            GetParametr("ширину", "помещения", out width);
            GetParametr("высоту", "помещения", out height);
        }

        static void GetParametr(string parametrType, string apertureType, out double parametr)
        {
            Console.Write($"\n\tВведите {parametrType} {apertureType}: ");
            if (!double.TryParse(Console.ReadLine(), out parametr))
                throw new Exception("Invalid value");
        }

        private static double CalculateWallsArea(double length, double width, double height)
        {
            return (2 * height * (length + width));
        }

        private static double CalculateApertureArea(double length, double width)
        {
            return (length * width);
        }

        private static void InputCotrol(string input, out int variable)
        {
            variable = int.Parse(input);
        }

        static void PrintResultWallsArea(double wallsArea)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Общая площадь стен: {wallsArea}");
        }
    }
}
