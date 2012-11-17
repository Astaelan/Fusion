using System;
using System.Collections.Generic;
using System.Text;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false)]
    public sealed class MethodImplAttribute : Attribute
    {
        private MethodImplOptions options;

        public MethodImplAttribute() { }
        public MethodImplAttribute(MethodImplOptions options) { this.options = options; }
        public MethodImplOptions Value { get { return options; } }
    }
}
