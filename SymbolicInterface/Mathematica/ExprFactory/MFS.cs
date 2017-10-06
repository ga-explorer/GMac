using System.Text;
using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica.ExprFactory
{
    /// <summary>
    /// This class is used to construct mathematica expressions by applying functions to 
    /// sub expressions in an easy way
    /// </summary>
    public sealed class Mfs
    {
        public static readonly Mfs NumericQ = new Mfs("NumericQ");

        public static readonly Mfs PossibleZeroQ = new Mfs("PossibleZeroQ");
        public static readonly Mfs Equal = new Mfs("Equal");
        public static readonly Mfs Unequal = new Mfs("Unequal");
        public static readonly Mfs Greater = new Mfs("Greater");
        public static readonly Mfs GreaterEqual = new Mfs("GreaterEqual");
        public static readonly Mfs Less = new Mfs("Less");
        public static readonly Mfs LessEqual = new Mfs("LessEqual");
        public static readonly Mfs Not = new Mfs("Not");
        public static readonly Mfs And = new Mfs("And");
        public static readonly Mfs Or = new Mfs("Or");
        public static readonly Mfs Nand = new Mfs("Nand");
        public static readonly Mfs Nor = new Mfs("Nor");
        
        public static readonly Mfs Plus = new Mfs("Plus");
        public static readonly Mfs Minus = new Mfs("Minus");
        public static readonly Mfs Subtract = new Mfs("Subtract");
        public static readonly Mfs Times = new Mfs("Times");
        public static readonly Mfs Divide = new Mfs("Divide");
        public static readonly Mfs Power = new Mfs("Power");
        public static readonly Mfs Dot = new Mfs("Dot");

        public static readonly Mfs Rational = new Mfs("Rational");
        public static readonly Mfs Abs = new Mfs("Abs");
        public static readonly Mfs Sqrt = new Mfs("Sqrt");
        public static readonly Mfs Sin = new Mfs("Sin");
        public static readonly Mfs Cos = new Mfs("Cos");
        public static readonly Mfs Tan = new Mfs("Tan");
        public static readonly Mfs Sinh = new Mfs("Sinh");
        public static readonly Mfs Cosh = new Mfs("Cosh");
        public static readonly Mfs Tanh = new Mfs("Tanh");
        public static readonly Mfs Exp = new Mfs("Exp");
        public static readonly Mfs Log = new Mfs("Log");
        public static readonly Mfs Log2 = new Mfs("Log2");
        public static readonly Mfs Log10 = new Mfs("Log10");
        public static readonly Mfs D = new Mfs("D");
        public static readonly Mfs N = new Mfs("N");

        public static readonly Mfs List = new Mfs("List");
        public static readonly Mfs IdentityMatrix = new Mfs("IdentityMatrix");
        public static readonly Mfs ConstantArray = new Mfs("ConstantArray");
        public static readonly Mfs DiagonalMatrix = new Mfs("DiagonalMatrix");
        public static readonly Mfs SparseArray = new Mfs("SparseArray");
        public static readonly Mfs Normal = new Mfs("Normal");
        public static readonly Mfs Norm = new Mfs("Norm");
        public static readonly Mfs VectorQ = new Mfs("VectorQ");
        public static readonly Mfs MatrixQ = new Mfs("MatrixQ");
        public static readonly Mfs Dimensions = new Mfs("Dimensions");
        public static readonly Mfs Det = new Mfs("Det");
        public static readonly Mfs Part = new Mfs("Part");
        public static readonly Mfs Diagonal = new Mfs("Diagonal");
        public static readonly Mfs Transpose = new Mfs("Transpose");
        public static readonly Mfs Inverse = new Mfs("Inverse");
        public static readonly Mfs SymmetricMatrixQ = new Mfs("SymmetricMatrixQ");
        public static readonly Mfs Eigenvalues = new Mfs("Eigenvalues");
        public static readonly Mfs Eigenvectors = new Mfs("Eigenvectors");
        public static readonly Mfs Eigensystem = new Mfs("Eigensystem");
        public static readonly Mfs Orthogonalize = new Mfs("Orthogonalize");

        public static readonly Mfs Apply = new Mfs("Apply");
        public static readonly Mfs Flatten = new Mfs("Flatten");
        public static readonly Mfs Rule = new Mfs("Rule");
        public static readonly Mfs RuleDelayed = new Mfs("RuleDelayed");
        public static readonly Mfs Element = new Mfs("Element");
        public static readonly Mfs Alternatives = new Mfs("Alternatives");
        public static readonly Mfs Simplify = new Mfs("Simplify");
        public static readonly Mfs FullSimplify = new Mfs("FullSimplify");
        public static readonly Mfs ReplaceAll = new Mfs("ReplaceAll");

        public static readonly Mfs CForm = new Mfs("CForm");
        public static readonly Mfs EToString = new Mfs("ToString");


        public Expr MathExpr { get; }

        public string FunctionName => MathExpr.ToString();


        public Mfs(string functionName)
        {
            MathExpr = new Expr(ExpressionType.Symbol, functionName);
        }


        public Expr this[params object[] parameters] => new Expr(MathExpr, parameters);

        public string this[string parametersText]
        {
            get
            {
                var stringBuilder = new StringBuilder(FunctionName.Length + parametersText.Length + 2);

                return 
                    stringBuilder
                    .Append(FunctionName)
                    .Append("[")
                    .Append(parametersText)
                    .Append("]")
                    .ToString();
            }
        }

        public override string ToString()
        {
            return MathExpr.ToString();
        }
    }
}
