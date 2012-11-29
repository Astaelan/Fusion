using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreParameterInstruction : IRInstruction
    {
        public uint ParameterIndex = 0;

        public IRStoreParameterInstruction(uint pParameterIndex)
            : base(IROpcode.StoreParameter)
        {
            ParameterIndex = pParameterIndex;
        }
    }
}
