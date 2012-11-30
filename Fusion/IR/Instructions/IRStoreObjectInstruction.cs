using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRStoreObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRStoreObjectInstruction(IRType pType) : base(IROpcode.StoreObject)
        {
            Type = pType;
        }
    }
}
