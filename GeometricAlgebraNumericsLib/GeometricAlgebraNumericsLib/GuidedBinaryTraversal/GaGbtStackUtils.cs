using System.Collections.Generic;
using GeometricAlgebraNumericsLib.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraNumericsLib.Multivectors.Numeric;

namespace GeometricAlgebraNumericsLib.GuidedBinaryTraversal
{
    public static class GaGbtStackUtils
    {
        public static IEnumerable<KeyValuePair<ulong, double>> TraverseForLeafIdValue(this GaGbtNumDarMultivectorStack1 stack)
        {
            stack.PushRootData();

            while (!stack.IsEmpty)
            {
                stack.PopNodeData();

                if (stack.TosIsLeaf)
                {
                    yield return new KeyValuePair<ulong, double>(
                        stack.TosId,
                        stack.TosValue
                    );

                    continue;
                }

                if (stack.TosHasChild(1))
                    stack.PushDataOfChild(1);

                if (stack.TosHasChild(0))
                    stack.PushDataOfChild(0);
            }
        }

        public static IEnumerable<GaNumTerm> TraverseForTerms(this GaGbtNumDarMultivectorStack1 stack)
        {
            stack.PushRootData();

            while (!stack.IsEmpty)
            {
                stack.PopNodeData();

                if (stack.TosIsLeaf)
                {
                    yield return GaNumTerm.Create(
                        stack.Multivector.GaSpaceDimension,
                        (int)stack.TosId,
                        stack.TosValue
                    );

                    continue;
                }

                if (stack.TosHasChild(1))
                    stack.PushDataOfChild(1);

                if (stack.TosHasChild(0))
                    stack.PushDataOfChild(0);
            }
        }
    }
}