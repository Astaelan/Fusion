using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadParameterInstruction : IRInstruction
    {
        public uint ParameterIndex = 0;

        public IRLoadParameterInstruction(uint pParameterIndex)
            : base(IROpcode.LoadParameter)
        {
            ParameterIndex = pParameterIndex;
        }
    }
}
