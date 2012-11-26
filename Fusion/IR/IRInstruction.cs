using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRInstruction
    {
        public readonly uint ILOffset;
        public uint IRIndex;
        public IROpcode Opcode;
        public List<object> Arguments = new List<object>();
        // Instruction Linearization
        public List<IRTargetTypeAndData> Sources = new List<IRTargetTypeAndData>();
        public IRTargetTypeAndData Destination = null;

        public IRInstruction(uint pILOffset, uint pIRIndex, IROpcode pOpcode)
        {
            ILOffset = pILOffset;
            IRIndex = pIRIndex;
            Opcode = pOpcode;
        }
    }
}
