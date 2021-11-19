using System;

namespace SymbolsEquation
{
    class Program
    {
        static void Main(string[] args)
        {
            Number n1 = new Number();
            n1.values.Add(new Symbol(5, 4, Symbols.x, 2));
            n1.values.Add(new Symbol(Symbols.y, 3));
            n1.values.Add(new Symbol(Symbols.z, 2));

            Number n2 = new Number();
            n2.values.Add(new Symbol(3, 1, Symbols.x, 1));
            n2.values.Add(new Symbol(Symbols.y, 5));
            n2.values.Add(new Symbol(Symbols.z, 3));

            Equation e = new Equation();
            e.values.Add(n1);
            e.values.Add(n2);

            Console.WriteLine(e);
            Console.ReadKey();
        }
    }
}
