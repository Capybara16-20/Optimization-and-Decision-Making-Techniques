using System;
using System.Collections.Generic;

namespace Analyzer
{
    public class HierarchyAnalyzer
    {
        public int n;
        public int m;

        public double[,] criteriaMatrix;
        public double[,,] alternativesCube;


        public double[] GetImportance(double[,] matrix)
        {
            double[] geometricMean = new double[matrix.GetLength(0)];
            double geometricMeanSum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double composition = 1;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    composition *= matrix[i, j];
                }

                geometricMean[i] = Math.Pow(composition, (1.0 / matrix.GetLength(0)));
                geometricMeanSum += geometricMean[i];
            }

            double[] importance = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                importance[i] = geometricMean[i] / geometricMeanSum;
            }

            return importance;
        }

        public double GetConsistencyIndex(double[,] matrix)
        {
            double matrixEigenvalue = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double sum = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sum += matrix[j, i];
                }

                matrixEigenvalue += sum * GetImportance(matrix)[i];
            }

            return (matrixEigenvalue - matrix.GetLength(0)) / (matrix.GetLength(0) - 1);
        }

        public double GetConsistencyRelation(double[,] matrix)
        {
            return GetConsistencyIndex(matrix) / randomConsistencyIndex[matrix.GetLength(0)];
        }

        private readonly Dictionary<int, double> randomConsistencyIndex = new Dictionary<int, double>()
        {
            [1] = 0.0,
            [2] = 0.0,
            [3] = 0.58,
            [4] = 0.90,
            [5] = 1.12,
            [6] = 1.24,
            [7] = 1.32,
            [8] = 1.41,
            [9] = 1.45,
            [10] = 1.49
        };
    }
}
