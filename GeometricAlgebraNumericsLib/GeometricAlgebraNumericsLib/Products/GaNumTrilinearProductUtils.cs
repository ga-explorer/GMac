using System.Collections.Generic;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Products;
using GeometricAlgebraNumericsLib.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.Products
{
    public static class GaNumTrilinearProductUtils
    {
        public static IEnumerable<GaTerm<double>> GetGbtEOpLcpLaTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMultivector mv3)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack3.Create(mv1, mv2, mv3);

            return stack.TraverseForEOpLcpLaTerms();
        }

        public static IEnumerable<GaTerm<double>> GetGbtEGpGpLaTerms(this IGaNumMultivector mv1, IGaNumMultivector mv2, IGaNumMultivector mv3)
        {
            var stack = GaGbtNumMultivectorOrthogonalProductsStack3.Create(mv1, mv2, mv3);

            return stack.TraverseForEGpGpTerms();
        }
    }
}