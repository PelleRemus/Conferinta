using System;
using System.Collections.Generic;

namespace SymbolsEquation
{
    public class Equation
    {
        public List<Number> values = new List<Number>();

        public void Simplify()
        {
            foreach (Number number in values)
            {
                number.Simplify();
                if (number.values[0].real == 0)
                    values.Remove(number);
            }
            for (int i = 0; i < values.Count; i++)
                for (int j = i + 1; j < values.Count; j++)
                    if (values[i].grade == values[j].grade)
                    {
                        CalculateReal(values[i], values[j]);
                        values.Remove(values[j]);
                    }
        }

        public void CalculateReal(Number n1, Number n2)
        {
            int real = (int)Math.Pow(n1.values[0].real, n1.values[0].realPower) + (int)Math.Pow(n2.values[0].real, n2.values[0].realPower);
            int realPower = 1;

            for (int i = n1.values[0].realPower + n2.values[0].realPower; i > 1; i--)
                if (Math.Pow(real, 1f / i) * 100 % 100 == 0)
                {
                    real = (int)Math.Pow(real, 1f / i);
                    realPower = i;
                    break;
                }

            n1.values[0].real = real;
            n1.values[0].realPower = realPower;
        }

        public override string ToString()
        {
            Simplify();
            string s = "";
            for (int i = 0; i < values.Count - 1; i++)
                s += values[i] + " + ";
            s += values[values.Count - 1];
            return s;
        }
    }
}
