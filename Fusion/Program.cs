using Fusion.PE;
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
            PEFile file = new PEFile();
            PEReader reader = new PEReader(File.ReadAllBytes(pArguments[0]));
            if (!file.Load(reader)) return;

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
