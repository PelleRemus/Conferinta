using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseGeneticAlgorithm
{
    public class Solution
    {
        public Gene[] genes;
        public long share;

        public Solution()
        {
            genes = new Gene[Engine.solutionLength];
            initRandom();
        }

        public Solution(List<int> result)
        {
            genes = new Gene[Engine.solutionLength];
            int count = Engine.solutionLength;
            while (count > 0)
            {
                int n = Engine.random.Next(3, result.Count / count);
                if (count == 1)
                    n = result.Count;

                int digits = Engine.random.Next(n / 3);
                genes[Engine.solutionLength - count] = new Gene(digits, n - digits - 1);

                for (int i = 0; i < n; i++)
                    genes[Engine.solutionLength - count].number[i] = result[i];
                result.RemoveRange(0, n);
                count--;
            }
        }

        public void initRandom()
        {
            for (int i = 0; i < genes.Length; i++)
            {
                int digits = Engine.random.Next(2, 5);
                int decimals = Engine.random.Next(3, 13);
                genes[i] = new Gene(digits, decimals);
            }
        }

        public double FAdeq()
        {
            double r = 0;
            for (int i = 0; i < genes.Length; i++)
            {
                r += genes[i].ConvertToDouble();
            }
            return r;
        }

        public void Mutate()
        {
            int i = Engine.random.Next(genes.Length);
            genes[i].Mutate();
        }

        public int[] getAllValues(int length)
        {
            int[] result = new int[length];
            int k = 0;
            for (int i = 0; i < genes.Length; i++)
            {
                for (int j = 0; j < genes[i].n; j++)
                {
                    result[k] = genes[i].number[j];
                    k++;
                }
            }
            return result;
        }

        public void View()
        {
            for (int i = 0; i < Engine.solutionLength; i++)
                Console.Write(genes[i].ConvertToDouble() + ", ");
            Console.WriteLine("FAdeq:\t" + FAdeq());
        }
    }
}
