using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRSizeOfInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRSizeOfInstruction(IRType pType) : base(IROpcode.SizeOf)
        {
            Type = pType;
        }
    }
}
