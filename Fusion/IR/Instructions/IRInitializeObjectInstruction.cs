using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRInitializeObjectInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRInitializeObjectInstruction(IRType pType)
            : base(IROpcode.InitializeObject)
        {
            Type = pType;
        }
    }
}
