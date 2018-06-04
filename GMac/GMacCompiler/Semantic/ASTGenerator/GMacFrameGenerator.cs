using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GeometricAlgebraSymbolicsLib.Frames;
using GMac.GMacAPI.CodeGen.BuiltIn.GMac.GMacFrame;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GMac.GMacCompiler.Syntax;
using GMac.GMacMath;
using Irony.Parsing;
using IronyGrammars.DSLException;
using IronyGrammars.SourceCode;
using TextComposerLib.Logs.Progress;
using UtilLib.DataStructures;

namespace GMac.GMacCompiler.Semantic.ASTGenerator
{
    internal sealed partial class GMacFrameGenerator : GMacAstSymbolGenerator
    {
        #region Static Members

        /// <summary>
        /// Translate the given parse tree node into a GMac frame
        /// </summary>
        /// <param name="context"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static GMacFrame Translate(GMacSymbolTranslatorContext context, ParseTreeNode node)
        {
            context.PushState(RoleNames.Frame, node);

            var translator = new GMacFrameGenerator();//new GMacFrameGenerator(context);

            translator.SetContext(context);
            translator.Translate();

            context.PopState();

            //MasterPool.Release(translator);

            var frame = translator._generatedFrame;

            if (frame == null)
                return null;

            if (frame.BaseFrame != null)
                DefineFrameDefaultSymbols(frame.BaseFrame, context);

            DefineFrameDefaultSymbols(frame, context);

            return frame;
        }

        private static void DefineFrameDefaultSymbols(GMacFrame frame, GMacSymbolTranslatorContext context)
        {
            if (GMacCompilerFeatures.DefineFrameDefaultSymbols == false)
                return;

            if (frame.DefaultSymbolsReady)
                return;

            var gmacCode = FrameLibrary.Generate(new AstFrame(frame));

            //context.CompilationLog.Trace.AppendLineAtNewLine("Generating default frame objects GMacDSL code");
            //context.CompilationLog.Trace.AppendLineAtNewLine(gmacCode);

            context.ParentProjectCompiler.CompileGeneratedCode(frame.SymbolAccessName + ".default.gmac", gmacCode);

            frame.DefaultSymbolsReady = true;
        }

        #endregion


        private int _vSpaceDim;

        private GMacFrame _baseFrame;

        private GMacFrame _generatedFrame;


        //public override void ResetOnAcquire()
        //{
        //    base.ResetOnAcquire();

        //    _vSpaceDim = 0;
        //    _baseFrame = null;
        //    _generatedFrame = null;
        //}


        ///Read the list of basis vectors names for the current frame
        private string[] translate_Frame_Vectors(ParseTreeNode node)
        {
            node.Assert(GMacParseNodeNames.FrameVectors);

            var basisVectorsNames = new List<string>();

            foreach (var subnode in node.ChildNodes)
            {
                var basisVectorName = GenUtils.Translate_Identifier(subnode);

                if (basisVectorsNames.Contains(basisVectorName))
                    CompilationLog.RaiseGeneratorError<int>("Basis vector name already defined", subnode);
                
                basisVectorsNames.Add(basisVectorName);
            }

            if (basisVectorsNames.Count > GMacMathUtils.MaxVSpaceDimension)
                CompilationLog.RaiseGeneratorError<int>("Cannot handle spaces with dimension larger than " + GMacMathUtils.MaxVSpaceDimension, node);

            return basisVectorsNames.ToArray();
        }

        ///Set the signature of the current frame to be defined by IPM
        private GaSymFrame translate_Frame_Signature_IPM(ParseTreeNode node)
        {
            //Read the IPM symbolic matrix
            var ipmMatrix = MathematicaMatrix.Create(Cas, GenUtils.Translate_StringLiteral(node.ChildNodes[0]));

            if (ipmMatrix.IsSymmetric() == false || ipmMatrix.RowCount != _vSpaceDim)
                CompilationLog.RaiseGeneratorError<int>("Expecting a square symmetric matrix with " + _vSpaceDim + " rows", node.ChildNodes[0]);

            return GaSymFrame.CreateFromIpm(ipmMatrix);
        }

