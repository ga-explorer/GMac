using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.SourceCode;
using DataStructuresLib.BooleanPattern;
using GeometricAlgebraStructuresLib.Frames;
using GeometricAlgebraSymbolicsLib.Frames;
using GMac.GMacCompiler.Semantic.AST;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed partial class GMacFrameGenerator
    {
        /// <summary>
        /// Create the default constant I for this frame (the unit pseudoscalar)
        /// </summary>
        /// <returns></returns>
        public GMacConstant DefineDefaultConstants_I(GMacFrame frame)
        {
            var finalExpr = GMacValueMultivector.CreateBasisBlade(frame.MultivectorType, frame.MaxBasisBladeId);

            var constant = frame.DefineFrameConstant("I", finalExpr);

            constant.AddCodeLocation(Context.GetCodeLocation(frame.ParseNode));

            return constant;
        }

        /// <summary>
        /// Create the default constants for this frame
        /// </summary>
        public void DefineDefaultConstants(GMacFrame frame)
        {
            DefineDefaultConstants_I(frame);
        }

        ///// <summary>
        ///// Create the default subspace of pseudo scalars named @pseudoscalars@
        ///// </summary>
        ///// <returns></returns>
        //private GMacFrameSubspace DefineDefaultSubspaces_PseudoScalar(GMacFrame frame)
        //{
        //    const string subspaceName = @"pseudoscalars";

        //    var idsList = new List<int> { frame.MaxBasisBladeId };

        //    var subspaceSignature = BooleanPattern.CreateFromTrueIndexes(frame.GaSpaceDimension, idsList);

        //    var subspace = frame.DefineSubspace(subspaceName, subspaceSignature);

        //    subspace.AddCodeLocation(Context.GetCodeLocation(frame.ParseNode));

        //    return subspace;
        //}

        /// <summary>
        /// Create the default subspace of even multivectors named @even@
        /// </summary>
        /// <returns></returns>
        private GMacFrameSubspace DefineDefaultSubspaces_Even(GMacFrame frame)
        {
            var idsList = new List<int>((int)frame.GaSpaceDimension);

            for (var grade = 0; grade <= frame.VSpaceDimension; grade = grade + 2)
            {
                var basisCount = GaFrameUtils.KvSpaceDimension(frame.VSpaceDimension, grade);

                for (var index = 0UL; index < basisCount; index++)
                    idsList.Add((int)GaFrameUtils.BasisBladeId(grade, index));
            }

            const string subspaceName = "Even";
            var subspaceSignature = BooleanPattern.CreateFromTrueIndexes((int)frame.GaSpaceDimension, idsList);

            var subspace = frame.DefineSubspace(subspaceName, subspaceSignature);

            subspace.AddCodeLocation(Context.GetCodeLocation(frame.ParseNode));

            return subspace;
        }

        /// <summary>
        /// Create the default subspace of odd multivectors named @odd@
        /// </summary>
        /// <returns></returns>
        private GMacFrameSubspace DefineDefaultSubspaces_Odd(GMacFrame frame)
        {
            var idsList = new List<int>((int)frame.GaSpaceDimension);

            for (var grade = 1; grade <= frame.VSpaceDimension; grade = grade + 2)
            {
                var basisCount = GaFrameUtils.KvSpaceDimension(frame.VSpaceDimension, grade);

                for (var index = 0UL; index < basisCount; index++)
                    idsList.Add((int)GaFrameUtils.BasisBladeId(grade, index));
            }

            const string subspaceName = "Odd";
            var subspaceSignature = BooleanPattern.CreateFromTrueIndexes((int)frame.GaSpaceDimension, idsList);

            var subspace = frame.DefineSubspace(subspaceName, subspaceSignature);

            subspace.AddCodeLocation(Context.GetCodeLocation(frame.ParseNode));

            return subspace;
        }

        /// <summary>
        /// Create a default subspaces of k-vectors with a given grade and name
        /// </summary>
        private void DefineDefaultSubspaces_KVectors(GMacFrame frame, int grade, string subspaceName)
        {
            var basisCount = GaFrameUtils.KvSpaceDimension(frame.VSpaceDimension, grade);
            var idsList = new List<int>((int)basisCount);

            for (var index = 0UL; index < basisCount; index++)
                idsList.Add((int)GaFrameUtils.BasisBladeId(grade, index));

            var subspaceSignature = BooleanPattern.CreateFromTrueIndexes((int)frame.GaSpaceDimension, idsList);

            var subspace = frame.DefineSubspace(subspaceName, subspaceSignature);

            subspace.AddCodeLocation(Context.GetCodeLocation(frame.ParseNode));
        }

        /// <summary>
        /// Create the default subspaces of k-vectors named @G0@, @G1@, @G2@, ... etc.
        /// </summary>
        private void DefineDefaultSubspaces_KVectors(GMacFrame frame)
        {
            for (var grade = 0; grade <= frame.VSpaceDimension; grade++)
                DefineDefaultSubspaces_KVectors(frame, grade, "G" + grade);

            DefineDefaultSubspaces_KVectors(frame, 0, "Scalar");
            DefineDefaultSubspaces_KVectors(frame, 1, "Vector");
            DefineDefaultSubspaces_KVectors(frame, 2, "Bivector");
            DefineDefaultSubspaces_KVectors(frame, frame.VSpaceDimension - 1, "PseudoVector");
            DefineDefaultSubspaces_KVectors(frame, frame.VSpaceDimension, "PseudoScalar");
        }

        /// <summary>
        /// Create the Full GA subspace named @ga@
        /// </summary>
        /// <returns></returns>
        private GMacFrameSubspace DefineDefaultSubspaces_FullGA(GMacFrame frame)
        {
            var idsList = Enumerable.Range(0, (int)frame.GaSpaceDimension).ToList();

            const string subspaceName = "GA";
            var subspaceSignature = BooleanPattern.CreateFromTrueIndexes((int)frame.GaSpaceDimension, idsList);

            var subspace = frame.DefineSubspace(subspaceName, subspaceSignature);

            subspace.AddCodeLocation(Context.GetCodeLocation(frame.ParseNode));

            return subspace;
        }

        /// <summary>
        /// Create the default subspaces for this frame
        /// </summary>
        public void DefineDefaultSubspaces(GMacFrame frame)
        {
            //DefineDefaultSubspaces_PseudoScalar(frame);
            DefineDefaultSubspaces_KVectors(frame);
            DefineDefaultSubspaces_Even(frame);
            DefineDefaultSubspaces_Odd(frame);
            DefineDefaultSubspaces_FullGA(frame);
        }

        private GMacFrame DefineFrameDefaultObjects(GMacFrame frame)
        {
            DefineDefaultSubspaces(frame);

            DefineDefaultConstants(frame);

            //DefineFrameDefaultSymbols(frame);

            return frame;
        }

        private GMacFrame DefineFrame(string frameName, string[] basisVectorsNames, GaSymFrame attachedSymbolicFrame, bool defineDefaultObjects)
        {
            if (Context.GMacRootAst.FramesCount + 1 > GMacCompilerFeatures.MaxFramesNumber)
                CompilationLog.RaiseGeneratorError<int>($"Can't define more than {GMacCompilerFeatures.MaxFramesNumber} frames", RootParseNode);

            var frame = Context.ParentNamespace.DefineFrame(frameName, basisVectorsNames, attachedSymbolicFrame);

            if (defineDefaultObjects)
                DefineFrameDefaultObjects(frame);

            return frame;
        }
    }
}
