using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalArray;

namespace DirectSearch
{
    public class DirectSearchProcessor
    {
        public int StrategiesNumber
        {
            get { return ProbabilityMatrix.GetRowsCount(); }
        }

        public int DecisionsNumber
        {
            get { return (int)Math.Pow(StrategiesNumber, StrategiesNumber); }
        }

        public double[,] ProbabilityMatrix { get; private set; }
        public int[,] PayoffMatrix { get; private set; }

        public DirectSearchProcessor(double[,] probabilityMatrix, int[,] payoffMatrix)
        {
            ProbabilityMatrix = CloneElements(probabilityMatrix);
            PayoffMatrix = CloneElements(payoffMatrix);
        }

        public int[,] Strategies
        {
            get
            {
                int[,] strategies = new int[DecisionsNumber, StrategiesNumber];

                IEnumerable<IEnumerable<int>> result = GetCombinations(Enumerable.Range(1, StrategiesNumber), StrategiesNumber);
                for (int i = 0; i < DecisionsNumber; i++)
                {
                    IEnumerable<int> decision = result.ElementAt(i);
                    for (int j = 0; j < StrategiesNumber; j++)
                        strategies[i, j] = decision.ElementAt(j);
                }

                return strategies;
            }
        }

        public double[,] Decisions
        {
            get
            {
                double[,] decisions = new double[DecisionsNumber * StrategiesNumber, StrategiesNumber];
                double[,] probabilityMatrix = CloneElements(ProbabilityMatrix);
                int[,] payoffMatrix = CloneElements(PayoffMatrix);
                int[,] strategies = CloneElements(Strategies);
                for (int k = 0; k < StrategiesNumber; k++)
                    for (int i = 0; i < DecisionsNumber; i++)
                        for (int j = 0; j < StrategiesNumber; j++)
                        {
                            int indexI = k;
                            int indexJ = strategies[i, j] - 1;

                            decisions[i + (DecisionsNumber * k), j] =
                                probabilityMatrix[indexI, indexJ] * payoffMatrix[indexI, indexJ];
                        }

                return decisions;
            }
        }

        public double[] Gains
        {
            get
            {
                double[,] decisions = CloneElements(Decisions);
                double[] gains = new double[DecisionsNumber * StrategiesNumber];
                for (int i = 0; i < decisions.GetRowsCount(); i++)
                {
                    double sum = 0;
                    for (int j = 0; j < decisions.GetColumnsCount(); j++)
                        sum += decisions[i, j];

                    gains[i] = sum;
                }

                return gains;
            }
        }

        public double[] StrategiesGains
        {
            get
            {
                double[] gains = CloneElements(Gains);
                double[] strategiesGains = new double[StrategiesNumber];
                for (int i = 0; i < StrategiesNumber; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < DecisionsNumber; j++)
                        sum += gains[j + (DecisionsNumber * i)];

                    strategiesGains[i] = sum;
                }

                return strategiesGains;
            }
        }

        private static double[,] CloneElements(double[,] elements)
        {
            int rows = elements.GetRowsCount();
            int columns = elements.GetColumnsCount();

            double[,] clonedElements = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    clonedElements[i, j] = elements[i, j];

            return clonedElements;
        }

        private static int[,] CloneElements(int[,] elements)
        {
            int rows = elements.GetRowsCount();
            int columns = elements.GetColumnsCount();

            int[,] clonedElements = new int[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    clonedElements[i, j] = elements[i, j];

            return clonedElements;
        }

        private static double[] CloneElements(double[] elements)
        {
            int length = elements.Length;

            double[] clonedElements = new double[length];
            for (int i = 0; i < length; i++)
                    clonedElements[i] = elements[i];

            return clonedElements;
        }

        private static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetCombinations(list, length - 1)
                .SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
