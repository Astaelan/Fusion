using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRStoreArrayElementInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRStoreArrayElementInstruction(IRType pType) : base(IROpcode.StoreArrayElement)
        {
            Type = pType;
        }
    }
}
