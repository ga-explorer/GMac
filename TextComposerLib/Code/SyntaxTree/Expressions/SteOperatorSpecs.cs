namespace TextComposerLib.Code.SyntaxTree.Expressions
{
    public enum TccOperatorPosition { Infix = 0, Prefix = 1, Suffix = 2 }

    public enum TccOperatorAssociation { None = 0, Left = 1, Right = 2 }


    public class SteOperatorSpecs : ISteCompositeHeadSpecs
    {
        public int Precedence { get; private set; }

        public string Symbol { get; }

        public string TrimmedSymbol => Symbol.Trim();

        public TccOperatorPosition Position { get; private set; }

        public TccOperatorAssociation Association { get; private set; }

        public string HeadText => Symbol;


        public SteOperatorSpecs(string opSymbol, int opPrecedence, TccOperatorPosition opPosition, TccOperatorAssociation opAssociation)
        {
            Precedence = opPrecedence;
            Association = opAssociation;
            Symbol = opSymbol;
            Position = opPosition;
        }


        public override string ToString()
        {
            return Symbol;
        }
    }
}
