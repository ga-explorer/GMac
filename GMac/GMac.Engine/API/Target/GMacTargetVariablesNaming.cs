using System;
using System.Collections.Generic;
using System.Linq;
using GMac.Engine.API.CodeBlock;
using GMac.Engine.AST;
using GMac.Engine.AST.Expressions;
using GMac.Engine.AST.Symbols;
using GMac.Engine.Compiler.Semantic.AST.Extensions;

namespace GMac.Engine.API.Target
{
    /// <summary>
    /// This class is used to set target variable names for input, output, and temp variables in the code block
    /// generated from the macro binding
    /// </summary>
    public sealed class GMacTargetVariablesNaming
    {
        /// <summary>
        /// The target language for this variables naming object
        /// </summary>
        public GMacLanguageServer GMacLanguage { get; private set; }

        /// <summary>
        /// The code block used in the naming process
        /// </summary>
        public GMacCodeBlock CodeBlock { get; }

        /// <summary>
        /// The base macro of the code block used in the naming process
        /// </summary>
        public AstMacro BaseMacro => CodeBlock.BaseMacro;


        internal GMacTargetVariablesNaming(GMacLanguageServer targetLanguage, GMacCodeBlock codeBlock)
        {
            GMacLanguage = targetLanguage;
            CodeBlock = codeBlock;
        }


        /// <summary>
        /// Sets the target variables name for a scalar input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="targetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetScalarParameter(AstDatastoreValueAccess valueAccess, string targetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (CodeBlock.TryGetParameterVariable(valueAccess, out var paramVar))
                paramVar.TargetVariableName = targetVarName;

            return this;
        }

        /// <summary>
        /// Sets the target variables name for a scalar input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="targetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetScalarParameter(string valueAccessName, string targetVarName)
        {
            return SetScalarParameter(BaseMacro.Parameter(valueAccessName), targetVarName);
        }

        /// <summary>
        /// Sets the target variables name for a scalar input variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="targetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetScalarInputParameter(AstDatastoreValueAccess valueAccess, string targetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (!CodeBlock.TryGetInputParameterVariable(valueAccess, out var paramVar))
                return this;

            paramVar.TargetVariableName = targetVarName;

