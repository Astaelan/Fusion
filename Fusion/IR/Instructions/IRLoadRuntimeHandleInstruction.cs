using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadRuntimeHandleInstruction : IRInstruction
    {
        public IRType Type = null;
        public IRMethod Method = null;
        public IRField Field = null;

        public IRLoadRuntimeHandleInstruction(IRType pType, IRMethod pMethod, IRField pField)
            : base(IROpcode.LoadRuntimeHandle)
        {
            Type = pType;
            Method = pMethod;
            Field = pField;
        }
    }
}
