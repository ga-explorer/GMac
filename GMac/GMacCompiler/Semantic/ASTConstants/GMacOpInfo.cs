using System;
using System.Collections.Generic;
using System.Linq;

namespace GMac.GMacCompiler.Semantic.ASTConstants
{
    /// <summary>
    /// Holds all information related to builtin GMac operators
    /// </summary>
    public sealed class GMacOpInfo
    {
        #region built-in operators
        public static readonly GMacOpInfo UnaryPlus = 
            CreateUnary("unary_plus", "+");

        public static readonly GMacOpInfo UnaryMinus = 
            CreateUnary("unary_minus", "-");

        public static readonly GMacOpInfo UnaryGradeInvolution =
            CreateUnary("grade_inv");

        public static readonly GMacOpInfo UnaryCliffordConjugate =
            CreateUnary("cliff_conj");

        public static readonly GMacOpInfo UnaryReverse =
            CreateUnary("reverse");

        public static readonly GMacOpInfo UnaryNorm2 =
            CreateUnary("norm2", false);

        //The Euclidean norm is equivalent to the euclidean magnitude

        public static readonly GMacOpInfo UnaryMagnitude =
            CreateUnary("mag", false);

        public static readonly GMacOpInfo UnaryEuclideanMagnitude =
            CreateUnary("emag");

        public static readonly GMacOpInfo UnaryMagnitude2 = 
            CreateUnary("mag2", false);

        public static readonly GMacOpInfo UnaryEuclideanMagnitude2 =
            CreateUnary("emag2");


        public static readonly GMacOpInfo BinaryDiff = 
            CreateBinary("diff");

        public static readonly GMacOpInfo BinaryPlus = 
            CreateBinary("plus", "+");

        public static readonly GMacOpInfo BinarySubtract =
            CreateBinary("subtract", "-");

        public static readonly GMacOpInfo BinaryTimesWithScalar = 
            CreateBinary("times", "*");

        public static readonly GMacOpInfo BinaryDivideByScalar = 
            CreateBinary("divide", "/");

        public static readonly GMacOpInfo BinaryOp = 
            CreateBinary("op", "^");

        public static readonly GMacOpInfo BinarySp = 
            CreateBinary("sp", "sp", false);

        public static readonly GMacOpInfo BinaryGp = 
            CreateBinary("gp", "gp", false);

        public static readonly GMacOpInfo BinaryLcp = 
            CreateBinary("lcp", "lcp", false);

        public static readonly GMacOpInfo BinaryRcp = 
            CreateBinary("rcp", "rcp", false);

        public static readonly GMacOpInfo BinaryHip = 
            CreateBinary("hip", "hip", false);

        public static readonly GMacOpInfo BinaryFdp = 
            CreateBinary("fdp", "fdp", false);

        public static readonly GMacOpInfo BinaryCp = 
            CreateBinary("cp", "cp", false);

        public static readonly GMacOpInfo BinaryAcp = 
            CreateBinary("acp", "acp", false);

        public static readonly GMacOpInfo BinaryESp = 
            CreateBinary("esp", "esp");

        public static readonly GMacOpInfo BinaryEGp = 
            CreateBinary("egp", "egp");

        public static readonly GMacOpInfo BinaryELcp = 
            CreateBinary("elcp", "elcp");

        public static readonly GMacOpInfo BinaryERcp = 
            CreateBinary("ercp", "ercp");

        public static readonly GMacOpInfo BinaryEHip = 
            CreateBinary("ehip", "ehip");

        public static readonly GMacOpInfo BinaryEFdp = 
            CreateBinary("efdp", "efdp");

        public static readonly GMacOpInfo BinaryECp = 
            CreateBinary("ecp", "ecp");

        public static readonly GMacOpInfo BinaryEAcp =
            CreateBinary("eacp", "eacp");


        public static GMacOpInfo[] AllOps =
            {
                UnaryPlus, UnaryMinus, UnaryGradeInvolution, UnaryCliffordConjugate, UnaryReverse, 
                UnaryNorm2, UnaryMagnitude2, UnaryMagnitude, UnaryEuclideanMagnitude2, UnaryEuclideanMagnitude,
                BinaryDiff, BinaryPlus, BinarySubtract, BinaryTimesWithScalar, BinaryDivideByScalar, 
                BinaryOp, BinarySp, BinaryGp, BinaryLcp, BinaryRcp, BinaryHip, BinaryFdp, BinaryCp, BinaryAcp,
                BinaryEAcp, BinaryECp, BinaryEFdp, BinaryEGp, BinaryEHip, BinaryELcp, BinaryERcp, BinaryESp
            };

        public static GMacOpInfo[] GaProducts =
        {
            BinaryAcp, BinaryCp, BinaryFdp, BinaryGp, BinaryHip, BinaryLcp, BinaryOp, BinaryRcp, BinarySp,
            BinaryEAcp, BinaryECp, BinaryEFdp, BinaryEGp, BinaryEHip, BinaryELcp, BinaryERcp, BinaryESp
        };

        public static IEnumerable<GMacOpInfo> UnaryOps
        {
            get { return AllOps.Where(op => op.IsUnary); }
        }

        public static IEnumerable<GMacOpInfo> BinaryOps
        {
            get { return AllOps.Where(op => op.IsBinary); }
        }
        #endregion

        #region static constructors
        private static GMacOpInfo CreateUnary(string opName, bool metricIndep = true)
        {
            return new GMacOpInfo(opName, null, false) { IsMetricIndependent = metricIndep };
        }

        private static GMacOpInfo CreateUnary(string opName, string opSymbol, bool metricIndep = true)
        {
            return new GMacOpInfo(opName, opSymbol, false) { IsMetricIndependent = metricIndep };
        }

        private static GMacOpInfo CreateBinary(string opName, bool metricIndep = true)
        {
            return new GMacOpInfo(opName, null, true) { IsMetricIndependent = metricIndep };
        }

        private static GMacOpInfo CreateBinary(string opName, string opSymbol, bool metricIndep = true)
        {
            return new GMacOpInfo(opName, opSymbol, true) { IsMetricIndependent = metricIndep };
        }
        #endregion


        public string OpName { get; }

        public string OpSymbol { get; }

        public bool IsBinary { get; }

        public bool IsUnary => !IsBinary;

        /// <summary>
        /// If this is true, the operator is independent of the metric for multivector operands
        /// </summary>
        public bool IsMetricIndependent { get; private set; }

        public bool HasSymbol => String.IsNullOrEmpty(OpSymbol) == false;


        private GMacOpInfo(string opName, string opSymbol, bool isBinary)
        {
            OpName = opName;
            OpSymbol = opSymbol ?? String.Empty;
            IsBinary = isBinary;
            IsMetricIndependent = true;
        }


        public override string ToString()
        {
            return OpName;
        }
    }
}
