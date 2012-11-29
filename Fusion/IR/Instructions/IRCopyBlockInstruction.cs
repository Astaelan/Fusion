using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRCopyBlockInstruction : IRInstruction
    {
        public IRCopyBlockInstruction() : base(IROpcode.CopyBlock)
        {
        }
    }
}
