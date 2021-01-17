using System;
using System.Globalization;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ClarkeRotation4DSample
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Define basis vectors before rotation
            var u1 = "'1'<1>".GaPoTSymParseVector();
            var u2 = "'1'<2>".GaPoTSymParseVector();
            var u3 = "'1'<3>".GaPoTSymParseVector();
            var u4 = "'1'<4>".GaPoTSymParseVector();
            var u1234 = GaPoTSymUtils.OuterProduct(u1, u2, u3, u4);
            
            Console.WriteLine($@"u1 = {u1.TermsToText()}");
            Console.WriteLine($@"u2 = {u2.TermsToText()}");
            Console.WriteLine($@"u3 = {u3.TermsToText()}");
            Console.WriteLine($@"u4 = {u4.TermsToText()}");
            Console.WriteLine();
            
            Console.WriteLine($@"u1234 = {u1234.TermsToText()}");
            Console.WriteLine($@"inverse(u1234) = {u1234.Inverse().TermsToText()}");
            Console.WriteLine();
            
                        
            //Define voltage vector basis e1, e2
            var e1 = u2 - u1;
            var e2 = u3 - u1;
            var e3 = u4 - u1;
            var e123 = GaPoTSymUtils.OuterProduct(e1, e2, e3);
            
            Console.WriteLine($@"e1 = {e1.TermsToText()}");
            Console.WriteLine($@"e2 = {e2.TermsToText()}");
            Console.WriteLine($@"e3 = {e3.TermsToText()}");
            Console.WriteLine($@"e123 = {e123.TermsToText()}");
            Console.WriteLine();
            
            //Ortho-normalize e1, e2, e3
            var orthoVectors = GaPoTSymUtils.ApplyGramSchmidt(new[] {e1, e2, e3}, true).ToArray();

            var c1 = orthoVectors[0];
            var c2 = orthoVectors[1];
            var c3 = orthoVectors[2];
            var c4 = -GaPoTSymUtils.OuterProduct(c1, c2, c3).Gp(u1234.Inverse()).GetVectorPart();
            var c1234 = GaPoTSymUtils.OuterProduct(c1, c2, c3, c4);
            
            Console.WriteLine($@"c1 = {c1.TermsToLaTeX()}");
            Console.WriteLine($@"c2 = {c2.TermsToLaTeX()}");
            Console.WriteLine($@"c3 = {c3.TermsToLaTeX()}");
            Console.WriteLine($@"c4 = {c4.TermsToLaTeX()}");
            Console.WriteLine();

            //Make sure c1,c2,c3,c4 are mutually orthonormal
            Console.WriteLine($@"c1 . c1 = {c1.DotProduct(c1)}");
            Console.WriteLine($@"c2 . c2 = {c2.DotProduct(c2)}");
            Console.WriteLine($@"c3 . c3 = {c3.DotProduct(c3)}");
            Console.WriteLine($@"c4 . c4 = {c4.DotProduct(c4)}");
            Console.WriteLine($@"c1 . c2 = {c1.DotProduct(c2)}");
            Console.WriteLine($@"c2 . c3 = {c2.DotProduct(c3)}");
            Console.WriteLine($@"c3 . c4 = {c3.DotProduct(c4)}");
            Console.WriteLine($@"c4 . c1 = {c4.DotProduct(c1)}");
            Console.WriteLine($@"c1 . c3 = {c1.DotProduct(c3)}");
            Console.WriteLine($@"c2 . c4 = {c2.DotProduct(c4)}");
            Console.WriteLine();
            
            Console.WriteLine($@"c1234 = {c1234.TermsToText()}");
            Console.WriteLine($@"inverse(c1234) = {c1234.Inverse().TermsToText()}");
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
            
            Console.WriteLine($@"rotation of u1 under rotor1 = {u1_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2 under rotor1 = {u2_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3 under rotor1 = {u3_1.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4 under rotor1 = {u4_1.TermsToLaTeX()}");
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
            
            //We can combine the two simple rotors to get <c1, c2> from <u1, u2> directly
            var rotor21 = rotor2.Gp(rotor1);
            Console.WriteLine($@"rotor21 = {rotor21.TermsToLaTeX()}");
            Console.WriteLine($@"rotor21 = {rotor21.TermsToText()}");
            Console.WriteLine($@"reverse(rotor21) = {rotor21.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor21 gp reverse(rotor21) = {rotor21.Gp(rotor21.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            Console.WriteLine($@"rotation of u1 under rotor21 = {u1.ApplyRotor(rotor21).TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2 under rotor21 = {u2.ApplyRotor(rotor21).TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3 under rotor21 = {u3.ApplyRotor(rotor21).TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4 under rotor21 = {u4.ApplyRotor(rotor21).TermsToLaTeX()}");
            Console.WriteLine();

            //Rotate all basis vectors using rotor2
            var u1_2 = u1_1.ApplyRotor(rotor2);
            var u2_2 = u2_1.ApplyRotor(rotor2);
            var u3_2 = u3_1.ApplyRotor(rotor2);
            var u4_2 = u4_1.ApplyRotor(rotor2);
            
            Console.WriteLine($@"rotation of u1_1 under rotor2 = {u1_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2_1 under rotor2 = {u2_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3_1 under rotor2 = {u3_2.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4_1 under rotor2 = {u4_2.TermsToLaTeX()}");
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
            
            Console.WriteLine($@"rotation of u1_2 under rotor3 = {u1_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u2_2 under rotor3 = {u2_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u3_2 under rotor3 = {u3_3.TermsToLaTeX()}");
            Console.WriteLine($@"rotation of u4_2 under rotor3 = {u4_3.TermsToLaTeX()}");
            Console.WriteLine();
            
            //Compute final rotor as rotor3 gp rotor2 gp rotor1
            var rotor = 
                rotor3.Gp(rotor2).Gp(rotor1).MapScalars(
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
            
            Console.WriteLine($@"rotation of u1 under rotor = {cc1.TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor = {cc2.TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor = {cc3.TermsToText()}");
            Console.WriteLine($@"rotation of u4 under rotor = {cc4.TermsToText()}");
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