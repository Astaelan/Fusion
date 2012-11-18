using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class DOSHeader
    {
        public ushort Signature;
        public ushort LastPageByteCount;
        public ushort PageCount;
        public ushort RelocationCount;
        public ushort HeaderParagraphCount;
        public ushort MinimumParagraphCount;
        public ushort MaximumParagraphCount;
        public ushort RegisterSS;
        public ushort RegisterSP;
        public ushort Checksum;
        public ushort RegisterIP;
        public ushort RegisterCS;
        public ushort RelocationOffset;
        public ushort Overlay;
        public ushort[] ReservedA = new ushort[4];
        public ushort OEMIdentifier;
        public ushort OEMInformation;
        public ushort[] ReservedB = new ushort[10];
        public uint NextHeaderOffset;

        public void Read(PEFile pFile)
        {
            Signature = pFile.ReadUInt16();
            LastPageByteCount = pFile.ReadUInt16();
            PageCount = pFile.ReadUInt16();
            RelocationCount = pFile.ReadUInt16();
            HeaderParagraphCount = pFile.ReadUInt16();
            MinimumParagraphCount = pFile.ReadUInt16();
            MaximumParagraphCount = pFile.ReadUInt16();
            RegisterSS = pFile.ReadUInt16();
            RegisterSP = pFile.ReadUInt16();
            Checksum = pFile.ReadUInt16();
            RegisterIP = pFile.ReadUInt16();
            RegisterCS = pFile.ReadUInt16();
            RelocationOffset = pFile.ReadUInt16();
            Overlay = pFile.ReadUInt16();
            for (int index = 0; index < ReservedA.Length; ++index) ReservedA[index] = pFile.ReadUInt16();
            OEMIdentifier = pFile.ReadUInt16();
            OEMInformation = pFile.ReadUInt16();
            for (int index = 0; index < ReservedB.Length; ++index) ReservedB[index] = pFile.ReadUInt16();
            NextHeaderOffset = pFile.ReadUInt32();
        }
    }
}
