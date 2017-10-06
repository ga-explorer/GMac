using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace SymbolicInterface.Mathematica.Expression
{
    public class MathematicaCondition : MathematicaExpression
    {
        public static MathematicaCondition CreateIsDomainMemberTest(MathematicaExpression testedExpr, Expr domainExpr)
        {
            var expr = testedExpr.CasInterface[Mfs.Element[testedExpr.MathExpr, domainExpr]];

            return new MathematicaCondition(testedExpr.CasInterface, expr);
        }

        public static MathematicaCondition CreateIsDomainMemberTest(MathematicaInterface parentCas, Expr testedExpr, Expr domainExpr)
        {
            var expr = parentCas[Mfs.Element[testedExpr, domainExpr]];

            return new MathematicaCondition(parentCas, expr);
        }

        public static MathematicaCondition CreateIsDomainMemberTest(MathematicaExpression testedExpr, Expr domainExpr, Expr assumeExpr)
        {
            var expr = testedExpr.CasInterface[Mfs.FullSimplify[Mfs.Element[testedExpr.MathExpr, domainExpr], assumeExpr]];

            return new MathematicaCondition(testedExpr.CasInterface, expr);
        }

        public static MathematicaCondition CreateIsDomainMemberTest(MathematicaInterface parentCas, Expr testedExpr, Expr domainExpr, Expr assumeExpr)
        {
            var expr = parentCas[Mfs.FullSimplify[Mfs.Element[testedExpr, domainExpr], assumeExpr]];

            return new MathematicaCondition(parentCas, expr);
        }

        public static MathematicaCondition Create(MathematicaInterface parentCas, bool value)
        {
            return new MathematicaCondition(parentCas, value.ToExpr());
        }

        public new static MathematicaCondition Create(MathematicaInterface parentCas, Expr mathExpr)
        {
            return new MathematicaCondition(parentCas, mathExpr);
        }

        public new static MathematicaCondition Create(MathematicaInterface parentCas, string mathExprText)
        {
            return new MathematicaCondition(parentCas, parentCas[mathExprText]);
        }


        public static MathematicaCondition operator !(MathematicaCondition s1)
        {
            var e = s1.CasInterface[Mfs.Not[s1.MathExpr]];

            return new MathematicaCondition(s1.CasInterface, e);
        }

        public static MathematicaCondition operator &(MathematicaCondition s1, MathematicaCondition s2)
        {
            var e = s1.CasInterface[Mfs.And[s1.MathExpr, s2.MathExpr]];

            return new MathematicaCondition(s1.CasInterface, e);
        }

        public static MathematicaCondition operator |(MathematicaCondition s1, MathematicaCondition s2)
        {
            var e = s1.CasInterface[Mfs.Or[s1.MathExpr, s2.MathExpr]];

            return new MathematicaCondition(s1.CasInterface, e);
        }


        public static MathematicaCondition And(params MathematicaCondition[] args)
        {
            var parentCas = args[0].CasInterface;
            var exprArgs = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                exprArgs[i] = args[i].MathExpr;

            var e = parentCas[Mfs.And[exprArgs]];

            return new MathematicaCondition(parentCas, e);
        }

        public static MathematicaCondition Or(params MathematicaCondition[] args)
        {
            var parentCas = args[0].CasInterface;
            var exprArgs = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                exprArgs[i] = args[i].MathExpr;

            var e = parentCas[Mfs.Or[exprArgs]];

            return new MathematicaCondition(parentCas, e);
        }

        public static MathematicaCondition Nand(params MathematicaCondition[] args)
        {
            var parentCas = args[0].CasInterface;
            var exprArgs = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                exprArgs[i] = args[i].MathExpr;

            var e = parentCas[Mfs.Nand[exprArgs]];

            return new MathematicaCondition(parentCas, e);
        }

        public static MathematicaCondition Nor(params MathematicaCondition[] args)
        {
            var parentCas = args[0].CasInterface;
            var exprArgs = new object[args.Length];

            for (var i = 0; i < args.Length; i++)
                exprArgs[i] = args[i].MathExpr;

            var e = parentCas[Mfs.Nor[exprArgs]];

            return new MathematicaCondition(parentCas, e);
        }


        protected MathematicaCondition(MathematicaInterface parentCas, Expr mathExpr)
            : base(parentCas, mathExpr)
        {
        }


        public bool IsConstant()
        {
            if (MathExpr.SymbolQ() == false) return false;

            var exprText = MathExpr.ToString();

            return exprText == "True" || exprText == "False";
        }

        public bool IsConstant(bool value)
        {
            if (MathExpr.SymbolQ() == false) return false;

            return 
                value
                ? MathExpr.ToString() == "True"
                : MathExpr.ToString() == "False";
        }

        public bool IsConstantTrue()
        {
            return MathExpr.SymbolQ() && MathExpr.ToString() == "True";
        }

        public bool IsConstantFalse()
        {
            return MathExpr.SymbolQ() && MathExpr.ToString() == "False";
        }
    }
}
