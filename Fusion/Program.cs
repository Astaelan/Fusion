using Fusion.CLI;
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
            CLIFile file = new CLIFile(File.ReadAllBytes(pArguments[0]));
            file.Load();

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
