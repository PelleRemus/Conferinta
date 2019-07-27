using System;

namespace SymbolsEquation
{
    class Program
    {
        static void Main(string[] args)
        {
            Equation e = new Equation();
            Number n1 = new Number();
            n1.values.Add(new Symbol(2, 3, Symbols.x, 1));
            n1.values.Add(new Symbol(1, 1, Symbols.y, 2));
            n1.values.Add(new Symbol(1, 1, Symbols.z, 1));
            e.values.Add(n1);

            Number n2 = new Number();
            n2.values.Add(new Symbol(2, 2, Symbols.x, 1));
            n2.values.Add(new Symbol(1, 1, Symbols.y, 2));
            n2.values.Add(new Symbol(2, 1, Symbols.z, 1));
            e.values.Add(n2);

            Console.WriteLine(e);
            Console.ReadKey();
        }
    }
}
