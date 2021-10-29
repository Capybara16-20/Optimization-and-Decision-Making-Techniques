using System;

namespace Desicion
{
    public class Calculator
    {
        readonly double[] probabilities;
        readonly double[,] utilityMatrix;

        public Calculator(double[] probabilities, double[,] utilityMatrix)
        {
            this.probabilities = probabilities;
            this.utilityMatrix = utilityMatrix;
        }

        double[,] UtilityProbabilitiesMatrix
        {
            get
            {
                double[,] utilityProbabilitiesMatrix = new double[utilityMatrix.GetLength(0), utilityMatrix.GetLength(1)];
                for (int j = 0; j < utilityProbabilitiesMatrix.GetLength(1); j++)
                    for (int i = 0; i < utilityProbabilitiesMatrix.GetLength(0); i++)
                        utilityProbabilitiesMatrix[i, j] = utilityMatrix[i, j] * probabilities[j];

                return utilityProbabilitiesMatrix;
            }
        }

        double[] ExpectedValues
        {
            get
            {
                double[] expectedValues = new double[UtilityProbabilitiesMatrix.GetLength(0)];
                for (int i = 0; i < UtilityProbabilitiesMatrix.GetLength(0); i++)
                    for (int j = 0; j < UtilityProbabilitiesMatrix.GetLength(1); j++)
                        expectedValues[i] += UtilityProbabilitiesMatrix[i, j];

                return expectedValues;
            }
        }

        public void ShowProbabilities()
        {
            for (int i = 0; i < probabilities.Length; i++)
            {
                Console.Write("{0,-10}|", "Состояние" + (i + 1));
                Console.Write("{0,-6:0.###}", probabilities[i]);
                Console.WriteLine();
            }
        }

        public void ShowUtilityMatrix()
        {
            ShowMatrix(utilityMatrix);
        }

        public void ShowUtilityProbabilitiesMatrix()
        {
            ShowMatrix(UtilityProbabilitiesMatrix);
        }

        private void ShowMatrix(double[,] matrix)
        {
            Console.Write("{0,-13}|", "");
            for (int i = 0; i < matrix.GetLength(1); i++)
                Console.Write("{0,-10}|", "Состояние" + (i + 1));
            Console.WriteLine();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("{0,-13}|", "Альтернатива" + (i + 1));
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write("{0,-10:0.###}|", matrix[i, j]);
                Console.WriteLine();
            }
        }

        public void ShowExpectedValues()
        {
            for (int i = 0; i < ExpectedValues.GetLength(0); i++)
            {
                Console.Write("{0,-13}|", "Альтернатива" + (i + 1));
                Console.Write("{0,-6:0.###}", ExpectedValues[i]);
                Console.WriteLine();
            }
        }

        public void ShowDecision()
        {
            int[] decision = GetDesicion();
            
            Console.Write("Оптимальное(ые) решение: {0}", decision[0]);
            for (int i = 0; i < decision.Length; i++)
                if (i > 0)
                    Console.Write(", {0}", decision[i]);
            Console.WriteLine();
        }

        private int[] GetDesicion()
        {
            double maxValue = 0;
            int count = 0;
            for (int i = 0; i < ExpectedValues.Length; i++)
            {
                if (ExpectedValues[i] > maxValue)
                {
                    count = 1;
                    maxValue = ExpectedValues[i];
                }
                else if (ExpectedValues[i] == maxValue)
                    count++;
            }

            int[] decision = new int[count];
            int index = 0;
            for (int i = 0; i < ExpectedValues.Length; i++)
                if (ExpectedValues[i] == maxValue)
                {
                    decision[index] = i + 1;
                    index++;
                }

            return decision;
        }
    }
}
