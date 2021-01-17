using System;
using System.Globalization;
using System.Linq;
using GeometricAlgebraSymbolicsLib.Applications.GAPoT;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.ExprFactory;

namespace GeometricAlgebraSymbolicsLibSamples.GAPoT
{
    public static class ClarkeRotation3DSample
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Define basis vectors before rotation
            var u1 = "'1'<1>".GaPoTSymParseVector();
            var u2 = "'1'<2>".GaPoTSymParseVector();
            var u3 = "'1'<3>".GaPoTSymParseVector();
            var u123 = GaPoTSymUtils.OuterProduct(u1, u2, u3);
            
            Console.WriteLine($@"u1 = {u1.TermsToText()}");
            Console.WriteLine($@"u2 = {u2.TermsToText()}");
            Console.WriteLine($@"u3 = {u3.TermsToText()}");
            Console.WriteLine();

            Console.WriteLine($@"u123 = {u123.TermsToText()}");
            Console.WriteLine($@"inverse(u1234) = {u123.Inverse().TermsToText()}");
            Console.WriteLine();
            
            //Define voltage vector basis e1, e2
            var e1 = u2 - u1;
            var e2 = u3 - u1;
            
            Console.WriteLine($@"e1 = {e1.TermsToText()}");
            Console.WriteLine($@"e2 = {e2.TermsToText()}");
            Console.WriteLine();
            
            //Ortho-normalize e1, e2
            var orthoVectors = GaPoTSymUtils.ApplyGramSchmidt(new[] {e1, e2}, true).ToArray();

            var c1 = orthoVectors[0];
            var c2 = orthoVectors[1];
            var c3 = -GaPoTSymUtils.OuterProduct(c1, c2).Gp(u123.Inverse()).GetVectorPart();
            
            //Define basis vectors after rotation
            var c123 = GaPoTSymUtils.OuterProduct(c1, c2, c3);
            
            Console.WriteLine($@"c1 = {c1.TermsToLaTeX()}");
            Console.WriteLine($@"c2 = {c2.TermsToLaTeX()}");
            Console.WriteLine($@"c3 = {c3.TermsToLaTeX()}");
            Console.WriteLine();

            //Make sure c1,c2,c3,c4 are mutually orthonormal
            Console.WriteLine($@"c1 . c1 = {c1.DotProduct(c1)}");
            Console.WriteLine($@"c2 . c2 = {c2.DotProduct(c2)}");
            Console.WriteLine($@"c3 . c3 = {c3.DotProduct(c3)}");
            Console.WriteLine($@"c1 . c2 = {c1.DotProduct(c2)}");
            Console.WriteLine($@"c2 . c3 = {c2.DotProduct(c3)}");
            Console.WriteLine($@"c1 . c3 = {c1.DotProduct(c3)}");
            Console.WriteLine();
            
            Console.WriteLine($@"c1234 = {c123.TermsToText()}");
            Console.WriteLine($@"inverse(c1234) = {c123.Inverse().TermsToText()}");
            Console.WriteLine();

            //Find a simple rotor to get c1 from u1
            var rotor1 = u1.GetRotorToVector(c1);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor1 = {rotor1.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(rotor1) = {rotor1.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor1 gp reverse(rotor1) = {rotor1.Gp(rotor1.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor1
            var u1_1 = u1.ApplyRotor(rotor1);
            var u2_1 = u2.ApplyRotor(rotor1);
            var u3_1 = u3.ApplyRotor(rotor1);
            
            Console.WriteLine($@"rotation of u1 under rotor1 = {u1_1.TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor1 = {u2_1.TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor1 = {u3_1.TermsToText()}");
            Console.WriteLine();
            
            //Find a simple rotor to get c2 from u2_1
            var rotor2 = u2_1.GetRotorToVector(c2);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor2 = {rotor2.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(rotor2) = {rotor2.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor2 gp reverse(rotor2) = {rotor2.Gp(rotor2.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor2
            var u1_2 = u1_1.ApplyRotor(rotor2);
            var u2_2 = u2_1.ApplyRotor(rotor2);
            var u3_2 = u3_1.ApplyRotor(rotor2);
            
            Console.WriteLine($@"rotation of u1_1 under rotor2 = {u1_2.TermsToText()}");
            Console.WriteLine($@"rotation of u2_1 under rotor2 = {u2_2.TermsToText()}");
            Console.WriteLine($@"rotation of u3_1 under rotor2 = {u3_2.TermsToText()}");
            Console.WriteLine();
            
            //Compute final rotor as rotor3 gp rotor2 gp rotor1
            var rotor = rotor2.Gp(rotor1);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor = {rotor.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(rotor) = {rotor.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor gp reverse(rotor) = {rotor.Gp(rotor.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all original basis vectors using final rotor
            var cc1 = u1.ApplyRotor(rotor);
            var cc2 = u2.ApplyRotor(rotor);
            var cc3 = u3.ApplyRotor(rotor);
            
            Console.WriteLine($@"rotation of u1 under rotor = {cc1.TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor = {cc2.TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor = {cc3.TermsToText()}");
            Console.WriteLine();
            
            //Apply the Prak transform as a time-varying rotation in the c1-c2 plane
            var halfTheta = Mfs.Divide[@"t".ToSymbolExpr(), 2.ToExpr()];
            var parkRotor = Mfs.Cos[halfTheta] + Mfs.Sin[halfTheta] * c1.Op(c2);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor = {parkRotor.TermsToLaTeX()}");
            Console.WriteLine($@"reverse(rotor) = {parkRotor.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor gp reverse(rotor) = {parkRotor.Gp(parkRotor.Reverse()).TermsToText()}");
            Console.WriteLine();
        }
    }
}