using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class OptionalHeader
    {
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public uint BaseOfData;

        public uint ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Reserved;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint Checksum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public uint SizeOfStackReserve;
        public uint SizeOfStackCommit;
        public uint SizeOfHeapReserve;
        public uint SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRVAAndSizes;
        public DataDirectory[] DataDirectories = new DataDirectory[16];

        public bool Read(PEFile pFile)
        {
            Magic = pFile.ReadUInt16();
            MajorLinkerVersion = pFile.ReadByte();
            MinorLinkerVersion = pFile.ReadByte();
            SizeOfCode = pFile.ReadUInt32();
            SizeOfInitializedData = pFile.ReadUInt32();
            SizeOfUninitializedData = pFile.ReadUInt32();
            AddressOfEntryPoint = pFile.ReadUInt32();
            BaseOfCode = pFile.ReadUInt32();
            BaseOfData = pFile.ReadUInt32();
            ImageBase = pFile.ReadUInt32();
            SectionAlignment = pFile.ReadUInt32();
            FileAlignment = pFile.ReadUInt32();
            MajorOperatingSystemVersion = pFile.ReadUInt16();
            MinorOperatingSystemVersion = pFile.ReadUInt16();
            MajorImageVersion = pFile.ReadUInt16();
            MinorImageVersion = pFile.ReadUInt16();
            MajorSubsystemVersion = pFile.ReadUInt16();
            MinorSubsystemVersion = pFile.ReadUInt16();
            Reserved = pFile.ReadUInt32();
            SizeOfImage = pFile.ReadUInt32();
            SizeOfHeaders = pFile.ReadUInt32();
            Checksum = pFile.ReadUInt32();
            Subsystem = pFile.ReadUInt16();
            DllCharacteristics = pFile.ReadUInt16();
            SizeOfStackReserve = pFile.ReadUInt32();
            SizeOfStackCommit = pFile.ReadUInt32();
            SizeOfHeapReserve = pFile.ReadUInt32();
            SizeOfHeapCommit = pFile.ReadUInt32();
            LoaderFlags = pFile.ReadUInt32();
            NumberOfRVAAndSizes = pFile.ReadUInt32();
            for (int index = 0; index < DataDirectories.Length; ++index)
            {
                DataDirectories[index] = new DataDirectory();
                DataDirectories[index].Read(pFile);
            }
            return true;
        }
    }
}
