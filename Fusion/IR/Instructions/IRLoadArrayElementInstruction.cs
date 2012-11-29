using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadArrayElementInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadArrayElementInstruction(IRType pType) : base(IROpcode.LoadArrayElement)
        {
            Type = pType;
        }
    }
}
