using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRInitializeBlockInstruction : IRInstruction
    {
        public IRInitializeBlockInstruction() : base(IROpcode.InitializeBlock)
        {
        }
    }
}
