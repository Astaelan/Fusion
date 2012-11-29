using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadLocalAddressInstruction : IRInstruction
    {
        public uint LocalIndex { get; private set; }

        public IRLoadLocalAddressInstruction(uint pLocalIndex) : base(IROpcode.LoadLocalAddress)
        {
            LocalIndex = pLocalIndex;
        }
    }
}
