using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class SectionHeader
    {
        public string Name;
        public uint PhysicalAddress;
        public uint VirtualSize;
        public uint VirtualAddress;
        public uint SizeOfRawData;
        public uint PointerToRawData;
        public uint PointerToRelocations;
        public uint PointerToLineNumbers;
        public ushort NumberOfRelocations;
        public ushort NumberOfLineNumbers;
        public uint Characteristics;

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadString(ref Name, 8) ||
                !pReader.ReadUInt32(ref PhysicalAddress) ||
                !pReader.ReadUInt32(ref VirtualSize) ||
                !pReader.ReadUInt32(ref VirtualAddress) ||
                !pReader.ReadUInt32(ref SizeOfRawData) ||
                !pReader.ReadUInt32(ref PointerToRawData) ||
                !pReader.ReadUInt32(ref PointerToRelocations) ||
                !pReader.ReadUInt32(ref PointerToLineNumbers) ||
                !pReader.ReadUInt16(ref NumberOfRelocations) ||
                !pReader.ReadUInt16(ref NumberOfLineNumbers) ||
                !pReader.ReadUInt32(ref Characteristics)) return false;
            return true;
        }
    }
}
