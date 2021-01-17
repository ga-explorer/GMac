using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricAlgebraStructuresLib.Frames;
using GMac.GMacAPI.CodeGen;
using GMac.GMacAPI.Target;
using GMac.GMacAST;
using GMac.GMacAST.Expressions;
using GMac.GMacAST.Symbols;
using GMac.GMacCompiler;
using GMac.GMacCompiler.Semantic.ASTConstants;
using GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass;
using GradedMultivectorsLibraryComposer.Composers.CSharp.OutermorphismClass;
using TextComposerLib.Logs.Progress;
using TextComposerLib.Text.Parametric;
using TextComposerLib.Text.Structured;
using ClassFileGenerator = GradedMultivectorsLibraryComposer.Composers.CSharp.KVectorClass.ClassFileGenerator;

namespace GradedMultivectorsLibraryComposer.Composers.CSharp
{
    public sealed partial class CodeLibraryComposer 
        : GMacCodeLibraryComposer
    {
        /// <summary>
        /// Generate for a single frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="generateMacroCode"></param>
        /// <returns></returns>
        public static CodeLibraryComposer Generate(AstFrame frame, bool generateMacroCode = true)
        {
            var libGen = new CodeLibraryComposer(frame.Root)
            {
                MacroGenDefaults = {AllowGenerateMacroCode = generateMacroCode}
            };


            libGen.SelectedSymbols.Add(frame);

            libGen.Generate();

            return libGen;
        }


        internal int MaxTargetLocalVars => 16;

        internal UniqueNameFactory UniqueNameGenerator { get; }


        internal AstFrame CurrentFrame { get; private set; }

        internal string CurrentFrameName { get; private set; }

        internal GMacTempSymbolCompiler TempSymbolsCompiler { get; }


        public override string Name 
            => "Graded Multivectors Library Generator";

        public override string Description 
            => "Generate multiple files holding a library for processing multivectors of a given GA frame. A Multivector is represented using zero or more non-zero k-vectors";


        public CodeLibraryComposer(AstRoot astInfo)
            : base(astInfo, GMacLanguageServer.CSharp4())
        {
            MacroGenDefaults = new GMacMacroCodeComposerDefaults(this);

            UniqueNameGenerator = new UniqueNameFactory() { IndexFormatString = "X4" };

            TempSymbolsCompiler = new GMacTempSymbolCompiler();
        }



        //private GMacInfoMacro AddEuclideanGeometricProductDualGMacMacro()
        //{
        //    var codeText =
        //        Templates["egp_dual_macro"].GenerateUsing(CurrentFrameName);

        //    var gmacMacro =
        //        _tempSymbolsCompiler.CompileMacro(
        //            codeText,
        //            _currentFrame.AssociatedFrame.ChildScope
        //            );

        //    return new GMacInfoMacro(gmacMacro);
        //}

        //private GMacInfoMacro AddSelfEuclideanGeometricProductGMacMacro()
        //{
        //    var codeText =
        //        Templates["self_egp_macro"].GenerateUsing(CurrentFrameName);

        //    var gmacMacro =
        //        _tempSymbolsCompiler.CompileMacro(
        //            codeText,
        //            _currentFrame.AssociatedFrame.ChildScope
        //            );

        //    return new GMacInfoMacro(gmacMacro);
        //}

        internal AstMacro AddVectorsOuterProductGMacMacro()
        {
            var vopInputsText = new ListTextComposer(", ") { ActiveItemPrefix = "v", ActiveItemSuffix = " : Multivector" };
            var vopExprText = new ListTextComposer(" ^ ") { ActiveItemPrefix = "v" };

            for (var num = 0; num < CurrentFrame.VSpaceDimension; num++)
            {
                vopInputsText.Add(num);
                vopExprText.Add(num);
            }

            var gmacDslCode =
                Templates["vectors_op_macro"]
                .GenerateText(
                    "frame", CurrentFrameName,
                    "vop_inputs", vopInputsText,
                    "vop_expr", vopExprText
                    );

            return AddGMacMacro(gmacDslCode);
        }

        private AstStructure AddFactorGMacStructure()
        {
            var membersText =
                new ListTextComposer("," + Environment.NewLine);

            for (var num = 1; num <= CurrentFrame.VSpaceDimension; num++)
                membersText.Add(Templates["factor_struct_member"], "num", num, "frame", CurrentFrameName);

            var gmacDslCode =
                Templates["factor_struct"]
                .GenerateText("frame", CurrentFrameName, "members", membersText);

            return AddGMacStructure(gmacDslCode);
        }

        private List<AstMacro> AddFactorGMacMacros()
        {
            var gmacMacroInfoList = new List<AstMacro>(CurrentFrame.VSpaceDimension - 1);

            for (var grade = 2; grade <= CurrentFrame.VSpaceDimension; grade++)
            {
                var stepsText = new ListTextComposer(Environment.NewLine);

                for (var num = 1; num < grade; num++)
                    stepsText.Add(Templates["factor_macro_step"], "num", num);

                var gmacDslCode =
                    Templates["factor_macro"]
                    .GenerateText("frame", CurrentFrameName, "num", grade, "steps", stepsText);

                gmacMacroInfoList.Add(
                    AddGMacMacro(gmacDslCode)
                );
            }

            return gmacMacroInfoList;
        }

        private AstStructure AddGMacStructure(string gmacDslCode)
        {
            TempSymbolsCompiler.ScopeResolutionContext.Clear(CurrentFrame);

            return TempSymbolsCompiler.CompileStructure(gmacDslCode);
        }
        
        private AstMacro AddGMacMacro(string gmacDslCode)
        {
            TempSymbolsCompiler.ScopeResolutionContext.Clear(CurrentFrame);

            return TempSymbolsCompiler.CompileMacro(gmacDslCode);
        }

        //private string BasisVectorIdToCoef(int id)
        //{
        //    return (GaUtils.ID_To_Index(id) - 1).ToString();
        //}

        internal string GetBinaryFunctionName(string baseName, int inGrade1, int inGrade2, int outGrade)
        {
            return baseName + "_" + inGrade1.ToString("X1") + inGrade2.ToString("X1") + outGrade.ToString("X1");
        }

        internal string GetBinaryFunctionName(string baseName, int inGrade1, int inGrade2)
        {
            return baseName + "_" + inGrade1.ToString("X1") + inGrade2.ToString("X1");
        }

        //internal string GetBinaryFunctionName(string baseName, int grade)
        //{
        //    return baseName + "_" + grade.ToString("X1");
        //}

        internal string BasisBladeIdToTargetArrayItem(string arrayVarName, int basisBladeId)
        {
            return
                new StringBuilder()
                .Append(arrayVarName)
                .Append("[")
                .Append(CurrentFrame.BasisBladeIndex(basisBladeId))
                .Append("]")
                .ToString();
        }

        //internal void SetBladeParameterBinding(GMacMacroBinding macroBinding, string macroParamName, int macroParamGrade)
        //{
        //    macroBinding.BindMultivectorToVariables(macroParamName, macroParamGrade);
        //}

        internal void SetBasisBladeToArrayNaming(GMacTargetVariablesNaming targetNaming, AstDatastoreValueAccess macroParam, int macroParamGrade, string arrayVarName)
        {
            targetNaming.SetMultivectorParameters(
                macroParam, 
                macroParamGrade,
                id => BasisBladeIdToTargetArrayItem(arrayVarName, id)
                );
        }

        internal void SetBasisBladeToArrayNaming(GMacTargetVariablesNaming targetNaming, string macroParamName, int macroParamGrade, string arrayVarName)
        {
            targetNaming.SetMultivectorParameters(
                macroParamName + ".@G" + macroParamGrade + "@",
                id => BasisBladeIdToTargetArrayItem(arrayVarName, id)
                );
        }


        private void GenerateKVectorClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "kVector.cs");

