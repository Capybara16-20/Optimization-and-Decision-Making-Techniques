using System;

namespace MaximumUtility
{
    public class MaximumUtilityCalculator
    {
        private double[] probabilityVector;
        private double[,] utilityMatrix;
        private int n;
        private int m;

        public MaximumUtilityCalculator(double[] probabilityVector, double[,] utilityMatrix)
        {
            this.probabilityVector = probabilityVector;
            this.utilityMatrix = utilityMatrix;

            n = probabilityVector.Length;
            m = utilityMatrix.GetLength(0);
        }

        private double[] GetProbabilityUtilities()
        {
            double[] probabilityUtilities = new double[m];
            for (int i = 0; i < m; i++)
            {
                double probabilityUtility = 0;
                for (int j = 0; j < n; j++)
                {
                    probabilityUtility += utilityMatrix[i, j] * probabilityVector[j];
                }
                probabilityUtilities[i] = probabilityUtility;
            }

            return probabilityUtilities;
        }

        public void ShowDecision()
        {
            int[] decisions = GetDecisions();
            double[] probabilityUtilities = GetProbabilityUtilities();

            Console.WriteLine("Оптимальные решения:");

            PrintLine(17, 15, n + 1);

            Console.Write("|{0,-17}", "");
            for (int i = 0; i < n; i++)
            {
                Console.Write("|{0} {1,-5}", "Состояние", i + 1);
            }

            Console.Write("|{0,-15}", "Выигрыш");
            Console.WriteLine("|");

            PrintLine(17, 15, n + 1);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < decisions.Length; j++)
                {
                    if (i == decisions[j])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }

                Console.Write("|{0} {1,-5}", "Алтернатива", i + 1);
                for (int j = 0; j < n; j++)
                {
                    Console.Write("|{0,-15:0.###}", utilityMatrix[i, j]);
                }

                Console.Write("|{0,-15:0.###}", probabilityUtilities[i]);
                Console.WriteLine("|");

                Console.ResetColor();

                PrintLine(17, 15, n + 1);
            }

            Console.WriteLine();
        }

        private int[] GetDecisions()
        {
            double[] probabilityUtilities = GetProbabilityUtilities();
            double max = 0;
            for (int i = 0; i < m; i++)
            {
                if (probabilityUtilities[i] > max)
                {
                    max = probabilityUtilities[i];
                }
            }

            int count = 0;
            for (int i = 0; i < m; i++)
            {
                if (probabilityUtilities[i] == max)
                {
                    count++;
                }
            }

            int[] decisions = new int[count];
            for (int i = 0; i < m; i++)
            {
                int index = 0;
                if (probabilityUtilities[i] == max)
                {
                    decisions[index] = i;
                    index++;
                }
            }

            return decisions;
        }

        public void ShowCalculation()
        {
            double[] probabilityUtilities = GetProbabilityUtilities();

            Console.WriteLine("Расчёт ожидаемой полезности альтернатив:");
            for (int i = 0; i < m; i++)
            {
                Console.Write("Полезность {0}-й альтернативы: ", i + 1);
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0:0.###} * {1:0.###}", utilityMatrix[i, j], probabilityVector[j]);
                    if (j != n - 1)
                    {
                        Console.Write(" + ");
                    }
                }
                Console.WriteLine(" = {0:0.###}", probabilityUtilities[i]);
            }
            Console.WriteLine();
        }

        public void ShowProbabilities()
        {
            Console.WriteLine("Вероятности состояний природы:");

            PrintLine(17, 15, n);

            Console.Write("|{0,-17}", "");
            for (int i = 0; i < n; i++)
            {
                Console.Write("|{0} {1,-5}", "Состояние", i + 1);
            }
            Console.WriteLine("|");

            PrintLine(17, 15, n);

            Console.Write("|{0,-17}", "Вероятность");
            for (int i = 0; i < n; i++)
            {
                Console.Write("|{0,-15:0.###}", probabilityVector[i]);
            }
            Console.WriteLine("|");
            PrintLine(17, 15, n);

            Console.WriteLine();
        }

        public void ShowUtilityMatrix()
        {
            Console.WriteLine("Матрица полезности:");

            PrintLine(17, 15, n);

            Console.Write("|{0,-17}", "");
            for (int i = 0; i < n; i++)
            {
                Console.Write("|{0} {1,-5}", "Состояние", i + 1);
            }
            Console.WriteLine("|");

            PrintLine(17, 15, n);

            for (int i = 0; i < m; i++)
            {
                Console.Write("|{0} {1,-5}", "Алтернатива", i + 1);
                for (int j = 0; j < n; j++)
                {
                    Console.Write("|{0,-15:0.###}", utilityMatrix[i, j]);
                }
                Console.WriteLine("|");

                PrintLine(17, 15, n);
            }

            Console.WriteLine();
        }

        private void PrintLine(int rowTitleLength, int colTitleLength, int colCount)
        {
            Console.Write("+");
            Console.Write(new string('-', rowTitleLength));
            Console.Write("+");
            for (int i = 0; i < colCount; i++)
            {
                Console.Write(new string('-', colTitleLength));
                Console.Write("+");
            }
            Console.WriteLine();
        }
    }
}
