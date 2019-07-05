using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseGeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please insert the number that you want the function of adequation to use: ");
            Engine.requestedNumber = float.Parse(Console.ReadLine());
            Engine.initStats();
            Engine.initPopulation();
            do
            {
                Console.Clear();
                Engine.ViewPopulation();
                Engine.ViewParents();
                for (int i = 0; i < 100; i++)
                {
                    Engine.OrderPopulation();
                    Engine.SelectParents();
                    Engine.NextGeneration();
                }
                Console.ReadKey();
            } while (true);
        }
    }
}
