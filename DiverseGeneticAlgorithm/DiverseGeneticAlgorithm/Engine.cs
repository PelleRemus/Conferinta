using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseGeneticAlgorithm
{
    public static class Engine
    {
        public static Random random = new Random();
        public static int populationLength, parentsLength, solutionLength;
        public static float requestedNumber;
        public static List<Solution> population = new List<Solution>();
        public static List<Solution> parents = new List<Solution>();

        public static void initStats()
        {
            populationLength = random.Next(80, 100);
            parentsLength = random.Next(2, 3);
            solutionLength = random.Next(8, 10);
        }

        public static void initPopulation()
        {
            for (int i = 0; i < populationLength; i++)
                population.Add(new Solution());
        }

        public static void OrderPopulation()
        {
            population.Sort(delegate (Solution a, Solution b)
            {
                return Math.Abs(b.FAdeq() - requestedNumber).CompareTo(Math.Abs(a.FAdeq() - requestedNumber));
            });
            for (int i = 0; i < populationLength; i++)
                population[i].share = (int)Math.Pow(i + 1, 3);
        }

        public static void SelectParents()
        {
            parents.Clear();
            for (int i = 0; i < parentsLength; i++)
            {
                parents.Add(population[MonteCarloAlgorythm()]);
                population.Remove(parents[i]);
            }
            parents.Sort(delegate (Solution a, Solution b)
            {
                return Math.Abs(a.FAdeq() - requestedNumber).CompareTo(Math.Abs(b.FAdeq() - requestedNumber));
            });
        }

        public static int MonteCarloAlgorythm()
        {
            float sum = 0;
            for (int i = 0; i < population.Count; i++)
                sum += population[i].share;

            float t = (float)random.NextDouble() * sum;
            int index = 0;

            while (t > 0)
            {
                t -= population[index].share;
                index++;
            }
            return index - 1;
        }

        public static void NextGeneration()
        {
            population.Clear();
            initStats();

            for (int i = 0; i < populationLength; i++)
            {
                int index1 = 0, index2 = 0;
                Solution aux;
                do
                {
                    index1 = random.Next(parents.Count);
                    index2 = random.Next(parents.Count);
                } while (index1 == index2);

                aux = CrossNCuts(parents[index1], parents[index2]);
                aux.Mutate();
                population.Add(aux);
            }
        }

        public static Solution CrossNCuts(Solution s1, Solution s2)
        {
            int N = 2; //number of cuts
            int n1 = 0, n2 = 0;
            for (int i = 0; i < s1.genes.Length; i++)
            {
                n1 += s1.genes[i].n;
                n2 += s2.genes[i].n;
            }
            int length1 = n1 / N, length2 = n2 / N;

            int[] solution1 = s1.getAllValues(n1), solution2 = s2.getAllValues(n2);
            List<int> result = new List<int>();
            int counter = 0;
            do
            {
                if (counter % 2 == 0)
                    for (int i = counter * length1; i < (counter + 1) * length1; i++)
                        result.Add(solution1[i]);
                else
                    for (int j = counter * length2; j < (counter + 1) * length2; j++)
                        result.Add(solution2[j]);
                counter++;
            } while (counter < N);

            return new Solution(result);
        }

        public static void ViewPopulation()
        {
            foreach (Solution solution in population)
                solution.View();
            Console.WriteLine();
        }

        public static void ViewParents()
        {
            foreach (Solution solution in parents)
                solution.View();
            Console.WriteLine();
        }
    }
}
