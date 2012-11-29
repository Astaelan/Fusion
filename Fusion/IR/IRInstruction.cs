using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public abstract class IRInstruction
    {
        public uint ILOffset = 0;
        public uint IRIndex = 0;
        public IROpcode Opcode = IROpcode.Nop;

        // Instruction Linearization
        public List<IRTargetTypeAndData> Sources = new List<IRTargetTypeAndData>();
        public IRTargetTypeAndData Destination = null;

        protected IRInstruction(IROpcode pOpcode)
        {
            Opcode = pOpcode;
        }
    }
}
