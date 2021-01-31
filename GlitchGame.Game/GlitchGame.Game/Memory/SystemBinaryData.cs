using GlitchGame.GameMain.Extensions;
using System;
using System.Text;

namespace GlitchGame.GameMain.Memory
{
    public static class SystemBinaryData
    {
        private static byte[] _data = new byte[5000]; //todo, size


        static SystemBinaryData()
        {
            Reset();
        }

        public static void Reset()
        {
            for (int i = 0; i < _data.Length; i++)
                _data[i] = 0;
        }

        public static void AlignToByte()
        {
            if(BitOffset > 0)
            {
                BitOffset = 0;
                IOPointer++;
            }
        }
            
        public static int IOPointer { get; private set; }

        public static void SetIOPointer(int byteAddress, int bitOffset)
        {
            IOPointer = byteAddress;
            BitOffset = bitOffset;

            while (BitOffset >= 8)
            {
                BitOffset -= 8;
                IOPointer++;
            }
        }

        public static int BitOffset { get; set; }

        public static byte[] PeekBytes(int bytes)
        {
            var oldAddr = IOPointer;
            var oldOffset = BitOffset;

            var ret = ReadBytes(bytes);

            IOPointer = oldAddr;
            BitOffset = oldOffset;

            return ret;
        }

        public static byte[] ReadBytes(int bytes)
        {
            byte[] b = new byte[bytes];

            for(int i = 0; i < b.Length; i++)
            {
                b[i] = ReadByte();
            }

            return b;
        }

        public static void WriteBytes(byte[] value)
        {
            for (int i = 0; i < value.Length; i++)
                WriteBits(8, value[i]);
        }

        public static byte ReadByte()
        {
            return ReadBits(8);
        }

        private static byte MaskLeft(int bits)
        {
            switch(bits)
            {
                case 1: return 128;
                case 2: return 192;
                case 3: return 224;
                case 4: return 240;
                case 5: return 248;
                case 6: return 252;
                case 7: return 254;
                case 8: return 255;
                default: return 0;
            }
        }

        private static byte MaskRight(int bits)
        {
            switch (bits)
            {
                case 1: return 1;
                case 2: return 3;
                case 3: return 7;
                case 4: return 15;
                case 5: return 31;
                case 6: return 63;
                case 7: return 127;
                case 8: return 255;
                default: return 0;
            }
        }

        public static byte ReadBits(int bits)
        {
            byte result;

            if (BitOffset + bits <= 8)
            {
                var readByte = (byte)(_data[IOPointer] >> (8-(BitOffset + bits)));
                readByte = (byte)(readByte & MaskRight(bits));

                result = readByte;
            }
            else
            {
                var leftPart = _data[IOPointer].ShiftLeft(BitOffset - (8 - bits));
                leftPart = (byte)(leftPart & MaskRight(bits));

                var rightPart = _data[IOPointer + 1].ShiftRight(8 - (BitOffset + bits - 8));

                result = (byte)(leftPart | rightPart);
            }

            BitOffset += bits;

            while (BitOffset >= 8)
            {
                BitOffset -= 8;
                IOPointer++;
            }

            return result;
        }

        public static byte ReadAt(PrecisionAddress addr, int bits)
        {
            byte result;

            if (addr.BitOffset + bits <= 8)
            {
                var readByte = (byte)(_data[addr.Address] >> (8 - (addr.BitOffset + bits)));
                readByte = (byte)(readByte & MaskRight(bits));

                result = readByte;
            }
            else
            {
                var leftPart = _data[addr.Address].ShiftLeft(addr.BitOffset - (8 - bits));
                leftPart = (byte)(leftPart & MaskRight(bits));

                var rightPart = _data[addr.Address + 1].ShiftRight(8 - (addr.BitOffset + bits - 8));

                result = (byte)(leftPart | rightPart);
            }

            return result;
        }

        public static void WriteBits(int bits, byte value)
        {
            if (BitOffset + bits <= 8)
            {
                var keep = _data[IOPointer] & MaskLeft(BitOffset);
                var writeValue = value << 8 - (bits+BitOffset);

                _data[IOPointer] = (byte)(keep | writeValue);
            }
            else
            {
                var leftKeep = (byte)(_data[IOPointer] & MaskLeft(BitOffset));
                var leftValue = value.ShiftLeft(8 - (bits + BitOffset));

                var rightKeep = _data[IOPointer+1] & MaskRight((byte)(16 - (BitOffset + bits)));
                var rightValue = value.ShiftLeft(8 - (BitOffset + bits - 8));

                _data[IOPointer] = (byte)(leftKeep | leftValue);
                _data[IOPointer + 1] = (byte)(rightKeep | rightValue);
            }

            BitOffset += bits;

            while (BitOffset >= 8)
            {
                BitOffset -= 8;
                IOPointer++;
            }
        }
    }
}
