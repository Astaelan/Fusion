using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRInitializeObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRInitializeObjectInstruction(IRType pType) : base(IROpcode.InitializeObject)
        {
            Type = pType;
        }
    }
}
