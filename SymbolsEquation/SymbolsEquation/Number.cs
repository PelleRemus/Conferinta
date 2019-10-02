using System;
using System.Collections.Generic;

namespace SymbolsEquation
{
    public class Number
    {
        public List<Symbol> values = new List<Symbol>();
        public string grade;

        public void Simplify()
        {
            List<Symbol> newValues = new List<Symbol>();
            newValues.Add(new Symbol(1, 1, Symbols.Null, 0));
            newValues.Add(new Symbol(1, 1, Symbols.x, 0));
            newValues.Add(new Symbol(1, 1, Symbols.y, 0));
            newValues.Add(new Symbol(1, 1, Symbols.z, 0));

            foreach (Symbol symbol in values)
            {
                symbol.Simplify();
                if(symbol.real==0)
                {
                    values = new List<Symbol>();
                    values.Add(symbol);
                    grade = "x0y0z0";
                    return;
                }
                newValues[(int)symbol.symbol].symbolPower += symbol.symbolPower;

                int[] realValues = CalculateReal(newValues[0].real, newValues[0].realPower, symbol.real, symbol.realPower);
                newValues[0].real = realValues[0];
                newValues[0].realPower = realValues[1];
            }
            grade = "x" + newValues[1].symbolPower + "y" + newValues[2].symbolPower + "z" + newValues[3].symbolPower;

            for (int i = newValues.Count - 1; i > 0; i--)
                if (newValues[i].symbolPower == 0 || newValues[i].symbol == Symbols.Null)
                    newValues.RemoveAt(i);
            if (newValues.Count != 1 && newValues[0].real == 1)
                newValues.RemoveAt(0);

            values = newValues;
        }

        public int[] CalculateReal(int r1, int rp1, int r2, int rp2)
        {
            if (r1 == 1)
                return new int[] { r2, rp2 };
            if (r2 == 1)
                return new int[] { r1, rp1 };
            if (r1 == r2)
                return new int[] { r1, rp1 + rp2 };
            if (rp1 == rp2)
                return new int[] { r1 * r2, rp1 };
            return new int[] { (int)Math.Pow(r1, rp1) * (int)Math.Pow(r2, rp2), 1 };
        }

        public override string ToString()
        {
            Simplify();
            string s = "(";
            foreach (Symbol symbol in values)
                s += symbol;
            s += ")";
            return s;
        }
    }
}
