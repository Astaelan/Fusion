using System.Runtime.InteropServices;
namespace Fusion.IR
{
    [StructLayout(LayoutKind.Explicit)]
    public class IRTargetData
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
            public IRTargetTypeAndData FieldTarget;
        }
        public struct FieldAddressTargetData
        {
            public uint FieldIndex;
            public IRType OwnerType;
            public IRTargetTypeAndData FieldTarget;
        }
        public struct StaticFieldTargetData { public IRField Field; }
        public struct StaticFieldAddressTargetData { public IRField Field; }
        public struct IndirectTargetData
        {
            public IRType Type;
            public IRTargetTypeAndData AddressTarget;
        }
        public struct SizeOfTargetData { public IRType Type; }
        public struct ArrayElementTargetData
        {
            public IRType ElementType;
            public IRTargetTypeAndData ArrayTarget;
            public IRTargetTypeAndData IndexTarget;
            public bool NoChecksRequired;
        }
        public struct ArrayElementAddressTargetData
        {
            public IRType ElementType;
            public IRTargetTypeAndData ArrayTarget;
            public IRTargetTypeAndData IndexTarget;
            public bool NoChecksRequired;
        }
        public struct ArrayLengthTargetData { public IRTargetTypeAndData ArrayTarget; }

        [FieldOffset(0)]
        public LocalVariableTargetData LocalVariable;
        [FieldOffset(0)]
        public LocalVariableAddressTargetData LocalVariableAddress;
        [FieldOffset(0)]
        public ParameterTargetData Parameter;
        [FieldOffset(0)]
        public ParameterAddressTargetData ParameterAddress;
        [FieldOffset(0)]
        public ConstantI4TargetData ConstantI4;
        [FieldOffset(0)]
        public ConstantI8TargetData ConstantI8;
        [FieldOffset(0)]
        public ConstantR4TargetData ConstantR4;
        [FieldOffset(0)]
        public ConstantR8TargetData ConstantR8;
        [FieldOffset(0)]
        public FieldTargetData Field;
        [FieldOffset(0)]
        public FieldAddressTargetData FieldAddress;
        [FieldOffset(0)]
        public StaticFieldTargetData StaticField;
        [FieldOffset(0)]
        public StaticFieldAddressTargetData StaticFieldAddress;
        [FieldOffset(0)]
        public IndirectTargetData Indirect;
        [FieldOffset(0)]
        public SizeOfTargetData SizeOf;
        [FieldOffset(0)]
        public ArrayElementTargetData ArrayElement;
        [FieldOffset(0)]
        public ArrayElementAddressTargetData ArrayElementAddress;
        [FieldOffset(0)]
        public ArrayLengthTargetData ArrayLength;
    }
}
