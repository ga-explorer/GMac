using System.Collections.Generic;
using GMac.GMacMath;
using GMac.GMacMath.Symbolic.Frames;
using GMac.GMacMath.Symbolic.Multivectors;
using TextComposerLib.Text.Linear;

namespace GMacTests.Symbolic
{
    /// <summary>
    /// Verify main products on multivectors using loop vs tree based multivector product representations
    /// </summary>
    public sealed class GMacSymbolicTest3 : IGMacTest
    {
        public string Title { get; } = "";

        public GaSymFrame Frame { get; set; }

        public LinearComposer LogComposer { get;  }
            = new LinearComposer();


        public GMacSymbolicTest3()
        {
            Frame = GaSymFrame.CreateEuclidean(3);
        }

        private void VerifyProductResults(string msg, GaSymMultivector result, GaSymMultivector referenceResult)
        {
            var diffResult = referenceResult - result;

            LogComposer
                .AppendLineAtNewLine(msg);

            if (diffResult.IsNullOrZero())
                return;

            LogComposer
                .IncreaseIndentation()
                .AppendLineAtNewLine("Result = ")
                .IncreaseIndentation()
                .AppendLine(result)
                .AppendLine()
                .DecreaseIndentation();

            LogComposer
                .AppendLineAtNewLine("Reference = ")
                .IncreaseIndentation()
                .AppendLine(referenceResult)
                .AppendLine()
                .DecreaseIndentation();

            LogComposer
                .AppendLineAtNewLine("Diff = ")
                .IncreaseIndentation()
                .AppendLine(diffResult)
                .AppendLine()
                .DecreaseIndentation()
                .DecreaseIndentation()
                .AppendLine();
        }


