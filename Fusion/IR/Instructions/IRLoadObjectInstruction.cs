using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadObjectInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRLoadObjectInstruction(IRType pType)
            : base(IROpcode.LoadObject)
        {
            Type = pType;
        }
    }
}
