using System;

namespace GlitchGame.GameMain.Memory
{
    public class AlignedByteValue : ByteValue
    {
        public AlignedByteValue() : base(true) { }

        public AlignedByteValue(byte value) : base(value,true)
        {
        }
    }
    
    public class ByteValue : IDataBlock
    {
        public int Address { get; }
        public byte Value { get; }

        public virtual int BitWidth => 8;

        public ByteValue() : this(false)
        {

        }

        public ByteValue(bool aligned)
        {
            if (aligned)
                SystemBinaryData.AlignToByte();

            Address = SystemBinaryData.IOPointer;
            var oldBitOffset = SystemBinaryData.BitOffset;
            Value = SystemBinaryData.ReadBits(BitWidth);

           // System.Diagnostics.Debug.WriteLine($"R\t{Address}\t{oldBitOffset}\t{BitWidth}\t{Value}");
        }

        public ByteValue(byte value) : this (value,false)
        {
        }

        public ByteValue(byte value, bool aligned)
        {
            if (aligned)
                SystemBinaryData.AlignToByte();

            Address = SystemBinaryData.IOPointer;
            Value = value;
            
            if(value >= Math.Pow(2, BitWidth))
                throw new Exception("Overflow");

           // System.Diagnostics.Debug.WriteLine($"W\t{SystemBinaryData.IOPointer}\t{SystemBinaryData.BitOffset}\t{BitWidth}\t{Value}");

            SystemBinaryData.WriteBits(BitWidth, value);      
        }
    }
    public class Value2 : ByteValue
    {
        public Value2(byte value) : base(value)
        {
        }

        public Value2() : base()
        {
        }

        public override int BitWidth => 2;
    }

    public class Value4 : ByteValue
    {
        public Value4(byte value) : base(value)
        {
        }

        public Value4() : base()
        {
        }

        public override int BitWidth => 4;
    }

    public class Value6 : ByteValue
    {
        public Value6(byte value) : base(value)
        {
        }

        public Value6() : base()
        {
        }

        public override int BitWidth => 6;
    }
}
