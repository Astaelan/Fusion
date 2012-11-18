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

        public void Read(PEFile pFile)
        {
            Name = pFile.ReadString(8);
            PhysicalAddress = VirtualSize = pFile.ReadUInt32();
            VirtualAddress = pFile.ReadUInt32();
            SizeOfRawData = pFile.ReadUInt32();
            PointerToRawData = pFile.ReadUInt32();
            PointerToRelocations = pFile.ReadUInt32();
            PointerToLineNumbers = pFile.ReadUInt32();
            NumberOfRelocations = pFile.ReadUInt16();
            NumberOfLineNumbers = pFile.ReadUInt16();
            Characteristics = pFile.ReadUInt32();
        }
    }
}
