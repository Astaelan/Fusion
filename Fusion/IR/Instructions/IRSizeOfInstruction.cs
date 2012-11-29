using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRSizeOfInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRSizeOfInstruction(IRType pType)
            : base(IROpcode.SizeOf)
        {
            Type = pType;
        }
    }
}
