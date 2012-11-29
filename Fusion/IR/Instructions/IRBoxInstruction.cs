using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public sealed class IRBoxInstruction : IRInstruction
    {
        private IRType mType = null;
        public IRType Type
        {
            get { return mType; }
            private set { mType = value; }
        }

        public IRBoxInstruction(IRType pType) : base(IROpcode.Box)
        {
            Type = pType;
        }
    }
}
