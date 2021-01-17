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


namespace Wolfram.NETLink.Internal.COM {

/// <summary>
/// This class takes a COM type library and programmatically builds a .NET interop assembly. 
/// </summary>
/// <remarks>
/// The name and version of the resulting assembly is created by reading the COM type information.
/// This code is modified from Troelsen's "COM and .NET Interoperability", Chap. 9, "MyTypeLibImporter".
/// </remarks>
/// 
internal class COMTypeLibraryLoader {
    
    [DllImport("oleaut32.dll", CharSet = CharSet.Unicode)] 
    private static extern void LoadTypeLibEx(string strTypeLibName, REGKIND regKind, out UCOMITypeLib TypeLib);

    /// <summary>
    /// This method creates (and optionally saves) a .NET assembly given a COM type library.
    /// </summary>
    /// <remarks>
    /// The name of the assembly is: "interop.[ComLibName].dll".
    /// The version of the assembly is: [ComLib.Major], [ComLib.Minor], 0, 0.
    /// </remarks>
    /// 
    internal static Assembly loadTypeLibrary(string pathToTypeLib, bool safeArrayAsArray, string assemFilePath, out bool foundPIAInstead) {
        LoadTypeLibEx(pathToTypeLib, REGKIND.REGKIND_NONE, out var typeLib);
        // Oddly enough, the above call will not throw an exception in at least one case: when the file specified
        // does not exist or is not a type library. So we must check null manually:
        if (typeLib == null)
            throw new ArgumentException("Could not find the specified file, or it did not have type library information in it.");
        return generateAssemblyFromTypeLib(typeLib, safeArrayAsArray, assemFilePath, out foundPIAInstead);
    }

    
    internal static Assembly generateAssemblyFromTypeLib(UCOMITypeLib typeLib, bool safeArrayAsArray, string asmFilePath, out bool foundPIAInstead) {

        foundPIAInstead = false;
        string typeLibName = Marshal.GetTypeLibName(typeLib);
        string asmDir, asmName, asmFullPath;
        if (asmFilePath == "") {
            // asmFilePath == "" means that user does not want the assembly saved in a file. A filename argument
            // is required by the conversion function, although no file is actually written by it. Therefore we 
            // supply one in case it is truly necessary.
            asmName = "interop." + typeLibName + ".dll";
            asmDir = "";
        } else {
            asmDir = System.IO.Path.GetDirectoryName(asmFilePath);
            if (asmDir == null) {
                // asmFilePath was a root dir, like c:\.
                string asmRoot = System.IO.Path.GetPathRoot(asmFilePath);
                asmDir = asmRoot == null ? "" : asmRoot;
            } else {
                asmDir = asmDir + System.IO.Path.DirectorySeparatorChar;
            }
            // M code ensures that if asmFilePath is a dir and not a filename it ends in \, so asmName will be ""
            // if it is a dir.
            asmName = System.IO.Path.GetFileName(asmFilePath);
            if (asmName == "")
                // asmFilePath was just a dir.
                asmName = "interop." + typeLibName + ".dll";
        }
        asmFullPath = asmDir + asmName;

        ImporterNotiferSink sink = new ImporterNotiferSink(safeArrayAsArray, asmDir);
        TypeLibConverter tlc = new TypeLibConverter();

        // Check for an existing PIA and use it if it exists.
        typeLib.GetLibAttr(out var pTLibAttr);
        TYPELIBATTR typeLibAttr = (TYPELIBATTR) Marshal.PtrToStructure(pTLibAttr, typeof(TYPELIBATTR));
        bool piaExists = tlc.GetPrimaryInteropAssembly(typeLibAttr.guid, typeLibAttr.wMajorVerNum, typeLibAttr.wMinorVerNum,
                                                        typeLibAttr.lcid, out var piaName, out var piaCodeBase);
        typeLib.ReleaseTLibAttr(pTLibAttr);
        if (piaExists) {
            Assembly pia = Assembly.LoadWithPartialName(piaName);
            if (pia != null) {
                foundPIAInstead = true;
                return pia;
            }
        }

        TypeLoader.isBuildingDynamicAssembly = true;
        try {
            AssemblyBuilder asmBuilder = tlc.ConvertTypeLibToAssembly(typeLib, asmFullPath,
                    safeArrayAsArray ? TypeLibImporterFlags.SafeArrayAsSystemArray : 0, sink, null, null, typeLibName, null);
            Type[] tt = asmBuilder.GetTypes();
            if (asmFilePath != "")
                asmBuilder.Save(asmName);
            return asmBuilder;
        } finally {
            TypeLoader.isBuildingDynamicAssembly = false;
        }
    }

    /// <summary>
    /// This enum is a .NET version of the COM REGKIND enum used in conjunction with the LoadTypeLibEx() API COM library function.
    /// </summary>
    /// 
    internal enum REGKIND {
        REGKIND_DEFAULT         = 0,
        REGKIND_REGISTER        = 1,
        REGKIND_NONE            = 2
    }

    /// <summary>
    /// When importing a COM type lib, this sink will be called in case of errors or unresolved type lib references.
    /// </summary>
    /// 
    internal class ImporterNotiferSink : ITypeLibImporterNotifySink {

        private string asmDir;
        private bool safeArrayAsArray;

        internal ImporterNotiferSink(bool safeArrayAsArray, string asmDir) {
            this.safeArrayAsArray = safeArrayAsArray;
            this.asmDir = asmDir;
        }

        public void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg) {
            System.Diagnostics.Debug.WriteLine("Event reported: {0}", eventMsg);
        }

        public Assembly ResolveRef(object typeLib) {
            // Return a new Assembly based on the incoming UCOMITypeLib interface (expressed as System.Object). Delegate to helper function.
            Assembly nestedRef = COMTypeLibraryLoader.generateAssemblyFromTypeLib((UCOMITypeLib)typeLib, safeArrayAsArray, asmDir, out var ignore);
            return nestedRef;
        }
    }

}

}
