using System.Collections.Generic;

namespace AxiomaticMethodProcessor
{
    public class Processor
    {
        private List<double> importance;
        private List<List<double>> wins;

        public Processor(List<double> importance, List<List<double>> wins)
        {
            this.importance = Clone(importance);
            this.wins = Clone(wins);
        }

        public List<List<double>> WinFunctions
        {
            get
            {
                List<List<double>> winFunctions = new List<List<double>>();
                for (int i = 0; i < wins.Count; i++)
                    winFunctions.Add(new List<double>());

                for (int i = 0; i < wins[0].Count; i++)
                {
                    double min = int.MaxValue;
                    double max = int.MinValue;
                    for (int j = 0; j < wins.Count; j++)
                    {
                        if (wins[j][i] < min)
                            min = wins[j][i];
                        if (wins[j][i] > max)
                            max = wins[j][i];
                    }
                    
                    for (int j = 0; j < wins.Count; j++)
                        winFunctions[j].Add((wins[j][i] - min) / (max - min));
                }

                return winFunctions;
            }
        }

        public List<double> Rating
        {
            get
            {
                List<double> rating = new List<double>();
                List<List<double>> winFunctions = Clone(WinFunctions);
                for (int i = 0; i < winFunctions.Count; i++)
                {
                    rating.Add(0);
                    for (int j = 0; j < winFunctions[i].Count; j++)
                        rating[i] += winFunctions[i][j] * importance[j];
                }

                return rating;
            }
        }

        public List<List<int>> GetResult()
        {
            List<List<int>> result = new List<List<int>>();
            List<double> rating = Clone(Rating);
            List<int> indexes = new List<int>();
            for (int i = 0; i < rating.Count; i++)
                indexes.Add(i + 1);

            for (int i = 1; i < rating.Count; i++)
                for (int j = 1; j < rating.Count; j++)
                    if (i != j)
                        if (rating[i] > rating[i - 1])
                        {
                            double temp = rating[i];
                            rating[i] = rating[i - 1];
                            rating[i - 1] = temp;

                            temp = indexes[i];
                            indexes[i] = indexes[i - 1];
                            indexes[i - 1] = (int)temp;
                        }

            result.Add(new List<int>());
            result[0].Add(indexes[0]);
            for (int i = 1; i < rating.Count; i++)
            {

                bool added = false;
                for (int j = 0; j < result[i - 1].Count; j++)
                    if (result[i - 1][j] == rating[i])
                    {
                        result[i - 1].Add(indexes[i]);
                        added = true;
                    }

                if (!added)
                {
                    result.Add(new List<int>());
                    result[i].Add(indexes[i]);
                }
            }

            return result;
        }

        private static List<double> Clone(List<double> list)
        {
            List<double> newList = new List<double>();
            for (int i = 0; i < list.Count; i++)
                newList.Add(list[i]);

            return newList;
        }

        private static List<List<double>> Clone(List<List<double>> matrix)
        {
            List<List<double>> newMatrix = new List<List<double>>();
            for (int i = 0; i < matrix.Count; i++)
            {
                newMatrix.Add(new List<double>());
                for (int j = 0; j < matrix[i].Count; j++)
                    newMatrix[i].Add(matrix[i][j]);
            }

            return newMatrix;
        }
    }
}
