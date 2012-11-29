using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadParameterAddressInstruction : IRInstruction
    {
        public uint ParameterIndex { get; private set; }

        public IRLoadParameterAddressInstruction(uint pParameterIndex) : base(IROpcode.LoadParameterAddress)
        {
            ParameterIndex = pParameterIndex;
        }
    }
}
