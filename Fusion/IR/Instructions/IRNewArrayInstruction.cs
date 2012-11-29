using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRNewArrayInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRNewArrayInstruction(IRType pType) : base(IROpcode.NewArray)
        {
            Type = pType;
        }
    }
}
