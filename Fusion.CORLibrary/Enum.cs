﻿using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public abstract class Enum : ValueType
    {
        private static unsafe void Internal_GetInfo(Type enumType, out string[] names, out int[] values)
        {
            Type.TypeData* typeData = enumType.GetTypeDataPointer();
            names = new string[typeData->EnumerationCount];
            values = new int[typeData->EnumerationCount];
            for (int i = 0; i < typeData->EnumerationCount; ++i)
            {
                names[i] = new string(typeData->Enumerations[i].Name, 0, typeData->Enumerations[i].NameLength);
                values[i] = typeData->Enumerations[i].Value;
            }
        }

        public static string[] GetNames(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException();
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException();
            }
            EnumInfo info = EnumInfo.GetInfo(enumType);
            return info.GetNames();
        }

        private struct EnumInfo
        {
            private string[] names;
            private int[] values;

            public static EnumInfo GetInfo(Type enumType)
            {
                EnumInfo info = new EnumInfo();
                Enum.Internal_GetInfo(enumType, out info.names, out info.values);
                return info;
            }

            public string GetName(int value)
            {
                int valuesLen = values.Length;
                for (int i = 0; i < valuesLen; i++)
                {
                    if (this.values[i] == value)
                    {
                        return this.names[i];
                    }
                }
                // Pretend it's got the [Flags] attribute, so look for bits set.
                // TODO Sort out Flags attribute properly
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < valuesLen; i++)
                {
                    int thisValue = this.values[i];
                    if ((value & thisValue) == thisValue)
                    {
                        sb.Append(this.names[i]);
                        sb.Append(", ");
                    }
                }
                if (sb.Length > 0)
                {
                    return sb.ToString(0, sb.Length - 2);
                }
                return null;
            }

            public string[] GetNames()
            {
                List<string> names = new List<string>();
                for (int i = 0; i < this.values.Length; i++)
                {
                    names.Add(this.GetName(this.values[i]));
                }
                return names.ToArray();
            }

        }

        protected Enum() { }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int Internal_GetValue();

        public static string GetName(Type enumType, object value)
        {
            if (enumType == null || value == null)
            {
                throw new ArgumentNullException();
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType is not an Enum type.");
            }
            EnumInfo info = EnumInfo.GetInfo(enumType);
            return info.GetName((int)value);
        }

        public static string Format(Type enumType, object value, string format)
        {
            if (enumType == null || value == null || format == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.");
            }
            string ret = GetName(enumType, value);
            if (ret == null)
            {
                return value.ToString();
            }
            else
            {
                return ret;
            }
        }

        public override string ToString()
        {
            return Format(this.GetType(), this.Internal_GetValue(), "G");
        }

    }
}
