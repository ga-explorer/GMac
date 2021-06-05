using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib;

namespace GMac.IDE.Scripting
{
    internal static class NamespacesUtils
    {
        public static readonly Dictionary<string, Type> ExportedTypes 
            = new Dictionary<string, Type>();

        public static readonly string[] Namespaces;


        static NamespacesUtils()
        {
            var assemblies = 
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Where(asm => asm.IsDynamic == false)
                    .SelectMany(a => a.GetExportedTypes());

            foreach (var exportedType in assemblies)
            {
                var key = exportedType.FullName ?? throw new InvalidOperationException();

                if (!ExportedTypes.ContainsKey(key))
                    ExportedTypes.Add(exportedType.FullName, exportedType);
            }

            Namespaces = 
                ExportedTypes
                .Select(t => t.Value.Namespace)
                .Distinct()
                .OrderBy(n => n)
                .ToArray();
        }

        public static IEnumerable<KeyValuePair<string, Type>> PublicTypes(string nameSpace)
        {
            var parentName = nameSpace + ".";

            var parentNameLength = parentName.Length - 1;

            return 
                ExportedTypes
                .Where(p =>
                    parentNameLength == p.Key.LastIndexOf('.') && 
                    p.Key.StartsWith(parentName)
                    );
        }

        public static IEnumerable<KeyValuePair<string, Type>> PublicClasses(string nameSpace)
        {
            var parentName = nameSpace + ".";

            var parentNameLength = parentName.Length - 1;

            return
                ExportedTypes
                .Where(p =>
                    p.Value.IsClass &&
                    parentNameLength == p.Key.LastIndexOf('.') &&
                    p.Key.StartsWith(parentName)
                    );
        }

        public static IEnumerable<string> MemberSignatures(this Type inputType)
        {
            if (inputType.IsClass == false) return Enumerable.Empty<string>();

            var signatures = new List<string>();

            signatures.AddRange(
                inputType
                    .GetProperties()
                    .Select(m => m.GetSignature())
            );

            signatures.AddRange(
                inputType
                    .GetMethods()
                    .Select(m => m.GetSignature())
            );

            return signatures;
        }
    }
}
