using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRLocal
    {
        public IRAssembly Assembly = null;
        public IRMethod ParentMethod = null;
        public IRType Type = null;

        public uint Index = 0;

        public bool Resolved { get { return Type.Resolved; } }

        public void Resolve(GenericParameterCollection typeParams, GenericParameterCollection methodParams)
        {
            Type.Resolve(ref Type, typeParams, methodParams);
        }

        public IRLocal(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        public IRLocal Clone(IRMethod newMethod)
        {
            IRLocal local = new IRLocal(this.Assembly);
            local.ParentMethod = newMethod;
            local.Type = this.Type;
            local.Index = (uint)newMethod.Locals.Count;
            return local;
        }
    }
}
