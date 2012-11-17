using System;

namespace System
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AttributeUsageAttribute : Attribute
    {
        private AttributeTargets validOn;
        private bool allowMultiple = false;
        private bool inherited = true;

        public AttributeUsageAttribute(AttributeTargets validOn)
        {
            this.validOn = validOn;
        }

        public bool AllowMultiple
        {
            get
            {
                return allowMultiple;
            }
            set
            {
                allowMultiple = value;
            }
        }

        public bool Inherited
        {
            get
            {
                return inherited;
            }
            set
            {
                inherited = value;
            }
        }

        public AttributeTargets ValidOn
        {
            get
            {
                return validOn;
            }
        }
    }
}
