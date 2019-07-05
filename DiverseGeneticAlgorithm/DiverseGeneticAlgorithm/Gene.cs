using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseGeneticAlgorithm
{
    public class Gene
    {
        public int digits, decimals, n;
        public int[] number;

        public Gene(int digits, int decimals)
        {
            this.digits = digits;
            this.decimals = decimals;
            n = digits + decimals + 1;
            number = new int[n];
            initRandom();
        }

        public void initRandom()
        {
            for (int i = 0; i < n; i++)
                number[i] = Engine.random.Next(10);
        }

        public double ConvertToDouble()
        {
            double nr = 0;
            for (int i = 1; i <= digits; i++)
                nr = nr * 10 + number[i];
            for (int i = digits + 1; i < n; i++)
            {
                double s2 = number[i];
                s2 /= Math.Pow(10, i - digits);
                nr += s2;
            }
            if (number[0] < 5)
                nr = nr * -1;
            return nr;
        }

        public void Mutate()
        {
            int index = Engine.random.Next(n);
            int value;
            do
            {
                value = Engine.random.Next(10);
            } while (number[index] == value);

            number[index] = value;
        }
    }
}
