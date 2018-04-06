using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GMac.GMacMath.Structures;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;
using SymbolicInterface.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GMac.GMacMath.Symbolic.Multivectors.Intermediate
{
    public sealed class GaSymMultivectorTempTree : IGaSymMultivectorTemp
    {
        public static GaSymMultivectorTempTree Create(int gaSpaceDim)
        {
            return new GaSymMultivectorTempTree(gaSpaceDim);
        }


        private readonly GMacBinaryTree<List<Expr>> _termsTree;


        public int VSpaceDimension
            => _termsTree.TreeDepth;

        public int GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();

        public Expr this[int id]
        {
            get
            {
                List<Expr> coef;
                _termsTree.TryGetLeafValue((ulong) id, out coef);

                return
                    ReferenceEquals(coef, null)
                        ? Expr.INT_ZERO
                        : coef.Simplify();
            }
        }

        public Expr this[int grade, int index]
        {
            get
            {
                var id = GMacMathUtils.BasisBladeId(grade, index);

                List<Expr> coef;
                _termsTree.TryGetLeafValue((ulong)id, out coef);

                return
                    ReferenceEquals(coef, null)
                        ? Expr.INT_ZERO
                        : coef.Simplify();
            }
        }

        public IEnumerable<int> BasisBladeIds 
            => _termsTree.LeafNodeIDs.Select(k => (int) k);

        public IEnumerable<int> NonZeroBasisBladeIds =>
            _termsTree
            .LeafValuePairs
            .Where(pair => !pair.Value.Simplify().IsNullOrZero())
            .Select(pair => (int)pair.Key);

        public IEnumerable<MathematicaScalar> BasisBladeScalars
            => _termsTree
                .LeafValuePairs
                .Select(p => p.Value.Simplify().ToMathematicaScalar());

        public IEnumerable<Expr> BasisBladeExprScalars
            => _termsTree
                .LeafValuePairs
                .Select(p => p.Value.Simplify());

        public IEnumerable<MathematicaScalar> NonZeroBasisBladeScalars
            => NonZeroExprTerms
                .Where(p => !p.Value.IsNullOrZero())
                .Select(p => p.Value.ToMathematicaScalar());

        public IEnumerable<Expr> NonZeroBasisBladeExprScalars
            => NonZeroExprTerms
                .Where(p => !p.Value.IsNullOrZero())
                .Select(p => p.Value);

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> Terms
            => _termsTree
                .LeafValuePairs
                .Select(p => new KeyValuePair<int, MathematicaScalar>(
                    (int)p.Key,
                    p.Value.Simplify().ToMathematicaScalar()
                ));

        public IEnumerable<KeyValuePair<int, Expr>> ExprTerms
            => _termsTree
                .LeafValuePairs
                .Select(p => new KeyValuePair<int, Expr>(
                    (int)p.Key,
                    p.Value.Simplify()
                ));

        public IEnumerable<KeyValuePair<int, MathematicaScalar>> NonZeroTerms
            => _termsTree
                .LeafValuePairs
                .Select(p => new KeyValuePair<int, MathematicaScalar>(
                    (int)p.Key, 
                    p.Value.Simplify().ToMathematicaScalar())
                )
                .Where(p => !p.Value.IsZero());

        public IEnumerable<KeyValuePair<int, Expr>> NonZeroExprTerms
            => _termsTree
                .LeafValuePairs
                .Select(p => new KeyValuePair<int, Expr>(
                    (int)p.Key, 
                    p.Value.Simplify())
                )
                .Where(p => !p.Value.IsZero());

        public bool ContainsBasisBlade(int id)
        {
            return _termsTree.ContainsLeafNodeId((ulong)id);
        }

        public bool IsTemp 
            => true;

        public int TermsCount 
            => _termsTree.LeafNodes.Count();

        public bool IsTerm()
        {
            return _termsTree.LeafNodes.Count() <= 1;
        }

        public bool IsScalar()
        {
            return _termsTree.HasNoChildNodes ||
                   _termsTree.LeafValuePairs.All(p => p.Key == 0 || p.Value.IsNullOrZero());
        }

        public bool IsZero()
        {
            return _termsTree.HasNoChildNodes ||
                   _termsTree.LeafValuePairs.All(p => p.Value.IsNullOrZero());
        }

        public bool IsEqualZero()
        {
            return _termsTree.HasNoChildNodes ||
                   _termsTree.LeafValuePairs.All(p => p.Value.IsNullOrEqualZero());
        }


        private GaSymMultivectorTempTree(int gaSpaceDim)
        {
            _termsTree = new GMacBinaryTree<List<Expr>>(gaSpaceDim.ToVSpaceDimension());
        }


        public IGaSymMultivectorTemp AddFactor(int id, Expr coef)
        {
            var index = (ulong)id;

            var node = _termsTree.GetLeafNode(index);
            if (ReferenceEquals(node, null))
            {
                _termsTree.SetLeafValue(
                    index,
                    new List<Expr>(1) { coef }
                );

                return this;
            }

            if (ReferenceEquals(node.Value, null))
                node.Value = new List<Expr>(1) {coef};
            else
                node.Value.Add(coef);

            return this;
        }

        public IGaSymMultivectorTemp AddFactor(int id, bool isNegative, Expr coef)
        {
            coef = isNegative ? Mfs.Minus[coef] : coef;

            var index = (ulong)id;

            var node = _termsTree.GetLeafNode(index);
            if (ReferenceEquals(node, null))
            {
                _termsTree.SetLeafValue(
                    index,
                    new List<Expr>(1) { coef }
                );

                return this;
            }

            if (ReferenceEquals(node.Value, null))
                node.Value = new List<Expr>(1) { coef };
            else
                node.Value.Add(coef);

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(int id, Expr coef)
        {
            var index = (ulong)id;

            var node = _termsTree.GetLeafNode(index);
            if (ReferenceEquals(node, null))
            {
                _termsTree.SetLeafValue(
                    index,
                    new List<Expr>(1) { coef }
                );

                return this;
            }

            if (ReferenceEquals(node.Value, null))
                node.Value = new List<Expr>(1) { coef };
            else
            {
                node.Value.Clear();
                node.Value.Add(coef);
            }

            return this;
        }

        public IGaSymMultivectorTemp SetTermCoef(int id, bool isNegative, Expr coef)
        {
            coef = isNegative ? Mfs.Minus[coef] : coef;

            var index = (ulong)id;

            var node = _termsTree.GetLeafNode(index);
            if (ReferenceEquals(node, null))
            {
                _termsTree.SetLeafValue(
                    index,
                    new List<Expr>(1) { coef }
                );

                return this;
            }

            if (ReferenceEquals(node.Value, null))
                node.Value = new List<Expr>(1) { coef };
            else
            {
                node.Value.Clear();
                node.Value.Add(coef);
            }

            return this;
        }

        public void Simplify()
        {
            var nonZeroTerms = NonZeroExprTerms.ToArray();

            _termsTree.RemoveChildNodes();

            foreach (var pair in nonZeroTerms)
                _termsTree.SetLeafValue(
                    (ulong)pair.Key,
                    new List<Expr>(1) { pair.Value }
                );
        }

        public MathematicaScalar[] TermsToArray()
        {
            var termsArray = new MathematicaScalar[GaSpaceDimension];

            foreach (var term in NonZeroTerms)
                termsArray[term.Key] = term.Value;

            return termsArray;
        }

        public Expr[] TermsToExprArray()
        {
            var termsArray = new Expr[GaSpaceDimension];

            foreach (var term in NonZeroExprTerms)
                termsArray[term.Key] = term.Value;

            return termsArray;
        }

        public GaSymMultivector ToMultivector()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            foreach (var term in NonZeroTerms)
                mv.SetTermCoef(term.Key, term.Value);

            return mv;
        }

        public GaSymMultivector GetVectorPart()
        {
            var mv = GaSymMultivector.CreateZero(GaSpaceDimension);

            foreach (var id in GMacMathUtils.BasisVectorIDs(VSpaceDimension))
            {
                var coef = this[id];
                if (!coef.IsNullOrZero())
                    mv.SetTermCoef(id, coef);
            }

            return mv;
        }

        public IEnumerator<KeyValuePair<int, Expr>> GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return NonZeroExprTerms.GetEnumerator();
        }
    }
}