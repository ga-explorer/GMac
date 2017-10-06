using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacCompiler.Symbolic.GAOM;
using GMac.GMacUtils;
using SymbolicInterface.Mathematica;
using SymbolicInterface.Mathematica.Expression;

namespace GMac.GMacCompiler.Symbolic.Frame
{
    public abstract class GaFrame : ISymbolicObject, IGMacFrame
    {
        /// <summary>
        /// Inner-Product Matrix
        /// </summary>
        protected ISymbolicMatrix InnerProductMatrix;

        protected MathematicaScalar InnerUnitPseudoScalarCoef;

        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;

        public ISymbolicMatrix Ipm
        {
            get
            {
                if (ReferenceEquals(InnerProductMatrix, null))
                    ComputeIpm();

                return InnerProductMatrix;
            }
        }

        protected MathematicaScalar UnitPseudoScalarCoef
        {
            get
            {
                if (ReferenceEquals(InnerUnitPseudoScalarCoef, null))
                    ComputeUnitPseudoScalarCoef();

                return InnerUnitPseudoScalarCoef;
            }
        }

        public abstract int VSpaceDimension { get; }

        public int MaxBasisBladeId => FrameUtils.MaxBasisBladeId(VSpaceDimension);

        public int GaSpaceDimension => FrameUtils.GaSpaceDimension(VSpaceDimension);

        public int GradesCount => VSpaceDimension + 1;

        public bool IsNonOrthogonal => !IsOrthogonal;


        public abstract GaFrame BaseOrthogonalFrame { get; }

        public abstract bool IsEuclidean { get; }

        public abstract bool IsOrthogonal { get; }

        public abstract bool IsOrthonormal { get; }


        protected GaFrame()
        {
            CasInterface = SymbolicUtils.Cas;
        }


        protected abstract void ComputeIpm();

        protected abstract void ComputeUnitPseudoScalarCoef();
        //{
        //    if (GAUtils.ID_To_Reverse(MaxID))
        //        _UnitPseudoScalarCoef = CASConstants.MinusOne / this.BasisBladeSignature(MaxID)[0];
        //    else
        //        _UnitPseudoScalarCoef = CASConstants.One / this.BasisBladeSignature(MaxID)[0];
        //}

        public abstract MathematicaScalar BasisVectorSignature(int basisVectorIndex);

        public abstract GaMultivector BasisBladeSignature(int id);

