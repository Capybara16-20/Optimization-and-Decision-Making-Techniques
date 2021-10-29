using System;
using Desicion;

namespace DesicionUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество состояний природы: ");
            int n = InputCount();

            Console.WriteLine("Введите вероятности состояний природы:");
            double[] probabilities = GetProbabilities(n);

            Console.Write("Введите количество альтернатив: ");
            int m = InputCount();

            Console.WriteLine("Введите матрицу полезности:");
            double[,] utilityMatrix = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("Альтернатива {0} Состояние {1}: ", i + 1, j + 1);
                    utilityMatrix[i, j] = InputValue(0);
                }
            }

            /*double[] probabilities = new double[] { 0.3, 0.5, 0.2 };
            double[,] utilityMatrix = new double[,] { { 3, 16, 5 }, { 6, 15, 8 }, { 6, 9, 4 }, { 6, 15, 8 } };*/
            
            Calculator calculator = new Calculator(probabilities, utilityMatrix);

            Console.WriteLine();
            Console.WriteLine("Вероятности:");
            calculator.ShowProbabilities();
            Console.WriteLine();

            Console.WriteLine("Матрица полезности:");
            calculator.ShowUtilityMatrix();
            Console.WriteLine();

            Console.WriteLine("Матрица вероятностей полезности:");
            calculator.ShowUtilityProbabilitiesMatrix();
            Console.WriteLine();

            Console.WriteLine("Математическое ожидание:");
            calculator.ShowExpectedValues();
            Console.WriteLine();

            calculator.ShowDecision();

            Console.ReadLine();
        }

        private static double[] GetProbabilities(int n)
        {
            double[] probabilities = new double[n];
            bool isCorrect = false;
            while (!isCorrect)
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    Console.Write("Вероятность {0}: ", i + 1);
                    probabilities[i] = InputValue(0, 1);
                    sum += probabilities[i];
                }

                if (sum == 1)
                    isCorrect = true;
                else
                    Console.WriteLine("Сумма вероятностей должна равняться 1");
            }

            return probabilities;
        }

        private static int InputCount()
        {
            int value = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                if (int.TryParse(Console.ReadLine(), out value))
                    if (value > 0)
                        isCorrect = true;

                if (!isCorrect)
                    Console.Write("Введите целое число больше 0: ");
            }

            return value;
        }

        private static double InputValue(int minBorder, int? maxBorder = null)
        {
            double value = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                if (double.TryParse(Console.ReadLine(), out value))
                    if ((value > minBorder) && ((maxBorder.HasValue && value < maxBorder) || !maxBorder.HasValue))
                        isCorrect = true;

                if (!isCorrect)
                    if (maxBorder.HasValue)
                        Console.Write("Введите вещественное число больше {0} меньше {1}: ", minBorder, maxBorder);
                    else
                        Console.Write("Введите вещественное число больше {0}: ", minBorder);
            }

            return value;
        }
    }
}
