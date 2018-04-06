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
using System.Collections;
using System.Reflection;
using System.Diagnostics;

namespace Wolfram.NETLink {

/* TODO

    Like J/Link, there really needs to be one of these per IKernelLink, not a single shared one.
    
*/

/// <summary>
/// TypeLoader is the class responsible for loading all assemblies and types from the Mathematica functions
/// LoadNETAssembly and LoadNETType.
/// </summary>
/// <remarks>
/// Use the methods in this class to load types and assemblies if you want to have the same
/// type-finding ability in your .NET code as Mathematica has in its LoadNETType and LoadNETAssembly
/// functions.
/// <para>
/// The methods in this class can be used in place of Assembly.Load() (and related methods)
/// and Type.GetType(). It can load assemblies via path, URL, or name.
/// You don't have to call this GetType method in order to get the full power of .NET/Link's
/// type-finding mechanism. This is just a convenience function. We hook the ResolveEvent's for
/// assemblies and types and put the search logic in there, so it is always called no matter how
/// you try to load a type. If you just call Type.GetType("name"), though, you cannot specify an
/// as-yet-unloaded assembly--you just get a search through all loaded assemblies.
/// </para>
/// </remarks>
///
public class TypeLoader {

    private static Hashtable assemblyCache = new Hashtable();
    private static System.IO.DirectoryInfo gacDir;
    private static char[] pathChars = {'/', '\\'};
    private static string dirToSearch = null;


    static TypeLoader() {
        // Set up the ResolveEvent handlers that apply our search logic.
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(TypeLoader.assemblyResolveEventHandler);
        AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(TypeLoader.typeResolveEventHandler);
        // Initialize the gacDir variable for later use.
        string gac = "";
        if (!Utils.IsMono) {
            gac = new System.IO.DirectoryInfo(Environment.SystemDirectory).Parent.FullName +
                        System.IO.Path.DirectorySeparatorChar + "assembly" + 
                        System.IO.Path.DirectorySeparatorChar + "GAC";
        } else {
            gac = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() +
                    System.IO.Path.DirectorySeparatorChar + "mono" + 
                    System.IO.Path.DirectorySeparatorChar + "gac";
        }
        gacDir = new System.IO.DirectoryInfo(gac);
    }


    /// <overloads>
    /// <summary>
    /// Searches for a specified type.
    /// </summary>
    /// </overloads>
    /// 
    /// <summary>
    /// Searches for the type among all loaded assemblies.
    /// </summary>
    /// <remarks>
    /// If throwOnError is true, it will always throw if the type cannot be found. Like Type.GetType(),
    /// it can still throw in unusual circumstances even if throwOnError is false.
    /// </remarks>
    /// <param name="typeName"></param>
    /// <param name="throwOnError"></param>
    ///     
    public static Type GetType(string typeName, bool throwOnError) {
        return GetType(typeName, "", throwOnError);
    }


