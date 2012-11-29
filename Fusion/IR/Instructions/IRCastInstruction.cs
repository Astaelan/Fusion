using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRCastInstruction : IRInstruction
    {
        public IRType Type = null;
        public bool ThrowExceptionOnFailure = false;

        public IRCastInstruction(IRType pType, bool pThrowExceptionOnFailure)
            : base(IROpcode.Cast)
        {
            Type = pType;
            ThrowExceptionOnFailure = pThrowExceptionOnFailure;
        }
    }
}
