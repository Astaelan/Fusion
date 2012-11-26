using System;
using System.IO;
using System.Collections.Generic;

namespace Fusion.IL
{
    public sealed class ILReader
    {
        private readonly byte[] mData;
        private readonly uint mStartOfCode;
        private readonly uint mSizeOfCode;
        private uint mCursor;

        public ILReader(byte[] pData, uint pStartOfCode, uint pSizeOfCode)
        {
            mData = pData;
            mStartOfCode = pStartOfCode;
            mSizeOfCode = pSizeOfCode;
            mCursor = mStartOfCode;
        }

        public bool EndOfCode { get { return mCursor >= mSizeOfCode; } }

        public uint Offset { get { return mCursor - mStartOfCode; } }

        private uint Remaining { get { return mSizeOfCode - Offset; } }

        public ILOpcode ReadOpcode()
        {
            if (Remaining < 1) throw new EndOfStreamException();
            return (ILOpcode)mData[mCursor++];
        }

        public byte ReadByte()
        {
            if (Remaining < 1) throw new EndOfStreamException();
            return mData[mCursor++];
        }

        public ushort ReadUInt16()
        {
            if (Remaining < 2) throw new EndOfStreamException();
            ushort value = mData[mCursor++];
            value |= (ushort)(mData[mCursor++] << 8);
            return value;
        }

        public uint ReadUInt32()
        {
            if (Remaining < 4) throw new EndOfStreamException();
            uint value = mData[mCursor++];
            value |= ((uint)mData[mCursor++] << 8);
            value |= ((uint)mData[mCursor++] << 16);
            value |= ((uint)mData[mCursor++] << 24);
            return value;
        }

        public ulong ReadUInt64()
        {
            if (Remaining < 8) throw new EndOfStreamException();
            ulong value = mData[mCursor++];
            value |= ((ulong)mData[mCursor++] << 8);
            value |= ((ulong)mData[mCursor++] << 16);
            value |= ((ulong)mData[mCursor++] << 24);
            value |= ((ulong)mData[mCursor++] << 32);
            value |= ((ulong)mData[mCursor++] << 40);
            value |= ((ulong)mData[mCursor++] << 48);
            value |= ((ulong)mData[mCursor++] << 56);
            return value;
        }
    }
}
