using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRBoxInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRBoxInstruction(IRType pType)
            : base(IROpcode.Box)
        {
            Type = pType;
        }
    }
}
