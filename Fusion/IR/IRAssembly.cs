using Fusion.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR
{
    public sealed class IRAssembly
    {
        public IRAppDomain AppDomain = null;
        public CLIFile File = null;

        public IRAssembly(IRAppDomain pAppDomain, CLIFile pFile)
        {
            AppDomain = pAppDomain;
            File = pFile;
        }

        public void Load()
        {
        }

        public void Link()
        {
        }
    }
}
