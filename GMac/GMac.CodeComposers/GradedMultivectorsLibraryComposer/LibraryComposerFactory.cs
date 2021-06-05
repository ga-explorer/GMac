using System.Collections.Generic;
using System.Windows.Forms;
using GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp;
using GMac.Engine;
using GMac.Engine.API.CodeGen;
using GMac.Engine.AST;
using GMac.Engine.Compiler;
using TextComposerLib.Files;
using TextComposerLib.WinForms.UserInterface.UI;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer
{
    public static class LibraryComposerFactory
    {
        internal static List<string> GMacDslCodeList { get; } 
            = new List<string>();


        static LibraryComposerFactory()
        {
            #region 5D Conformal Geometric Algebra GMacDSL code

            GMacDslCodeList.Add(@"
namespace geometry3d
frame Cga5D (e1, e2, e3, ep, en) orthonormal '++++-'

constant Cga5D.no = (en - ep) / 2
constant Cga5D.ni = en + ep
constant Cga5D.Inorm2 = I sp reverse(I)
constant Cga5D.Iinv = I / (I sp reverse(I))

macro Cga5D.ToPointMultivector(point : Multivector) : Multivector
begin
    return no + point + ((point sp point) / 2) * ni
end


macro Cga5D.SdfOpns(point : Multivector, mv : Multivector) : scalar
begin
    let mv1 = ToPointMultivector(point) op mv
    
    return mv1 sp reverse(mv1)
end

macro Cga5D.SdfIpns(point : Multivector, mv : Multivector) : scalar
begin
    let mv1 = ToPointMultivector(point) lcp mv
    
    return mv1 sp reverse(mv1)
end


structure SdfRayStepResult (sdf0 : scalar, sdf1 : scalar)

macro Cga5D.SdfRayStepOpns(
    mv : Multivector,
    rayOrigin : Multivector,
    rayDirection : Multivector,
    distanceDelta : scalar,
    t0 : scalar
    ) : SdfRayStepResult
begin
    let p0 = rayOrigin + (t0 * rayDirection)
    let p1 = p0 + (distanceDelta * rayDirection)
    
    let sdf0 = SdfOpns(p0, mv)
    let sdf1 = SdfOpns(p1, mv)
    
    return SdfRayStepResult(sdf0, sdf1)
end

macro Cga5D.SdfRayStepIpns(
    mv : Multivector,
    rayOrigin : Multivector,
    rayDirection : Multivector,
    distanceDelta : scalar,
    t0 : scalar
    ) : SdfRayStepResult
begin
    let p0 = rayOrigin + (t0 * rayDirection)
    let p1 = p0 + (distanceDelta * rayDirection)
    
    let sdf0 = SdfIpns(p0, mv)
    let sdf1 = SdfIpns(p1, mv)
    
    return SdfRayStepResult(sdf0, sdf1)
end


structure SdfNormalResult (d1 : scalar, d2 : scalar, d3 : scalar, d4 : scalar)

macro Cga5D.SdfNormalOpns(
    mv : Multivector,
    point : Multivector,
    distanceDelta : scalar
    ) : SdfNormalResult
begin
    let v1 = Multivector(#e1# = ' 1', #e2# = '-1', #e3# = '-1')
    let v2 = Multivector(#e1# = '-1', #e2# = '-1', #e3# = ' 1')
    let v3 = Multivector(#e1# = '-1', #e2# = ' 1', #e3# = '-1')
    let v4 = Multivector(#e1# = ' 1', #e2# = ' 1', #e3# = ' 1')
    
    let p1 = point + distanceDelta * v1
    let p2 = point + distanceDelta * v2
    let p3 = point + distanceDelta * v3
    let p4 = point + distanceDelta * v4
    
    let d1 = SdfOpns(p1, mv)
    let d2 = SdfOpns(p2, mv)
    let d3 = SdfOpns(p3, mv)
    let d4 = SdfOpns(p4, mv)
    
    return SdfNormalResult(d1, d2, d3, d4)
end

macro Cga5D.SdfNormalIpns(
    mv : Multivector,
    point : Multivector,
    distanceDelta : scalar
    ) : SdfNormalResult
begin
    let v1 = Multivector(#e1# = ' 1', #e2# = '-1', #e3# = '-1')
    let v2 = Multivector(#e1# = '-1', #e2# = '-1', #e3# = ' 1')
    let v3 = Multivector(#e1# = '-1', #e2# = ' 1', #e3# = '-1')
    let v4 = Multivector(#e1# = ' 1', #e2# = ' 1', #e3# = ' 1')
    
    let p1 = point + distanceDelta * v1
    let p2 = point + distanceDelta * v2
    let p3 = point + distanceDelta * v3
    let p4 = point + distanceDelta * v4
    
    let d1 = SdfIpns(p1, mv)
    let d2 = SdfIpns(p2, mv)
    let d3 = SdfIpns(p3, mv)
    let d4 = SdfIpns(p4, mv)
    
    return SdfNormalResult(d1, d2, d3, d4)
end
");
            #endregion

            #region 3D Euclidean Geometric Algebra GMacDSL code

            GMacDslCodeList.Add(@"
namespace geometry3d
frame Ega3D (e1, e2, e3) euclidean
");

            #endregion


        }


        /// <summary>
        /// Compile given GMacDSL code into a GMacAST structure
        /// </summary>
        /// <param name="dslCode"></param>
        /// <returns></returns>
        private static AstRoot BeginCompilation(string dslCode)
        {
            //GMacSystemUtils.InitializeGMac();

            //Compile GMacDSL code into GMacAST
            var compiler = GMacProjectCompiler.CompileDslCode(dslCode, Application.LocalUserAppDataPath, "tempTask");

            //Reduce details of progress reporting during code composition
            compiler.Progress.DisableAfterNextReport = true;

            if (compiler.Progress.History.HasErrorsOrFailures)
            {
                //Compilation of GMacDSL code failed
                var formProgress = new FormProgress(compiler.Progress, null, null);
                formProgress.ShowDialog();

                return null;
            }

            //Compilation of GMacDSL code successful, return constructed GMacAST root
            return compiler.Root;
        }

        /// <summary>
        /// Given GMacDSL code this factory class compiles the code into a GMacAST structure and
        /// use the blades code library composer for C# to compose target C# code
        /// </summary>
        /// <param name="dslCode"></param>
        /// <param name="outputFolder"></param>
        /// <param name="generateMacros"></param>
        /// <param name="targetLanguageName"></param>
        /// <returns></returns>
        public static TextFilesComposer ComposeLibrary(string dslCode, string outputFolder, bool generateMacros, string targetLanguageName)
        {
            //Clear the progress log composer
            GMacEngineUtils.ResetProgress();

            //Compile GMacDSL code into a GMacAST structure
            var ast = BeginCompilation(dslCode);

            //If compilation fails return nothing
            if (ReferenceEquals(ast, null)) return null;

            //Create and initialize code library composer for C#
            GMacCodeLibraryComposer activeGenerator;

            //Select the composer based on the target language name
            switch (targetLanguageName)
            {
                case "C#":
                    activeGenerator = new CodeLibraryComposer(ast);
                    break;

                default:
                    activeGenerator = new CodeLibraryComposer(ast);
                    break;
            }

            //Set the output folder for generated files
            activeGenerator.CodeFilesComposer.RootFolder = 
                outputFolder;

            //Select option for generating macros code, this takes the longest time
            //in the composition process and may be skipped initially while designing
            //structure of composed library
            activeGenerator.MacroGenDefaults.AllowGenerateMacroCode = 
                generateMacros;

            //Specify GMacAST frames to be used for code compositions
            activeGenerator.SelectedSymbols.SetSymbols(ast.Frames);

            //Start code composition process and display its progress
            var formProgress = new FormProgress(
                activeGenerator.Progress, 
                activeGenerator.Generate, 
                null
            );

            formProgress.ShowDialog();

            //Save all generated files
            //activeGenerator.CodeFilesComposer.SaveToFolder();

            //Return generated folders\files as a FilesComposer object
            return activeGenerator.CodeFilesComposer;
        }
    }
}
