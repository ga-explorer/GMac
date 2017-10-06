using SymbolicInterface.Mathematica;

namespace GMac.GMacCompiler.Symbolic
{
    public static class SymbolicUtils
    {
        private static MathematicaInterface _cas;

        public static MathematicaInterface Cas
        {
            get
            {
                if (ReferenceEquals(_cas, null))
                    _cas = MathematicaInterface.Create();

                return _cas;
            }
        }

        public static MathematicaEvaluator Evaluator => Cas.Evaluator;

        public static MathematicaConstants Constants => Cas.Constants;
    }
}
