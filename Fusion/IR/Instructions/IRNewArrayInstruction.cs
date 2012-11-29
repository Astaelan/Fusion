using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRNewArrayInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRNewArrayInstruction(IRType pType)
            : base(IROpcode.NewArray)
        {
            Type = pType;
        }
    }
}
