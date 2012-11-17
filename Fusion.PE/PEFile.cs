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

        public bool Load(PEReader pReader)
        {
            if (!DOSHeader.Read(pReader)) return false;
            pReader.Cursor = DOSHeader.NextHeaderOffset;
            if (!PEHeader.Read(pReader)) return false;
            if (!OptionalHeader.Read(pReader)) return false;
            SectionHeaders = new SectionHeader[PEHeader.NumberOfSections];
            foreach (SectionHeader sectionHeader in SectionHeaders) if (sectionHeader.Read(pReader)) return false;
            return true;
        }
    }
}