            return this;
        }

        /// <summary>
        /// Sets the target variables name for a scalar input variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="targetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetScalarInputParameter(string valueAccessName, string targetVarName)
        {
            return SetScalarInputParameter(BaseMacro.Parameter(valueAccessName), targetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, Func<ulong, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = (ulong)primitiveValueAccess.GetBasisBladeId();

                var targetVarName = getTargetVarName(id);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, Func<ulong, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, Func<AstFrame, int, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            var frame = valueAccess.TypeAsFrameMultivector.ParentFrame;

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                var targetVarName = getTargetVarName(frame, id);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, Func<AstFrame, int, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, Func<AstFrameBasisBlade, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var basisBlade = primitiveValueAccess.GetBasisBlade();

                var targetVarName = getTargetVarName(basisBlade);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, Func<AstFrameBasisBlade, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, params string[] getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                if (getTargetVarName.Length <= id) continue;

                var targetVarName = getTargetVarName[id];

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, params string[] getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, IDictionary<int, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                if (getTargetVarName.TryGetValue(id, out var targetVarName) == false) continue;

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="targetVarNamesDictionary"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, IDictionary<int, string> targetVarNamesDictionary)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), targetVarNamesDictionary);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="subspace"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, AstFrameSubspace subspace, Func<int, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            return SetMultivectorParameters(valueAccess, subspace.BasisBladeIDs, getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="subspace"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, AstFrameSubspace subspace, Func<int, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), subspace.BasisBladeIDs, getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="idsList"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, IEnumerable<int> idsList, Func<int, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList =
                CodeBlock.GetParametersValueAccess(
                    valueAccess.SelectMultivectorComponents(idsList)
                    );

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                var targetVarName = getTargetVarName(id);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="idsList"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, IEnumerable<int> idsList, Func<int, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), idsList, getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="grade"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, int grade, Func<ulong, string> getTargetVarName)
        {
            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList = 
                CodeBlock.GetParametersValueAccess(
                    valueAccess.SelectMultivectorComponents(grade)
                    );

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                var targetVarName = getTargetVarName((ulong)id);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="grade"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, int grade, Func<ulong, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), grade, getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(AstDatastoreValueAccess valueAccess, int grade, IEnumerable<int> indexList, Func<int, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            if (valueAccess.AssociatedValueAccess.ExpressionType.IsFrameMultivector() == false)
                throw new InvalidOperationException("Specified macro parameter is not of multivector type");

            //Find the parameters in the code block that are components of the given multivector value access
            var primitiveValueAccessList =
                CodeBlock.GetParametersValueAccess(
                    valueAccess.SelectMultivectorComponents(grade, indexList)
                    );

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var id = primitiveValueAccess.GetBasisBladeId();

                var targetVarName = getTargetVarName(id);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for a multivector input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="grade"></param>
        /// <param name="indexList"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetMultivectorParameters(string valueAccessName, int grade, IEnumerable<int> indexList, Func<int, string> getTargetVarName)
        {
            return SetMultivectorParameters(BaseMacro.Parameter(valueAccessName), grade, indexList, getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(AstDatastoreValueAccess valueAccess, Func<string, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            //Find the parameters in the code block that are components of the given value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var name = primitiveValueAccess.ValueAccessName;

                var targetVarName = getTargetVarName(name);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(string valueAccessName, Func<string, string> getTargetVarName)
        {
            return SetParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="targetVarNamesDict"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(AstDatastoreValueAccess valueAccess, IDictionary<string, string> targetVarNamesDict)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            //Find the parameters in the code block that are components of the given value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var name = primitiveValueAccess.ValueAccessName;

                if (targetVarNamesDict.TryGetValue(name, out var targetVarName))
                    SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="targetVarNamesDict"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(string valueAccessName, IDictionary<string, string> targetVarNamesDict)
        {
            return SetParameters(BaseMacro.Parameter(valueAccessName), targetVarNamesDict);
        }

        /// <summary>
        /// Sets the target variables names for an input\output parameter
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(AstDatastoreValueAccess valueAccess, Func<AstDatastoreValueAccess, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            //Find the parameters in the code block that are components of the given value access
            var primitiveValueAccessList = CodeBlock.GetParametersValueAccess(valueAccess);

            foreach (var primitiveValueAccess in primitiveValueAccessList)
            {
                var targetVarName = getTargetVarName(primitiveValueAccess);

                SetScalarParameter(primitiveValueAccess, targetVarName);
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(string valueAccessName, Func<AstDatastoreValueAccess, string> getTargetVarName)
        {
            return SetParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccess"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(AstDatastoreValueAccess valueAccess, Func<IGMacCbParameterVariable, string> getTargetVarName)
        {
            if (valueAccess.IsNullOrInvalid()) return this;

            //Find the parameters in the code block that are components of the given value access
            var paramVarsList = CodeBlock.GetParameters(valueAccess);

            foreach (var paramVar in paramVarsList)
                paramVar.TargetVariableName = getTargetVarName(paramVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for an input\output variable parameter in the code block
        /// </summary>
        /// <param name="valueAccessName"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetParameters(string valueAccessName, Func<IGMacCbParameterVariable, string> getTargetVarName)
        {
            return SetParameters(BaseMacro.Parameter(valueAccessName), getTargetVarName);
        }

        /// <summary>
        /// Sets the target variables names for input, output, and temp code block variables
        /// </summary>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetVariables(Func<GMacCbVariable, string> getTargetVarName)
        {
            foreach (var tempVar in CodeBlock.Variables)
                tempVar.TargetVariableName = getTargetVarName(tempVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for input, output, and temp code block variables
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetVariables(IEnumerable<GMacCbVariable> variables, Func<GMacCbVariable, string> getTargetVarName)
        {
            foreach (var tempVar in variables)
                tempVar.TargetVariableName = getTargetVarName(tempVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for input code block variables
        /// </summary>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetInputVariables(Func<GMacCbInputVariable, string> getTargetVarName)
        {
            foreach (var inputVar in CodeBlock.InputVariables)
                inputVar.TargetVariableName = getTargetVarName(inputVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for input code block variables
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetInputVariables(IEnumerable<GMacCbInputVariable> variables, Func<GMacCbInputVariable, string> getTargetVarName)
        {
            foreach (var inputVar in variables)
                inputVar.TargetVariableName = getTargetVarName(inputVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for output code block variables
        /// </summary>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetOutputVariables(Func<GMacCbOutputVariable, string> getTargetVarName)
        {
            foreach (var tempVar in CodeBlock.OutputVariables)
                tempVar.TargetVariableName = getTargetVarName(tempVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for output code block variables
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetOutputVariables(IEnumerable<GMacCbOutputVariable> variables, Func<GMacCbOutputVariable, string> getTargetVarName)
        {
            foreach (var tempVar in variables)
                tempVar.TargetVariableName = getTargetVarName(tempVar);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for temp code block variables
        /// </summary>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetTempVariables(Func<int, string> getTargetVarName)
        {
            foreach (var tempVar in CodeBlock.TempVariables)
                tempVar.TargetVariableName = getTargetVarName(tempVar.NameIndex);

            return this;
        }

        /// <summary>
        /// Sets the target variables names for temp code block variables
        /// </summary>
        /// <param name="targetVarNamesDict"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetTempVariables(IDictionary<int, string> targetVarNamesDict)
        {
            foreach (var tempVar in CodeBlock.TempVariables)
            {
                if (targetVarNamesDict.TryGetValue(tempVar.NameIndex, out var targetVarName))
                    tempVar.TargetVariableName = targetVarName;
            }

            return this;
        }

        /// <summary>
        /// Sets the target variables names for temp code block variables
        /// </summary>
        /// <param name="targetVarNamesArray"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetTempVariables(params string[] targetVarNamesArray)
        {
            var length = targetVarNamesArray.Length;

            var tempVarsList =
                CodeBlock
                .TempVariables
                .Where(tempVar => tempVar.NameIndex >= 0 && tempVar.NameIndex < length);

            foreach (var tempVar in tempVarsList)
                tempVar.TargetVariableName = targetVarNamesArray[tempVar.NameIndex];

            return this;
        }

        /// <summary>
        /// Sets the target variables names for temp code block variables
        /// </summary>
        /// <param name="getTargetVarName"></param>
        /// <returns></returns>
        public GMacTargetVariablesNaming SetTempVariables(Func<GMacCbTempVariable, string> getTargetVarName)
        {
            foreach (var tempVar in CodeBlock.TempVariables)
                tempVar.TargetVariableName = getTargetVarName(tempVar);

            return this;
        }
    }
}