        ///Set the signature of the current frame to a base frame with a change of basis matrix
        private GaSymFrame translate_Frame_Signature_CBM(ParseTreeNode node)
        {
            var baseFrame = 
                (GMacFrame)GMacValueAccessGenerator.Translate_Direct(Context, node.ChildNodes[0], RoleNames.Frame);

            if (baseFrame.VSpaceDimension != _vSpaceDim)
                CompilationLog.RaiseGeneratorError<int>("Base frame must be of dimension " + _vSpaceDim, node.ChildNodes[0]);

            var cbmMatrix = MathematicaMatrix.Create(Cas, GenUtils.Translate_StringLiteral(node.ChildNodes[1]));

            if (cbmMatrix.IsInvertable() == false || cbmMatrix.RowCount != _vSpaceDim)
                CompilationLog.RaiseGeneratorError<int>("Expecting a square invertable matrix with " + _vSpaceDim + " rows", node.ChildNodes[1]);

            _baseFrame = baseFrame;

            var derivedFrameSystem = GaSymFrame.CreateDerivedCbmFrameSystem(baseFrame.SymbolicFrame, cbmMatrix);

            return derivedFrameSystem.DerivedFrame;
        }

        ///Set the signature of the current frame to a signature vector of +1's and -1's (diagonal IPM)
        private GaSymFrame translate_Frame_Signature_Orthonormal(ParseTreeNode node)
        {
            var bvSigString = GenUtils.Translate_StringLiteral(node.ChildNodes[0]).Trim();

            if (bvSigString.Count(c => c == '+' || c == '-') != _vSpaceDim || bvSigString.Length != _vSpaceDim)
                CompilationLog.RaiseGeneratorError<int>("Expecting a vector of " + _vSpaceDim + @" (+\-) items", node.ChildNodes[0]);

            return GaSymFrame.CreateOrthonormal(bvSigString);
        }

        ///Set the signature of the current frame to a signature vector (diagonal IPM)
        private GaSymFrame translate_Frame_Signature_Orthogonal(ParseTreeNode node)
        {
            var bvSigVector = MathematicaVector.Create(Cas, GenUtils.Translate_StringLiteral(node.ChildNodes[0]));

            if (bvSigVector.Size != _vSpaceDim)
                CompilationLog.RaiseGeneratorError<int>("Expecting a vector with " + _vSpaceDim + " items", node.ChildNodes[0]);

            return GaSymFrame.CreateOrthogonal(bvSigVector);
        }

        ///Set the signature of the current frame to a reciprocal frame
        private GaSymFrame translate_Frame_Signature_Reciprocal(ParseTreeNode node)
        {
            var baseFrame = 
                (GMacFrame)GMacValueAccessGenerator.Translate_Direct(Context, node.ChildNodes[0], RoleNames.Frame);

            if (baseFrame.VSpaceDimension != _vSpaceDim)
                CompilationLog.RaiseGeneratorError<int>("Base frame must be of dimension " + _vSpaceDim, node.ChildNodes[0]);

            _baseFrame = baseFrame;

            var derivedFrameSystem = GaSymFrame.CreateReciprocalCbmFrameSystem(baseFrame.SymbolicFrame);

            return derivedFrameSystem.DerivedFrame;
        }

        ///Select the signature definition method of the current frame
        private GaSymFrame translate_Frame_Signature(ParseTreeNode node)
        {
            var subnode = node.ChildNodes[0];

            switch (subnode.ToString())
            {
                case GMacParseNodeNames.FrameSignatureEuclidean:
                    return GaSymFrame.CreateEuclidean(_vSpaceDim);

                case GMacParseNodeNames.FrameSignatureIpm:
                    return translate_Frame_Signature_IPM(subnode);

                case GMacParseNodeNames.FrameSignatureCbm:
                    return translate_Frame_Signature_CBM(subnode);

                case GMacParseNodeNames.FrameSignatureOrthonormal:
                    return translate_Frame_Signature_Orthonormal(subnode);

                case GMacParseNodeNames.FrameSignatureOrthogonal:
                    return translate_Frame_Signature_Orthogonal(subnode);

                case GMacParseNodeNames.FrameSignatureReciprocal:
                    return translate_Frame_Signature_Reciprocal(subnode);

                default:
                    return CompilationLog.RaiseGeneratorError<GaSymFrame>("Illegal frame signature definition", subnode);
            }
        }

