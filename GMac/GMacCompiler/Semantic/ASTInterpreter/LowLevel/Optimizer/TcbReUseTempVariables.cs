using System.Collections.Generic;
using GMac.GMacAPI.CodeBlock;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer
{
    internal sealed class TcbReUseTempVariables : TcbProcessor
    {
        internal static GMacCodeBlock Process(GMacCodeBlock codeBlock)
        {
            var processor = new TcbReUseTempVariables(codeBlock);

            processor.BeginProcessing();

            return codeBlock;
        }


        private TcbReUseTempVariables(GMacCodeBlock codeBlock)
            : base(codeBlock)
        {
            
        }


        protected override void BeginProcessing()
        {
            var finalNameIndexList = new List<int>();
        
            //Go through all temp variables
            foreach (var tempVar in CodeBlock.TempVariables)
            {
                var nameIndex = 0;
                var isReused = false;

                //Find the first available index for re-use; if any
                for (var i = 0; i < finalNameIndexList.Count; i++)
                {
                    if (finalNameIndexList[i] > tempVar.ComputationOrder)
                        continue;

                    nameIndex = i + 1;
                    isReused = true;
                    break;
                }

                //Find the computation order of the last variable using this temp in its RHS
                var lastUseEvalOrder = tempVar.LastUsingComputationOrder;

                if (nameIndex > 0)
                    //Re-use the found name index and update the re-use list
                    finalNameIndexList[nameIndex - 1] = lastUseEvalOrder;

                else
                {
                    //No temp can be re-used. 
                    finalNameIndexList.Add(lastUseEvalOrder);

                    //This LHS variable will have a new use index
                    nameIndex = finalNameIndexList.Count;
                }

                //Modify the reuse information of this temp variable
                tempVar.SetReuseInfo(isReused, nameIndex - 1);
            }
        }
    }
}
