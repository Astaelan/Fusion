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

        public void Read(PEFile pFile)
        {
            Signature = pFile.ReadUInt32();
            Machine = pFile.ReadUInt16();
            NumberOfSections = pFile.ReadUInt16();
            Timestamp = pFile.ReadUInt32();
            PointerToSymbolTable = pFile.ReadUInt32();
            NumberOfSymbols = pFile.ReadUInt32();
            SizeOfOptionalHeader = pFile.ReadUInt16();
            Characteristics = pFile.ReadUInt16();
        }
    }
}
