using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRCopyObjectInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRCopyObjectInstruction(IRType pType)
            : base(IROpcode.CopyObject)
        {
            Type = pType;
        }
    }
}
