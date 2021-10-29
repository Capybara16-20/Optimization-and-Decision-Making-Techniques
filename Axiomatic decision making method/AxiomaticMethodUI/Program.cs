using System;
using System.Collections.Generic;
using AxiomaticMethodProcessor;

namespace AxiomaticMethodUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*List<double> a = new List<double>() { 180, 70, 10 };
            List<double> b = new List<double>() { 170, 40, 15 };
            List<double> c = new List<double>() { 160, 55, 20 };
            List<double> d = new List<double>() { 150, 50, 25 };
            List<List<double>> wins = new List<List<double>>() { a, b, c, d };

            List<double> importance = new List<double>() { 0.5, 0.3, 0.2 };*/

            int importanceCount = InputInteger("Количество критериев: ", "Введите целое число больше 1.");
            List<double> importance = InputImportance(importanceCount, "Важности критериев:");
            Console.WriteLine();

            int alternativesCount = InputInteger("Количество альтернатив: ", "Введите целое число больше 1.");
            List<List<double>> wins = InputWins(alternativesCount, importanceCount, "Выигрыши альтернатив по критериям:");
            Console.WriteLine();

            Processor processor = new Processor(importance, wins);

            Console.WriteLine("Функции выигрышей:");
            List<List<double>> winFunctions = processor.WinFunctions;
            for (int i = 0; i < winFunctions.Count; i++)
            {
                Console.WriteLine("А{0}:", i + 1);
                for (int j = 0; j < winFunctions[i].Count; j++)
                    Console.WriteLine("c{0}:{1:0.###} ", (j + 1), winFunctions[i][j]);

                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Полезность альтернатив:");
            List<double> rating = processor.Rating;
            for (int i = 0; i < rating.Count; i++)
                Console.WriteLine("A{0}: {1:0.###} ", (i + 1), rating[i]);
            Console.ReadKey();
            Console.Clear();

            ShowResult(processor.GetResult());
            Console.WriteLine();
        }



        private static void ShowResult(List<List<int>> result)
        {
            Console.WriteLine("Иерархия оптимальности альтернатив:");
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].Count; j++)
                    Console.Write("A{0:0.###}  ", result[i][j]);

                if (i != result.Count - 1)
                    Console.Write(">>  ");
            }
        }

        private static List<double> InputImportance(int importanceCount, string inputMessage)
        {
            Console.WriteLine(inputMessage);

            List<double> importance = new List<double>();
            bool isProbabilitiesCorrect = false;
            while (!isProbabilitiesCorrect)
            {
                double sum = 0;
                for (int i = 0; i < importanceCount; i++)
                {
                    importance.Add(0);
                    importance[i] = InputDouble(string.Format("w{0}: ", (i + 1)), "Введите число ", 0, 1);
                    sum = Math.Round(sum + importance[i], 1);
                }

                if (sum == 1)
                    isProbabilitiesCorrect = true;
                else
                    Console.WriteLine("Сумма должна равняться 1.");
            }

            return importance;
        }

        private static List<List<double>> InputWins(int alternativesCount, int importanceCount, string inputMessage)
        {
            Console.WriteLine(inputMessage);

            List<List<double>> wins = new List<List<double>>();
            for (int i = 0; i < alternativesCount; i++)
            {
                wins.Add(new List<double>());
                for (int j = 0; j < importanceCount; j++)
                {
                    wins[i].Add(0);
                    wins[i][j] = InputDouble(string.Format("A{0} c{1}: ", i + 1, j + 1), "Введите число ");
                }

            }

            return wins;
        }

        private static int InputInteger(string inputMessage, string failureMessage)
        {
            int x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (int.TryParse(Console.ReadLine(), out x))
                    if (x > 0)
                        isCorrect = true;

                if (!isCorrect)
                    Console.WriteLine(failureMessage);
            }

            return x;
        }

        private static double InputDouble(string inputMessage, string failureMessage, double lowBound = 0, double? highBound = null)
        {
            double x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (double.TryParse(Console.ReadLine(), out x))
                    if ((x >= lowBound) && ((highBound.HasValue && x <= highBound) || !highBound.HasValue))
                        isCorrect = true;

                if (!isCorrect)
                    if (highBound.HasValue)
                        Console.WriteLine("{0} от {1} до {2}.", failureMessage, lowBound, highBound);
                    else
                        Console.WriteLine("{0} больше {1}.", failureMessage, lowBound);
            }

            return x;
        }
    }
}
