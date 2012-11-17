using System.Runtime.CompilerServices;

namespace System
{
    public class Object
    {
        public Object() { }

        ~Object() { }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern Type GetType();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern virtual bool Equals(object obj);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern virtual int GetHashCode();

        public virtual string ToString() { return this.GetType().FullName; }

        public static bool Equals(object a, object b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            return a.Equals(b);
        }

        public static bool ReferenceEquals(object a, object b) { return (a == b); }

        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static object Clone(object obj);
    }
}
