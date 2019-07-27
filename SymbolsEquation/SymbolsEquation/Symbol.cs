namespace SymbolsEquation
{
    public enum Symbols { Null, x, y, z }

    public class Symbol
    {
        public Symbols symbol;
        public int real, realPower, symbolPower;

        public Symbol(int real, int realPower, Symbols symbol, int symbolPower)
        {
            this.real = real;
            this.symbol = symbol;
            this.realPower = realPower;
            this.symbolPower = symbolPower;
        }

        public void Simplify()
        {
            if(real==1 || realPower==0)
            {
                real = 1;
                realPower = 1;
            }
            if (real == 0)
            {
                realPower = 1;
                symbol = Symbols.Null;
            }
            if (symbol == Symbols.Null || symbolPower == 0)
            {
                symbol = Symbols.Null;
                symbolPower = 0;
            }
        }

        public override string ToString()
        {
            Simplify();
            string s = "";
            if(symbol==Symbols.Null || real!=1)
            {
                s += real;
                if (realPower != 1)
                    s += "^" + realPower;
            }
            if (symbolPower != 0)
            {
                s += symbol;
                if (symbolPower != 1)
                    s += "^" + symbolPower;
            }
            return s;
        }
    }
}
