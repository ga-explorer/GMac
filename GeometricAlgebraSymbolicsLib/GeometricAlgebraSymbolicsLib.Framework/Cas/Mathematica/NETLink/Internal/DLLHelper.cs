//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;


namespace Wolfram.NETLink.Internal {

/// <summary>
/// This class creates dynamically-defined calls to DLL functions needed by the Mathematica function DefineDLLFunction.
/// </summary>
///
internal class DLLHelper {    
    
    // All types that hold dynamically created DLL call defs are part of this module.
    private static ModuleBuilder dllModuleBuilder;
    private static string dllNamespace = "Wolfram.NETLink.DynamicDLLNamespace";
    private static string dllTypePrefix = "DLLWrapper";
    private static string dllAssemblyName = "DynamicDLLAssembly";
    private static string dllModuleName = "DynamicDLLModule";
    // Used only to ensure that each dynamic type has a unique name.
    private static int index = 1;


    static DLLHelper() {

        AssemblyName assemblyName = new AssemblyName();
        assemblyName.Name = dllAssemblyName;
        AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        dllModuleBuilder = assemblyBuilder.DefineDynamicModule(dllModuleName);
    }


    /// <summary>
    /// This signature is for calls where the func name, arg types, etc., are supplied individually.
    /// </summary>
    /// 
    internal static string CreateDLLCall(string funcName, string dllName, string callConv,
                                            string retTypeName, string[] argTypeNames, bool[] areOutParams, string strFormat) {

        // TypeLoadException from these...
        Type retType = retTypeName == null ? typeof(void) : TypeLoader.GetType(Utils.addSystemNamespace(retTypeName), true);
        Type[] argTypes = new Type[argTypeNames == null ? 0 : argTypeNames.Length];
        if (argTypeNames != null) {
            for (int i = 0; i < argTypes.Length; i++)
                argTypes[i] = TypeLoader.GetType(Utils.addSystemNamespace(argTypeNames[i]), true);
        }
        // strFormat is guaranteed to be lower case by M code.
        CharSet charSet = strFormat == "ansi" ? CharSet.Ansi : (strFormat == "unicode" ? CharSet.Unicode : CharSet.Auto);
        CallingConvention callingConv;
        // callConv is sent from M as lower case.
        if (callConv == "cdecl")
            callingConv = CallingConvention.Cdecl;
        else if (callConv == "thiscall")
            callingConv = CallingConvention.ThisCall;
        else if (callConv == "stdcall")
            callingConv = CallingConvention.StdCall;
        else
            callingConv = CallingConvention.Winapi;

        TypeBuilder typeBuilder = dllModuleBuilder.DefineType(dllNamespace + "." + dllTypePrefix + index++, TypeAttributes.Public);
        MethodBuilder methodBuilder = typeBuilder.DefinePInvokeMethod(funcName, dllName,
                MethodAttributes.PinvokeImpl | MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard, retType, argTypes, callingConv, charSet);
        // Don't ask me why this is not default with the PInvokeImpl attribute, but it isn't, at least in the
        // first release of .NET:
        methodBuilder.SetImplementationFlags(MethodImplAttributes.PreserveSig);
        // Mark any that are out-only params.
        for (int i = 0; i < areOutParams.Length; i++) {
            if (areOutParams[i])
                // Note that params are indexed starting with 1, not 0.
                methodBuilder.DefineParameter(i + 1, ParameterAttributes.Out, null);
        }
        Type t = typeBuilder.CreateType();

        return t.FullName;
    }


    /// <summary>
    /// This signature is for calls where the "extern" declaration is provided as a complete string of C# code.
    /// </summary>
    /// 
    internal static string[] CreateDLLCall(string declaration, string[] referencedAssemblies, string language) {

        string newClassName = dllTypePrefix + index++;

        string source = null;
        CodeSnippetCompileUnit code = null;
        if (language == "csharp") {
            code = new CodeSnippetCompileUnit(
                "using System;" +
                "using System.Runtime.InteropServices;" +
                "namespace " + dllNamespace + "{" +
                    "public class " + newClassName + "{" +
                        declaration + (declaration.EndsWith(";") ? "" : ";") +
                    "}" +
                "}"
                );
        } else {
            // Visual Basic
            // TODO: This is not working. Error is currently issued in M code if you try to use VB syntax.
            source =
                "Imports System" + "\n" +
                "Imports System.Runtime.InteropServices" + "\n" +
                "Namespace " + dllNamespace + "\n" + 
                    "Public Class " + newClassName + "\n" +
                        declaration + "\n" +
                    "End Class" + "\n" +
                "End Namespace";
        }

        CompilerParameters compilerParams = new CompilerParameters(referencedAssemblies);
        compilerParams.GenerateInMemory = true;
        CompilerResults cr;
        if (language == "csharp")
            cr = new CSharpCodeProvider().CreateCompiler().CompileAssemblyFromDom(compilerParams, code);
        else
            cr = new CSharpCodeProvider().CreateCompiler().CompileAssemblyFromSource(compilerParams, source);
        CompilerErrorCollection errors = cr.Errors;
        if (errors.HasErrors) {
            string report = "";
            foreach (CompilerError err in errors) {
                if (!err.IsWarning)
                    report += (err.ErrorText + "\n");
            }
            return new string[]{report};
        } else {
            Assembly a = cr.CompiledAssembly;
            Type newType = a.GetType(dllNamespace + "." + newClassName, true);
            string newTypeName = newType.FullName;
            string methName = newType.GetMethods(BindingFlags.Static | BindingFlags.Public)[0].Name;
            string argCount = newType.GetMethods(BindingFlags.Static | BindingFlags.Public)[0].GetParameters().Length.ToString();
            return new string[]{newTypeName, methName, argCount};
        }
    }

}

}
