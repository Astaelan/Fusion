using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadArrayElementAddressInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadArrayElementAddressInstruction(IRType pType) : base(IROpcode.LoadArrayElementAddress)
        {
            Type = pType;
        }
    }
}
