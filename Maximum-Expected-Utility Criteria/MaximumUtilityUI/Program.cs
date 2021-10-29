using System;
using MaximumUtility;

namespace MaximumUtilityUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = InputInteger("Введите количество состояний природы: ", "Введите целое число больше 0!");
            double[] probabilityVector = InputProbabilityVector(n, "Введите вероятности состояний природы:");
            int m = InputInteger("Введите количество альтернатив: ", "Введите целое число больше 0!");
            double[,] utilityMatrix = InputUtilityMatrix(n, m, "Введите матрицу полезности:");
            Console.Clear();

            MaximumUtilityCalculator calculator = new MaximumUtilityCalculator(probabilityVector, utilityMatrix);
            calculator.ShowProbabilities();
            calculator.ShowUtilityMatrix();
            calculator.ShowCalculation();
            calculator.ShowDecision();
        }

        private static double[] InputProbabilityVector(int n, string inputMessage)
        {
            const string failureMessage = "Введите число в диапазоне [0; 1]!";

            Console.WriteLine(inputMessage);

            double[] vector = new double[n];
            bool isProbabilitiesCorrect = false;
            while (!isProbabilitiesCorrect)
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    vector[i] = InputDouble(string.Format("Состояние {0}: ", i + 1), failureMessage, true, 1);
                    sum = Math.Round(sum + vector[i], 1);
                }

                if (sum == 1)
                    isProbabilitiesCorrect = true;
                else
                    Console.WriteLine("Сумма вероятностей должна быть равна 1!");
            }

            return vector;
        }

        private static double[,] InputUtilityMatrix(int n, int m, string inputMessage)
        {
            const string failureMessage = "Введите число!";

            Console.WriteLine(inputMessage);

            double[,] matrix = new double[m, n];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = InputDouble(string.Format("[{0}, {1}]: ", i + 1, j + 1), failureMessage, false);

            Console.WriteLine();

            return matrix;
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

        private static double InputDouble(string inputMessage, string failureMessage, bool isPositiv, int? maxValue = null)
        {
            double x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (double.TryParse(Console.ReadLine(), out x))
                {
                    if (isPositiv)
                    {
                        if (x >= 0)
                        {
                            if (maxValue.HasValue)
                            {
                                if (x <= maxValue)
                                {
                                    isCorrect = true;
                                }
                            }
                            else
                            {
                                isCorrect = true;
                            }
                            
                        }
                    }
                    else
                    {
                        if (maxValue.HasValue)
                        {
                            if (x <= maxValue)
                            {
                                isCorrect = true;
                            }
                        }
                        else
                        {
                            isCorrect = true;
                        }
                    }
                }

                if (!isCorrect)
                {
                    Console.WriteLine(failureMessage);
                }
            }

            return x;
        }
    }
}
