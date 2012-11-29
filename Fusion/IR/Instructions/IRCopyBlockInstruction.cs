using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRCopyBlockInstruction : IRInstruction
    {
        public IRCopyBlockInstruction()
            : base(IROpcode.CopyBlock)
        {
        }
    }
}
