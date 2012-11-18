using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public enum FieldAttributes : ushort
    {
        None = 0,

        CompilerControlled = 0x0000,
        Private = 0x0001,
        FamilyAndAssembly = 0x0002,
        Assembly = 0x0003,
        Family = 0x0004,
        FamilyOrAssembly = 0x0005,
        Public = 0x0006,
        FieldAccessMask = 0x0007,

        Static = 0x0010,
        InitOnly = 0x0020,
        Literal = 0x0040,
        NotSerialized = 0x0080,
        SpecialName = 0x0200,

        PInvokeImplementation = 0x2000,

        RTSpecialName = 0x0400,
        HasFieldMarshal = 0x1000,
        HasDefault = 0x8000,
        HasFieldRVA = 0x0100,
    }

    [Flags]
    public enum MethodAttributes : ushort
    {
        None = 0x0000,

        CompilerControlled = 0x0000,
        Private = 0x0001,
        FamANDAssem = 0x0002,
        Assem = 0x0003,
        Family = 0x0004,
        FamORAssem = 0x0005,
        Public = 0x0006,
        MemberAccessMask = 0x0007,

        Static = 0x0010,
        Final = 0x0020,
        Virtual = 0x0040,
        HideBySig = 0x0080,
        VtableLayoutMask = 0x0100,

        ReuseSlot = 0x0000,
        NewSlot = 0x0100,

        Strict = 0x0200,
        Abstract = 0x0400,
        SpecialName = 0x0800,

        PInvokeImpl = 0x2000,
        UnmanagedExport = 0x0008,

        RTSpecialName = 0x1000,
        HasSecurity = 0x4000,
        RequireSecObject = 0x8000,
    }
}
