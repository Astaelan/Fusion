using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRNopInstruction : IRInstruction
    {
        public bool ForceEmit = false;

        public IRNopInstruction(bool pForceEmit)
            : base(IROpcode.Nop) 
        {
            ForceEmit = pForceEmit;
        }
    }
}