            var fileGen = new ClassFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            //Note: You can use this method instead to finalize, save, free memory, 
            //and unselect the active file composer in one step during code generation 
            //when the you are done with the file:
            //CodeFilesComposer.SaveActiveFile();
        }

        private void GenerateKVectorStaticFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "kVectorStatic.cs");

            var fileGen = new StaticCodeFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorIsZeroMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("IsZero.cs");

            var fileGen = new IsZeroMethodsFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorEqualsMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Equals.cs");

            var fileGen = new EqualsMethodsFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorInvolutionMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Involutions.cs");

            var fileGen = new InvolutionMethodsFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorNormMethodsFile()
        {
            var operatorNames = new[]
            {
                DefaultMacro.MetricUnary.NormSquared,
                DefaultMacro.MetricUnary.Magnitude,
                DefaultMacro.MetricUnary.MagnitudeSquared,
                DefaultMacro.EuclideanUnary.Magnitude,
                DefaultMacro.EuclideanUnary.MagnitudeSquared
            };

            CodeFilesComposer.InitalizeFile("Norms.cs");

            var fileGen = new NormMethodsFileGenerator(this, operatorNames);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorSdfMethodsFile()
        {
            //Make sure the PointToMultivector3D macro is defined inside the frame
            var pointToMultivectorMacro =
                CurrentFrame.Macros.FirstOrDefault(
                    m => m.Name == "PointToMultivector3D"
                );

            if (ReferenceEquals(pointToMultivectorMacro, null))
                return;

            var sdfRayStepResultStruct = AddGMacStructure(
                "structure SdfRayStepResult (sdf0 : scalar, sdf1 : scalar)"
            );

            var sdfNormalResultStruct = AddGMacStructure(
                "structure SdfNormalResult (d1 : scalar, d2 : scalar, d3 : scalar, d4 : scalar)"
            );

            var sdfOpnsMacroCode = @"
macro #frame#.SdfOpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
begin
    let mv1 = PointToMultivector3D(x, y, z) op mv
    
    return mv1 sp reverse(mv1)
end
".Replace("#frame#", CurrentFrameName);

            var sdfIpnsMacroCode = @"
macro #frame#.SdfIpns(mv : Multivector, x : scalar, y : scalar, z : scalar) : scalar
begin
    let mv1 = PointToMultivector3D(x, y, z) lcp mv
    
    return mv1 sp reverse(mv1)
end
".Replace("#frame#", CurrentFrameName);

            var operatorNames = new[]
            {
                DefaultMacro.MetricUnary.NormSquared,
                DefaultMacro.MetricUnary.Magnitude,
                DefaultMacro.MetricUnary.MagnitudeSquared,
                DefaultMacro.EuclideanUnary.Magnitude,
                DefaultMacro.EuclideanUnary.MagnitudeSquared
            };

            CodeFilesComposer.InitalizeFile("ScalarDistanceFunctions.cs");

            var fileGen = new SdfMethodsFileGenerator(this, operatorNames);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateKVectorMiscMethodsFile()
        {
            CodeFilesComposer.InitalizeFile("Misc.cs");

            var fileGen = new MiscMethodsFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        internal void GenerateBilinearProductMethodFile(string opName, string methodName, int inGrade1, int inGrade2, int outGrade)
        {
            CodeFilesComposer.DownFolder(opName);

            CodeFilesComposer.InitalizeFile(methodName + ".cs");

            var fileGen =
                new BilinearProductMethodFileGenerator(
                    this,
                    opName,
                    methodName,
                    inGrade1,
                    inGrade2,
                    outGrade
                    );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        private void GenerateBilinearProductMainMethodFile(string opName, string zeroCondition, Func<int, int, int> getFinalGrade, Func<int, int, bool> isLegalGrade)
        {
            CodeFilesComposer.InitalizeFile(opName + ".cs");

            var fileGen =
                new BilinearProductMainMethodFileGenerator(
                    this,
                    opName,
                    zeroCondition,
                    getFinalGrade,
                    isLegalGrade
                    );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateVectorsOuterProductFile()
        {
            var macroInfo = AddVectorsOuterProductGMacMacro();

            if (macroInfo.IsNullOrInvalid()) return;

            CodeFilesComposer.InitalizeFile("VectorsOP.cs");

            var fileGen = new VectorsOpMethodsFileGenerator(this, macroInfo);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateOuterProductFiles()
        {
            const string opName = DefaultMacro.EuclideanBinary.OuterProduct;

            foreach (var inGrade1 in CurrentFrame.Grades())
                foreach (var inGrade2 in CurrentFrame.Grades())
                {
                    var outGrade = inGrade1 + inGrade2;

                    if (CurrentFrame.IsValidGrade(outGrade) == false)
                        continue;

                    GenerateBilinearProductMethodFile(
                        opName,
                        GetBinaryFunctionName(opName, inGrade1, inGrade2, outGrade),
                        inGrade1,
                        inGrade2,
                        outGrade
                        );
                }

            GenerateBilinearProductMainMethodFile(
                    opName,
                    "Grade + blade2.Grade > " + CurrentFrame.VSpaceDimension,
                    (g1, g2) => g1 + g2,
                    (g1, g2) => g1 + g2 <= CurrentFrame.VSpaceDimension
                    );

            GenerateVectorsOuterProductFile();
        }

        private void GenerateLeftContractionProductFiles()
        {
            const string opName = DefaultMacro.MetricBinary.LeftContractionProduct;

            foreach (var inGrade1 in CurrentFrame.Grades())
                foreach (var inGrade2 in CurrentFrame.Grades())
                {
                    var outGrade = inGrade2 - inGrade1;

                    if (CurrentFrame.IsValidGrade(outGrade) == false)
                        continue;

                    GenerateBilinearProductMethodFile(
                        opName,
                        GetBinaryFunctionName(opName, inGrade1, inGrade2, outGrade),
                        inGrade1,
                        inGrade2,
                        outGrade
                        );
                }

            GenerateBilinearProductMainMethodFile(
                opName,
                "Grade > blade2.Grade",
                (g1, g2) => g2 - g1,
                (g1, g2) => g1 <= g2
                );
        }

        private void GenerateEuclideanLeftContractionProductFiles()
        {
            const string opName = DefaultMacro.EuclideanBinary.LeftContractionProduct;

            foreach (var inGrade1 in CurrentFrame.Grades())
                foreach (var inGrade2 in CurrentFrame.Grades())
                {
                    var outGrade = inGrade2 - inGrade1;

                    if (CurrentFrame.IsValidGrade(outGrade) == false)
                        continue;

                    GenerateBilinearProductMethodFile(
                        opName,
                        GetBinaryFunctionName(opName, inGrade1, inGrade2, outGrade),
                        inGrade1,
                        inGrade2,
                        outGrade
                        );
                }

            GenerateBilinearProductMainMethodFile(
                opName,
                "Grade > blade2.Grade",
                (g1, g2) => g2 - g1,
                (g1, g2) => g1 <= g2
                );
        }

        private void GenerateRightContractionProductFiles()
        {
            const string opName = DefaultMacro.MetricBinary.RightContractionProduct;

            foreach (var inGrade1 in CurrentFrame.Grades())
                foreach (var inGrade2 in CurrentFrame.Grades())
                {
                    var outGrade = inGrade1 - inGrade2;

                    if (CurrentFrame.IsValidGrade(outGrade) == false)
                        continue;

                    GenerateBilinearProductMethodFile(
                        opName,
                        GetBinaryFunctionName(opName, inGrade1, inGrade2, outGrade),
                        inGrade1,
                        inGrade2,
                        outGrade
                        );
                }

            GenerateBilinearProductMainMethodFile(
                opName,
                "Grade < blade2.Grade",
                (g1, g2) => g1 - g2,
                (g1, g2) => g1 >= g2
                );
        }

        private void GenerateEuclideanRightContractionProductFiles()
        {
            const string opName = DefaultMacro.EuclideanBinary.RightContractionProduct;

            foreach (var inGrade1 in CurrentFrame.Grades())
                foreach (var inGrade2 in CurrentFrame.Grades())
                {
                    var outGrade = inGrade1 - inGrade2;

                    if (CurrentFrame.IsValidGrade(outGrade) == false)
                        continue;

                    GenerateBilinearProductMethodFile(
                        opName,
                        GetBinaryFunctionName(opName, inGrade1, inGrade2, outGrade),
                        inGrade1,
                        inGrade2,
                        outGrade
                        );
                }

            GenerateBilinearProductMainMethodFile(
                opName,
                "Grade < blade2.Grade",
                (g1, g2) => g1 - g2,
                (g1, g2) => g1 >= g2
                );
        }

        private void GenerateScalarProductFiles()
        {
            const string opName = DefaultMacro.MetricBinary.ScalarProduct;

            foreach (var inGrade in CurrentFrame.Grades())
            {
                GenerateBilinearProductMethodFile(
                    opName,
                    GetBinaryFunctionName(opName, inGrade, inGrade, 0),
                    inGrade,
                    inGrade,
                    0
                    );
            }

            GenerateBilinearProductMainMethodFile(
                opName,
                "Grade != blade2.Grade",
                (g1, g2) => g1 - g2,
                (g1, g2) => g1 == g2
                );
        }

        private void GenerateEuclideanScalarProductFiles()
        {
            const string opName = DefaultMacro.EuclideanBinary.ScalarProduct;

            foreach (var inGrade in CurrentFrame.Grades())
            {
                GenerateBilinearProductMethodFile(
                    opName,
                    GetBinaryFunctionName(opName, inGrade, inGrade, 0),
                    inGrade,
                    inGrade,
                    0
                    );
            }

            GenerateBilinearProductMainMethodFile(
                opName,
                "Grade != blade2.Grade",
                (g1, g2) => g1 - g2,
                (g1, g2) => g1 == g2
                );
        }

        private void GenerateGeometricProductFiles()
        {
            const string opName = DefaultMacro.MetricBinary.GeometricProduct;

            var codeGen = new GpFilesGenerator(this, opName, false);

            codeGen.Generate();
        }

        private void GenerateEuclideanGeometricProductFiles()
        {
            const string opName = DefaultMacro.EuclideanBinary.GeometricProduct;

            var codeGen = new GpFilesGenerator(this, opName, false);

            codeGen.Generate();
        }

        private void GenerateEuclideanGeometricProductDualFiles()
        {
            const string opName = DefaultMacro.EuclideanBinary.GeometricProductDual;

            var codeGen = new GpFilesGenerator(this, opName, true);

            codeGen.Generate();
        }

        private void GenerateDeltaProductFile()
        {
            const string opName = "DP";

            CodeFilesComposer.InitalizeFile(opName + ".cs");

            var codeGen = new DpMethodsFileGenerator(this, opName);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateDeltaProductDualFile()
        {
            const string opName = "DPDual";

            CodeFilesComposer.InitalizeFile(opName + ".cs");

            var codeGen = new DpDualMethodsFileGenerator(this, opName);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateSelfEuclideanGeometricProductFile()
        {
            const string opName = DefaultMacro.EuclideanUnary.SelfGeometricProduct;

            CodeFilesComposer.InitalizeFile(opName + ".cs");

            var codeGen = new SelfEgpMethodsFileGenerator(this, opName);

            codeGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateApplyVersorFunction(string opName, string funcName, int inGrade1, int inGrade2, int outGrade)
        {
            CodeFilesComposer.DownFolder(opName);

            CodeFilesComposer.InitalizeFile(funcName + ".cs");

            var fileGen = new ApplyVersorMethodFileGenerator(
                this,
                opName,
                funcName,
                inGrade1,
                inGrade2,
                outGrade
                );

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        private void GenerateApplyVersorMainMethod(string opName)
        {
            CodeFilesComposer.InitalizeFile(opName + ".cs");

            var fileGen = new ApplyVersorMainMethodFileGenerator(this, opName);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateApplyVersorFiles()
        {
            var opNames = new[]
            {
                DefaultMacro.MetricVersor.Apply,
                DefaultMacro.MetricVersor.ApplyRotor,
                DefaultMacro.MetricVersor.ApplyReflector,
                DefaultMacro.EuclideanVersor.Apply,
                DefaultMacro.EuclideanVersor.ApplyRotor,
                DefaultMacro.EuclideanVersor.ApplyReflector
            };

            foreach (var opName in opNames)
            {
                foreach (var inGrade1 in CurrentFrame.Grades())
                    foreach (var inGrade2 in CurrentFrame.Grades())
                    {
                        var outGrade = inGrade2;

                        GenerateApplyVersorFunction(
                            opName,
                            GetBinaryFunctionName(opName, inGrade1, inGrade2, outGrade),
                            inGrade1,
                            inGrade2,
                            outGrade
                            );
                    }

                GenerateApplyVersorMainMethod(opName);
            }
        }

        private void GenerateFactorMethod(int inGrade, int inId, AstMacro gmacMacroInfo)
        {
            CodeFilesComposer.DownFolder("Factor");

            CodeFilesComposer.InitalizeFile("Factor" + inId + ".cs");

            var fileGen = new FactorMethodFileGenerator(this, inGrade, inId, gmacMacroInfo);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            CodeFilesComposer.UpFolder();
        }

        private void GenerateFactorFiles()
        {
            var gmacStructInfo = AddFactorGMacStructure();

            if (gmacStructInfo.IsNullOrInvalid()) 
                return;

            var factorMacroList = AddFactorGMacMacros();

            if (factorMacroList.Any(s => s.IsNullOrInvalid())) 
                return;

            CodeFilesComposer.InitalizeFile("Factor.cs");

            var fileGen = new FactorMainMethodsFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();

            for (var inGrade = 2; inGrade <= CurrentFrame.VSpaceDimension; inGrade++)
                for (var inIndex = 0; inIndex < CurrentFrame.KvSpaceDimension(inGrade); inIndex++)
                {
                    var inId = CurrentFrame.BasisBladeId(inGrade, inIndex);

                    GenerateFactorMethod(inGrade, inId, factorMacroList[inGrade - 2]);
                }

        }

        //TODO: Meet and join algorithm without factorization
        //Given blades A and B of grades gA and gB
        //
        //EGPDual_gAgBgX is given by ((A gp B) lcp reverse(I)).@GgX@
        //
        //1) find grade of C = (A dp B) call it gC. Use DPGrade() function
        //2) compute the blade D = EGPDual_gAgBgD(A, B) with grade gD = n - gC
        //that is, D = dual(A dp B)
        //3) find grade of S = D dp A call it gS
        //4) compute the blade S = EGP_gDgAgS(S, A) with grade gS
        //that is, S = dual(A dp B) dp A
        //5) Meet = S lcp B
        //6) Join = S ^ B

        private void GenerateKVectorFiles()
        {
            Progress.Enabled = true;
            var progressId = this.ReportStart(
                "Generating KVector class code files for frame " + CurrentFrame.AccessName
                );

            GenerateKVectorClassFile();

            GenerateKVectorStaticFile();

            CodeFilesComposer.DownFolder("KVectors");

            GenerateKVectorIsZeroMethodsFile();

            GenerateKVectorEqualsMethodsFile();

            GenerateKVectorInvolutionMethodsFile();

            GenerateKVectorNormMethodsFile();

            GenerateKVectorSdfMethodsFile();

            GenerateKVectorMiscMethodsFile();


            GenerateOuterProductFiles();

            GenerateLeftContractionProductFiles();

            GenerateEuclideanLeftContractionProductFiles();

            GenerateRightContractionProductFiles();

            GenerateEuclideanRightContractionProductFiles();


            GenerateScalarProductFiles();

            GenerateEuclideanScalarProductFiles();


            GenerateGeometricProductFiles();

            GenerateEuclideanGeometricProductFiles();

            GenerateEuclideanGeometricProductDualFiles();


            GenerateDeltaProductFile();

            GenerateDeltaProductDualFile();

            GenerateSelfEuclideanGeometricProductFile();

            //GenerateApplyVersorFiles();

            GenerateFactorFiles();

            //TODO: Create blade subroutines to reflect blades in other blades:
            /*
                If A, B are blades, we can define several blade-redlection GA relations (table 7.1):

                //This relation does not take orientation of resulting blade into consideration
                A.ReflectInBlade(B) => B gp A gp reverse(B)/norm2(B)

                //These take the orientation of the result into consideration
                A.DirectReflectInDirectBlade(B) => -1 ^ (grade(A) * [grade(B) + 1]) * A.ReflectInBlade(B)
                A.DirectReflectInDualBlade(B) => -1 ^ (grade(A) * grade(B)) * A.ReflectInBlade(B)
                A.DualReflectInDirectBlade(B) => -1 ^ ([n - 1] * [grade(A) + 1] * [grade(B) + 1]) * A.ReflectInBlade(B)
                A.DualReflectInDualBlade(B) => -1 ^ ([grade(A) + 1] * grade(B)) * A.ReflectInBlade(B)
             */

            CodeFilesComposer.UpFolder();

            Progress.Enabled = true;
            this.ReportFinish(progressId);
        }


        private void GenerateFrameUtilsClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "Utils.cs");

            var fileGen = new FrameUtilsClass.ClassFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateFrameUtilsFiles()
        {
            Progress.Enabled = true;
            var progressId = this.ReportStart(
                "Generating Frame Utils class code files for frame " + CurrentFrame.AccessName
            );

            GenerateFrameUtilsClassFile();

            CodeFilesComposer.DownFolder("FrameUtils");

            CodeFilesComposer.UpFolder();

            Progress.Enabled = true;
            this.ReportFinish(progressId);
        }


        private void GenerateMultivectorClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "Multivector.cs");

            var fileGen = new MultivectorClass.ClassFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateMultivectorFiles()
        {
            Progress.Enabled = true;
            var progressId = this.ReportStart(
                "Generating Multivector class code files for frame " + CurrentFrame.AccessName
            );

            GenerateMultivectorClassFile();

            CodeFilesComposer.DownFolder("Multivectors");

            CodeFilesComposer.UpFolder();

            Progress.Enabled = true;
            this.ReportFinish(progressId);
        }


        private void GenerateVectorClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "Vector.cs");

            var fileGen = new VectorClass.ClassFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateVectorFiles()
        {
            var progressId = this.ReportStart(
                "Generating Vector class code files for frame " + CurrentFrame.AccessName
                );

            GenerateVectorClassFile();

            CodeFilesComposer.DownFolder("Vectors");

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }


        private void GenerateFactoredBladeClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "FactoredBlade.cs");

            var fileGen = new FactoredBladeClass.ClassFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateFactoredBladeFiles()
        {
            var progressId = this.ReportStart(
                "Generating FactoredBlade class code files for frame " + CurrentFrame.AccessName
                );

            GenerateFactoredBladeClassFile();

            CodeFilesComposer.DownFolder("FactoredBlades");

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }


        private void GenerateOutermorphismClassFile()
        {
            CodeFilesComposer.InitalizeFile(CurrentFrameName + "Outermorphism.cs");

            var fileGen = new OutermorphismClass.ClassFileGenerator(this);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateOutermorphismApplyMethodFile(int inGrade)
        {
            CodeFilesComposer.InitalizeFile("Map_" + inGrade + ".cs");

            var fileGen = new MapMethodFileGenerator(this, inGrade);

            fileGen.Generate();

            CodeFilesComposer.UnselectActiveFile();
        }

        private void GenerateOutermorphismFiles()
        {
            var progressId = this.ReportStart(
                "Generating Outermorphism class code files for frame " + CurrentFrame.AccessName
                );

            GenerateOutermorphismClassFile();

            CodeFilesComposer.DownFolder("Outermorphisms");

            CodeFilesComposer.DownFolder("Map");

            for (var inGrade = 1; inGrade <= CurrentFrame.VSpaceDimension; inGrade++)
                GenerateOutermorphismApplyMethodFile(inGrade);

            CodeFilesComposer.UpFolder();

            CodeFilesComposer.UpFolder();

            this.ReportFinish(progressId);
        }


        private void GenerateFrameCode(AstFrame frameInfo)
        {
            Progress.Enabled = true;
            var progressId = this.ReportStart(
                "Generating code files for frame " + frameInfo.AccessName
                );

            CurrentFrame = frameInfo;

            CurrentFrameName = GetSymbolTargetName(CurrentFrame);

            CodeFilesComposer.DownFolder(CurrentFrameName);


            GenerateFrameUtilsFiles();

            GenerateMultivectorFiles();

            GenerateKVectorFiles();

            GenerateVectorFiles();

            GenerateFactoredBladeFiles();

            GenerateOutermorphismFiles();


            CodeFilesComposer.UpFolder();

            Progress.Enabled = true;
            this.ReportFinish(progressId);
        }


        internal void SetTargetTempVariablesNames(GMacTargetVariablesNaming targetNaming)
        {
            //Temp variables target naming
            if (targetNaming.CodeBlock.TargetTempVarsCount > MaxTargetLocalVars)
            {
                //Name as array items
                targetNaming.SetTempVariables((int index) => "tempArray[" + index + "]");
            }
            else
            {
                //Name as set of local variables
                targetNaming.SetTempVariables(index => "tempVar" + index.ToString("X4") + "");
            }
        }

        private bool GeneratePreComputationsCode(GMacMacroCodeComposer macroCodeGen)
        {
            //Generate comments
            GMacMacroCodeComposer.DefaultGenerateCommentsBeforeComputations(macroCodeGen);

            //Temp variables declaration
            if (macroCodeGen.CodeBlock.TargetTempVarsCount > MaxTargetLocalVars)
            {
                //Add array declaration code
                macroCodeGen.SyntaxList.Add(
                    macroCodeGen.SyntaxFactory.DeclareLocalArray(
                        GMacLanguage.ScalarTypeName,
                        "tempArray",
                        macroCodeGen.CodeBlock.TargetTempVarsCount.ToString()
                        )
                    );

                macroCodeGen.SyntaxList.AddEmptyLine();
            }
            else
            {
                var tempVarNames =
                    macroCodeGen.CodeBlock
                    .TempVariables
                    .Select(item => item.TargetVariableName)
                    .Distinct();

                //Add temp variables declaration code
                foreach (var tempVarName in tempVarNames)
                    macroCodeGen.SyntaxList.Add(
                        macroCodeGen.SyntaxFactory.DeclareLocalVariable(GMacLanguage.ScalarTypeName, tempVarName)
                        );

                macroCodeGen.SyntaxList.AddEmptyLine();
            }

            return true;
        }

        private void GeneratePostComputationsCode(GMacMacroCodeComposer macroCodeGen)
        {
            //Generate comments
            GMacMacroCodeComposer.DefaultGenerateCommentsAfterComputations(macroCodeGen);
        }


        protected override bool InitializeTemplates()
        {
            //Initialize templates used in code composition

            Templates.Parse(GMacCodeTemplates);

            Templates.Parse(FrameUtilsTemplates);

            Templates.Parse(KVectorTemplates);

            Templates.Parse(MultivectorTemplates);

            Templates.Parse(VectorTemplates);

            Templates.Parse(OutermorphismTemplates);

            Templates.Parse(KVectorStaticTemplates);

            Templates.Parse(KVectorEqualsTemplates);

            Templates.Parse(KVectorIsZeroTemplates);

            Templates.Parse(KVectorInvolutionsTemplates);

            Templates.Parse(KVectorNormTemplates);

            Templates.Parse(KVectorSdfTemplates);

            Templates.Parse(KVectorMiscTemplates);

            Templates.Parse(KVectorBilinearProductsTemplates);

            Templates.Parse(KVectorFactorizationTemplates);

            //Template for encoding grade1 multivectors as variables by basis blade index
            Templates.Add("vmv", new ParametricTextComposer("#", "#", "#Var##index#"));

            return true;
        }

        protected override void InitializeOtherComponents()
        {
            //Setup some components of the code library composer

            MacroGenDefaults.ActionBeforeGenerateComputations = 
                GeneratePreComputationsCode;

            MacroGenDefaults.ActionAfterGenerateComputations = 
                GeneratePostComputationsCode;
        }

        protected override void FinalizeOtherComponents()
        {
            //Remove temporary GMacDSL symbols created during code composition
            TempSymbolsCompiler.RemoveCompiledSymbolsFromAst();
        }

        protected override string GetSymbolTargetName(AstSymbol symbol)
        {
            if (symbol.IsValidFrame)
                return UniqueNameGenerator.GetUniqueName(symbol.Name);

            throw new InvalidOperationException();
        }

        protected override bool VerifyReadyToGenerate()
        {
            //This library starts code composition from valid GMacAST frames
            return SelectedSymbols.All(s => s.IsValidFrame);
        }

        protected override void ComposeTextFiles()
        {
            var framesList =
                Root
                .Frames
                .Where(SelectedSymbols.IsEmptyOrContains)
                .ToArray();

            foreach (var frameInfo in framesList)
                GenerateFrameCode(frameInfo);
        }

        public override GMacCodeLibraryComposer CreateEmptyGenerator()
        {
            return new CodeLibraryComposer(Root);
        }

        public override IEnumerable<AstSymbol> GetBaseSymbolsList()
        {
            return Root.Frames;
        }
    }
}
