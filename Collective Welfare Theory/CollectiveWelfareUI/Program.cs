using System;
using System.Collections.Generic;
using CollectiveWelfare;

namespace CollectiveWelfareUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = InputNumber("Введите число векторов распределения: ", "Введите целое число больше 1.");
            List<int>[] vectors = new List<int>[n];

            int m = InputNumber("Введите размер векторов распределения: ", "Введите целое число больше 1.");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите значения {0}-го вектора:", i + 1);
                vectors[i] = new List<int>();
                for (int j = 0; j < m; j++)
                {
                    vectors[i].Add(default);
                    vectors[i][j] = InputInteger(string.Format("{0}: ", j + 1), "Введите целое число.");
                }
            }

            Solver solver = new Solver(vectors);

            Console.WriteLine();
            Console.WriteLine("Вектора полезностей:");
            List<int>[] utilityVectors = solver.UtilityVectors;
            for (int i = 0; i < utilityVectors.Length; i++)
            {
                Console.Write("{0}: ", i + 1);
                for (int j = 0; j < utilityVectors[i].Count; j++)
                {
                    Console.Write("{0} ", utilityVectors[i][j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Доминирование:");
            List<Dominance> dominances =  solver.Dominances;
            foreach(var dominance in dominances)
                Console.WriteLine("Вектор {0} доминирует над вектором {1}", dominance.Dominant + 1, dominance.Dominated + 1);
            if (dominances.Count == 0)
            {
                Console.WriteLine("Доминирующие векторы отсутствуют. Оптимальны все решения.");
                Environment.Exit(0);
            }

            Console.WriteLine();
            Console.WriteLine("Оптимальные решения:");
            List<int> decisions = solver.Decisions;
            foreach (var decision in decisions)
                Console.WriteLine("{0} ", decision);
        }

        private static int InputNumber(string inputMessage, string failureMessage)
        {
            int x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                x = InputInteger(inputMessage, failureMessage);
                if (x > 1)
                    isCorrect = true;
                
                if (!isCorrect)
                    Console.WriteLine(failureMessage);
            }

            return x;
        }

        private static int InputInteger(string inputMessage, string failureMessage)
        {
            int x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (int.TryParse(Console.ReadLine(), out x))
                        isCorrect = true;

                if (!isCorrect)
                    Console.WriteLine(failureMessage);
            }

            return x;
        }
    }
}
