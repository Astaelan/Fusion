using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreArrayElementInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRStoreArrayElementInstruction(IRType pType)
            : base(IROpcode.StoreArrayElement)
        {
            Type = pType;
        }
    }
}
