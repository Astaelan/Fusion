﻿namespace System.Runtime.Versioning
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class TargetFrameworkAttribute : Attribute
    {
        public string FrameworkName { get; private set; }

        public string FrameworkDisplayName { get; set; }

        public TargetFrameworkAttribute(string frameworkName)
        {
            if (frameworkName == null)
                throw new ArgumentNullException("frameworkName");

            FrameworkName = frameworkName;
        }
    }
}
