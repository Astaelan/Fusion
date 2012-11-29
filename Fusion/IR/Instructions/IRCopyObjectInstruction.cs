using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRCopyObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRCopyObjectInstruction(IRType pType) : base(IROpcode.CopyObject)
        {
            Type = pType;
        }
    }
}
