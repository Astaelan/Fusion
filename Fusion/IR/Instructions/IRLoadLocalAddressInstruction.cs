using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadLocalAddressInstruction : IRInstruction
    {
        public uint LocalIndex = 0;

        public IRLoadLocalAddressInstruction(uint pLocalIndex)
            : base(IROpcode.LoadLocalAddress)
        {
            LocalIndex = pLocalIndex;
        }
    }
}
