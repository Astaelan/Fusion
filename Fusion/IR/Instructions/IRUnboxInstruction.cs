using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRUnboxInstruction : IRInstruction
    {
        public IRType Type = null;
        public bool Value = false;

        public IRUnboxInstruction(IRType pType, bool pValue)
            : base(IROpcode.Unbox)
        {
            Type = pType;
            Value = pValue;
        }
    }
}
