using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRBoxInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRBoxInstruction(IRType pType) : base(IROpcode.Box)
        {
            Type = pType;
        }
    }
}
