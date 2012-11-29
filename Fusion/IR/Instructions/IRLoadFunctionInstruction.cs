using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadFunctionInstruction : IRInstruction
    {
        public IRMethod Method { get; private set; }
        public bool Virtual { get; protected set; }

        public IRLoadFunctionInstruction(IRMethod pMethod, bool pVirtual) : base(IROpcode.LoadFunction)
        {
            Method = pMethod;
            Virtual = pVirtual;
        }
    }
}
