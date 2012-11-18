using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI
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

        public void Read(PEFile pFile)
        {
            SizeOfHeader = pFile.ReadUInt32();
            MajorRuntimeVersion = pFile.ReadUInt16();
            MinorRuntimeVersion = pFile.ReadUInt16();
            Metadata.Read(pFile);
            Flags = pFile.ReadUInt32();
            EntryPointToken = pFile.ReadUInt32();
            EntryPointRVA = pFile.ReadUInt16();
            Resources.Read(pFile);
            StrongNameSignature.Read(pFile);
            CodeManagerTable.Read(pFile);
            VTableFixups.Read(pFile);
            ExportAddressTableJumps.Read(pFile);
            ManagedNativeHeader.Read(pFile);
        }
    }
}
