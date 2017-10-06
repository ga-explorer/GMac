using System.Collections.Generic;
using SymbolicInterface.Mathematica.ExprFactory;
using TextComposerLib.Code.SyntaxTree.Expressions;
using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica.Expression
{
    public class MathematicaExpression : ISymbolicObject
    {
        private static int _exprIdCounter;

        private static int GetNewExprId()
        {
            return _exprIdCounter++;
        }


        public static MathematicaExpression Create(MathematicaInterface parentCas, Expr mathExpr)
        {
            return new MathematicaExpression(parentCas, mathExpr);
        }

        public static MathematicaExpression Create(MathematicaInterface parentCas, string mathExprText)
        {
            var mathExpr = parentCas.Connection.EvaluateToExpr(mathExprText);

            return new MathematicaExpression(parentCas, mathExpr);
        }


        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;

        public int ExpressionId { get; private set; }

        public Expr MathExpr { get; private set; }

        public object Tag;


        public string ExpressionText => MathExpr.ToString();


        protected MathematicaExpression(MathematicaInterface parentCas, Expr mathExpr)
        {
            CasInterface = parentCas;
            ExpressionId = GetNewExprId();
            MathExpr = mathExpr;
        }


        public Dictionary<string, MathematicaExpression> GetAllSubexpressions()
        {
            var subList = new Dictionary<string, MathematicaExpression>();

            var stk = new Stack<Expr>();

            var rootExpr = MathExpr;

            stk.Push(rootExpr);
            subList.Add(rootExpr.ToString(), Create(CasInterface, rootExpr));

            while (stk.Count > 0)
            {
                var curExpr = stk.Pop();

                foreach (var childExpr in curExpr.Args)
                {
                    var childExprText = childExpr.ToString();

                    if (subList.ContainsKey(childExprText))
                        continue;

                    stk.Push(childExpr);
                    subList.Add(childExprText, Create(CasInterface, childExpr));
                }
            }

            return subList;
        }

        public SteExpression ToTextExpressionTree()
        {
            return MathExpr.ToTextExpressionTree();
        }


        public bool IsSymbol()
        {
            return MathExpr.SymbolQ();
        }

        /// <summary>
        /// Replace the internal Mathematica expression by a simpler one using the 
        /// Simplify[] Mathematica function
        /// </summary>
        public void Simplify()
        {
            if (MathExpr.AtomQ()) return;

            MathExpr = CasInterface[Mfs.Simplify[MathExpr]];
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equal(obj as MathematicaExpression);
        }

        public bool Equal(MathematicaExpression expr2)
        {
            if (ReferenceEquals(expr2, null))
                return false;

            return (ToString() == expr2.ToString());
        }

        public sealed override int GetHashCode()
        {
            return ExpressionText.GetHashCode();
        }

        public override string ToString()
        {
            return ExpressionText;
        }
    }
}
