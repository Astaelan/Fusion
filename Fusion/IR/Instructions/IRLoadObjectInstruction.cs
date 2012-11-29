using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadObjectInstruction(IRType pType) : base(IROpcode.LoadObject)
        {
            Type = pType;
        }
    }
}