        public abstract GaMultivector Gp(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Sp(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Lcp(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Rcp(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Hip(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Fdp(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Acp(GaMultivector mv1, GaMultivector mv2);

        public abstract GaMultivector Cp(GaMultivector mv1, GaMultivector mv2);


        public GaMultivector CreateUnitPseudoScalar()
        {
            return GaMultivector.CreateBasisBlade(GaSpaceDimension, MaxBasisBladeId);
        }

        public GaMultivector CreateInverseUnitPseudoScalar()
        {
            //TODO: Review this computation
            return GaMultivector.CreateTerm(GaSpaceDimension, MaxBasisBladeId, UnitPseudoScalarCoef);
        }

        public GaMultivector Op(GaMultivector mv1, GaMultivector mv2)
        {
            return mv1.OuterProduct(mv2);
        }

        //TODO: Use TextExpr instead of Expr to speed up computations
        public MathematicaScalar Magnitude(GaMultivector mv)
        {
            return
                mv
                .ToKVectors()
                .Select(pair => Sp(pair.Value, pair.Value.Reverse()))
                .Aggregate(CasConstants.Zero, (current, mv1) => current + mv1[0].Abs())
                .Sqrt();
        }

        public MathematicaScalar Magnitude2(GaMultivector mv)
        {
            return 
                mv
                .ToKVectors()
                .Select(pair => Sp(pair.Value, pair.Value.Reverse()))
                .Aggregate(CasConstants.Zero, (current, mv1) => current + mv1[0].Abs());
        }

        public MathematicaScalar Norm2(GaMultivector mv)
        {
            return Sp(mv, mv.Reverse())[0];
        }


        private static readonly Dictionary<string, GaFrame> OrthonormalFramesCache = new Dictionary<string,GaFrame>();

        private static void VerifyVSpaceDim(int vSpaceDim)
        {
            if (vSpaceDim <= 0 || vSpaceDim > FrameUtils.MaxVSpaceDimension)
                throw new GMacSymbolicException("Invalid vector space dimension");
        }

        private static string GetOrthonormalSignatureString(int vSpaceDim)
        {
            return "".PadLeft(vSpaceDim, '+');
        }

        public static GaFrameEuclidean CreateEuclidean(int vSpaceDim)
        {
            VerifyVSpaceDim(vSpaceDim);

            GaFrame gaFrame;
            var frameSig = GetOrthonormalSignatureString(vSpaceDim);

            if (OrthonormalFramesCache.TryGetValue(frameSig, out gaFrame))
                return (GaFrameEuclidean) gaFrame;

            gaFrame = new GaFrameEuclidean(vSpaceDim);

            OrthonormalFramesCache.Add(frameSig, gaFrame);

            return (GaFrameEuclidean)gaFrame;
        }

        public static GaFrame CreateOrthonormal(IEnumerable<char> frameSigSignList)
        {
            var psgCount = 0;
            var nsgCount = 0;
            var s = new StringBuilder();
            var sigList = new List<int>(FrameUtils.MaxVSpaceDimension);

            //Convert the string signature into a list of integers with values of 1 and -1.
            foreach (var c in frameSigSignList)
                switch (c)
                {
                    case '+':
                        sigList.Add(1);
                        s.Append("+");
                        psgCount++;
                        break;

                    case '-':
                        sigList.Add(-1);
                        s.Append("-");
                        nsgCount++;
                        break;

                    default:
                        throw new GMacSymbolicException("Invalid orthonormal frame signature");
                }

            var vSpaceDim = psgCount + nsgCount;

            VerifyVSpaceDim(vSpaceDim);

            var frameSig = s.ToString();
            GaFrame gaFrame;

            if (OrthonormalFramesCache.TryGetValue(frameSig, out gaFrame))
                return gaFrame;

            //The frame signature has no vectors with negative signature; create a Euclidean frame
            if (nsgCount == 0)
                gaFrame = new GaFrameEuclidean(vSpaceDim);

            //Else create an orthonormal frame
            else
                gaFrame = new GaFrameOrthonormal(sigList.ToArray());

            OrthonormalFramesCache.Add(frameSig, gaFrame);

            return gaFrame;
        }

        public static GaFrame CreateOrthonormal(IEnumerable<int> frameSigList)
        {
            var psgCount = 0;
            var nsgCount = 0;
            var sigList = new List<int>(FrameUtils.MaxVSpaceDimension);
            var frameSig = new StringBuilder(FrameUtils.MaxVSpaceDimension);

            //Convert the string signature into a list of integers with values of 1 and -1.
            foreach (var s in frameSigList)
                switch (s)
                {
                    case 1:
                        frameSig.Append("+");
                        sigList.Add(1);
                        psgCount++;
                        break;

                    case -1:
                        frameSig.Append("-");
                        sigList.Add(-1);
                        nsgCount++;
                        break;

                    default:
                        throw new GMacSymbolicException("Invalid orthonormal frame signature");
                }

            var vSpaceDim = psgCount + nsgCount;

            VerifyVSpaceDim(vSpaceDim);

            GaFrame gaFrame;

            if (OrthonormalFramesCache.TryGetValue(frameSig.ToString(), out gaFrame))
                return gaFrame;

            //The frame signature has no vectors with negative signature; create a Euclidean frame
            if (nsgCount == 0)
                gaFrame = new GaFrameEuclidean(vSpaceDim);

            //Else create an orthonormal frame
            else
                gaFrame = new GaFrameOrthonormal(sigList.ToArray());

            OrthonormalFramesCache.Add(frameSig.ToString(), gaFrame);

            return gaFrame;
        }

        public static GaFrame CreateOrthogonal(IEnumerable<MathematicaScalar> frameSigList)
        {
            var frameSigListArray = frameSigList.ToArray();

            var vSpaceDim = 0;

            //Positive and negative unity signature vectors count
            var pusvCount = 0; 
            var nusvCount = 0;

            //Orthonormal Basis Vectors Signatures
            var onSigList = new List<int>();

            foreach (var bvSig in frameSigListArray)
            {
                if (ReferenceEquals(bvSig, null) || bvSig.IsNonZeroRealConstant() == false)
                    throw new GMacSymbolicException("Invalid basis vector signature");

                if (bvSig.IsEqualScalar(SymbolicUtils.Constants.One))
                {
                    pusvCount++;
                    onSigList.Add(1);
                }
                else if (bvSig.IsEqualScalar(SymbolicUtils.Constants.One))
                {
                    nusvCount++;
                    onSigList.Add(-1);
                }

                vSpaceDim++;
            }

            //Test if an orthonormal basis is given
            return 
                vSpaceDim == (pusvCount + nusvCount) 
                ? CreateOrthonormal(onSigList)
                : new GaFrameOrthogonal(frameSigListArray);
        }

        /// <summary>
        /// Create a non orthogonal frame based on an asymmetric inner product matrix
        /// </summary>
        /// <param name="ipm"></param>
        /// <returns></returns>
        private static GaFrameNonOrthogonal CreateNonOrthogonalFromIpm(MathematicaMatrix ipm)
        {
            MathematicaVector eigenValues;
            MathematicaMatrix eigenVectors;

            if (ipm.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.OrthogonalInMatrixColumns, out eigenValues, out eigenVectors) == false)
                throw new GMacSymbolicException("Cannot obtain orthogonal eigen system of the given matrix");

            var baseFrame = CreateOrthogonal(eigenValues);

            var baseToDerivedOm = GaOuterMorphismFull.Create(eigenVectors);
            var derivedToBaseOm = GaOuterMorphismFull.Create(eigenVectors.Transpose());

            return new GaFrameNonOrthogonal(baseFrame, ipm, derivedToBaseOm, baseToDerivedOm);
        }

        public static GaFrame CreateFromIpm(ISymbolicMatrix ipm)
        {
            if (ipm.IsSymmetric() == false)
                throw new GMacSymbolicException("Inner product matrix must be symmetric");

            if (ipm.IsIdentity())
                return CreateEuclidean(ipm.Rows);

            return 
                ipm.IsDiagonal() 
                ? CreateOrthogonal(ipm.GetDiagonal()) 
                : CreateNonOrthogonalFromIpm(ipm.ToMathematicaMatrix());
        }

        /// <summary>
        /// Create a derived frame system using a change of basis matrix for the basis vectors
        /// </summary>
        /// <param name="baseFrame">The base frame. It may be any kind of frame</param>
        /// <param name="cbm">The 'Change Of Basis Vectors' matrix. It must be invertable</param>
        /// <returns></returns>
        public static DerivedFrameSystem CreateDerivedCbmFrameSystem(GaFrame baseFrame, MathematicaMatrix cbm)
        {
            var baseIpm = baseFrame.Ipm.ToMathematicaMatrix();
            var cbmTrans = (MathematicaMatrix)cbm.Transpose();
            var cbmInverseTrans = (MathematicaMatrix)cbmTrans.Inverse();

            var ipm = cbm * baseIpm * cbmTrans;

            var baseToDerivedOm = GaOuterMorphismFull.Create(cbmInverseTrans);
            var derivedToBaseOm = GaOuterMorphismFull.Create(cbmTrans);

            if (ipm.IsDiagonal())
            {
                var derivedFrame = CreateOrthogonal(ipm.GetDiagonal());
                return new DerivedFrameSystem(baseFrame, derivedFrame, derivedToBaseOm, baseToDerivedOm);
            }

            if (baseFrame.IsOrthogonal)
            {
                var derivedFrame = new GaFrameNonOrthogonal(baseFrame, ipm, derivedToBaseOm, baseToDerivedOm);
                return derivedFrame.Dfs;
            }

            var gaFrame =
                //new GaFrameNonOrthogonal(baseFrame, ipm, derivedToBaseOm, baseToDerivedOm);
                CreateNonOrthogonalFromIpm(ipm.ToMathematicaMatrix());

            return gaFrame.Dfs;
            //return new DerivedFrameSystem(baseFrame, gaFrame, derivedToBaseOm, baseToDerivedOm);
        }

        /// <summary>
        /// Create a derived frame system where the derived frame is the reciprocal of the base frame
        /// </summary>
        /// <param name="baseFrame"></param>
        /// <returns></returns>
        public static DerivedFrameSystem CreateReciprocalCbmFrameSystem(GaFrame baseFrame)
        {
            if (baseFrame.IsOrthogonal)
            {
                var cbmat = baseFrame.Ipm.Inverse();

                var b2DOm = GaOuterMorphismFull.Create(baseFrame.Ipm);
                var d2BOm = GaOuterMorphismFull.Create(cbmat);

                var derivedFrame = CreateOrthogonal(cbmat.GetDiagonal());
                return new DerivedFrameSystem(baseFrame, derivedFrame, d2BOm, b2DOm);
            }

            var cbmArray = new MathematicaScalar[baseFrame.VSpaceDimension, baseFrame.VSpaceDimension];

            var mv2 = baseFrame.CreateInverseUnitPseudoScalar();

            for (var i = 0; i < baseFrame.VSpaceDimension; i++)
            {
                var id = (1 << i) ^ baseFrame.MaxBasisBladeId;
                var mv1 = GaMultivector.CreateTerm(baseFrame.GaSpaceDimension, id, SymbolicUtils.Constants.One);
                
                var mv = baseFrame.Lcp(mv1, mv2);

                foreach (var term in mv)
                {
                    var j = term.Key.BasisBladeIndex();

                    if ((i & 1) == 1)
                        cbmArray[i, j] = term.Value;
                    else
                        cbmArray[i, j] = -term.Value;
                }
            }

            var cbm = MathematicaMatrix.CreateFullMatrix(SymbolicUtils.Cas, cbmArray);

            return CreateDerivedCbmFrameSystem(baseFrame, cbm);
        }
    }
}
