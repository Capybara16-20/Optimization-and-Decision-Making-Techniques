using System;
using DirectSearch;

namespace DirectSearchUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = InputInteger("Введите количество стратегий: ", "Должно быть целое положительное число.");
            Console.WriteLine();

            double[,,] probabilityMatrices = new double[n, n, n];
            int[,,] payoffMatrices = new int[n, n, n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Стратегия {0}:", i + 1);
                double[,] probabilityMatrix = InputProbabilityMatrix(n, "Введите матрицу вероятностей:");
                for (int j = 0; j < n; j++)
                {
                    for (int z = 0; z < n; z++)
                    {
                        probabilityMatrices[i, j, z] = probabilityMatrix[j, z];
                    }
                }

                int[,] payoffMatrix = InputPayoffMatrix(n, "Введите матрицу выигрышей:");
                for (int j = 0; j < n; j++)
                {
                    for (int z = 0; z < n; z++)
                    {
                        payoffMatrices[i, j, z] = payoffMatrix[j, z];
                    }
                }
            }
            Console.Clear();


            /*double[,,] probabilityMatrices = { { { 0.4, 0.45, 0.15 }, { 0.1, 0.6, 0.3 }, { 0.1, 0.4, 0.5 } },
                                               { { 0.7, 0.15, 0.15 }, { 0.25, 0.6, 0.15 }, { 0.1, 0.7, 0.2} },
                                               { { 0.2, 0.6, 0.2 }, { 0, 0.8, 0.2 }, { 0, 0.3, 0.7} } };

            int[,,] payoffMatrices = { { { 400, 520, 650 }, { 350, 420, 700 }, { 200, 300, 500 } },
                                       { { 900, 1100, 1700 }, { 350, 950, 1750 }, { 550, 750, 1150} },
                                       { { 400, 550, 700 }, { 370, 480, 1750 }, { 270, 450, 1150} } };

            DirectSearchProcessor processor = new DirectSearchProcessor(probabilityMatrices, payoffMatrices, 3);*/

            DirectSearchProcessor processor = new DirectSearchProcessor(probabilityMatrices, payoffMatrices, n);
             processor.Calculate();
            

        }

        private static double[,] InputProbabilityMatrix(int n, string inputMessage)
        {
            const string failureMessage = "Должно быть вещественное неотрицательное число.";

            Console.WriteLine(inputMessage);

            double[,] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                bool isProbabilitiesCorrect = false;
                while (!isProbabilitiesCorrect)
                {
                    double sum = 0;
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
                    if (x >= 0)
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
