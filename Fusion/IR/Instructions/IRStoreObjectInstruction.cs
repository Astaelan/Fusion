using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreObjectInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRStoreObjectInstruction(IRType pType)
            : base(IROpcode.StoreObject)
        {
            Type = pType;
        }
    }
}