    /// <summary>
    /// Searches for the type in the named assembly.
    /// </summary>
    /// <remarks>
    /// The assembly will be loaded if necessary (the assemblyName argument must include enough
    /// information for a load to succeed based on the name alone). See the <see cref="LoadAssembly"/> method
    /// in this class for details on what forms the assemblyName argument can take.
    /// If throwOnError is true, it will always throw if the type cannot be found or other errors occur
    /// (e.g., SecurityException, BadImageFormatException, etc.) Like Type.GetType(), it can still throw in unusual
    /// circumstances even if throwOnError is false.
    /// </remarks>
    /// <param name="typeName"></param>
    /// <param name="assemblyName"></param>
    /// <param name="throwOnError"></param>
    ///     
    public static Type GetType(string typeName, string assemblyName, bool throwOnError) {

        if (assemblyName == "") {
            Type t = null;
            // Cannot just look for a comma to determine if this is an eassembly-qualified name, as commas
            // are part of the names of generic types. Look instead for comma after last closing ] if one exists.
            int closingBracketPos = typeName.LastIndexOf(']');
            if (closingBracketPos < 0)
                closingBracketPos = 0;
            int commaPos = typeName.IndexOf(",", closingBracketPos);
            if (commaPos != -1) {
                // This is an AQ type name.
                // For type names that are not assembly-qualified, just ask the system to find the type
                // using whatever lookup mechanism it uses. But for AQ type names this is not ideal. You
                // could manually load a strong-named assembly from the file system and then call a method
                // in that assembly that returns a type from the assembly, which would come through here as
                // an AQ name. But if we called just Type.GetType(), the system would find the type from a
                // a copy of that same assembly in the GAC if one existed there. This won't work because
                // although the assemblies are identical they have different codebases and are thus different,
                // and types from these assemblies are therefore different as well. Therefore we iterate
                // through the previously-loaded assemblies and see if the type exists in that assembly
                // before falling back to Type.GetType().

                // Need to truncate the assembly part from the type name before calling Assembly.GetType().
                string typeNameWithoutAssembly = typeName.Substring(0, commaPos);
                // We canonicalize the name to allow matching of names that arrive from Mathematica
                // with various spaces and mixed-case in, say, the hex PublicKeyToken spec.
                string asmName = typeName.Substring(commaPos + 1).Replace(" ", "").ToLower();
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
                    if (asm.FullName.Replace(" ", "").ToLower() == asmName) {
                        // Don't think GetType() will ever throw when 2nd arg is false, but be safe
                        // and wrap each call in a try block.
                        try {
                            t = asm.GetType(typeNameWithoutAssembly, false);
                            if (t != null)
                                break;
                        } catch (Exception) {}
                    }
                }              
            }
            if (t == null)
                t = Type.GetType(typeName, false);
            if (t == null && throwOnError)
                throw new TypeLoadException("Type " + typeName + " not found.");
            return t;
        } else {
            Assembly a = LoadAssembly(assemblyName);
            if (a == null) {
                if (throwOnError)
                    throw new TypeLoadException("Assembly " + assemblyName + " not found.");
                else
                    return null;
            } else {
                return a.GetType(typeName, throwOnError);
            }
        }
    }


    /// <summary>
    /// Searches for the type in the named assembly.
    /// </summary>
    /// <remarks>
    /// If throwOnError is true, it will always throw if the type cannot be found or other errors occur
    /// (e.g., SecurityException, BadImageFormatException, etc.) Like Type.GetType(), it can still throw in unusual
    /// circumstances even if throwOnError is false.
    /// </remarks>
    /// <param name="typeName"></param>
    /// <param name="assembly"></param>
    /// <param name="throwOnError"></param>
    ///     
    public static Type GetType(string typeName, Assembly assembly, bool throwOnError) {
        return assembly.GetType(typeName, throwOnError);
    }


