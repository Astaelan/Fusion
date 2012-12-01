using System;
using System.Runtime.InteropServices;

namespace Fusion.IR
{
    [StructLayout(LayoutKind.Explicit)]
    public class IRLinearizedTarget
    {
        public struct LocalVariableTargetData { public uint LocalVariableIndex; }
        public struct LocalVariableAddressTargetData { public uint LocalVariableIndex; }
        public struct ParameterTargetData { public uint ParameterIndex; }
        public struct ParameterAddressTargetData { public uint ParameterIndex; }
        public struct ConstantI4TargetData { public uint Value; }
        public struct ConstantI8TargetData { public ulong Value; }
        public struct ConstantR4TargetData { public float Value; }
        public struct ConstantR8TargetData { public double Value; }
        public struct FieldTargetData
        {
            public uint FieldIndex;
            public IRType OwnerType;
            public IRLinearizedTarget FieldTarget;
        }
        public struct FieldAddressTargetData
        {
            public uint FieldIndex;
            public IRType OwnerType;
            public IRLinearizedTarget FieldTarget;
        }
        public struct StaticFieldTargetData { public IRField Field; }
        public struct StaticFieldAddressTargetData { public IRField Field; }
        public struct IndirectTargetData
        {
            public IRType Type;
            public IRLinearizedTarget AddressTarget;
        }
        public struct SizeOfTargetData { public IRType Type; }
        public struct ArrayElementTargetData
        {
            public IRType ElementType;
            public IRLinearizedTarget ArrayTarget;
            public IRLinearizedTarget IndexTarget;
            public bool NoChecksRequired;
        }
        public struct ArrayElementAddressTargetData
        {
            public IRType ElementType;
            public IRLinearizedTarget ArrayTarget;
            public IRLinearizedTarget IndexTarget;
            public bool NoChecksRequired;
        }
        public struct ArrayLengthTargetData { public IRLinearizedTarget ArrayTarget; }

        [FieldOffset(0)]
        public IRLinearizedTargetType Type;
        [FieldOffset(1)]
        public LocalVariableTargetData LocalVariable;
        [FieldOffset(1)]
        public LocalVariableAddressTargetData LocalVariableAddress;
        [FieldOffset(1)]
        public ParameterTargetData Parameter;
        [FieldOffset(1)]
        public ParameterAddressTargetData ParameterAddress;
        [FieldOffset(1)]
        public ConstantI4TargetData ConstantI4;
        [FieldOffset(1)]
        public ConstantI8TargetData ConstantI8;
        [FieldOffset(1)]
        public ConstantR4TargetData ConstantR4;
        [FieldOffset(1)]
        public ConstantR8TargetData ConstantR8;
        [FieldOffset(1)]
        public FieldTargetData Field;
        [FieldOffset(1)]
        public FieldAddressTargetData FieldAddress;
        [FieldOffset(1)]
        public StaticFieldTargetData StaticField;
        [FieldOffset(1)]
        public StaticFieldAddressTargetData StaticFieldAddress;
        [FieldOffset(1)]
        public IndirectTargetData Indirect;
        [FieldOffset(1)]
        public SizeOfTargetData SizeOf;
        [FieldOffset(1)]
        public ArrayElementTargetData ArrayElement;
        [FieldOffset(1)]
        public ArrayElementAddressTargetData ArrayElementAddress;
        [FieldOffset(1)]
        public ArrayLengthTargetData ArrayLength;

        public IRLinearizedTarget(IRLinearizedTargetType pType) { Type = pType; }
        public IRLinearizedTarget(IRLinearizedTarget pLinearizedTarget)
        {
            Type = pLinearizedTarget.Type;
            switch (Type)
            {
                case IRLinearizedTargetType.Null: break;
                case IRLinearizedTargetType.Local: LocalVariable = pLinearizedTarget.LocalVariable; break;
                case IRLinearizedTargetType.LocalAddress: LocalVariableAddress = pLinearizedTarget.LocalVariableAddress; break;
                case IRLinearizedTargetType.Parameter: Parameter = pLinearizedTarget.Parameter; break;
                case IRLinearizedTargetType.ParameterAddress: ParameterAddress = pLinearizedTarget.ParameterAddress; break;
                case IRLinearizedTargetType.ConstantI4: ConstantI4 = pLinearizedTarget.ConstantI4; break;
                case IRLinearizedTargetType.ConstantI8: ConstantI8 = pLinearizedTarget.ConstantI8; break;
                case IRLinearizedTargetType.ConstantR4: ConstantR4 = pLinearizedTarget.ConstantR4; break;
                case IRLinearizedTargetType.ConstantR8: ConstantR8 = pLinearizedTarget.ConstantR8; break;
                case IRLinearizedTargetType.Field: Field = pLinearizedTarget.Field; break;
                case IRLinearizedTargetType.FieldAddress: FieldAddress = pLinearizedTarget.FieldAddress; break;
                case IRLinearizedTargetType.StaticField: StaticField = pLinearizedTarget.StaticField; break;
                case IRLinearizedTargetType.StaticFieldAddress: StaticFieldAddress = pLinearizedTarget.StaticFieldAddress; break;
                case IRLinearizedTargetType.Indirect: Indirect = pLinearizedTarget.Indirect; break;
                case IRLinearizedTargetType.SizeOf: SizeOf = pLinearizedTarget.SizeOf; break;
                case IRLinearizedTargetType.ArrayElement: ArrayElement = pLinearizedTarget.ArrayElement; break;
                case IRLinearizedTargetType.ArrayElementAddress: ArrayElementAddress = pLinearizedTarget.ArrayElementAddress; break;
                case IRLinearizedTargetType.ArrayLength: ArrayLength = pLinearizedTarget.ArrayLength; break;
                default: throw new ArgumentException("Type");
            }
        }
    }
}
