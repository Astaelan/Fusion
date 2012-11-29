using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadArrayElementAddressInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRLoadArrayElementAddressInstruction(IRType pType)
            : base(IROpcode.LoadArrayElementAddress)
        {
            Type = pType;
        }
    }
}
