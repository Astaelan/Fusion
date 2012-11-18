using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public class PEFile
    {
        public byte[] Data = null;
        public uint Cursor = 0;

        public DOSHeader DOSHeader = new DOSHeader();
        public PEHeader PEHeader = new PEHeader();
        public OptionalHeader OptionalHeader = new OptionalHeader();
        public SectionHeader[] SectionHeaders = null;

        public PEFile(byte[] pData)
        {
            Data = pData;
        }

        public byte ReadByte()
        {
            byte value = Data[Cursor];
            Cursor += 1;
            return value;
        }

        public sbyte ReadSByte()
        {
            sbyte value = (sbyte)Data[Cursor];
            Cursor += 1;
            return value;
        }

        public ushort ReadUInt16()
        {
            ushort value = Data[Cursor];
            value |= (ushort)(Data[Cursor + 1] << 8);
            Cursor += 2;
            return value;
        }

        public short ReadInt16()
        {
            short value = Data[Cursor];
            value |= (short)(Data[Cursor + 1] << 8);
            Cursor += 2;
            return value;
        }

        public uint ReadUInt32()
        {
            uint value = Data[Cursor];
            value |= (uint)(Data[Cursor + 1] << 8);
            value |= (uint)(Data[Cursor + 2] << 16);
            value |= (uint)(Data[Cursor + 3] << 24);
            Cursor += 4;
            return value;
        }

        public int ReadInt32()
        {
            int value = Data[Cursor];
            value |= (int)(Data[Cursor + 1] << 8);
            value |= (int)(Data[Cursor + 2] << 16);
            value |= (int)(Data[Cursor + 3] << 24);
            Cursor += 4;
            return value;
        }

        public ulong ReadUInt64()
        {
            ulong value = Data[Cursor];
            value |= (ulong)((ulong)Data[Cursor + 1] << 8);
            value |= (ulong)((ulong)Data[Cursor + 2] << 16);
            value |= (ulong)((ulong)Data[Cursor + 3] << 24);
            value |= (ulong)((ulong)Data[Cursor + 4] << 32);
            value |= (ulong)((ulong)Data[Cursor + 5] << 40);
            value |= (ulong)((ulong)Data[Cursor + 6] << 48);
            value |= (ulong)((ulong)Data[Cursor + 7] << 56);
            Cursor += 8;
            return value;
        }

        public long ReadInt64()
        {
            long value = Data[Cursor];
            value |= (long)((long)Data[Cursor + 1] << 8);
            value |= (long)((long)Data[Cursor + 2] << 16);
            value |= (long)((long)Data[Cursor + 3] << 24);
            value |= (long)((long)Data[Cursor + 4] << 32);
            value |= (long)((long)Data[Cursor + 5] << 40);
            value |= (long)((long)Data[Cursor + 6] << 48);
            value |= (long)((long)Data[Cursor + 7] << 56);
            Cursor += 8;
            return value;
        }

        public byte[] ReadBytes(uint pLength)
        {
            byte[] value = new byte[pLength];
            Buffer.BlockCopy(Data, (int)Cursor, value, 0, (int)pLength);
            Cursor += pLength;
            return value;
        }

        public string ReadString()
        {
            uint length = 0;
            while (Data[Cursor + length] != 0x00)
            {
                ++length;
                if (length >= Data.Length) return "";
            }
            string value = "";
            if (length > 0) value = Encoding.ASCII.GetString(Data, (int)Cursor, (int)length);
            Cursor += length + 1;
            return value;
        }

        public string ReadString(uint pLength)
        {
            string value = Encoding.ASCII.GetString(Data, (int)Cursor, (int)pLength);
            Cursor += pLength;
            return value;
        }

        public virtual void Load()
        {
            DOSHeader.Read(this);
            Cursor = DOSHeader.NextHeaderOffset;
            PEHeader.Read(this);
            OptionalHeader.Read(this);
            SectionHeaders = new SectionHeader[PEHeader.NumberOfSections];
            for (int index = 0; index < SectionHeaders.Length; ++index)
            {
                SectionHeaders[index] = new SectionHeader();
                SectionHeaders[index].Read(this);
            }
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
