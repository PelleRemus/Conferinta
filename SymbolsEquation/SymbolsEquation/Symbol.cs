namespace SymbolsEquation
{
    public enum Symbols { Null, x, y, z }

    public class Symbol
    {
        public Symbols symbols;
        public int real, realPower, symbolsPower;

        public Symbol(int real, int realPower, Symbols symbols, int symbolsPower)
        {
            this.real = real;
            this.symbols = symbols;
            this.realPower = realPower;
            this.symbolsPower = symbolsPower;
        }
        public Symbol(Symbol symbol) : this(symbol.real, symbol.realPower, symbol.symbols, symbol.symbolsPower) { }
        public Symbol(int real, int realPower) : this(real, realPower, Symbols.Null, 0) { }
        public Symbol(int real) : this(real, 1) { }
        public Symbol(Symbols symbols, int symbolsPower) : this(1, 1, symbols, symbolsPower) { }
        public Symbol(Symbols symbols) : this(symbols, 1) { }

        public void Simplify()
        {
            if (real == 1 || realPower == 0)
            {
                real = 1;
                realPower = 1;
            }
            if (real == 0)
            {
                realPower = 1;
                symbols = Symbols.Null;
            }
            if (symbols == Symbols.Null || symbolsPower == 0)
            {
                symbols = Symbols.Null;
                symbolsPower = 0;
            }
        }

        public override string ToString()
        {
            Simplify();
            string s = "";
            if (symbols == Symbols.Null || real != 1)
            {
                s += real;
                if (realPower != 1)
                    s += "^" + realPower;
            }
            if (symbolsPower != 0)
            {
                s += symbols;
                if (symbolsPower != 1)
                    s += "^" + symbolsPower;
            }
            return s;
        }
    }
}
