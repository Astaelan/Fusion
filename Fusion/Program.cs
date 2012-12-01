using Fusion.CLI;
using Fusion.IR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fusion
{
    internal static class Program
    {
        private static void Main(string[] pArguments)
        {
            Console.SetWindowSize(128, 40);
            Console.SetBufferSize(128, 1000);

            IRAppDomain appDomain = new IRAppDomain();
            IRAssembly entryAssembly = appDomain.LoadEntryAssembly(new CLIFile(Path.GetFileNameWithoutExtension(pArguments[0]), File.ReadAllBytes(pArguments[0])));
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
