using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class PEFile
    {
        public DOSHeader DOSHeader = new DOSHeader();
        public PEHeader PEHeader = new PEHeader();
        public OptionalHeader OptionalHeader = new OptionalHeader();
        public SectionHeader[] SectionHeaders = null;
        public CLIHeader CLIHeader = new CLIHeader();
        public CLIMetadataHeader CLIMetadataHeader = new CLIMetadataHeader();
        public CLIMetadataStreamHeader TablesStream = null;
        public CLIMetadataStreamHeader StringsStream = null;
        public CLIMetadataStreamHeader USStream = null;
        public CLIMetadataStreamHeader GUIDStream = null;
        public CLIMetadataStreamHeader BlobStream = null;
        public CLIMetadataTablesHeader CLIMetadataTablesHeader = new CLIMetadataTablesHeader();

        public bool Load(PEReader pReader)
        {
            if (!DOSHeader.Read(pReader)) return false;
            pReader.Cursor = DOSHeader.NextHeaderOffset;
            if (!PEHeader.Read(pReader)) return false;
            if (!OptionalHeader.Read(pReader)) return false;
            SectionHeaders = new SectionHeader[PEHeader.NumberOfSections];
            for (int index = 0; index < SectionHeaders.Length; ++index) SectionHeaders[index] = new SectionHeader();
            foreach (SectionHeader sectionHeader in SectionHeaders) if (!sectionHeader.Read(pReader)) return false;
            DataDirectory headerDataDirectory = OptionalHeader.DataDirectories[14];
            SectionHeader headerSectionHeader = GetSection(headerDataDirectory.VirtualAddress);
            pReader.Cursor = headerSectionHeader.PointerToRawData + (headerDataDirectory.VirtualAddress - headerSectionHeader.VirtualAddress);
            if (!CLIHeader.Read(pReader)) return false;
            SectionHeader metadataSectionHeader = GetSection(CLIHeader.Metadata.VirtualAddress);
            uint offsetCLIMetadataHeader = metadataSectionHeader.PointerToRawData + (CLIHeader.Metadata.VirtualAddress - metadataSectionHeader.VirtualAddress);
            pReader.Cursor = offsetCLIMetadataHeader;
            if (!CLIMetadataHeader.Read(pReader)) return false;
            pReader.Cursor = offsetCLIMetadataHeader;
            foreach (CLIMetadataStreamHeader streamHeader in CLIMetadataHeader.Streams)
            {
                switch (streamHeader.Name)
                {
                    case "#~": TablesStream = streamHeader; break;
                    case "#Strings": StringsStream = streamHeader; break;
                    case "#US": USStream = streamHeader; break;
                    case "#GUID": GUIDStream = streamHeader; break;
                    case "#Blob": BlobStream = streamHeader; break;
                    default: throw new BadImageFormatException("Invalid CLIMetadataHeader Stream");
                }
            }
            pReader.Cursor = offsetCLIMetadataHeader + TablesStream.Offset;
            if (!CLIMetadataTablesHeader.Read(pReader)) return false;

            return true;
        }

        public SectionHeader GetSection(uint pVirtualAddress)
        {
            for (int index = 0; index < SectionHeaders.Length; ++index)
            {
                if (pVirtualAddress >= SectionHeaders[index].VirtualAddress &&
                    pVirtualAddress < (SectionHeaders[index].VirtualAddress + SectionHeaders[index].VirtualSize)) return SectionHeaders[index];
            }
            return null;
        }
    }
}
