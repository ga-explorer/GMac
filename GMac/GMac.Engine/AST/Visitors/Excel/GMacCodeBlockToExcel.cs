using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeComposerLib.SyntaxTree.Expressions;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.API.Target;
using GMac.Engine.API.Target.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace GMac.Engine.AST.Visitors.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GMacCodeBlockToExcel : GMacAstToExcelConverter
    {
        private ExcelWorksheet _activeWorksheet;

        private readonly Dictionary<string, int> _variableColumn 
            = new Dictionary<string, int>();

        private readonly Dictionary<string, string> _variableRow
             = new Dictionary<string, string>();


        public GMacCodeBlock CodeBlock { get; }

        public int MaxLevels { get; private set; }


        public GMacCodeBlockToExcel(GMacCodeBlock codeBlock)
        {
            CodeBlock = codeBlock;
        }


        private int Sheet2_GetVariableColumn(IGMacCbVariable variable)
        {
            return variable.IsTemp 
                ? _variableColumn["temp" + ((GMacCbTempVariable)variable).NameIndex] 
                : _variableColumn[variable.LowLevelName];
        }

        private void Sheet2_AddVariableNames()
        {
            var colIndex = 2;
            foreach (var inputVar in CodeBlock.InputVariables)
            {
                _activeWorksheet.Cells[1, colIndex].Value = 
                    inputVar.ValueAccessName;

                _activeWorksheet.Cells[2, colIndex].Value =
                    inputVar.LowLevelName;

                _variableColumn.Add(inputVar.LowLevelName, colIndex);

                colIndex++;
            }

            for (var tempIndex = 0; tempIndex < CodeBlock.TargetTempVarsCount; tempIndex++)
            {
                _activeWorksheet.Cells[1, colIndex].Value = 
                    "temp" + tempIndex;

                _variableColumn.Add("temp" + tempIndex, colIndex);

                colIndex++;
            }

            foreach (var outputVar in CodeBlock.OutputVariables)
            {
                _activeWorksheet.Cells[1, colIndex].Value = 
                    outputVar.ValueAccessName;

                _activeWorksheet.Cells[2, colIndex].Value =
                    outputVar.LowLevelName;

                _variableColumn.Add(outputVar.LowLevelName, colIndex);

                colIndex++;
            }

            _activeWorksheet.Cells[1, colIndex].Value = "Computed Variable";
            _activeWorksheet.Cells[2, colIndex++].Value = "Kind";
            _activeWorksheet.Cells[2, colIndex++].Value = "Name";
            _activeWorksheet.Cells[2, colIndex].Value = "Expression";
        }

        private void Sheet2_AddComputations()
        {
            var lastDataColumn = 1 + 
                                 CodeBlock.InputVariablesCount + 
                                 CodeBlock.TargetTempVarsCount +
                                 CodeBlock.OutputVariables.Count();

            var rowIndex = 3;
            foreach (var computedVar in CodeBlock.ComputedVariables)
            {
                _activeWorksheet.Cells[rowIndex, 1].Value = rowIndex - 2;

                _activeWorksheet.Cells[rowIndex, lastDataColumn + 1].Value = 
                    computedVar.IsOutput ? "Output " : "Temp";

                _activeWorksheet.Cells[rowIndex, lastDataColumn + 2].Value =
                    computedVar.IsOutput
                    ? ((GMacCbOutputVariable)computedVar).ValueAccessName
                    : computedVar.LowLevelName;

                _activeWorksheet.Cells[rowIndex, lastDataColumn + 3].Value =
                    computedVar.RhsExpr.ToString();

                var computedVarColumn = Sheet2_GetVariableColumn(computedVar);

                _activeWorksheet.Cells[rowIndex, computedVarColumn].Style.Fill.PatternType = ExcelFillStyle.Solid;
                _activeWorksheet.Cells[rowIndex, computedVarColumn].Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);

                foreach (var rhsVar in computedVar.RhsVariables)
                {
                    var rhsVarColumn = Sheet2_GetVariableColumn(rhsVar);

                    _activeWorksheet.Cells[rowIndex, rhsVarColumn].Value = "R";
                }

                rowIndex++;
            }
        }

        private void Sheet2_PreFormatSheet()
        {
            var firstColumn = 1;
            var firstRow = 1;
            var lastColumn = firstColumn +
                             CodeBlock.InputVariablesCount +
                             CodeBlock.TargetTempVarsCount +
                             CodeBlock.OutputVariables.Count() +
                             3;
            var lastRow = firstRow + 1 + CodeBlock.ComputedVariables.Count;

            //Set global style for sheet
            using (var range = _activeWorksheet.Cells[firstRow, firstColumn, lastRow, lastColumn])
            {
                range.Style.Font.Name = "Consolas";
                range.Style.Font.Size = 12;

                range.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Color.SetColor(Color.Gray);
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Color.SetColor(Color.Gray);
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Color.SetColor(Color.Gray);
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Color.SetColor(Color.Gray);

                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            //Set alignment of last column
            using (var range = _activeWorksheet.Cells[firstRow + 2, lastColumn - 1, lastRow, lastColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }

            //Set columns header style
            using (var range = _activeWorksheet.Cells[firstRow, firstColumn + 1, firstRow + 1, lastColumn - 3])
            {
                range.Style.TextRotation = 90;
            }

            using (var range = _activeWorksheet.Cells[firstRow, firstColumn + 1, firstRow + 1, lastColumn])
            {
                range.Style.Font.Bold = true;
            }

            //Set rows header style
            using (var range = _activeWorksheet.Cells[firstRow + 2, firstColumn, lastRow, firstColumn])
            {
                range.Style.Font.Bold = true;
            }

            //Set proper row heights
            _activeWorksheet.DefaultColWidth = 3.3;
            _activeWorksheet.DefaultRowHeight = 22.5;

            for (var rowIndex = firstRow + 2; rowIndex <= lastRow; rowIndex++)
            {
                var row = _activeWorksheet.Row(firstRow);

                row.CustomHeight = true;
                row.Height = 22.5;
            }

            //Merge first two rows of first column and last 3 columns
            _activeWorksheet.Cells[firstRow, firstColumn, firstRow + 1, firstColumn].Merge = true;
            _activeWorksheet.Cells[firstRow, lastColumn - 2, firstRow, lastColumn].Merge = true;

            //Merge first two rows of temp variables columns
            var c1 = firstColumn + 1 + CodeBlock.InputVariablesCount;
            var c2 = c1 + CodeBlock.TargetTempVarsCount - 1;
            for (var colIndex = c1; colIndex <= c2; colIndex++)
            {
                _activeWorksheet.Cells[firstRow, colIndex, firstRow + 1, colIndex].Merge = true;
            }
        }

        private void Sheet2_AddData()
        {
            //Add column headers containing code block input, temp, and output
            //variables
            Sheet2_AddVariableNames();

            Sheet2_AddComputations();
        }

        private void Sheet2_PostFormatSheet()
        {
            var firstColumn = 1;
            var firstRow = 1;
            var lastColumn = firstColumn +
                             CodeBlock.InputVariablesCount +
                             CodeBlock.TargetTempVarsCount +
                             CodeBlock.OutputVariables.Count() +
                             3;
            var lastRow = firstRow + 1 + CodeBlock.ComputedVariables.Count;

            //Set color and border around columns header
            using (var range = _activeWorksheet.Cells[firstRow, firstColumn + 1, firstRow + 1, lastColumn])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
                range.Style.Font.Bold = true;
            }

            //Set color and border around rows header
            using (var range = _activeWorksheet.Cells[firstRow + 2, firstColumn, lastRow, firstColumn])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
                range.Style.Font.Bold = true;
            }

            //Set border around input variables area
            var varsCol1 = firstColumn + 1;
            var varsCol2 = varsCol1 + CodeBlock.InputVariablesCount - 1;
            using (var range = _activeWorksheet.Cells[firstRow, varsCol1, lastRow, varsCol2])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
            }

            //Set border around temp variables area
            varsCol1 = varsCol2 + 1;
            varsCol2 = varsCol1 + CodeBlock.TargetTempVarsCount - 1;
            using (var range = _activeWorksheet.Cells[firstRow, varsCol1, lastRow, varsCol2])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
            }

            //Set border around output variables area
            varsCol1 = varsCol2 + 1;
            varsCol2 = varsCol1 + CodeBlock.OutputVariables.Count() - 1;
            using (var range = _activeWorksheet.Cells[firstRow, varsCol1, lastRow, varsCol2])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Medium, Color.Black);
            }

            //Set border around sheet data
            using (var range = _activeWorksheet.Cells[firstRow, firstColumn, lastRow, lastColumn])
            {
                range.Style.Border.BorderAround(ExcelBorderStyle.Thick, Color.Black);

                range.AutoFitColumns();
            }

            _activeWorksheet.View.FreezePanes(firstRow + 2, firstColumn + 1);
            _activeWorksheet.Select(_activeWorksheet.Cells[firstRow + 2, firstColumn + 1]);
        }

        private void CreateSheet2()
        {
            _activeWorksheet = Worksheets.Add("Data Flow");

            Sheet2_PreFormatSheet();

            Sheet2_AddData();

            Sheet2_PostFormatSheet();
        }


        private string Sheet1_GetFormula(SteExpression symbolicExpr)
        {
            var converter = new MathematicaToExcelConverter();

            var formulaExpr = converter.Convert(symbolicExpr, _variableRow);

            var excelServer = GMacLanguageServer.Excel2007();

            return excelServer
                .CodeGenerator
                .GenerateCode(formulaExpr);
        }

        private void Sheet1_PreFormatSheet()
        {
            var firstRow = 1;
            var lastRow = firstRow +
                          CodeBlock.InputVariablesCount +
                          CodeBlock.ComputedVariables.Count;

            using (var range = _activeWorksheet.Cells[firstRow, 1, lastRow, 3])
            {
                range.Style.Font.Name = "Consolas";
                range.Style.Font.Size = 12;
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            using (var range = _activeWorksheet.Cells[firstRow, 1, firstRow, 3])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                range.Style.Font.Bold = true;
            }

            var colorRow = false;
            var color = Color.LightBlue;
            for (var rowIndex = firstRow + 1; rowIndex <= lastRow; rowIndex++)
            {
                if (colorRow)
                    using (var range = _activeWorksheet.Cells[rowIndex, 1, rowIndex, 3])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(color);
                    }

                colorRow = !colorRow;
            }
        }

        private void Sheet1_AddData()
        {
            _activeWorksheet.Cells[1, 1].Value = "Variable";
            _activeWorksheet.Cells[1, 2].Value = "Symbolic Relation";
            _activeWorksheet.Cells[1, 3].Value = "Value";

            var rowIndex = 2;
            foreach (var inputVar in CodeBlock.InputVariables)
            {
                _activeWorksheet.Cells[rowIndex, 1].Value = 
                    inputVar.ValueAccessName;

                _activeWorksheet.Cells[rowIndex, 2].Value =
                    inputVar.LowLevelName;

                var valueCell = _activeWorksheet.Cells[rowIndex, 3];
                valueCell.Value = 0.0d;
                _variableRow.Add(inputVar.LowLevelName, valueCell.Address);

                rowIndex++;
            }

            foreach (var tempVar in CodeBlock.TempVariables)
            {
                _activeWorksheet.Cells[rowIndex, 1].Value =
                    tempVar.MidLevelName;

                _activeWorksheet.Cells[rowIndex, 2].Value =
                    tempVar.LowLevelName + " = " + tempVar.RhsExpr;

                var valueCell = _activeWorksheet.Cells[rowIndex, 3];
                valueCell.Formula =
                    "=" + Sheet1_GetFormula(tempVar.RhsExpr);

                _variableRow.Add(tempVar.LowLevelName, valueCell.Address);

                rowIndex++;
            }

            foreach (var outputVar in CodeBlock.OutputVariables)
            {
                _activeWorksheet.Cells[rowIndex, 1].Value =
                    outputVar.ValueAccessName;

                _activeWorksheet.Cells[rowIndex, 2].Value =
                    outputVar.LowLevelName + " = " + outputVar.RhsExpr; ;

                var valueCell = _activeWorksheet.Cells[rowIndex, 3];
                valueCell.Formula =
                    "=" + Sheet1_GetFormula(outputVar.RhsExpr);

                _variableRow.Add(outputVar.LowLevelName, valueCell.Address);

                rowIndex++;
            }
        }

        private void Sheet1_PostFormatSheet()
        {
            var firstRow = 1;
            var lastRow = firstRow +
                          CodeBlock.InputVariablesCount +
                          CodeBlock.ComputedVariables.Count;

            using (var range = _activeWorksheet.Cells[firstRow, 1, lastRow, 3])
            {
                range.AutoFitColumns(3.3);
            }

            _activeWorksheet.View.FreezePanes(firstRow + 1, 3);
            _activeWorksheet.Select(_activeWorksheet.Cells[firstRow + 1, 3]);
        }

        private void CreateSheet1()
        {
            _activeWorksheet = Worksheets.Add("Computations");

            Sheet1_PreFormatSheet();

            Sheet1_AddData();

            Sheet1_PostFormatSheet();
        }


        public ExcelPackage ToExcel()
        {
            var computationLevels =
                CodeBlock
                    .NonConstantOutputVariables
                    .Select(v => v.MaxComputationLevel)
                    .ToArray();

            MaxLevels = computationLevels.Length == 0 ? 1 : computationLevels.Max();

            CreateSheet1();

            CreateSheet2();

            return Package;
        }
    }
}
