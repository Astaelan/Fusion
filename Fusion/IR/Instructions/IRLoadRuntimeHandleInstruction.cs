using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadRuntimeHandleInstruction : IRInstruction
    {
        public IRType Type { get; private set; }
        public IRMethod Method { get; private set; }
        public IRField Field { get; private set; }

        public IRLoadRuntimeHandleInstruction(IRType pType, IRMethod pMethod, IRField pField) : base(IROpcode.LoadRuntimeHandle)
        {
            Type = pType;
            Method = pMethod;
            Field = pField;
        }
    }
}