    /// <overloads>
    /// <summary>
    /// Loads the named assembly. If the assembly has already been loaded, it will simply return the loaded assembly.
    /// </summary>
    /// </overloads>
    /// 
    /// <remarks>
    /// The assemblyName argument can be one of many types:
    /// <code>
    ///      Simple.Assembly
    ///      Simple.Assembly, Version=...., etc.   (a full display name)
    ///      SimpleAssembly.dll
    ///      c:\path\to\SimpleAssembly.dll
    ///      http://url/to/SimpleAssembly.dll
    /// </code>
    /// Can return null or throw on failure.
    /// </remarks>
    /// <param name="assemblyName"></param>
    ///     
    public static Assembly LoadAssembly(string assemblyName) {

        Assembly a = (Assembly) assemblyCache[assemblyName];
        if (a != null)
            return a;

        string lowerCaseAssemblyName = assemblyName.ToLower();
        if (assemblyName.IndexOfAny(pathChars) != -1) {
            // assemblyName is a path or URL to an assembly file. LoadFrom() will throw if it fails.
            a = Assembly.LoadFrom(assemblyName);
        } else if (lowerCaseAssemblyName.EndsWith(".dll") || lowerCaseAssemblyName.EndsWith(".exe")) {
            // assmblyName is a DLL or EXE name without path info. We will search among already-loaded asms for
            // one with that filename, and then search GAC and load if that filename is found.
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
                try {
                    // Don't know if Path.GetFileName would ever throw, but use a try block to be safe and
                    // make sure it doesn't break out of the whole loop.
                    string asmFileName = System.IO.Path.GetFileName(asm.Location);
                    if (String.Compare(asmFileName, assemblyName, true /* ignoreCase */) == 0) {
                        a = asm;
                        break;
                    }
                } catch (Exception) {}
            }
            if (a == null) {
                // If we haven't found it among already-loaded assemblies, search and load from GAC.
                System.IO.FileInfo asmFile = FindAssemblyFile(gacDir, assemblyName);
                if (asmFile != null)
                    a = Assembly.LoadFrom(asmFile.FullName);
            }
        } else {
            // assemblyName is an assembly name like System.Windows.Forms and not a path to an assembly file.
            // LoadWithPartialName() will internally search the GAC and main .NET/Link dir only, not loaded assemblies.
            // It will fire AssemblyResolveEvents, however, which we handle below, so that we can find the
            // assembly with the given name if it has been previously loaded by path.
            a = Assembly.LoadWithPartialName(assemblyName);
        }
        if (a != null) {
            assemblyCache.Add(assemblyName, a);
            String fullName = a.FullName;
            if (!assemblyCache.ContainsKey(fullName))
                assemblyCache.Add(fullName, a);
        }
        return a;
    }


    /// <summary>
    /// Loads the named assembly from the specified directory, if possible.
    /// </summary>
    /// <remarks>
    /// This overload adds the ability to specify a directory in which to
    /// search. It is not necessarily the case that if the assembly is found it will be loaded from the named dir.
    /// If you supply a full assembly name and the assembly is strong-named, you will either get the assembly loaded from
    /// the named dir, or an identical one loaded from somewhere else earlier (since its full name matches exactly,
    /// it doesn't matter if it's not from the specified dir). If the assembly is not strong-named, you will get an
    /// assembly from a different dir if one with the same short name was loaded from somewhere else previously.
    /// It is a design decision not to fail in this case (we couldn't make it succeed--a weak-named assembly can only
    /// be loaded once.
    /// </remarks>
    /// <param name="assemblyName"></param>
    /// <param name="dir"></param>
    ///     
    public static Assembly LoadAssembly(string assemblyName, string dir) {

        // This DirectoryInfo ctor will throw if path not found. That should be prevented by checks in M code.
        dirToSearch = new System.IO.DirectoryInfo(dir).FullName;
        Assembly asm = null;
        // We use a 2-pass strategy. First, look using a dirToSearch setting. This will find the requested assembly
        // in either of two cases:
        //   (a) The asm is strong-named and present in the specified dir. This is the scenario for which this function
        //       was primarily intended.
        //   (b) the asm is not strong-named, but happens to have been loaded from the directory requested. Remember that
        //       non-strong-named assemblies are only loaded once, so it is possible that another call might have loaded
        //       this one from a different location. If it was previously loaded from a different dir, then we will
        //       fail to find it in this first pass.
        // The 2nd pass tries to find an assembly with that short name loaded from anywhere. It is a design decision to
        // not fail if an assembly with the short name was loaded previously from some other location. In other words,
        // if you try to use the LoadNETAssembly["Assembly.Name", "dir"] syntax, you are not guaranteed to get that
        // dir as the location if the assembly is not strong-named. Note that it is not possible to load the assembly from
        // the requested dir if it has already been loaded from somewhere else. Non-strong-named assemblies are only
        // loaded from one location--after that, all calls to load from anywhere simply return the first-loaded instance.
        try {
            asm = LoadAssembly(assemblyName);
        } catch (Exception) {
        } finally {
            dirToSearch = null;
        }
        if (asm == null)
            asm = LoadAssembly(assemblyName);
        return asm;
    }


    /******************************************  Private  ********************************************/

    /// <summary>
    /// This event handler function is called by the system when loading an assembly by name, not by path
    /// (i.e., not via Assembly.LoadFrom()).
    /// </summary>
    /// <remarks>
    /// What it does is look for an assembly being loaded by name among
    /// the set of assemblies loaded by path. We must do this manually.
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    ///     
    private static Assembly assemblyResolveEventHandler(object sender, ResolveEventArgs args) {

        Debug.WriteLine("AssemblyResolveEvent: " + args.Name);
        string assemblyName = args.Name;

        // assemblyName should be an assembly name like System.Windows.Forms and not a path to an assembly file.
        // The next test can be removed once I am confident about the circumstances in which this event handler is called.
        if (assemblyName.IndexOfAny(pathChars) != -1) {
            Debug.Fail("In assemblyResolveEventHandler, assemblyName looks like a pathname: " + assemblyName);
            return null;
        }

        // First see if we have already loaded an assembly of that name.
        // First look for an exact match against the full name for the assembly.
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly a in loadedAssemblies) {
            if (a.FullName == assemblyName) {
                // Assembly name was passed in as a full name and matched this asm exactly.
                return a;
            } else if (a.GetName().Name == assemblyName) {
                // Assembly name was passed in as a short name and matched this asm short name.
                // Note that because we only enter this branch if there was no version/culture/key info in
                // the name, users must either specify a full assembly name or only the simple name--no partial
                // extra info, such as "Some.Assembly,Version=1.2.3.4". It's all or nothing. The only exception
                // to this is if the assembly is in the GAC, because then the earlier call to LoadWithPartialName()
                // would have found it. We wouldn't even get into assemblyResolveEventHandler().
                // This shortcoming could be fixed by parsing the name into its comma-separated pieces and comparing
                // them individually. That is nontrivial since the AssemblyName class does not make all the elements
                // available individually in an obvious way (e.g., publickey, strongname).
                if (dirToSearch == null) {
                    return a;
                } else {
                    // Ignore any exceptions thrown during creation of DirectoryInfo
                    // (e.g., a.Location was "" because it was a dynamic assembly).
                    try {
                        string asmLoc = new System.IO.DirectoryInfo(a.Location).Parent.FullName;
                        if (String.Compare(asmLoc, dirToSearch, true /* ignoreCase */) == 0)
                            return a;
                    } catch (Exception) { }
                }
            }
        }
        return null;
    }


    /// <summary>
    /// This event handler function is called by the system when loading a type.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    ///     
    private static Assembly typeResolveEventHandler(object sender, ResolveEventArgs args) {

        Debug.WriteLine("TypeResolveEvent: " + args.Name);

        string typeName = args.Name;
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly a in loadedAssemblies) {
            // Avoid recursion when building a dynamic assembly. See comments on isBuildingDynamicAssembly prop.
            if (isBuildingDynamicAssembly && a is System.Reflection.Emit.AssemblyBuilder)
                continue;
            Type t = a.GetType(typeName, false);
            if (t != null)
                return a;
        }
        Debug.WriteLine("   returning null");
        return null;
    }


    // This property was added as a way to avoid an infinite recursion in typeResolveEventHandler when
    // using COMTypeLibraryLoader. The system would be trying to resolve a type in the dynamically-created
    // interop assembly and would call a.GetType() on the dynamic assembly. That would trigger a recursive call
    // to typeResolveEventHandler, and so on. There might be a better way to avoid the problem.
    private static bool isBuildingDynAssembly = false;
    internal static bool isBuildingDynamicAssembly {
        get { return isBuildingDynAssembly; }
        set { isBuildingDynAssembly = value; }
    }
 
 
    /// <summary>
    /// Searches recursively through subdirs looking for the given file name. Used to search the GAC.
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="fileToFind"></param>
    /// <returns></returns>
    private static System.IO.FileInfo FindAssemblyFile(System.IO.DirectoryInfo dir, string fileToFind) {

        System.IO.FileInfo[] files = dir.GetFiles(fileToFind);
        if (files.Length > 0) {
            return files[0];
        } else {
            System.IO.DirectoryInfo[] subDirs = dir.GetDirectories();
            // Search backwards in hopes of getting the most recent version (because dirs are typically named with
            // version numbers).
            for (int i = subDirs.Length; i > 0; i--) {
                System.IO.FileInfo found = FindAssemblyFile(subDirs[i-1], fileToFind);
                if (found != null)
                    return found;
            }
            return null;
        }
    }

}

}
