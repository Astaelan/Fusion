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
            IRAppDomain appDomain = new IRAppDomain(pArguments[0]);

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
