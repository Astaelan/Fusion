using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRCallInstruction : IRInstruction
    {
        public IRMethod Target = null;
        public bool Virtual = false;

        public IRCallInstruction(IRMethod pTarget, bool pVirtual)
            : base(IROpcode.Call)
        {
            Target = pTarget;
            Virtual = pVirtual;
        }
    }
}
