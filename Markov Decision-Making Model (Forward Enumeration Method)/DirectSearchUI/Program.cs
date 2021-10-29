using System;
using DirectSearch;

namespace DirectSearchUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = InputInteger("Введите количество стратегий: ", "Должно быть целое положительное число.");
            double[,] probabilityMatrix = InputProbabilityMatrix(n, "Введите матрицу вероятностей:");
            int[,] payoffMatrix = InputPayoffMatrix(n, "Введите матрицу выигрышей:");
            Console.Clear();

            DirectSearchProcessor processor = new DirectSearchProcessor(probabilityMatrix, payoffMatrix);

            /*Console.WriteLine("Множество стационарных стратегий:");
            int[,] strategies = processor.Strategies;
            for (int i = 0; i < strategies.GetLength(0); i++)
            {
                for (int j = 0; j < strategies.GetLength(1); j++)
                    Console.Write(strategies[i, j] + " ");

                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Принятые решения:");
            double[,] decisions = processor.Decisions;
            for (int i = 0; i < decisions.GetLength(0); i++)
            {
                for (int j = 0; j < decisions.GetLength(1); j++)
                    Console.Write("{0:0.###} ", decisions[i, j]);

                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Значения выигрышей:");
            double[] gains = processor.Gains;
            for (int i = 0; i < gains.Length; i++)
                Console.WriteLine("{0:0.###} ", gains[i]);
            Console.ReadKey();
            Console.Clear();*/

            Console.WriteLine("Выигрыши стратегий:");
            double[] strategiesGains = processor.StrategiesGains;
            double max = 0;
            int bestStrategie = 0;
            for (int i = 0; i < strategiesGains.Length; i++)
            {
                if (strategiesGains[i] > max)
                {
                    max = strategiesGains[i];
                    bestStrategie = i + 1;
                }

                Console.WriteLine("{0}: {1:0.###} ", (i + 1), strategiesGains[i]);
            }
            Console.WriteLine();

            Console.WriteLine("Оптимальная стратегия: {0}", bestStrategie);

        }

        private static double[,] InputProbabilityMatrix(int n, string inputMessage)
        {
            const string failureMessage = "Должно быть вещественное положительное число.";

            Console.WriteLine(inputMessage);

            double[,] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                bool isProbabilitiesCorrect = false;
                while (!isProbabilitiesCorrect)
                {
                    sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = InputDouble(string.Format("[{0}, {1}]: ", i + 1, j + 1), failureMessage);
                        sum = Math.Round(sum + matrix[i, j], 1);
                    }

                    if (sum == 1)
                        isProbabilitiesCorrect = true;
                    else
                        Console.WriteLine("Сумма вероятностей должна равняться 1.");
                }
            }

            return matrix;
        }

        private static int[,] InputPayoffMatrix(int n, string inputMessage)
        {
            const string failureMessage = "Должно быть целое положительное число.";

            Console.WriteLine(inputMessage);

            int[,] matrix = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = InputInteger(string.Format("[{0}, {1}]: ", i + 1, j + 1), failureMessage);

            Console.WriteLine();

            return matrix;
        }

        private static double InputDouble(string inputMessage, string failureMessage)
        {
            double x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (double.TryParse(Console.ReadLine(), out x))
                    if (x > 0)
                        isCorrect = true;

                if (!isCorrect)
                    Console.WriteLine(failureMessage);
            }

            return x;
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
    }
}
