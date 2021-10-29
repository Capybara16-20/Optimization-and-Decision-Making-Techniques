using System;

namespace DirectSearch
{
    public class DirectSearchProcessor
    {
        public int N { get; }
        public double[,,] ProbabilityMatrices { get; private set; }
        public int[,,] PayoffMatrix { get; private set; }


        public DirectSearchProcessor(double[,,] probabilityMatrices, int[,,] payoffMatrices, int n)
        {
            ProbabilityMatrices = probabilityMatrices;
            PayoffMatrix = payoffMatrices;
            N = n;
        }

        public void Calculate()
        {
            double[] stepCosts = new double[N];
            double[,] probabilityCosts = GetProbabilityCosts(ProbabilityMatrices, PayoffMatrix);
            for (int i = 0; i < N; i++)
            {
                double[,] probabilityMatrix = new double[N, N];
                int[,] payoffMatrix = new int[N, N];
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        probabilityMatrix[j, k] = ProbabilityMatrices[i, j, k];
                        payoffMatrix[j, k] = PayoffMatrix[i, j, k];
                    }
                }

                double[,] stepProbabilityCosts = GetStepProbabilityCosts(probabilityCosts, stepCosts);
                
                stepCosts = GetStepCosts(stepProbabilityCosts);

                int[] decision = GetDecision(stepProbabilityCosts, stepCosts);

                ShowStepResults(stepProbabilityCosts, stepCosts, decision, i + 1);
            }

        }

        private double[,] GetProbabilityCosts(double[,,] probabilityMatrices, int[,,] payoffMatrices)
        {
            double[,] probabilityCosts = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        probabilityCosts[j, i] += probabilityMatrices[i, j, k] * payoffMatrices[i, j, k];
                    }
                }
            }

            return probabilityCosts;
        }

        private double[,] GetStepProbabilityCosts(double[,] probabilityCosts, double[] stepCosts)
        {
            double[,] stepProbabilityCosts = new double[N, N];
            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    double a = 0;
                    for (int k = 0; k < N; k++)
                    {
                        a += stepCosts[k] * ProbabilityMatrices[j, i, k];
                    }
                    stepProbabilityCosts[i, j] = probabilityCosts[i, j] + a;
                }
            }

            return stepProbabilityCosts;
        }

        private double[] GetStepCosts(double[,] probabilityCosts)
        {
            double[] stepCosts = new double[N];
            for (int i = 0; i < N; i++)
            {
                double max = 0;
                for (int j = 0; j < N; j++)
                {
                    if (probabilityCosts[i, j] > max)
                    {
                        max = probabilityCosts[i, j];
                    }
                }

                stepCosts[i] = max;
            }

            return stepCosts;
        }

        private int[] GetDecision(double[,] stepProbabilityCosts, double[] stepCosts)
        {
            int[] decision = new int[N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (stepProbabilityCosts[i, j] == stepCosts[i])
                    {
                        decision[i] = j + 1;
                    }
                }
            }

            return decision;
        }

        private void ShowStepResults(double[,] stepProbabilityCosts, double[] stepCosts, int[] decision, int step)
        {

            Console.WriteLine("Результат выполнения {0}-го шага:", step);

            Console.Write("{0,-15}", "Состояние");
            for (int i = 0; i < N; i++)
            {
                
                Console.Write("{0} {1,-5}", "Стратегия", i + 1);
            }
            Console.Write("{0,-15}", "F");
            Console.Write("{0,-15}", "Z");
            Console.WriteLine();

            for (int i = 0; i < N; i++)
            {
                Console.Write("{0,-15}", i + 1);
                for (int j = 0; j < N; j++)
                {
                    Console.Write("{0,-15:0.###}", stepProbabilityCosts[i, j]);
                }
                Console.Write("{0,-15:0.###}", stepCosts[i]);
                Console.Write("{0,-15}", decision[i]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
