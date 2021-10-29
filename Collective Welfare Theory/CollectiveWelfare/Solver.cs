using System.Collections.Generic;

namespace CollectiveWelfare
{
    public class Solver
    {
        private readonly List<int>[] distributionVectors;

        public Solver(params List<int>[] vectors)
        {
            distributionVectors = new List<int>[vectors.Length];
            for (int i = 0; i < vectors.Length; i++)
                distributionVectors[i] = CopyVector(vectors[i]);
        }

        public List<int>[] UtilityVectors
        {
            get
            {
                List<int>[] utilityVectors = new List<int>[distributionVectors.Length];
                for (int i = 0; i < utilityVectors.Length; i++)
                {
                    utilityVectors[i] = CopyVector(distributionVectors[i]);
                    for (int j = 1; j < utilityVectors[i].Count; j++)
                        utilityVectors[i][j] += utilityVectors[i][j - 1];
                }

                return utilityVectors;
            }
        }

        public List<Dominance> Dominances
        {
            get
            {
                List<Dominance> dominances = new List<Dominance>();
                List<int>[] utilityVectors = UtilityVectors;
                for (int i = 0; i < utilityVectors.Length; i++)
                {
                    for (int j = 0; j < utilityVectors.Length; j++)
                    {
                        if (i != j)
                        {
                            int count = 0;
                            for (int z = 0; z < utilityVectors[i].Count; z++)
                                if (utilityVectors[i][z] >= utilityVectors[j][z])
                                    count++;

                            bool more = false;
                            for (int z = 0; z < utilityVectors[i].Count; z++)
                                if (utilityVectors[i][z] > utilityVectors[j][z])
                                    more = true;

                            if ((count == utilityVectors[i].Count) && more)
                                dominances.Add(new Dominance(i, j));
                        }
                    }
                }

                return dominances;
            }
        }

        public List<int> Decisions
        {
            get
            {
                List<int> sum = new List<int>();
                for (int i = 0; i < distributionVectors.Length; i++)
                    sum.Add(0);

                List<Dominance> dominances = Dominances;
                for (int i = 0; i < dominances.Count; i++)
                    sum[dominances[i].Dominant]++;

                int max = 0;
                for (int i = 0; i < distributionVectors.Length; i++)
                    if (sum[i] > max)
                        max = sum[i];

                List<int> decisions = new List<int>();
                for (int i = 0; i < distributionVectors.Length; i++)
                    if (sum[i] == max)
                        decisions.Add(i + 1);

            return decisions;
            }
        }


        private static List<int> CopyVector(List<int> vector)
        {
            List<int> newVector = new List<int>();
            for (int i = 0; i < vector.Count; i++)
                newVector.Add(vector[i]);

            return newVector;
        }
    }
}
