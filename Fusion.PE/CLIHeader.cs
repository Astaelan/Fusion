using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class CLIHeader
    {
        public uint SizeOfHeader;
        public ushort MajorRuntimeVersion;
        public ushort MinorRuntimeVersion;
        public DataDirectory Metadata = new DataDirectory();
        public uint Flags;
        public uint EntryPointToken;
        public ushort EntryPointRVA;
        public DataDirectory Resources = new DataDirectory();
        public DataDirectory StrongNameSignature = new DataDirectory();
        public DataDirectory CodeManagerTable = new DataDirectory();
        public DataDirectory VTableFixups = new DataDirectory();
        public DataDirectory ExportAddressTableJumps = new DataDirectory();
        public DataDirectory ManagedNativeHeader = new DataDirectory();

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt32(ref SizeOfHeader) ||
                !pReader.ReadUInt16(ref MajorRuntimeVersion) ||
                !pReader.ReadUInt16(ref MinorRuntimeVersion) ||
                !Metadata.Read(pReader) ||
                !pReader.ReadUInt32(ref Flags) ||
                !pReader.ReadUInt32(ref EntryPointToken) ||
                !pReader.ReadUInt16(ref EntryPointRVA) ||
                !Resources.Read(pReader) ||
                !StrongNameSignature.Read(pReader) ||
                !CodeManagerTable.Read(pReader) ||
                !VTableFixups.Read(pReader) ||
                !ExportAddressTableJumps.Read(pReader) ||
                !ManagedNativeHeader.Read(pReader)) return false;
            return true;
        }
    }
}
