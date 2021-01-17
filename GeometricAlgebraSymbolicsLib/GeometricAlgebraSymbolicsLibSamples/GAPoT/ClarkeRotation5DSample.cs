using System;
using System.Globalization;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ClarkeRotation5DSample
    {
        private static GaPoTSymVector[] GetClarkeFrame(int vectorsCount)
        {
            var frame = new GaPoTSymVector[vectorsCount];
            
            //See the paper "Generalized Clarke Components for Polyphase Networks", 1969
            var m = vectorsCount;
            var s = $"Sqrt[2 / {m.ToString()}]";

            //m is odd, fill all columns except the last
            var n = (m - 1) / 2;
            for (var k = 0; k < n; k++)
            {
                var vectorIndex1 = 2 * k;
                var vectorIndex2 = 2 * k + 1;
                
                frame[vectorIndex1] = new GaPoTSymVector();
                frame[vectorIndex2] = new GaPoTSymVector();
                
                frame[vectorIndex1].SetTerm(1, $"{s}".GaPoTSymSimplify());
                
                for (var i = 1; i < m; i++)
                {
                    var angle = $"2 * Pi * {(k + 1).ToString()} * {i.ToString()} / {m.ToString()}";
                    var cosAngle = $"{s} * Cos[{angle}]".GaPoTSymSimplify();
                    var sinAngle = $"{s} * Sin[{angle}]".GaPoTSymSimplify();
                    
                    frame[vectorIndex1].SetTerm(i + 1, cosAngle);
                    frame[vectorIndex2].SetTerm(i + 1, sinAngle);
                }
            }

            //Fill the last column
            frame[m - 1] = new GaPoTSymVector();

            var v = $"1 / Sqrt[{m}]".GaPoTSymSimplify();
            for (var i = 0; i < m; i++)
            {
                frame[m - 1].SetTerm(i + 1, v);
            }
            
            return frame;
        }
        
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Define basis vectors before rotation
            var u1 = "'1'<1>".GaPoTSymParseVector();
            var u2 = "'1'<2>".GaPoTSymParseVector();
            var u3 = "'1'<3>".GaPoTSymParseVector();
            var u4 = "'1'<4>".GaPoTSymParseVector();
            var u5 = "'1'<5>".GaPoTSymParseVector();
            var u12345 = GaPoTSymUtils.OuterProduct(u1, u2, u3, u4, u5);
            
            Console.WriteLine($@"u1 = {u1.TermsToText()}");
            Console.WriteLine($@"u2 = {u2.TermsToText()}");
            Console.WriteLine($@"u3 = {u3.TermsToText()}");
            Console.WriteLine($@"u4 = {u4.TermsToText()}");
            Console.WriteLine($@"u5 = {u5.TermsToText()}");
            Console.WriteLine();
            
            Console.WriteLine($@"u12345 = {u12345.TermsToText()}");
            Console.WriteLine($@"inverse(u12345) = {u12345.Inverse().TermsToText()}");
            Console.WriteLine();
            
                        
            //Define voltage vector basis e1, e2
            var e1 = u2 - u1;
            var e2 = u3 - u2;
            var e3 = u4 - u3;
            var e4 = u5 - u4;
            var e1234 = GaPoTSymUtils.OuterProduct(e1, e2, e3, e4);
            
            Console.WriteLine($@"e1 = {e1.TermsToText()}");
            Console.WriteLine($@"e2 = {e2.TermsToText()}");
            Console.WriteLine($@"e3 = {e3.TermsToText()}");
            Console.WriteLine($@"e4 = {e4.TermsToText()}");
            Console.WriteLine($@"e1234 = {e1234.TermsToText()}");
            Console.WriteLine();
            
            //Ortho-normalize e1, e2, e3, e4
            var orthoVectors = 
                new[] {e1, e2, e3, e4}.ApplyGramSchmidt(true).ToArray();

            // var frame = GetClarkeFrame(5);
            //
            // var c1 = frame[0];
            // var c2 = frame[1];
            // var c3 = frame[2];
            // var c4 = frame[3];
            // var c5 = frame[4];
            
            var c1 = orthoVectors[0];
            var c2 = orthoVectors[1];
            var c3 = orthoVectors[2];
            var c4 = orthoVectors[3];
            var c5 = GaPoTSymUtils.OuterProduct(c1, c2, c3, c4).Gp(u12345.Inverse()).GetVectorPart();
            
            var c12345 = GaPoTSymUtils.OuterProduct(c1, c2, c3, c4, c5);
            
            
            Console.WriteLine($@"c1 = {c1.TermsToLaTeX()}");
            Console.WriteLine($@"c2 = {c2.TermsToLaTeX()}");
            Console.WriteLine($@"c3 = {c3.TermsToLaTeX()}");
            Console.WriteLine($@"c4 = {c4.TermsToLaTeX()}");
            Console.WriteLine($@"c5 = {c5.TermsToLaTeX()}");
            Console.WriteLine();

            //Make sure c1,c2,c3,c4 are mutually orthonormal
            Console.WriteLine($@"c1 . c1 = {c1.DotProduct(c1)}");
            Console.WriteLine($@"c2 . c2 = {c2.DotProduct(c2)}");
            Console.WriteLine($@"c3 . c3 = {c3.DotProduct(c3)}");
            Console.WriteLine($@"c4 . c4 = {c4.DotProduct(c4)}");
            Console.WriteLine($@"c5 . c5 = {c5.DotProduct(c5)}");
            Console.WriteLine($@"c1 . c2 = {c1.DotProduct(c2)}");
            Console.WriteLine($@"c2 . c3 = {c2.DotProduct(c3)}");
            Console.WriteLine($@"c3 . c4 = {c3.DotProduct(c4)}");
            Console.WriteLine($@"c4 . c5 = {c4.DotProduct(c5)}");
            Console.WriteLine($@"c5 . c1 = {c5.DotProduct(c1)}");
            Console.WriteLine($@"c1 . c3 = {c1.DotProduct(c3)}");
            Console.WriteLine($@"c2 . c4 = {c2.DotProduct(c4)}");
            Console.WriteLine($@"c3 . c5 = {c2.DotProduct(c4)}");
            Console.WriteLine($@"c1 . c4 = {c1.DotProduct(c4)}");
            Console.WriteLine($@"c2 . c5 = {c2.DotProduct(c5)}");
            Console.WriteLine();
            
            Console.WriteLine($@"c12345 = {c12345.TermsToText()}");
            Console.WriteLine($@"inverse(c12345) = {c12345.Inverse().TermsToText()}");
            Console.WriteLine();

            //Find a simple rotor to get c1 from u1
            var rotor1 = 
                u1.GetRotorToVector(c1).MapScalars(
                    e => Mfs.ToRadicals[e].GaPoTSymSimplify()
                );
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor1 = {rotor1.TermsToLaTeX()}");
            Console.WriteLine($@"rotor1 = {rotor1.TermsToText()}");
            Console.WriteLine($@"reverse(rotor1) = {rotor1.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor1 gp reverse(rotor1) = {rotor1.Gp(rotor1.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor1
            var u1_1 = u1.ApplyRotor(rotor1);
            var u2_1 = u2.ApplyRotor(rotor1);
            var u3_1 = u3.ApplyRotor(rotor1);
            var u4_1 = u4.ApplyRotor(rotor1);
            var u5_1 = u5.ApplyRotor(rotor1);
            
            Console.WriteLine($@"rotation of u1 under rotor1 = {u1_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2 under rotor1 = {u2_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3 under rotor1 = {u3_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4 under rotor1 = {u4_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u5 under rotor1 = {u5_1.TermsToLaTeX()}");
            Console.WriteLine();
            
            //Find a simple rotor to get c2 from u2_1
            var rotor2 = 
                u2_1.GetRotorToVector(c2).MapScalars(
                    e => Mfs.ToRadicals[e].GaPoTSymSimplify()
                );
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor2 = {rotor2.TermsToLaTeX()}");
            Console.WriteLine($@"rotor2 = {rotor2.TermsToText()}");
            Console.WriteLine($@"reverse(rotor2) = {rotor2.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor2 gp reverse(rotor2) = {rotor2.Gp(rotor2.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor2
            var u1_2 = u1_1.ApplyRotor(rotor2);
            var u2_2 = u2_1.ApplyRotor(rotor2);
            var u3_2 = u3_1.ApplyRotor(rotor2);
            var u4_2 = u4_1.ApplyRotor(rotor2);
            var u5_2 = u5_1.ApplyRotor(rotor2);
            
            Console.WriteLine($@"rotation of u1_1 under rotor2 = {u1_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2_1 under rotor2 = {u2_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3_1 under rotor2 = {u3_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4_1 under rotor2 = {u4_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u5_1 under rotor2 = {u5_2.TermsToLaTeX()}");
            Console.WriteLine();
            
            //Find a simple rotor to get c3 from u3_2
            var rotor3 = 
                u3_2.GetRotorToVector(c3).MapScalars(
                    e => Mfs.ToRadicals[e].GaPoTSymSimplify()
                );
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor3 = {rotor3.TermsToLaTeX()}");
            Console.WriteLine($@"rotor3 = {rotor3.TermsToText()}");
            Console.WriteLine($@"reverse(rotor3) = {rotor3.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor3 gp reverse(rotor3) = {rotor3.Gp(rotor3.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor2
            var u1_3 = u1_2.ApplyRotor(rotor3);
            var u2_3 = u2_2.ApplyRotor(rotor3);
            var u3_3 = u3_2.ApplyRotor(rotor3);
            var u4_3 = u4_2.ApplyRotor(rotor3);
            var u5_3 = u5_2.ApplyRotor(rotor3);
            
            Console.WriteLine($@"rotation of u1_2 under rotor3 = {u1_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2_2 under rotor3 = {u2_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3_2 under rotor3 = {u3_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4_2 under rotor3 = {u4_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u5_2 under rotor3 = {u5_3.TermsToLaTeX()}");
            Console.WriteLine();
            
            //Find a simple rotor to get c3 from u3_2
            var rotor4 = 
                u4_3.GetRotorToVector(c4).MapScalars(
                    e => Mfs.ToRadicals[e].GaPoTSymSimplify()
                );
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor4 = {rotor4.TermsToLaTeX()}");
            Console.WriteLine($@"rotor4 = {rotor4.TermsToText()}");
            Console.WriteLine($@"reverse(rotor4) = {rotor4.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor4 gp reverse(rotor4) = {rotor4.Gp(rotor4.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor2
            var u1_4 = u1_3.ApplyRotor(rotor4);
            var u2_4 = u2_3.ApplyRotor(rotor4);
            var u3_4 = u3_3.ApplyRotor(rotor4);
            var u4_4 = u4_3.ApplyRotor(rotor4);
            var u5_4 = u5_3.ApplyRotor(rotor4);
            
            Console.WriteLine($@"rotation of u1_3 under rotor4 = {u1_4.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2_3 under rotor4 = {u2_4.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3_3 under rotor4 = {u3_4.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4_3 under rotor4 = {u4_4.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u5_3 under rotor4 = {u5_4.TermsToLaTeX()}");
            Console.WriteLine();
            
            //Compute final rotor as rotor3 gp rotor2 gp rotor1
            var rotor = 
                rotor4.Gp(rotor3).Gp(rotor2).Gp(rotor1).MapScalars(
                    e => Mfs.ToRadicals[e].GaPoTSymSimplify()
                );
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor = {rotor.TermsToLaTeX()}");
            Console.WriteLine($@"rotor = {rotor.TermsToText()}");
            Console.WriteLine($@"reverse(rotor) = {rotor.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor gp reverse(rotor) = {rotor.Gp(rotor.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all original basis vectors using final rotor
            var cc1 = u1.ApplyRotor(rotor);
            var cc2 = u2.ApplyRotor(rotor);
            var cc3 = u3.ApplyRotor(rotor);
            var cc4 = u4.ApplyRotor(rotor);
            var cc5 = u5.ApplyRotor(rotor);
            
            Console.WriteLine($@"rotation of u1 under rotor = {cc1.TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor = {cc2.TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor = {cc3.TermsToText()}");
            Console.WriteLine($@"rotation of u4 under rotor = {cc4.TermsToText()}");
            Console.WriteLine($@"rotation of u5 under rotor = {cc5.TermsToText()}");
            Console.WriteLine();
            
            // //Define general symbolic vector to compute action of rotation
            // var x = "'x1'<1>, 'x2'<2>, 'x3'<3>, 'x4'<4>".GaPoTSymParseVector();
            //
            // Console.WriteLine($@"rotation of x under rotor = {x.ApplyRotor(rotor).TermsToText()}");
            // Console.WriteLine();
            
            //Apply the Prak transform as a time-varying rotation in the c1-c2 plane
            var halfTheta12 = Mfs.Divide[@"t12".ToSymbolExpr(), 2.ToExpr()];
            var parkRotor12 = Mfs.Cos[halfTheta12] + Mfs.Sin[halfTheta12] * c1.Op(c2);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor = {parkRotor12.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(rotor) = {parkRotor12.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor gp reverse(rotor) = {parkRotor12.Gp(parkRotor12.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            var halfTheta34 = Mfs.Divide[@"t34".ToSymbolExpr(), 2.ToExpr()];
            var parkRotor34 = Mfs.Cos[halfTheta34] + Mfs.Sin[halfTheta34] * c3.Op(c4);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor = {parkRotor34.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(rotor) = {parkRotor34.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor gp reverse(rotor) = {parkRotor34.Gp(parkRotor34.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Are the two Park rotors commutative?
            var rotorsDiff = parkRotor34.Gp(parkRotor12) - parkRotor12.Gp(parkRotor34); 
            Console.WriteLine($@"Are they commutative? = {rotorsDiff.TermsToLaTeX()}");
            Console.WriteLine();
        }
    }
}