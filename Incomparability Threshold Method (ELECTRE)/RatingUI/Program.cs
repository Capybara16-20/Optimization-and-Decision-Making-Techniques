using System;
using Rating;

namespace RatingUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Criterion[] criteria = GetCriteria();
            Alternative[] alternatives = GetAlternatives(criteria);
            RatingProcessor rating = new RatingProcessor(criteria, alternatives);*/

            /*Criterion[] criteria = new Criterion[] { new Criterion("PHP", 10), new Criterion("HTML/CSS", 9), new Criterion("JavaScript", 10) };
            Alternative[] alternatives = new Alternative[] 
            { 
                new Alternative("Ivan", new int[] { 6, 3, 7 }), 
                new Alternative("Petya", new int[] { 1, 2, 3 }), 
                new Alternative("Konstantin", new int[] { 9, 3, 6 }),
                new Alternative("Dmitry", new int[] { 6, 9, 9 })
            };*/

            Criterion[] criteria = new Criterion[] { new Criterion("PHP", 10), new Criterion("HTML/CSS", 9), new Criterion("JavaScript", 10),
                new Criterion("Имидж", 7), new Criterion("Диплом", 6), new Criterion("Стаж", 6), new Criterion("ЗП", 6) };
            Alternative[] alternatives = new Alternative[]
            {
                new Alternative("P1", new int[] { 7, 7, 5, 10, 10, 6, 9, 7 }),
                new Alternative("P2", new int[] { 10, 9, 1, 9, 6, 10, 2, 4 }),
                new Alternative("P3", new int[] { 7, 7, 5, 10, 10, 6, 9, 7 }),
                new Alternative("P4", new int[] { 7, 7, 5, 10, 10, 6, 9, 7 })
            };

            RatingProcessor rating = new RatingProcessor(criteria, alternatives);

            Console.WriteLine();
            rating.ShowRatingTable();

            Console.WriteLine();
            rating.ShowConsensusMatrix();

            Console.WriteLine();
            rating.ShowDisagreementMatrix();

            Console.WriteLine();
            double consensusThreshold = GetThreshold("Порог согласия: C>=");
            double disagreementThreshold = GetThreshold("Порог несогласия: D<=");

            Console.WriteLine();
            rating.ShowSuperiorityMatrix(consensusThreshold, disagreementThreshold);

            Console.WriteLine();
            rating.ShowDecision(consensusThreshold, disagreementThreshold);
        }

        private static Alternative[] GetAlternatives(Criterion[] criteria)
        {
            int n = InputInteger("Количество альтернатив: ", "Должно быть целое число");

            Alternative[] alternatives = new Alternative[n];
            for (int i = 0; i < n; i++)
            {
                alternatives[i] = GetAlternative(criteria, i);
            }

            return alternatives;
        }

        private static Alternative GetAlternative(Criterion[] criteria, int index)
        {
            const double lowBound = 0;
            const double highBound = 11;

            string name = InputString(string.Format("Название {0}-й альтернативы: ", index + 1));

            Console.WriteLine("Оценки {0}-го критерия:", index + 1);
            
            int[] rating = new int[criteria.Length];
            for (int i = 0; i < criteria.Length; i++)
            {
                rating[i] = InputInteger(string.Format("{0}: ", criteria[i].Name), "Должно быть целое число", lowBound, highBound);
            }

            return new Alternative(name, rating);
        }

        private static Criterion[] GetCriteria()
        {
            int n = InputInteger("Количество критериев: ", "Должно быть целое число");

            Criterion[] criteria = new Criterion[n];
            for (int i = 0; i < n; i++)
            {
                criteria[i] = GetCriterion(i);
            }

            return criteria;
        }

        private static Criterion GetCriterion(int index)
        {
            const double lowBound = 0;
            const double highBound = 11;

            string name = InputString(string.Format("Название {0}-го критерия: ", index + 1));
            int value = InputInteger(string.Format("Вес {0}-го критерия: ", index + 1), "Должно быть целое число", lowBound, highBound);

            return new Criterion(name, value);
        }

        private static double GetThreshold(string inputMessage)
        {
            const string failureMessage = "Должно быть вещественное число";
            const double lowBound = 0;

            return InputDouble(inputMessage, failureMessage, lowBound);
        }

        private static string InputString(string inputMessage)
        {
            Console.Write(inputMessage);

            return Console.ReadLine();
        }

        private static int InputInteger(string inputMessage, string failureMessage, double lowBound = 0, double? highBound = null)
        {
            int x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (int.TryParse(Console.ReadLine(), out x))
                {
                    if ((x > lowBound) && ((highBound.HasValue && x < highBound) || !highBound.HasValue))
                    {
                        isCorrect = true;
                    }
                }

                if (!isCorrect)
                {
                    if (highBound.HasValue)
                    {
                        Console.WriteLine("{0} больше {1} и меньше {2}", failureMessage, lowBound, highBound);
                    }
                    else
                    {
                        Console.WriteLine("{0} больше {1}", failureMessage, lowBound);
                    }
                }
            }

            return x;
        }

        private static double InputDouble(string inputMessage, string failureMessage, double lowBound = 0, double? highBound = null)
        {
            double x = 0;
            bool isCorrect = false;
            while (!isCorrect)
            {
                Console.Write(inputMessage);

                if (double.TryParse(Console.ReadLine(), out x))
                {
                    if ((x > lowBound) && ((highBound.HasValue && x < highBound) || !highBound.HasValue))
                    {
                        isCorrect = true;
                    }
                }

                if (!isCorrect)
                {
                    if (highBound.HasValue)
                    {
                        Console.WriteLine("{0} больше {1} и меньше {2}", failureMessage, lowBound, highBound);
                    }
                    else
                    {
                        Console.WriteLine("{0} больше {1}", failureMessage, lowBound);
                    }
                }
            }

            return x;
        }
    }
}
