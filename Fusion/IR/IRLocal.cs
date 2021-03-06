﻿using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRLocal
    {
        public IRAssembly Assembly = null;
        public IRMethod ParentMethod = null;

		private IRLocal mParentLocal = null;
		private IRType mType;
		public IRType Type
		{
			get
			{
				if (mType == null && mParentLocal != null)
				{
					mType = mParentLocal.Type;
					if (mType == null) throw new Exception();
					mParentLocal = null;
				}
				return mType;
			}
			set
			{
				if (value == null) throw new Exception();
				mType = value;
			}
		}

        public uint Index = 0;

        public bool Resolved { get { return Type.Resolved; } }

		public void Substitute() { Resolve(); }

        public void Resolve()
        {
			Type.Resolve(ref mType, ParentMethod.ParentType.GenericParameters, ParentMethod.GenericParameters);
        }

        public IRLocal(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        public IRLocal Clone(IRMethod newMethod)
        {
            IRLocal local = new IRLocal(this.Assembly);
			local.ParentMethod = newMethod;
			local.mParentLocal = this.Type == null ? this : null;
			if (this.Type != null)
				local.mType = this.Type;
            local.Index = (uint)newMethod.Locals.Count;
            return local;
        }

		public override string ToString()
		{
			return Type.ToString() + ": " + Index.ToString();
		}
    }
}
