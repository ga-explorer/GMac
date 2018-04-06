using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMac.GMacMath.Numeric.Maps.Bilinear;
using GMac.GMacMath.Numeric.Multivectors;
using GMac.GMacMath.Numeric.Maps;
using GMac.GMacMath.Numeric.Metrics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GMac.GMacMath.Numeric.Frames
{
    public abstract class GaNumFrame : IGMacFrame
    {
        #region Static Members
        private static readonly Dictionary<string, GaNumFrame> OrthonormalFramesCache
            = new Dictionary<string, GaNumFrame>();

        private static void VerifyVSpaceDim(int vSpaceDim, int minVSpaceDim = 1)
        {
            if (vSpaceDim < minVSpaceDim || vSpaceDim > GMacMathUtils.MaxVSpaceDimension)
                throw new GMacNumericException("Invalid vector space dimension");
        }

        private static string GetOrthonormalSignatureString(int vSpaceDim)
        {
            return "".PadLeft(vSpaceDim, '+');
        }

        /// <summary>
        /// Create a non orthogonal frame based on a symmetric inner product matrix
        /// </summary>
        /// <param name="ipm"></param>
        /// <returns></returns>
        private static GaNumFrameNonOrthogonal CreateNonOrthogonalFromIpm(Matrix ipm)
        {
            var eigenSystem = ipm.Evd(Symmetricity.Symmetric);
            var eigenValues = eigenSystem.EigenValues.Select(v => v.Real < 0 ? -v.Magnitude : v.Magnitude);
            var eigenVectors = eigenSystem.EigenVectors;

            //if (ipm.EigenSystem(MathematicaMatrix.EigenVectorsSpecs.OrthogonalInMatrixColumns, out eigenValues, out eigenVectors) == false)
            //    throw new GMacNumericException("Cannot obtain orthogonal eigen system of the given matrix");

            var baseFrame = CreateOrthogonal(eigenValues);

            //var baseToDerivedOm = GaNumOutermorphismHash.Create(eigenVectors);
            //var derivedToBaseOm = GaNumOutermorphismHash.Create(eigenVectors.Transpose());

            var baseToDerivedOm = (eigenVectors as Matrix).ToOutermorphismTree();
            var derivedToBaseOm = (eigenVectors.Transpose() as Matrix).ToOutermorphismTree();

            return new GaNumFrameNonOrthogonal(baseFrame, ipm, derivedToBaseOm, baseToDerivedOm);
        }


        public static GaNumFrameEuclidean CreateEuclidean(int vSpaceDim)
        {
            VerifyVSpaceDim(vSpaceDim);

            GaNumFrame gaFrame;
            var frameSig = GetOrthonormalSignatureString(vSpaceDim);

            if (OrthonormalFramesCache.TryGetValue(frameSig, out gaFrame))
                return (GaNumFrameEuclidean)gaFrame;

            gaFrame = new GaNumFrameEuclidean(vSpaceDim);

            OrthonormalFramesCache.Add(frameSig, gaFrame);

            return (GaNumFrameEuclidean)gaFrame;
        }

        public static GaNumFrameNonOrthogonal CreateConformal(int vSpaceDim)
        {
            VerifyVSpaceDim(vSpaceDim, 3);

            var ipm = DenseMatrix.CreateIdentity(vSpaceDim);

            ipm[vSpaceDim - 2, vSpaceDim - 2] = 0;
            ipm[vSpaceDim - 2, vSpaceDim - 1] = -1.0d;
            ipm[vSpaceDim - 1, vSpaceDim - 2] = -1.0d;
            ipm[vSpaceDim - 1, vSpaceDim - 1] = 0;

            return CreateNonOrthogonalFromIpm(ipm);
        }

        public static GaNumFrameOrthogonal CreateProjective(int vSpaceDim)
        {
            VerifyVSpaceDim(vSpaceDim, 2);

            var sigArray = Enumerable.Repeat(1, vSpaceDim).ToArray();
            sigArray[vSpaceDim - 1] = 0;

            var frameSigList =
                sigArray.Select(i => (double)i);

            return new GaNumFrameOrthogonal(frameSigList);
        }

        public static GaNumFrame CreateOrthonormal(IEnumerable<char> frameSigSignList)
        {
            var psgCount = 0;
            var nsgCount = 0;
            var s = new StringBuilder();
            var sigList = new List<bool>(GMacMathUtils.MaxVSpaceDimension);

            //Convert the string signature into a list of integers with values of 1 and -1.
            foreach (var c in frameSigSignList)
                switch (c)
                {
                    case '+':
                        sigList.Add(false);
                        s.Append("+");
                        psgCount++;
                        break;

                    case '-':
                        sigList.Add(true);
                        s.Append("-");
                        nsgCount++;
                        break;

                    default:
                        throw new GMacNumericException("Invalid orthonormal frame signature");
                }

            var vSpaceDim = psgCount + nsgCount;

            VerifyVSpaceDim(vSpaceDim);

            var frameSig = s.ToString();
            GaNumFrame gaFrame;

            if (OrthonormalFramesCache.TryGetValue(frameSig, out gaFrame))
                return gaFrame;

            //The frame signature has no vectors with negative signature; create a Euclidean frame
            if (nsgCount == 0)
                gaFrame = new GaNumFrameEuclidean(vSpaceDim);

            //Else create an orthonormal frame
            else
                gaFrame = new GaNumFrameOrthonormal(sigList.ToArray());

            OrthonormalFramesCache.Add(frameSig, gaFrame);

            return gaFrame;
        }

        public static GaNumFrame CreateOrthonormal(IEnumerable<int> frameSigList)
        {
            var psgCount = 0;
            var nsgCount = 0;
            var sigList = new List<bool>(GMacMathUtils.MaxVSpaceDimension);
            var frameSig = new StringBuilder(GMacMathUtils.MaxVSpaceDimension);

            //Convert the string signature into a list of integers with values of 1 and -1.
            foreach (var s in frameSigList)
                switch (s)
                {
                    case 1:
                        frameSig.Append('+');
                        sigList.Add(false);
                        psgCount++;
                        break;

                    case -1:
                        frameSig.Append('-');
                        sigList.Add(true);
                        nsgCount++;
                        break;

                    default:
                        throw new GMacNumericException("Invalid orthonormal frame signature");
                }

            var vSpaceDim = psgCount + nsgCount;

            VerifyVSpaceDim(vSpaceDim);

            GaNumFrame gaFrame;

            if (OrthonormalFramesCache.TryGetValue(frameSig.ToString(), out gaFrame))
                return gaFrame;

            //The frame signature has no vectors with negative signature; create a Euclidean frame
            if (nsgCount == 0)
                gaFrame = new GaNumFrameEuclidean(vSpaceDim);

            //Else create an orthonormal frame
            else
                gaFrame = new GaNumFrameOrthonormal(sigList);

            OrthonormalFramesCache.Add(frameSig.ToString(), gaFrame);

            return gaFrame;
        }

        public static GaNumFrame CreateOrthogonal(IEnumerable<double> frameSigList)
        {
            var frameSigArray = frameSigList.ToArray();

            var vSpaceDim = 0;

            //Positive and negative unity signature vectors count
            var pusvCount = 0;
            var nusvCount = 0;

            //Orthonormal Basis Vectors Signatures
            var onSigList = new List<char>();

            foreach (var bvSig in frameSigArray)
            {
                if (bvSig.IsNearEqual(1.0d))
                {
                    pusvCount++;
                    onSigList.Add('+');
                }
                else if (bvSig.IsNearEqual(-1.0d))
                {
                    nusvCount++;
                    onSigList.Add('-');
                }

                vSpaceDim++;
            }

            //Test if an orthonormal basis is given
            return
                vSpaceDim == (pusvCount + nusvCount)
                ? CreateOrthonormal(onSigList)
                : new GaNumFrameOrthogonal(frameSigArray);
        }

        public static GaNumFrame CreateFromIpm(Matrix ipm)
        {
            if (ipm.IsIdentity())
                return CreateEuclidean(ipm.RowCount);

            return
                ipm.IsDiagonal()
                ? CreateOrthogonal(ipm.Diagonal())
                : CreateNonOrthogonalFromIpm(ipm);
        }

        /// <summary>
        /// Create a derived frame system using a change of basis matrix for the basis vectors
        /// </summary>
        /// <param name="baseFrame">The base frame. It may be any kind of frame</param>
        /// <param name="cbm">The 'Change Of Basis Vectors' matrix. It must be invertable</param>
        /// <returns></returns>
        public static GaNumMetricNonOrthogonal CreateDerivedCbmFrameSystem(GaNumFrame baseFrame, Matrix cbm)
        {
            var baseIpm = baseFrame.Ipm;
            var cbmTrans = (Matrix)cbm.Transpose();
            var cbmInverseTrans = (Matrix)cbmTrans.Inverse();

            var ipm = cbm * baseIpm * cbmTrans;

            var baseToDerivedCba = cbmInverseTrans.ToOutermorphismTree();
            var derivedToBaseCba = cbmTrans.ToOutermorphismTree();

            if ((ipm as Matrix).IsDiagonal())
            {
                var derivedFrame = CreateOrthogonal(ipm.Diagonal());
                return new GaNumMetricNonOrthogonal(baseFrame, derivedFrame, derivedToBaseCba, baseToDerivedCba);
            }

            if (baseFrame.IsOrthogonal)
            {
                var derivedFrame = new GaNumFrameNonOrthogonal(baseFrame, ipm as Matrix, derivedToBaseCba, baseToDerivedCba);
                return derivedFrame.NonOrthogonalMetric;
            }

            var gaFrame =
                //new GaFrameNonOrthogonal(baseFrame, ipm, derivedToBaseOm, baseToDerivedOm);
                CreateNonOrthogonalFromIpm(ipm as Matrix);

            return gaFrame.NonOrthogonalMetric;
            //return new DerivedFrameSystem(baseFrame, gaFrame, derivedToBaseOm, baseToDerivedOm);
        }

        /// <summary>
        /// Create a derived frame system where the derived frame is the reciprocal of the base frame
        /// </summary>
        /// <param name="baseFrame"></param>
        /// <returns></returns>
        public static GaNumMetricNonOrthogonal CreateReciprocalCbmFrameSystem(GaNumFrame baseFrame)
        {
            if (baseFrame.IsOrthogonal)
            {
                var cbmat = baseFrame.Ipm.Inverse();

                var b2DOm = baseFrame.Ipm.ToOutermorphismTree();
                var d2BOm = (cbmat as Matrix).ToOutermorphismTree();

                var derivedFrame = CreateOrthogonal(cbmat.Diagonal());
                return new GaNumMetricNonOrthogonal(baseFrame, derivedFrame, d2BOm, b2DOm);
            }

            var cbmArray = new double[baseFrame.VSpaceDimension, baseFrame.VSpaceDimension];

            var mv2 = baseFrame.CreateInverseUnitPseudoScalar();

            for (var i = 0; i < baseFrame.VSpaceDimension; i++)
            {
                var id = (1 << i) ^ baseFrame.MaxBasisBladeId;
                var mv1 = GaNumMultivector.CreateTerm(baseFrame.GaSpaceDimension, id, 1.0d);

                var mv = baseFrame.Lcp[mv1, mv2];

                foreach (var term in mv.NonZeroTerms)
                {
                    var j = term.Key.BasisBladeIndex();

                    if ((i & 1) == 1)
                        cbmArray[i, j] = term.Value;
                    else
                        cbmArray[i, j] = -term.Value;
                }
            }

            var cbm = DenseMatrix.OfArray(cbmArray);

            return CreateDerivedCbmFrameSystem(baseFrame, cbm);
        }
        #endregion


        /// <summary>
        /// Inner-Product Matrix
        /// </summary>
        protected Matrix InnerProductMatrix;

        public double UnitPseudoScalarCoef { get; protected set; }

        public Matrix Ipm
        {
            get
            {
                if (ReferenceEquals(InnerProductMatrix, null))
                    ComputeIpm();

                return InnerProductMatrix;
            }
        }

        public abstract int VSpaceDimension { get; }

        public int MaxBasisBladeId => GMacMathUtils.MaxBasisBladeId(VSpaceDimension);

        public int GaSpaceDimension => VSpaceDimension.ToGaSpaceDimension();

        public int GradesCount => VSpaceDimension + 1;

        public bool IsNonOrthogonal => !IsOrthogonal;


        /// <summary>
        /// A fixed computed bilinear map implementation of the Outer product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedOp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Geometric product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedGp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Scalar product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedSp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Left Contraction product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedLcp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Right Contraction product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedRcp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Fat-Dor Contraction product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedFdp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Hestenes Inner product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedHip { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Anti-Commutator product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedAcp { get; }

        /// <summary>
        /// A fixed computed bilinear map implementation of the Commutator product of this frame
        /// </summary>
        public abstract IGaNumMapBilinear ComputedCp { get; }


        /// <summary>
        /// A bilinear map implementation of the Outer product of this frame
        /// </summary>
        public IGaNumMapBilinear Op { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Geometrc product of this frame
        /// </summary>
        public IGaNumMapBilinear Gp { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Scalar product of this frame
        /// </summary>
        public IGaNumMapBilinear Sp { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Left Contraction product product of this frame
        /// </summary>
        public IGaNumMapBilinear Lcp { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Right Contraction product product of this frame
        /// </summary>
        public IGaNumMapBilinear Rcp { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Fat-Dot product of this frame
        /// </summary>
        public IGaNumMapBilinear Fdp { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Hestenes Inner product of this frame
        /// </summary>
        public IGaNumMapBilinear Hip { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Anti-Commutator product of this frame
        /// </summary>
        public IGaNumMapBilinear Acp { get; internal set; }

        /// <summary>
        /// A bilinear map implementation of the Commutator product of this frame
        /// </summary>
        public IGaNumMapBilinear Cp { get; internal set; }


        public abstract IGaNumMetric Metric { get; }

        public abstract IGaNumMetricOrthogonal BaseOrthogonalMetric { get; }

        public abstract GaNumFrame BaseOrthogonalFrame { get; }

        public abstract bool IsEuclidean { get; }

        public abstract bool IsOrthogonal { get; }

        public abstract bool IsOrthonormal { get; }


        private void SetProductsImplementationToComputed()
        {
            Op = ComputedOp;
            Gp = ComputedGp;
            Sp = ComputedSp;
            Lcp = ComputedLcp;
            Rcp = ComputedRcp;
            Fdp = ComputedFdp;
            Hip = ComputedHip;
            Acp = ComputedAcp;
            Cp = ComputedCp;
        }

        private void SetProductsImplementationToLookupArray()
        {
            Op = ComputedOp.ToArrayMap();
            Gp = ComputedGp.ToArrayMap();
            Sp = ComputedSp.ToArrayMap();
            Lcp = ComputedLcp.ToArrayMap();
            Rcp = ComputedRcp.ToArrayMap();
            Fdp = ComputedFdp.ToArrayMap();
            Hip = ComputedHip.ToArrayMap();
            Acp = ComputedAcp.ToArrayMap();
            Cp = ComputedCp.ToArrayMap();
        }

        private void SetProductsImplementationToLookupHash()
        {
            Op = ComputedOp.ToHashMap();
            Gp = ComputedGp.ToHashMap();
            Sp = ComputedSp.ToHashMap();
            Lcp = ComputedLcp.ToHashMap();
            Rcp = ComputedRcp.ToHashMap();
            Fdp = ComputedFdp.ToHashMap();
            Hip = ComputedHip.ToHashMap();
            Acp = ComputedAcp.ToHashMap();
            Cp = ComputedCp.ToHashMap();
        }

        private void SetProductsImplementationToLookupTree()
        {
            Op = ComputedOp.ToTreeMap();
            Gp = ComputedGp.ToTreeMap();
            Sp = ComputedSp.ToTreeMap();
            Lcp = ComputedLcp.ToTreeMap();
            Rcp = ComputedRcp.ToTreeMap();
            Fdp = ComputedFdp.ToTreeMap();
            Hip = ComputedHip.ToTreeMap();
            Acp = ComputedAcp.ToTreeMap();
            Cp = ComputedCp.ToTreeMap();
        }

        private void SetProductsImplementationToLookupCoefSums()
        {
            Op = ComputedOp.ToCoefSumsMap();
            Gp = ComputedGp.ToCoefSumsMap();
            Sp = ComputedSp.ToCoefSumsMap();
            Lcp = ComputedLcp.ToCoefSumsMap();
            Rcp = ComputedRcp.ToCoefSumsMap();
            Fdp = ComputedFdp.ToCoefSumsMap();
            Hip = ComputedHip.ToCoefSumsMap();
            Acp = ComputedAcp.ToCoefSumsMap();
            Cp = ComputedCp.ToCoefSumsMap();
        }

        /// <summary>
        /// Select and initialize an implementation method for the bilinear products of this frame
        /// </summary>
        /// <param name="method"></param>
        public void SetProductsImplementation(GaBilinearProductImplementation method)
        {
            switch (method)
            {
                case GaBilinearProductImplementation.LookupArray:
                    SetProductsImplementationToLookupArray();
                    break;

                case GaBilinearProductImplementation.LookupHash:
                    SetProductsImplementationToLookupHash();
                    break;

                case GaBilinearProductImplementation.LookupTree:
                    SetProductsImplementationToLookupTree();
                    break;

                case GaBilinearProductImplementation.LookupCoefSums:
                    SetProductsImplementationToLookupCoefSums();
                    break;

                case GaBilinearProductImplementation.Computed:
                default:
                    SetProductsImplementationToComputed();
                    break;
            }
        }

        protected abstract void ComputeIpm();

        public abstract double BasisVectorSignature(int basisVectorIndex);

        public abstract GaNumMultivector BasisBladeSignature(int id);


        public GaNumMultivector CreateUnitPseudoScalar()
        {
            return GaNumMultivector.CreateBasisBlade(GaSpaceDimension, MaxBasisBladeId);
        }

        public GaNumMultivector CreateInverseUnitPseudoScalar()
        {
            return GaNumMultivector.CreateTerm(GaSpaceDimension, MaxBasisBladeId, UnitPseudoScalarCoef);
        }

        public double Magnitude(GaNumMultivector mv)
        {
            return Math.Sqrt(
                mv
                .GetKVectorParts()
                .Select(pair => Sp[pair.Value, pair.Value.Reverse()])
                .Aggregate(0.0d, (current, mv1) => current + Math.Abs(mv1[0]))
                );
        }

        public double Magnitude2(GaNumMultivector mv)
        {
            return
                mv
                .GetKVectorParts()
                .Select(pair => Sp[pair.Value, pair.Value.Reverse()])
                .Aggregate(0.0d, (current, mv1) => current + Math.Abs(mv1[0]));
        }

        public double Norm2(GaNumMultivector mv)
        {
            return Sp[mv, mv.Reverse()][0];
        }

        /// <summary>
        /// Odd Versor Product of a list of basis blades given by their IDs
        /// </summary>
        /// <param name="oddVersor"></param>
        /// <param name="basisBladeIDs"></param>
        /// <returns></returns>
        public IEnumerable<GaNumMultivector> OddVersorProduct(GaNumMultivector oddVersor, IEnumerable<int> basisBladeIDs)
        {
            var oddVersorReverse = oddVersor.Reverse();
            var oddVersorNorm2Inverse = 1.0d / Sp[oddVersor, oddVersorReverse][0];
            var oddVersorInverse = oddVersorReverse * oddVersorNorm2Inverse;

            return basisBladeIDs.Select(id =>
                {
                    var mv = GaNumMultivector.CreateTerm(
                        GaSpaceDimension,
                        id,
                        id.BasisBladeIdHasNegativeGradeInv() ? -1.0d : 1.0d
                    );

                    return 
                        Gp[Gp[oddVersor, mv], oddVersorInverse]
                            .GetKVectorPart(id.BasisBladeGrade());
                });
        }

        public IEnumerable<GaNumMultivector> RotorProduct(GaNumMultivector rotorVersor, IEnumerable<int> basisBladeIDs)
        {
            var rotorVersorInverse = rotorVersor.Reverse();

            return basisBladeIDs.Select(id =>
            {
                var mv = GaNumMultivector.CreateBasisBlade(GaSpaceDimension, id);

                return
                    Gp[Gp[rotorVersor, mv], rotorVersorInverse]
                        .GetKVectorPart(id.BasisBladeGrade());
            });
        }

        public IEnumerable<GaNumMultivector> EvenVersorProduct(GaNumMultivector evenVersor, IEnumerable<int> basisBladeIDs)
        {
            var evenVersorReverse = evenVersor.Reverse();
            var evenVersorNorm2Inverse = 1.0d / Sp[evenVersor, evenVersorReverse][0];
            var evenVersorInverse = evenVersorReverse * evenVersorNorm2Inverse;

            return basisBladeIDs.Select(id =>
            {
                var mv = GaNumMultivector.CreateBasisBlade(GaSpaceDimension, id);

                return
                    Gp[Gp[evenVersor, mv], evenVersorInverse]
                        .GetKVectorPart(id.BasisBladeGrade());
            });
        }

        /// <summary>
        /// Odd Versor Product
        /// </summary>
        /// <param name="oddVersor"></param>
        /// <param name="mv"></param>
        /// <returns></returns>
        public GaNumMultivector OddVersorProduct(GaNumMultivector oddVersor, GaNumMultivector mv)
        {
            var oddVersorReverse = oddVersor.Reverse();
            var oddVersorNorm2Inverse = 1.0d / Sp[oddVersor, oddVersorReverse][0];
            var oddVersorInverse = oddVersorReverse * oddVersorNorm2Inverse;

            return Gp[Gp[oddVersor, mv.GradeInv()], oddVersorInverse];
        }

        /// <summary>
        /// Even Versor Product
        /// </summary>
        /// <param name="evenVersor"></param>
        /// <param name="mv"></param>
        /// <returns></returns>
        public GaNumMultivector EvenVersorProduct(GaNumMultivector evenVersor, GaNumMultivector mv)
        {
            var evenVersorReverse = evenVersor.Reverse();
            var evenVersorNorm2Inverse = 1.0d / Sp[evenVersor, evenVersorReverse][0];
            var evenVersorInverse = evenVersorReverse * evenVersorNorm2Inverse;

            return Gp[Gp[evenVersor, mv], evenVersorInverse];
        }

        /// <summary>
        /// Rotor Product
        /// </summary>
        /// <param name="rotorVersor"></param>
        /// <param name="mv"></param>
        /// <returns></returns>
        public GaNumMultivector RotorProduct(GaNumMultivector rotorVersor, GaNumMultivector mv)
        {
            return Gp[Gp[rotorVersor, mv], rotorVersor.Reverse()];
        }
    }
}
