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

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt16(ref Magic) ||
                !pReader.ReadByte(ref MajorLinkerVersion) ||
                !pReader.ReadByte(ref MinorLinkerVersion) ||
                !pReader.ReadUInt32(ref SizeOfCode) ||
                !pReader.ReadUInt32(ref SizeOfInitializedData) ||
                !pReader.ReadUInt32(ref SizeOfUninitializedData) ||
                !pReader.ReadUInt32(ref AddressOfEntryPoint) ||
                !pReader.ReadUInt32(ref BaseOfCode) ||
                !pReader.ReadUInt32(ref BaseOfData) ||
                !pReader.ReadUInt32(ref ImageBase) ||
                !pReader.ReadUInt32(ref SectionAlignment) ||
                !pReader.ReadUInt32(ref FileAlignment) ||
                !pReader.ReadUInt16(ref MajorOperatingSystemVersion) ||
                !pReader.ReadUInt16(ref MinorOperatingSystemVersion) ||
                !pReader.ReadUInt16(ref MajorImageVersion) ||
                !pReader.ReadUInt16(ref MinorImageVersion) ||
                !pReader.ReadUInt16(ref MajorSubsystemVersion) ||
                !pReader.ReadUInt16(ref MinorSubsystemVersion) ||
                !pReader.ReadUInt32(ref Reserved) ||
                !pReader.ReadUInt32(ref SizeOfImage) ||
                !pReader.ReadUInt32(ref SizeOfHeaders) ||
                !pReader.ReadUInt32(ref Checksum) ||
                !pReader.ReadUInt16(ref Subsystem) ||
                !pReader.ReadUInt16(ref DllCharacteristics) ||
                !pReader.ReadUInt32(ref SizeOfStackReserve) ||
                !pReader.ReadUInt32(ref SizeOfStackCommit) ||
                !pReader.ReadUInt32(ref SizeOfHeapReserve) ||
                !pReader.ReadUInt32(ref SizeOfHeapCommit) ||
                !pReader.ReadUInt32(ref LoaderFlags) ||
                !pReader.ReadUInt32(ref NumberOfRVAAndSizes) ||
                !DataDirectories[0].Read(pReader) ||
                !DataDirectories[1].Read(pReader) ||
                !DataDirectories[2].Read(pReader) ||
                !DataDirectories[3].Read(pReader) ||
                !DataDirectories[4].Read(pReader) ||
                !DataDirectories[5].Read(pReader) ||
                !DataDirectories[6].Read(pReader) ||
                !DataDirectories[7].Read(pReader) ||
                !DataDirectories[8].Read(pReader) ||
                !DataDirectories[9].Read(pReader) ||
                !DataDirectories[10].Read(pReader) ||
                !DataDirectories[11].Read(pReader) ||
                !DataDirectories[12].Read(pReader) ||
                !DataDirectories[13].Read(pReader) ||
                !DataDirectories[14].Read(pReader) ||
                !DataDirectories[15].Read(pReader)) return false;
            return true;
        }
    }
}