        private void translate_Frame_Subspace(ParseTreeNode node)
        {
            try
            {
                Context.MarkCheckPointState();

                Context.PushState(_generatedFrame.ChildSymbolScope, RoleNames.FrameSubspace);

                //Read the name of the new subspace
                var subspaceName = TranslateChildSymbolName(node.ChildNodes[0]);

                BooleanPattern subspacePattern =
                    GMacFrameSubspacePatternGenerator.Translate(Context, node.ChildNodes[1], _generatedFrame);

                var frameSubspace = _generatedFrame.DefineSubspace(subspaceName, subspacePattern);

                frameSubspace.CodeLocation = Context.GetCodeLocation(node);

                Context.PopState();

                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Frame Subspace: " + frameSubspace.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Frame Subspace Failed: " + _generatedFrame.SymbolAccessName, ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Frame Subspace Failed With Error: " + _generatedFrame.SymbolAccessName, e);
            }
        }

        private void translate_Frame_DerivedFrameSystem(GMacFrame derivedFrame, GMacFrame baseFrame)
        {
            var symbolicDerivedFrame = (GaSymFrameNonOrthogonal)derivedFrame.SymbolicFrame;

            if (ReferenceEquals(baseFrame, null))
            {
                var basisVectorsNames =
                    Enumerable
                    .Range(1, derivedFrame.VSpaceDimension)
                    .Select(i => "e" + i.ToString())
                    .ToArray();

                baseFrame = DefineFrame(
                    derivedFrame.ObjectName + "_ortho_base",
                    basisVectorsNames, 
                    symbolicDerivedFrame.BaseOrthogonalFrame,
                    true
                    );
            }

            var d2BOm = 
                new GMacMultivectorTransform(
                    derivedFrame.ObjectName + "_d2b_om",
                    derivedFrame.ParentScope,
                    derivedFrame,
                    baseFrame,
                    symbolicDerivedFrame.ThisToBaseFrameCba
                    );

            var b2DOm =
                new GMacMultivectorTransform(
                    derivedFrame.ObjectName + "_b2d_om",
                    derivedFrame.ParentScope,
                    baseFrame,
                    derivedFrame,
                    symbolicDerivedFrame.BaseFrameToThisCba
                    );

            derivedFrame.SetDfs(baseFrame, d2BOm, b2DOm);
        }


        private void translate_Frame()
        {
            try
            {
                //Mark the active context state for error recovery
                Context.MarkCheckPointState();

                //Read the name of the new frame
                var frameName = TranslateChildSymbolName(RootParseNode.ChildNodes[0]);

                //Check if the name is already used
                if (Context.ParentNamespace.CanDefineChildSymbol(frameName) == false)
                    CompilationLog.RaiseGeneratorError<int>("Symbol name already used", RootParseNode);

                //Read the basis vectors names to be added later to the frame scope
                var basisVectorsNames = translate_Frame_Vectors(RootParseNode.ChildNodes[1]);

                _vSpaceDim = basisVectorsNames.Length;

                //Read and construct the attached symbolic GA frame
                var attachedSymbolicFrame = translate_Frame_Signature(RootParseNode.ChildNodes[2]);

                //Define the frame and all its default sub-objects
                _generatedFrame = DefineFrame(frameName, basisVectorsNames, attachedSymbolicFrame, false);

                //Set the derived frame system information for this derived frame
                if (_generatedFrame.SymbolicFrame.IsNonOrthogonal || ReferenceEquals(_baseFrame, null) == false)
                    translate_Frame_DerivedFrameSystem(_generatedFrame, _baseFrame);

                _generatedFrame.CodeLocation = Context.GetCodeLocation(RootParseNode);

                //Define all default sub-objects for the generated frame
                DefineFrameDefaultObjects(_generatedFrame);

                //Unmark the active state
                Context.UnmarkCheckPointState();
                Context.CompilationLog.ReportNormal("Translated Frame: " + _generatedFrame.SymbolAccessName, ProgressEventArgsResult.Success);
            }
            catch (CompilerException)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportNormal("Translate Frame", ProgressEventArgsResult.Failure);
            }
            catch (Exception e)
            {
                Context.RestoreToCheckPointState();
                Context.CompilationLog.ReportError("Translate Frame Failed With Error", e);
            }
        }

        protected override void Translate()
        {
            var progressId = Context.CompilationLog.ReportStart("Translate Frame");

            //Translate the main information of the frame
            translate_Frame();

            if (_generatedFrame == null) 
                return;

            //Translate the frame subspaces
            var nodeFrameSubspaceList = RootParseNode.ChildNodes[3];

            foreach (var nodeFrameSubspace in nodeFrameSubspaceList.ChildNodes)
                translate_Frame_Subspace(nodeFrameSubspace);

            Context.CompilationLog.ReportFinish(progressId);
        }
    }
}
