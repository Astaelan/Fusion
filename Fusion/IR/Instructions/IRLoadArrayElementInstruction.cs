using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadArrayElementInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRLoadArrayElementInstruction(IRType pType)
            : base(IROpcode.LoadArrayElement)
        {
            Type = pType;
        }
    }
}
