using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadParameterInstruction : IRInstruction
    {
        public uint ParameterIndex { get; private set; }

        public IRLoadParameterInstruction(uint pParameterIndex) : base(IROpcode.LoadParameter)
        {
            ParameterIndex = pParameterIndex;
        }
    }
}
