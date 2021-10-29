using System;

namespace Calculator
{
    public class CostCalculator
    {
        public double RepairСost { get; }
        public double PreventiveMaintenanceCost { get; }
        public int NumberOfBroken { get; }

        public CostCalculator(double repairСosts, double preventiveMaintenanceCosts, int numberOfBroken)
        {
            RepairСost = repairСosts;
            PreventiveMaintenanceCost = preventiveMaintenanceCosts;
            NumberOfBroken = numberOfBroken;
        }

        public double[] GetTemporalProbabilities(int t, double[] probabilities)
        {
            double[] temporalProbabilities = new double[t];
            temporalProbabilities[0] = probabilities[0];
            for (int i = 1; i < t; i++)
                temporalProbabilities[i] = probabilities[i] + temporalProbabilities[i - 1];

            return temporalProbabilities;
        }

        public double[] GetTemporalProbabilitiesSquares(int t, double[] probabilities)
        {
            double[] temporalProbabilitiesSquares = GetTemporalProbabilities(t, probabilities);
            for (int i = 0; i < t; i++)
                temporalProbabilitiesSquares[i] *= temporalProbabilitiesSquares[i];

            return temporalProbabilitiesSquares;
        }

        public double[] GetTemporalProbabilitiesSums(int t, double[] probabilities)
        {
            double[] temporalProbabilitiesSums = new double[t];
            double[] temporalProbabilities = GetTemporalProbabilities(t, probabilities);
            for (int i = 1; i < t; i++)
                temporalProbabilitiesSums[i] = temporalProbabilities[i - 1] + temporalProbabilitiesSums[i - 1];

            return temporalProbabilitiesSums;
        }

        public double[] GetTemporalProbabilitiesSquaresSums(int t, double[] probabilities)
        {
            double[] temporalProbabilitiesSquaresSums = new double[t];
            double[] temporalProbabilitiesSquares = GetTemporalProbabilitiesSquares(t, probabilities);
            for (int i = 1; i < t; i++)
                temporalProbabilitiesSquaresSums[i] = 
                    temporalProbabilitiesSquares[i - 1] + temporalProbabilitiesSquaresSums[i - 1];

            return temporalProbabilitiesSquaresSums;
        }

        public double[] GetTotalCosts(int t, double[] probabilities)
        {
            double[] totalCosts = new double[t];
            double[] temporalProbabilitiesSums = GetTemporalProbabilitiesSums(t, probabilities);
            double[] temporalProbabilitiesSquaresSums = GetTemporalProbabilitiesSquaresSums(t, probabilities);
            for (int i = 0; i < t; i++)
            {
                double variableCosts = 
                    (RepairСost / (i + 1) + RepairСost * RepairСost / ((i + 1) * (i + 1))) * temporalProbabilitiesSums[i] 
                        - RepairСost / (i + 1) * temporalProbabilitiesSquaresSums[i];
                double constCosts = PreventiveMaintenanceCost / (i + 1);
                totalCosts[i] = NumberOfBroken * (variableCosts + constCosts);
            }

            return totalCosts;
        }

        public double GetMinTotalCost(int t, double[] probabilities)
        {
            double minTotalCost = double.MaxValue;
            double[] totalCosts = GetTotalCosts(t, probabilities);
            for (int i = 0; i < t; i++)
                if (totalCosts[i] < minTotalCost)
                    minTotalCost = totalCosts[i];

            return minTotalCost;
        }
    }
}
