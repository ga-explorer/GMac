using System.Collections.Generic;
using GMac.GMacCompiler.Symbolic;
using GMac.GMacCompiler.Symbolic.Frame;
using IronyGrammars.Semantic;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Type;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Semantic.AST
{
    public sealed class GMacValueMultivector : ILanguageValueComposite
    {
        internal GMacFrameMultivector ValueMultivectorType { get; }

        internal GaMultivector MultivectorCoefficients { get; }


        internal TypePrimitive CoefficientType => ((GMacAst)ValueMultivectorType.RootAst).ScalarType;

        internal GMacFrame MultivectorFrame => ValueMultivectorType.ParentFrame;

        internal GaFrame SymbolicFrame => ValueMultivectorType.ParentFrame.AssociatedSymbolicFrame;

        public IronyAst RootAst => ValueMultivectorType.RootAst;

        internal GMacAst GMacRootAst => (GMacAst)ValueMultivectorType.RootAst;


        /// <summary>
        /// A language value is always a simple expression
        /// </summary>
        public bool IsSimpleExpression => true;


        private GMacValueMultivector(GMacFrameMultivector mvType, GaMultivector mvCoefs)
        {
            ValueMultivectorType = mvType;

            MultivectorCoefficients = mvCoefs;
        }


        public ILanguageValue DuplicateValue(bool deepCopy)
        {
            return new GMacValueMultivector(ValueMultivectorType, GaMultivector.CreateCopy(MultivectorCoefficients));
        }

        public ILanguageType ExpressionType => ValueMultivectorType;


        internal GMacValueMultivector this[IEnumerable<int> idsList]
        {
            get
            {
                var mv = CreateZero(ValueMultivectorType);

                foreach (var id in idsList)
                    mv.MultivectorCoefficients[id] = MultivectorCoefficients[id];

                return mv;
            }
            set
            {
                foreach (var id in idsList)
                    MultivectorCoefficients[id] = value.MultivectorCoefficients[id];
            }
        }

        internal ValuePrimitive<MathematicaScalar> this[int id]
        {
            get
            {
                return ValuePrimitive<MathematicaScalar>.Create(CoefficientType, MultivectorCoefficients[id]);
            }
            set
            {
                MultivectorCoefficients[id] = value.Value;
            }
        }


        public override string ToString()
        {
            return RootAst.Describe(this);
        }


        internal static GMacValueMultivector Create(GMacFrameMultivector mvType, GaMultivector mvCoefs)
        {
            return new GMacValueMultivector(mvType, mvCoefs);
        }

        internal static GMacValueMultivector CreateZero(GMacFrameMultivector mvType)
        {
            var mvCoefs = GaMultivector.CreateZero(mvType.ParentFrame.GaSpaceDimension);

            return new GMacValueMultivector(mvType, mvCoefs);
        }

        internal static GMacValueMultivector CreateTerm(GMacFrameMultivector mvType, int id, MathematicaScalar coef)
        {
            var mvCoefs = GaMultivector.CreateTerm(mvType.ParentFrame.GaSpaceDimension, id, coef);

            return new GMacValueMultivector(mvType, mvCoefs);
        }

        internal static GMacValueMultivector CreateScalar(GMacFrameMultivector mvType, MathematicaScalar coef)
        {
            var mvCoefs = GaMultivector.CreateScalar(mvType.ParentFrame.GaSpaceDimension, coef);

            return new GMacValueMultivector(mvType, mvCoefs);
        }

        internal static GMacValueMultivector CreatePseudoScalar(GMacFrameMultivector mvType, MathematicaScalar coef)
        {
            var mvCoefs = GaMultivector.CreatePseudoScalar(mvType.ParentFrame.GaSpaceDimension, coef);

            return new GMacValueMultivector(mvType, mvCoefs);
        }

        internal static GMacValueMultivector CreateBasisBlade(GMacFrameMultivector mvType, int id)
        {
            var mvCoefs = GaMultivector.CreateBasisBlade(mvType.ParentFrame.GaSpaceDimension, id);

            return new GMacValueMultivector(mvType, mvCoefs);
        }

    }
}
