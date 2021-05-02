using System;
using System.Globalization;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.NETLink;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ClarkeRotationNDSample
    {
        private static int _frameSize = 12;
        
        private static GaPoTSymFrame _uFrame;
        private static GaPoTSymFrame _eFrame;
        private static GaPoTSymFrame _cFrame;

        private static GaPoTSymMultivector _uPseudoScalar;
        private static GaPoTSymMultivector _ePseudoScalar;
        private static GaPoTSymMultivector _cPseudoScalar;

        private static GaPoTSymRotorsSequence _rotorSequence;
        
        private static void DisplayFrame(string vectorName, GaPoTSymFrame frame, GaPoTSymMultivector pseudoScalar)
        {
            for (var i = 0; i < frame.Count; i++)
                Console.WriteLine($@"{vectorName}{(i + 1).ToString()} = {frame[i].TermsToText()}");
            
            Console.WriteLine($@"{vectorName} pseudo-scalar = {pseudoScalar.TermsToText()}");
            Console.WriteLine($@"{vectorName} pseudo-scalar inverse = {pseudoScalar.Inverse().TermsToText()}");
            Console.WriteLine();
        }
        
        private static void DisplayFrameMatrix(string vectorName, GaPoTSymFrame frame, GaPoTSymMultivector pseudoScalar)
        {
            var matrixExpr = frame.GetMatrix(_frameSize).ToArrayExpr();
            
            Console.WriteLine($@"{vectorName} matrix = {matrixExpr}");
            Console.WriteLine($@"{vectorName} pseudo-scalar = {pseudoScalar.TermsToText()}");
            Console.WriteLine($@"{vectorName} pseudo-scalar inverse = {pseudoScalar.Inverse().TermsToText()}");
            Console.WriteLine();
        }

        private static void DisplayFrameSignatures(string vectorName, GaPoTSymFrame frame)
        {
            for (var i = 0; i < frame.Count; i++)
            {
                var vectorName1 = $"{vectorName}{i + 1}";
                
                Console.WriteLine($@"{vectorName1} . {vectorName1} = {frame[i].DotProduct(frame[i])}");
            }
            
            Console.WriteLine();
            
            for (var i = 0; i < frame.Count; i++)
            {
                var vectorName1 = $"{vectorName}{i + 1}";
                
                for (var j = i + 1; j < frame.Count; j++)
                {
                    var vectorName2 = $"{vectorName}{j + 1}";
                    
                    Console.WriteLine($@"{vectorName1} . {vectorName2} = {frame[i].DotProduct(frame[j])}");
                }
                
                Console.WriteLine();
            }
        }

        private static void DisplayRotor(string rotorName, GaPoTSymMultivector rotor)
        {
            Console.WriteLine($@"{rotorName} = {rotor.TermsToLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($@"{rotorName} = {rotor.TermsToText()}");
            Console.WriteLine();
            
            // Console.WriteLine($@"{rotorName} reverse = {rotor.Reverse().TermsToText()}");
            // Console.WriteLine();
            
            //Make sure this is a rotor
            Console.WriteLine($@"{rotorName} norm = {rotor.Gp(rotor.Reverse()).TermsToText()}");
            Console.WriteLine();
        }
        
        private static void CreateFrames()
        {
            // var frame = GetClarkeFrame(5);
            //
            // var c1 = frame[0];
            // var c2 = frame[1];
            // var c3 = frame[2];
            // var c4 = frame[3];
            // var c5 = frame[4];

            _uFrame = GaPoTSymFrame.CreateBasisFrame(_frameSize);
            _eFrame = GaPoTSymFrame.CreateEmptyFrame();

            for (var i = 1; i < _frameSize; i++)
            {
                _eFrame.AppendVector(
                    new GaPoTSymVector()
                        .SetTerm(1, Expr.INT_MINUSONE)
                        .SetTerm(i + 1, Expr.INT_ONE)
                );
            }
            
            _uPseudoScalar = _uFrame.GetPseudoScalar();
            _ePseudoScalar = _eFrame.GetPseudoScalar();
            
            _cFrame = _eFrame.GetOrthogonalFrame(true);
            
            _cFrame.AppendVector(
                -GaPoTSymUtils
                    .OuterProduct(_cFrame)
                    .Gp(_uPseudoScalar.CliffordConjugate())
                    .GetVectorPart()
            );

            _cPseudoScalar = _cFrame.GetPseudoScalar();
            
            DisplayFrameMatrix("u", _uFrame, _uPseudoScalar);
            DisplayFrameMatrix("e", _eFrame, _ePseudoScalar);
            DisplayFrameMatrix("c", _cFrame, _cPseudoScalar);
            
            DisplayFrameSignatures("c", _cFrame);
        }
        
        private static GaPoTSymVector[] GetClarkeFrame(int vectorsCount)
        {
            var frame = new GaPoTSymVector[vectorsCount];
            
            //See the paper "Generalized Clarke Components for Polyphase Networks", 1969
            var m = vectorsCount;
            var s = $"Sqrt[2 / {m}]";

            //m is odd, fill all columns except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frame[vectorIndex1] = new GaPoTSymVector();
                frame[vectorIndex2] = new GaPoTSymVector();
                
                frame[vectorIndex1].SetTerm(1, $"{s}".SimplifyToExpr());
                
                for (var i = 1; i < m; i++)
                {
                    var angle = $"2 * Pi * {(k + 1).ToString()} * {i.ToString()} / {m.ToString()}";
                    var cosAngle = $"{s} * Cos[{angle}]".SimplifyToExpr();
                    var sinAngle = $"{s} * Sin[{angle}]".SimplifyToExpr();
                    
                    frame[vectorIndex1].SetTerm(i + 1, cosAngle);
                    frame[vectorIndex2].SetTerm(i + 1, sinAngle);
                }
            }

            //Fill the last column
            frame[m - 1] = new GaPoTSymVector();

            var v = $"1 / Sqrt[{m}]".SimplifyToExpr();
            for (var i = 0; i < m; i++)
            {
                frame[m - 1].SetTerm(i + 1, v);
            }
            
            return frame;
        }

        private static GaPoTSymMultivector CreateRotor(int index)
        {
            var k = index;

            var rk0 = $"Power[-1, {k}] / Sqrt[{k}]";
            var rk1 = $"Power[-1, {k + 1}] / Sqrt[{k + 1}]";
            
            var cosAngle = $"Sqrt[(1 - {rk1}) / 2]".SimplifyToExpr();
            var sinAngle = $"{rk0} * Sqrt[(1 + {rk1}) / 2]".SimplifyToExpr();

            var uVector1 = new GaPoTSymVector(
                Enumerable
                    .Range(1, k)
                    .Select(i => new GaPoTSymVectorTerm(i, Expr.INT_ONE))
                ).ToMultivector();

            var uVector2 = new GaPoTSymVector()
                .SetTerm(k + 1, Expr.INT_ONE)
                .ToMultivector();

            var uBivector = uVector1.Gp(uVector2);
            
            return cosAngle + sinAngle * uBivector;
        }
        
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            CreateFrames();

            _rotorSequence = GaPoTSymRotorsSequence.CreateFromOrthonormalFrames(
                _uFrame, 
                _cFrame
            );

            //var rotatedFrames = _rotorSequence.GetRotations(_uFrame);

            //foreach (var frame in rotatedFrames)
            //{
            //    Console.WriteLine($"MatrixForm[{frame.GetMatrixExpr(_frameSize)}]");
            //    Console.WriteLine();
            //}

            var rotationMatrices = _rotorSequence.GetRotationMatrices(_frameSize);

            foreach (var matrix in rotationMatrices)
            {
                Console.WriteLine($"MatrixForm[{matrix}]");
                Console.WriteLine();
            }

            //var inputFrame = new GaPoTSymVector[_frameSize];

            //for (var i = 0; i < _frameSize; i++)
            //    inputFrame[i] = _uFrame[i];
            
            //for (var i = 0; i < _frameSize - 1; i++)
            //{
            //    _rotors[i] = inputFrame[i].GetRotorToVector(_cFrame[i]);

            //    DisplayRotor($"R{i + 1}", _rotors[i]);

            //    var readyRotor = CreateRotor(i + 1);

            //    Console.WriteLine($"Rotors Difference: {(readyRotor - _rotors[i]).TermsToText()}");
            //    Console.WriteLine();
                
            //    for (var j = 0; j < _frameSize; j++)
            //        inputFrame[j] = inputFrame[j].ApplyRotor(_rotors[i]);
            //}
        }
    }
}