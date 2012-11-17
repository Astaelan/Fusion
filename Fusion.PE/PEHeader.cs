using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class PEHeader
    {
        public uint Signature;
        public ushort Machine;
        public ushort NumberOfSections;
        public uint Timestamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt32(ref Signature) ||
                !pReader.ReadUInt16(ref Machine) ||
                !pReader.ReadUInt16(ref NumberOfSections) ||
                !pReader.ReadUInt32(ref Timestamp) ||
                !pReader.ReadUInt32(ref PointerToSymbolTable) ||
                !pReader.ReadUInt32(ref NumberOfSymbols) ||
                !pReader.ReadUInt16(ref SizeOfOptionalHeader) ||
                !pReader.ReadUInt16(ref Characteristics)) return false;
            return true;
        }
    }
}
