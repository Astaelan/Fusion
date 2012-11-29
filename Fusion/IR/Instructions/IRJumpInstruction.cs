using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRJumpInstruction : IRInstruction
    {
        public IRMethod Target = null;

        public IRJumpInstruction(IRMethod pTarget)
            : base(IROpcode.Jump)
        {
            Target = pTarget;
        }
    }
}
