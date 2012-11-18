using Fusion.CLI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fusion.IR
{
    public sealed class IRAppDomain
    {
        public List<IRAssembly> Assemblies = new List<IRAssembly>();

        public IRAppDomain(string pAssemblyPath)
        {
            Load(pAssemblyPath);
        }

        public IRAssembly Load(string pAssemblyPath)
        {
            pAssemblyPath = Path.GetFullPath(pAssemblyPath).ToLower();
            string referenceName = Path.GetFileNameWithoutExtension(pAssemblyPath);
            if (!File.Exists(pAssemblyPath)) return null;
            IRAssembly assembly = Assemblies.Find(a => a.File.ReferenceName == referenceName);
            if (assembly != null) return assembly;

            CLIFile file = new CLIFile(referenceName, File.ReadAllBytes(pAssemblyPath));
            file.Load();

            assembly = new IRAssembly(this, file);
            Assemblies.Add(assembly);

            assembly.Load();

            Array.ForEach(file.AssemblyRefTable, a => Load(a.Name + ".dll"));

            assembly.Link();
            return assembly;
        }

    }
}
