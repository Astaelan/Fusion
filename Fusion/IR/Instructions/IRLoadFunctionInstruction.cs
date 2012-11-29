using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadFunctionInstruction : IRInstruction
    {
        public IRMethod Method = null;
        public bool Virtual = false;

        public IRLoadFunctionInstruction(IRMethod pMethod, bool pVirtual)
            : base(IROpcode.LoadFunction)
        {
            Method = pMethod;
            Virtual = pVirtual;
        }
    }
}
