using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLib.Applications.GAPoT
{
    public sealed class GaPoTSymFrame : IReadOnlyList<GaPoTSymVector>
    {
        public static GaPoTSymFrame CreateEmptyFrame()
        {
            return new GaPoTSymFrame();
        }
        
        public static GaPoTSymFrame Create(params GaPoTSymVector[] vectorsList)
        {
            return new GaPoTSymFrame(vectorsList);
        }
        
        public static GaPoTSymFrame Create(IEnumerable<GaPoTSymVector> vectorsList)
        {
            return new GaPoTSymFrame(vectorsList);
        }
        
        public static GaPoTSymFrame CreateBasisFrame(int vectorsCount)
        {
            var frame = new GaPoTSymFrame();

            for (var i = 0; i < vectorsCount; i++)
            {
                var vector = new GaPoTSymVector().AddTerm(i + 1, Expr.INT_ONE);
                
                frame.AppendVector(vector);
            }
            
            return frame;
        }
        
        public static GaPoTSymFrame CreateShiftedBasisFrame(int vectorsCount, int offset)
        {
            var frame = new GaPoTSymFrame();

            for (var i = 0; i < vectorsCount; i++)
            {
                var index = (i + offset) % vectorsCount;

                var vector = new GaPoTSymVector().AddTerm(index + 1, Expr.INT_ONE);
                
                frame.AppendVector(vector);
            }
            
            return frame;
        }

        public static GaPoTSymFrame CreateFromRows(Expr[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var frame = new GaPoTSymFrame();

            for (var i = 0; i < rowsCount; i++)
            {
                var vector = new GaPoTSymVector();

                for (var j = 0; j < colsCount; j++)
                {
                    var value = matrix[i, j];

                    if (value.IsNullOrZero())
                        continue;

                    vector.AddTerm(j + 1, value);
                }

                frame.AppendVector(vector);
            }

            return frame;
        }

        public static GaPoTSymFrame CreateFromColumns(Expr[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = matrix.GetLength(1);

            var frame = new GaPoTSymFrame();

            for (var j = 0; j < colsCount; j++)
            {
                var vector = new GaPoTSymVector();

                for (var i = 0; i < rowsCount; i++)
                {
                    var value = matrix[i, j];

                    if (value.IsNullOrZero())
                        continue;

                    vector.AddTerm(i + 1, value);
                }

                frame.AppendVector(vector);
            }

            return frame;
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static GaPoTSymFrame CreateClarkeFrameOdd(int vectorsCount)
        {
            var frameVectorsArray = new GaPoTSymVector[vectorsCount];
            
            var m = vectorsCount;
            var s = $"Sqrt[2 / {m}]";

            //m is odd, fill all columns except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frameVectorsArray[vectorIndex1] = new GaPoTSymVector();
                frameVectorsArray[vectorIndex2] = new GaPoTSymVector();
                
                frameVectorsArray[vectorIndex1].SetTerm(1, $"{s}".SimplifyToExpr());
                
                for (var i = 1; i < m; i++)
                {
                    var angle = $"2 * Pi * {k + 1} * {i} / {m}";
                    var cosAngle = $"{s} * Cos[{angle}]".SimplifyToExpr();
                    var sinAngle = $"{s} * Sin[{angle}]".SimplifyToExpr();
                    
                    frameVectorsArray[vectorIndex1].SetTerm(i + 1, cosAngle);
                    frameVectorsArray[vectorIndex2].SetTerm(i + 1, sinAngle);
                }
            }

            //Fill the last column
            frameVectorsArray[m - 1] = new GaPoTSymVector();

            var v = $"1 / Sqrt[{m}]".SimplifyToExpr();
            for (var i = 0; i < m; i++)
            {
                frameVectorsArray[m - 1].SetTerm(i + 1, v);
            }
            
            return new GaPoTSymFrame(frameVectorsArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        private static GaPoTSymFrame CreateClarkeFrameEven(int vectorsCount)
        {
            var frameVectorsArray = new GaPoTSymVector[vectorsCount];
            
            var m = vectorsCount;
            var s = $"Sqrt[2 / {m}]";

            //m is even, fill all columns except the last two
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frameVectorsArray[vectorIndex1] = new GaPoTSymVector();
                frameVectorsArray[vectorIndex2] = new GaPoTSymVector();
                
                frameVectorsArray[vectorIndex1].SetTerm(1, $"{s}".SimplifyToExpr());
                
                for (var i = 1; i < m; i++)
                {
                    var angle = $"2 * Pi * ({k + 1}) * ({i}) / ({m})";
                    var cosAngle = $"{s} * Cos[{angle}]".SimplifyToExpr();
                    var sinAngle = $"{s} * Sin[{angle}]".SimplifyToExpr();
                    
                    frameVectorsArray[vectorIndex1].SetTerm(i + 1, cosAngle);
                    frameVectorsArray[vectorIndex2].SetTerm(i + 1, sinAngle);
                }
            }

            //Fill the last column
            frameVectorsArray[m - 2] = new GaPoTSymVector();
            frameVectorsArray[m - 1] = new GaPoTSymVector();

            var v0 = $"1 / Sqrt[{m}]".SimplifyToExpr();
            var v1 = $"-1 / Sqrt[{m}]".SimplifyToExpr();

            for (var i = 0; i < m; i++)
            {
                frameVectorsArray[m - 2].SetTerm(i + 1, i % 2 == 0 ? v0 : v1);
                frameVectorsArray[m - 1].SetTerm(i + 1, v0);
            }
            
            return new GaPoTSymFrame(frameVectorsArray);
        }

        /// <summary>
        /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GaPoTSymFrame CreateClarkeFrame(int vectorsCount)
        {
            return vectorsCount % 2 == 0 
                ? CreateClarkeFrameEven(vectorsCount) 
                : CreateClarkeFrameOdd(vectorsCount);
        }
        
        public static GaPoTSymFrame CreateKirchhoffFrame(int vectorsCount)
        {
            return CreateKirchhoffFrame(vectorsCount, 0);
        }

        public static GaPoTSymFrame CreateKirchhoffFrame(int vectorsCount, int refVectorIndex)
        {
            var uFrame = CreateBasisFrame(vectorsCount);
            var eFrame = CreateEmptyFrame();

            var refVector = uFrame[refVectorIndex];
            for (var i = 0; i < vectorsCount; i++)
            {
                if (i == refVectorIndex) 
                    continue;

                eFrame.AppendVector(uFrame[i] - refVector);
            }

            return eFrame;
        }

        public static GaPoTSymFrame CreateGramSchmidtFrame(int vectorsCount)
        {
            return CreateGramSchmidtFrame(vectorsCount, out _);
        }

        public static GaPoTSymFrame CreateGramSchmidtFrame(int vectorsCount, out GaPoTSymFrame kirchhoffFrame)
        {
            return CreateGramSchmidtFrame(vectorsCount, 0, out kirchhoffFrame);
        }

        public static GaPoTSymFrame CreateGramSchmidtFrame(int vectorsCount, int refVectorIndex)
        {
            return CreateGramSchmidtFrame(vectorsCount, refVectorIndex, out _);
        }

        public static GaPoTSymFrame CreateGramSchmidtFrame(int vectorsCount, int refVectorIndex, out GaPoTSymFrame kirchhoffFrame)
        {
            kirchhoffFrame = CreateKirchhoffFrame(vectorsCount, refVectorIndex);
            
            var cFrame = kirchhoffFrame.GetOrthogonalFrame(true);

            var uPseudoScalar = new GaPoTSymMultivector()
                .SetTerm(
                    (1UL << vectorsCount) - 1,
                    Expr.INT_ONE
                );

            //cFrame.PrependVector(
            //    GaPoTSymVector.CreateUnitAutoVector(vectorsCount)
            //);

            cFrame.AppendVector(
                -GaPoTSymUtils
                    .OuterProduct(cFrame)
                    .Gp(uPseudoScalar.CliffordConjugate())
                    .GetVectorPart()
                    .FullSimplifyScalars()
            );

            //cFrame.PrependVector(
            //    GaPoTSymUtils
            //        .OuterProduct(cFrame)
            //        .Gp(uPseudoScalar.Reverse())
            //        .GetVectorPart()
            //        .FullSimplifyScalars()
            //);

            var cPseudoScalar =
                cFrame.GetPseudoScalar().MapScalars(e => e.FullSimplify());

            //Console.WriteLine($"U frame pseudo-scalar: {uPseudoScalar}");
            //Console.WriteLine();

            //Console.WriteLine($"C frame pseudo-scalar: {cPseudoScalar}");
            //Console.WriteLine();

            //Debug.Assert(
            //    cFrame.IsOrthonormal()
            //);

            //Debug.Assert(
            //    CreateBasisFrame(vectorsCount).HasSameHandedness(cFrame)
            //);

            return cFrame;
        }

        /// <summary>
        /// See paper: Generalized space vector transformation, 2000
        /// </summary>
        /// <param name="vectorsCount"></param>
        /// <returns></returns>
        public static GaPoTSymFrame CreateHyperVectorsFrame(int vectorsCount)
        {
            var n = vectorsCount;
            var fbdMatrix = new Expr[n - 1, n];

            for (var i = 0; i < n - 1; i++)
            {
                var k1 = (n - i - 1).ToExpr();
                var k2 = (n - i).ToExpr();

                var c1 = Mfs.Sqrt[Mfs.Divide[k1, k2]].Evaluate();
                var c2 = Mfs.Divide[Expr.INT_MINUSONE, Mfs.Sqrt[Mfs.Times[k1, k2]]].Evaluate();

                fbdMatrix[i, i] = c1;

                for (var j = i + 1; j < n; j++)
                    fbdMatrix[i, j] = c2;
            }

            return CreateFromColumns(fbdMatrix);
        }
        

        private readonly List<GaPoTSymVector> _vectorsList
            = new List<GaPoTSymVector>();


        public int Count 
            => _vectorsList.Count;
        
        public GaPoTSymVector this[int index]
        {
            get => _vectorsList[index];
            set => _vectorsList[index] = value;
        }


        internal GaPoTSymFrame()
        {
        }

        internal GaPoTSymFrame(IEnumerable<GaPoTSymVector> vectorsList)
        {
            _vectorsList.AddRange(vectorsList);
        }
        
        
        public GaPoTSymFrame AppendVector(GaPoTSymVector vector)
        {
            _vectorsList.Add(vector);

            return this;
        }
        
        public GaPoTSymFrame PrependVector(GaPoTSymVector vector)
        {
            _vectorsList.Insert(0, vector);

            return this;
        }
        
        public GaPoTSymFrame InsertVector(int index, GaPoTSymVector vector)
        {
            _vectorsList.Insert(index, vector);

            return this;
        }

        public GaPoTSymFrame GetSubFrame(int startIndex, int count)
        {
            return new GaPoTSymFrame(
                _vectorsList.Skip(startIndex).Take(count)
            );
        }

        public GaPoTSymFrame GetOrthogonalFrame(bool makeUnitVectors)
        {
            return new GaPoTSymFrame(
                _vectorsList.ApplyGramSchmidt(makeUnitVectors)
            );
        }

        public GaPoTSymFrame GetNegativeFrame()
        {
            return new GaPoTSymFrame(
                _vectorsList.Select(v => -v)
            );
        }

        public GaPoTSymFrame GetSwappedPairsFrame()
        {
            var frame = new GaPoTSymFrame();

            //Swap each pair of two consecutive vectors in the frame
            for (var i = 0; i < _vectorsList.Count - 1; i += 2)
            {
                frame.AppendVector(_vectorsList[i + 1]);
                frame.AppendVector(_vectorsList[i]);
            }

            if (_vectorsList.Count % 2 == 1)
            {
                //To keep the same handedness we count the number of swaps and
                //negate the final vector if the number is odd

                var numberOfSwaps = (_vectorsList.Count - 1) / 2;

                var lastVector = numberOfSwaps % 2 == 0
                    ? _vectorsList[_vectorsList.Count - 1]
                    : -_vectorsList[_vectorsList.Count - 1];

                frame.AppendVector(lastVector);
            }

            return frame;
        }

        public GaPoTSymFrame ApplyRotor(GaPoTSymMultivector rotor)
        {
            var r1 = rotor;
            var r2 = rotor.Reverse();

            return new GaPoTSymFrame(
                _vectorsList.Select(v => r1.Gp(v.ToMultivector()).Gp(r2).GetVectorPart())
            );
        }

        public GaPoTSymMultivector GetPseudoScalar()
        {
            return GaPoTSymUtils.OuterProduct(_vectorsList);
        }

        public bool IsOrthonormal()
        {
            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                var dii = Mfs.Subtract[
                    v1.DotProduct(v1), 
                    Expr.INT_ONE
                ].FullSimplify();

                if (!dii.IsZero()) 
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = v1.DotProduct(_vectorsList[j]).FullSimplify();

                    if (!dij.IsZero())
                        return false;
                }
            }

            return true;
        }

        public bool HasSameHandedness(GaPoTSymFrame targetFrame)
        {
            var ps1 = GetPseudoScalar();
            var ps2 = targetFrame.GetPseudoScalar();

            var s = (ps1 - ps2).FullSimplifyScalars();

            return s.IsZero();
        }

        public Expr[,] GetMatrix()
        {
            return GetMatrix(Count);
        }

        public Expr[,] GetMatrix(int rowsCount)
        {
            var colsCount = Count;
            var itemsArray = new Expr[rowsCount, colsCount];
            
            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                itemsArray[i, j] = Expr.INT_ZERO;

            for (var j = 0; j < Count; j++)
            {
                var vector = _vectorsList[j];

                foreach (var term in vector.GetTerms())
                {
                    var i = term.TermId - 1;

                    itemsArray[i, j] = term.Value;
                }
            }
            
            return itemsArray;
        }


        public Expr[,] GetInnerProductsMatrix()
        {
            var ipm = new Expr[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                ipm[i, i] = v1.DotProduct(v1);

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.DotProduct(_vectorsList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public Expr[,] GetInnerAnglesMatrix()
        {
            var ipm = new Expr[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorsList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = v1.GetAngle(_vectorsList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        
        public IEnumerable<Expr> GetAnglesToFrame(GaPoTSymFrame targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
                yield return _vectorsList[i].GetAngle(targetFrame[i]);
        }

        public IEnumerable<GaPoTSymFrame> GetFramePermutations()
        {
            var indexPermutationsList = 
                GaPoTSymUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new GaPoTSymFrame();

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorsList[index]);

                yield return frame;
            }
        }

        public GaPoTSymFrame GetProjectionOnFrame(GaPoTSymFrame frame)
        {
            var ps = frame.GetPseudoScalar();

            return new GaPoTSymFrame(
                _vectorsList.Select(v => v.GetProjectionOnBlade(ps))
            );
        }

        public string ToLaTeXEquationsArray(string vectorName, string basisName)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\begin{eqnarray*}");

            for (var i = 0; i < _vectorsList.Count; i++)
            {
                var vector = _vectorsList[i];

                var termLaTeX = vector
                    .TermsToLaTeX()
                    .Replace(@"\sigma_", $"{basisName}_");

                var line = $@"{vectorName}_{i + 1} & = & {termLaTeX}";

                if (i < _vectorsList.Count - 1)
                    line += @"\\";

                textComposer.AppendLine(line);
            }

            textComposer.AppendLine(@"\end{eqnarray*}");

            return textComposer.ToString();
        }

        public IEnumerator<GaPoTSymVector> GetEnumerator()
        {
            return _vectorsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}