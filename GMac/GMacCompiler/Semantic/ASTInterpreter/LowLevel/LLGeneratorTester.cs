using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraSymbolicsLib;
using GeometricAlgebraSymbolicsLib.Cas.Mathematica.Expression;
using GMac.GMacAPI.CodeBlock;
using GMac.GMacCompiler.Semantic.AST;
using GMac.GMacCompiler.Semantic.AST.Extensions;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Generator;
using GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel.Optimizer;
using IronyGrammars.Semantic.Expression.Value;
using IronyGrammars.Semantic.Type;
using TextComposerLib.Text.Linear;
using Wolfram.NETLink;

namespace GMac.GMacCompiler.Semantic.ASTInterpreter.LowLevel
{
    internal sealed class LlGeneratorTester
    {
        private Random _randomSource;

        public GMacMacro BaseMacro { get; }

        private readonly Stopwatch _timer = new Stopwatch();

        public LinearComposer Log { get; } = new LinearComposer();


        public LlGeneratorTester(GMacMacro baseMacro)
        {
            BaseMacro = baseMacro;
        }


        private ValuePrimitive<MathematicaScalar> GetRandomValue(ILanguageType valueType)
        {
            Expr valueExpr;

            if (valueType.IsInteger())
                valueExpr = new Expr(_randomSource.Next(1, 10));

            else if (valueType.IsBoolean())
                valueExpr = new Expr(_randomSource.Next(0, 1) != 0);

            else if (valueType.IsScalar())
                valueExpr = new Expr(_randomSource.NextDouble() * 10.0 - 5.0);
                //value_expr = new Expr(new Expr(ExpressionType.Symbol, "Rational"), _RandomSource.Next(1, 10), _RandomSource.Next(1, 10));

            else
                throw new InvalidOperationException();

            return 
                ValuePrimitive<MathematicaScalar>.Create(
                    (TypePrimitive)valueType, 
                    MathematicaScalar.Create(GaSymbolicsUtils.Cas, valueExpr)
                    );
        }

        private static void ExecuteAssignment(GMacCbComputedVariable item, Dictionary<string, ValuePrimitive<MathematicaScalar>> valuesDict)
        {
            //var rhsExprText = 
            //    item
            //    .AssignedRhsValue
            //    .ToString();

            //var rhsValueText = 
            //    item
            //    .AssignedRhsSymbolicScalar
            //    .GetDistinctLowLevelVariablesNames()
            //    .Aggregate(
            //        rhsExprText, 
            //        (current, rhsItemName) => 
            //            current.Replace(rhsItemName, valuesDict[rhsItemName].ToString())
            //    );

            //var rhsValueExpr = SymbolicUtils.Evaluator.Simplify(rhsValueText);

            //var rhsValue = ValuePrimitive<MathematicaScalar>.Create(
            //    item.DataItemType,
            //    MathematicaScalar.Create(SymbolicUtils.Cas, rhsValueExpr)
            //    );

            //if (valuesDict.ContainsKey(item.ItemName))
            //    valuesDict[item.ItemName] = rhsValue;
            //else
            //    valuesDict.Add(item.ItemName, rhsValue);
        }

        public string TestGenerationByConstantInputs()
        {
            _randomSource = new Random(DateTime.Now.Millisecond);

            var gen1 = new LlGenerator(BaseMacro);

            //Fill input parameters by random values
            foreach (var param in BaseMacro.Parameters)
                if (param.DirectionIn)
                {
                    var valueAccessList = param.ExpandAll();

                    foreach (var valueAccess in valueAccessList)
                    {
                        var assignedValue = GetRandomValue(valueAccess.ExpressionType);

                        gen1.DataTable.DefineConstantInputParameter(valueAccess, assignedValue);
                    }
                }
                else
                    gen1.DataTable.DefineOutputParameter(param);

            //Evaluate (generate) the outputs using the constant inputs
            gen1.GenerateLowLevelItems();

            Log.AppendLine();
            Log.AppendLineAtNewLine("First Generator Result");
            Log.AppendAtNewLine(gen1.ToString());

            Log.AppendLine();
            Log.AppendAtNewLine("Begin full low level symbolic generation ... ");
            _timer.Reset();
            _timer.Start();

            //Use another generator with all input parameters set as variable
            var gen2 = new LlGenerator(BaseMacro);

            gen2.DefineAllParameters();

            //Generate the symbolic expressions without optimizations
            gen2.GenerateLowLevelItems();

            //Optimize the generated symbolic expressions
            var genOpt = TcbOptimizer.Process(gen2);
            //genOpt.OptimizeLowLevelItems();

            _timer.Stop();
            Log.AppendLine(" Finished after " + _timer.Elapsed);
            Log.AppendLine();

            Log.AppendLine();
            Log.AppendLineAtNewLine("Second Generator Result");
            Log.AppendAtNewLine(genOpt.ToString());

            //Evaluate all symbolic expressions in order using the constant values for inputs from the first generator
            var valuesDict = 
                new Dictionary<string, ValuePrimitive<MathematicaScalar>>();

            foreach (var inputItem in genOpt.InputVariables)
            {
                var inputItemName = inputItem.LowLevelName;
                var inputItemValue = gen1.DataTable.GetItemByName(inputItem.LowLevelName).AssignedRhsValue;

                valuesDict.Add(inputItemName, inputItemValue);

                Log.AppendAtNewLine("Input ");
                Log.Append(inputItem.ValueAccessName);
                Log.Append(" = ");
                Log.AppendLine(inputItemValue.ToString());
            }

            foreach (var item in genOpt.ComputedVariables)
                ExecuteAssignment(item, valuesDict);

            //Report the differences between the output parameters of the two methods of computation
            foreach (var outputItem1 in gen1.DataTable.Outputs)
            {
                var outputItem1Value = outputItem1.AssignedRhsValue;
                var outputItem2Value = valuesDict[outputItem1.ItemName];

                Log.AppendAtNewLine("Input ");
                Log.Append(outputItem1.HlItemName);
                Log.AppendLine(" = ");
                Log.IncreaseIndentation();
                Log.AppendLineAtNewLine(outputItem1Value.ToString());
                Log.AppendLineAtNewLine(outputItem2Value.ToString());
                Log.DecreaseIndentation();
            }

            return Log.ToString();
        }
    }
}
