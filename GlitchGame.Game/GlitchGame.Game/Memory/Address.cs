using System;
using System.Collections.Generic;
using System.Text;

namespace GlitchGame.GameMain.Memory
{
    public struct ByteAddress
    {
        public byte Address;
    }

    public struct PrecisionAddress
    {
        public int Address { get; }
        public byte BitOffset { get; }

        public PrecisionAddress(int address, int bitOffset)
        {
            while (bitOffset >= 8)
            {
                bitOffset -= 8;
                address++;
            }

            Address = address;
            BitOffset = (byte)bitOffset;
        }

        public override string ToString()
        {
            return $"{Address} +{BitOffset}";
        }
    }
}
