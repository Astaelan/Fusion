using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadParameterAddressInstruction : IRInstruction
    {
        public uint ParameterIndex = 0;

        public IRLoadParameterAddressInstruction(uint pParameterIndex)
            : base(IROpcode.LoadParameterAddress)
        {
            ParameterIndex = pParameterIndex;
        }
    }
}