        public string Execute()
        {
            LogComposer.Clear();

            for (var vSpaceDim = 3; vSpaceDim <= 5; vSpaceDim++)
            {
                Frame = GaSymFrame.CreateConformal(vSpaceDim);

                //var randGen = new GMacRandomGenerator(10);
                var mvList = new List<GaSymMultivector>();

                mvList.Add(GaSymMultivector.CreateSymbolic(Frame.GaSpaceDimension, "A"));
                mvList.Add(GaSymMultivector.CreateSymbolic(Frame.GaSpaceDimension, "B"));

                //var treeMvList = mvList.Select(mv => mv.ToTreeMultivector()).ToList();

                var i = 0;
                var j = 1;

                var computedOp = Frame.ComputedOp[mvList[i], mvList[j]];
                var computedGp = Frame.ComputedGp[mvList[i], mvList[j]];
                var computedSp = Frame.ComputedSp[mvList[i], mvList[j]];
                var computedLcp = Frame.ComputedLcp[mvList[i], mvList[j]];
                var computedRcp = Frame.ComputedRcp[mvList[i], mvList[j]];
                var computedFdp = Frame.ComputedFdp[mvList[i], mvList[j]];
                var computedHip = Frame.ComputedHip[mvList[i], mvList[j]];
                var computedAcp = Frame.ComputedAcp[mvList[i], mvList[j]];
                var computedCp = Frame.ComputedCp[mvList[i], mvList[j]];

                Frame.SetProductsImplementation(GaBilinearProductImplementation.LookupHash);
                var hashOp = Frame.Op[mvList[i], mvList[j]];
                var hashGp = Frame.Gp[mvList[i], mvList[j]];
                var hashSp = Frame.Sp[mvList[i], mvList[j]];
                var hashLcp = Frame.Lcp[mvList[i], mvList[j]];
                var hashRcp = Frame.Rcp[mvList[i], mvList[j]];
                var hashFdp = Frame.Fdp[mvList[i], mvList[j]];
                var hashHip = Frame.Hip[mvList[i], mvList[j]];
                var hashAcp = Frame.Acp[mvList[i], mvList[j]];
                var hashCp = Frame.Cp[mvList[i], mvList[j]];

                Frame.SetProductsImplementation(GaBilinearProductImplementation.LookupArray);
                var arrayOp = Frame.Op[mvList[i], mvList[j]];
                var arrayGp = Frame.Gp[mvList[i], mvList[j]];
                var arraySp = Frame.Sp[mvList[i], mvList[j]];
                var arrayLcp = Frame.Lcp[mvList[i], mvList[j]];
                var arrayRcp = Frame.Rcp[mvList[i], mvList[j]];
                var arrayFdp = Frame.Fdp[mvList[i], mvList[j]];
                var arrayHip = Frame.Hip[mvList[i], mvList[j]];
                var arrayAcp = Frame.Acp[mvList[i], mvList[j]];
                var arrayCp = Frame.Cp[mvList[i], mvList[j]];

                Frame.SetProductsImplementation(GaBilinearProductImplementation.LookupTree);
                var treeOp = Frame.Op[mvList[i], mvList[j]];
                var treeGp = Frame.Gp[mvList[i], mvList[j]];
                var treeSp = Frame.Sp[mvList[i], mvList[j]];
                var treeLcp = Frame.Lcp[mvList[i], mvList[j]];
                var treeRcp = Frame.Rcp[mvList[i], mvList[j]];
                var treeFdp = Frame.Fdp[mvList[i], mvList[j]];
                var treeHip = Frame.Hip[mvList[i], mvList[j]];
                var treeAcp = Frame.Acp[mvList[i], mvList[j]];
                var treeCp = Frame.Cp[mvList[i], mvList[j]];

                Frame.SetProductsImplementation(GaBilinearProductImplementation.LookupCoefSums);
                var combinationsOp = Frame.Op[mvList[i], mvList[j]];
                var combinationsGp = Frame.Gp[mvList[i], mvList[j]];
                var combinationsSp = Frame.Sp[mvList[i], mvList[j]];
                var combinationsLcp = Frame.Lcp[mvList[i], mvList[j]];
                var combinationsRcp = Frame.Rcp[mvList[i], mvList[j]];
                var combinationsFdp = Frame.Fdp[mvList[i], mvList[j]];
                var combinationsHip = Frame.Hip[mvList[i], mvList[j]];
                var combinationsAcp = Frame.Acp[mvList[i], mvList[j]];
                var combinationsCp = Frame.Cp[mvList[i], mvList[j]];


                LogComposer
                    .AppendLineAtNewLine("VSpaceDimension: " + vSpaceDim)
                    .AppendLine();

                VerifyProductResults("Outer Product - Hash Table", hashOp, computedOp);
                VerifyProductResults("Geometric Product - Hash Table", hashGp, computedGp);
                VerifyProductResults("Scalar Product - Hash Table", hashSp, computedSp);
                VerifyProductResults("Left Contraction Product - Hash Table", hashLcp, computedLcp);
                VerifyProductResults("Right Contraction Product - Hash Table", hashRcp, computedRcp);
                VerifyProductResults("Fat-Dot Product - Hash Table", hashFdp, computedFdp);
                VerifyProductResults("Hestenes Inner Product - Hash Table", hashHip, computedHip);
                VerifyProductResults("Anti-Commutator Product - Hash Table", hashAcp, computedAcp);
                VerifyProductResults("Commutator Product - Hash Table", hashCp, computedCp);

                VerifyProductResults("Outer Product - Array Table Table", arrayOp, computedOp);
                VerifyProductResults("Geometric Product - Array Table Table", arrayGp, computedGp);
                VerifyProductResults("Scalar Product - Array Table Table", arraySp, computedSp);
                VerifyProductResults("Left Contraction Product - Array Table Table", arrayLcp, computedLcp);
                VerifyProductResults("Right Contraction Product - Array Table Table", arrayRcp, computedRcp);
                VerifyProductResults("Fat-Dot Product - Array Table Table", arrayFdp, computedFdp);
                VerifyProductResults("Hestenes Inner Product - Array Table Table", arrayHip, computedHip);
                VerifyProductResults("Anti-Commutator Product - Array Table Table", arrayAcp, computedAcp);
                VerifyProductResults("Commutator Product - Array Table Table", arrayCp, computedCp);

                VerifyProductResults("Outer Product - Tree Table", treeOp, computedOp);
                VerifyProductResults("Geometric Product - Tree Table", treeGp, computedGp);
                VerifyProductResults("Scalar Product - Tree Table", treeSp, computedSp);
                VerifyProductResults("Left Contraction Product - Tree Table", treeLcp, computedLcp);
                VerifyProductResults("Right Contraction Product - Tree Table", treeRcp, computedRcp);
                VerifyProductResults("Fat-Dot Product - Tree Table", treeFdp, computedFdp);
                VerifyProductResults("Hestenes Inner Product - Tree Table", treeHip, computedHip);
                VerifyProductResults("Anti-Commutator Product - Tree Table", treeAcp, computedAcp);
                VerifyProductResults("Commutator Product - Tree Table", treeCp, computedCp);

                VerifyProductResults("Outer Product - Combinations Table", combinationsOp, computedOp);
                VerifyProductResults("Geometric Product - Combinations Table", combinationsGp, computedGp);
                VerifyProductResults("Scalar Product - Combinations Table", combinationsSp, computedSp);
                VerifyProductResults("Left Contraction Product - Combinations Table", combinationsLcp, computedLcp);
                VerifyProductResults("Right Contraction Product - Combinations Table", combinationsRcp, computedRcp);
                VerifyProductResults("Fat-Dot Product - Combinations Table", combinationsFdp, computedFdp);
                VerifyProductResults("Hestenes Inner Product - Combinations Table", combinationsHip, computedHip);
                VerifyProductResults("Anti-Commutator Product - Combinations Table", combinationsAcp, computedAcp);
                VerifyProductResults("Commutator Product - Combinations Table", combinationsCp, computedCp);

                LogComposer.AppendLine().AppendLine();
            }


            return LogComposer.ToString();
        }
    }
}
