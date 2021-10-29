using System;
using System.Collections.Generic;

namespace Rating
{
    public class RatingProcessor
    {
        const int maxWeight = 10;

        private readonly Criterion[] criteria;
        private readonly Alternative[] alternatives;

        public RatingProcessor(Criterion[] criteria, Alternative[] alternatives)
        {
            this.criteria = criteria;
            this.alternatives = alternatives;
        }

        private double[,] ConsensusMatrix
        {
            get
            {
                double[,] consensusMatrix = new double[alternatives.Length, alternatives.Length];
                double weightSum = 0;
                foreach (Criterion criterion in criteria)
                {
                    weightSum += criterion.Weight;
                }

                for (int i = 0; i < alternatives.Length; i++)
                {
                    for (int j = 0; j < alternatives.Length; j++)
                    {
                        if (i != j)
                        {
                            double currentSum = 0;
                            for (int v = 0; v < criteria.Length; v++)
                            {
                                if (alternatives[i].Rating[v] >= alternatives[j].Rating[v])
                                {
                                    currentSum += alternatives[i].Rating[v];
                                }
                            }

                            consensusMatrix[i, j] = currentSum / weightSum;
                        }
                        else
                        {
                            consensusMatrix[i, j] = -1;
                        }
                    }
                }

                return consensusMatrix;
            }
        }

        private double[,] DisagreementMatrix
        {
            get
            {
                double[,] disagreementMatrix = new double[alternatives.Length, alternatives.Length];
                for (int i = 0; i < alternatives.Length; i++)
                {
                    for (int j = 0; j < alternatives.Length; j++)
                    {
                        if (i != j)
                        {
                            double maxDifference = 0;
                            for (int v = 0; v < criteria.Length; v++)
                            {
                                double difference = alternatives[j].Rating[v] - alternatives[i].Rating[v];
                                if (difference > maxDifference)
                                {
                                    maxDifference = difference;
                                }
                            }

                            disagreementMatrix[i, j] = maxDifference / maxWeight;
                        }
                        else
                        {
                            disagreementMatrix[i, j] = -1;
                        }
                    }
                }

                return disagreementMatrix;
            }
        }

        private int[,] GetSuperiorityMatrix(double consensusThreshold, double disagreementThreshold)
        {
            int[,] superiorityMatrix = new int[alternatives.Length, alternatives.Length];
            for (int i = 0; i < alternatives.Length; i++)
            {
                for (int j = 0; j < alternatives.Length; j++)
                {
                    if (i != j)
                    {
                        if ((ConsensusMatrix[i, j] >= consensusThreshold) && (DisagreementMatrix[i, j] <= disagreementThreshold))
                        {
                            superiorityMatrix[i, j] = 1;
                            //superiorityMatrix[j, i] = 0;
                        }
                        else
                        {
                            superiorityMatrix[i, j] = 0;
                            //superiorityMatrix[j, i] = 1;
                        }
                    }
                    else if (i == j)
                    {
                        superiorityMatrix[i, j] = -1;
                    }
                }
            }

            return superiorityMatrix;
        }

        private List<Alternative> GetDecision(double consensusThreshold, double disagreementThreshold)
        {
            List<Alternative> decision = new List<Alternative>();
            int[,] superiorityMatrix = GetSuperiorityMatrix(consensusThreshold, disagreementThreshold);
            int max = 0;
            for (int i = 0; i < superiorityMatrix.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < superiorityMatrix.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        sum += superiorityMatrix[i, j];
                    }
                }

                if (sum > max)
                {
                    decision.Clear();
                    max = sum;
                    decision.Add(alternatives[i]);
                }
                else if (sum == max)
                {
                    decision.Add(alternatives[i]);
                }
            }

            return decision;
        }

        public void ShowRatingTable()
        {
            Console.WriteLine("Оценки альтернатив:");

            Console.Write("{0,-15}", "Критерии");
            foreach (Alternative alternative in alternatives)
            {
                Console.Write("{0,-15}", alternative.Name);
            }
            Console.WriteLine("Вес критерия");

            for (int i = 0; i < criteria.Length; i++)
            {
                Console.Write("{0,-15}", criteria[i].Name);

                for (int j = 0; j < alternatives.Length; j++)
                {
                    Console.Write("{0,-15}", alternatives[j].Rating[i]);
                }

                Console.Write(criteria[i].Weight);
                Console.WriteLine();
            }
        }

        public void ShowConsensusMatrix()
        {
            Console.WriteLine("Матрица согласия:");

            Console.Write("{0,-15}", "");
            foreach (Alternative alternative in alternatives)
            {
                Console.Write("{0,-15}", alternative.Name);
            }
            Console.WriteLine();

            for (int i = 0; i < alternatives.Length; i++)
            {
                Console.Write("{0,-15}", alternatives[i].Name);

                for (int j = 0; j < alternatives.Length; j++)
                {
                    if (ConsensusMatrix[i, j] >= 0)
                    {
                        Console.Write("{0,-15:0.###}", ConsensusMatrix[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,-15}", "-");
                    }
                }

                Console.WriteLine();
            }
        }

        public void ShowDisagreementMatrix()
        {
            Console.WriteLine("Матрица несогласия:");

            Console.Write("{0,-15}", "");
            foreach (Alternative alternative in alternatives)
            {
                Console.Write("{0,-15}", alternative.Name);
            }
            Console.WriteLine();

            for (int i = 0; i < alternatives.Length; i++)
            {
                Console.Write("{0,-15}", alternatives[i].Name);

                for (int j = 0; j < alternatives.Length; j++)
                {
                    if (DisagreementMatrix[i, j] >= 0)
                    {
                        Console.Write("{0,-15:0.###}", DisagreementMatrix[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,-15}", "-");
                    }
                }

                Console.WriteLine();
            }
        }

        public void ShowSuperiorityMatrix(double consensusThreshold, double disagreementThreshold)
        {
            int[,] superiorityMatrix = GetSuperiorityMatrix(consensusThreshold, disagreementThreshold);

            Console.WriteLine("Матрица превосходства:");

            Console.Write("{0,-15}", "");
            foreach (Alternative alternative in alternatives)
            {
                Console.Write("{0,-15}", alternative.Name);
            }
            Console.WriteLine();

            for (int i = 0; i < alternatives.Length; i++)
            {
                Console.Write("{0,-15}", alternatives[i].Name);

                for (int j = 0; j < alternatives.Length; j++)
                {
                    if (superiorityMatrix[i, j] == 1)
                    {
                        Console.Write("{0,-15:0.###}", "Истина");
                    }
                    else if (superiorityMatrix[i, j] == 0)
                    {
                        Console.Write("{0,-15:0.###}", "Ложь");
                    }
                    else
                    {
                        Console.Write("{0,-15}", "-");
                    }
                }

                Console.WriteLine();
            }
        }

        public void ShowDecision(double consensusThreshold, double disagreementThreshold)
        {
            List<Alternative> decision = GetDecision(consensusThreshold, disagreementThreshold);

            if (decision.Count == 1)
                Console.Write("Предпочтительная альернатива: {0}", decision[0].Name);
            else
            {
                Console.Write("Предпочтительные альернативы: {0}", decision[0].Name);
                for (int i = 0; i < decision.Count; i++)
                    if (i > 0)
                        Console.Write(", {0}", decision[i].Name);
            }
            
            Console.WriteLine();
        }
    }
}
