using System;

namespace GlitchGame.GameMain.Memory
{
    public struct Value4
    {
        public byte Value { get; }

        public Value4(byte value)
        {
            Value = value;
            if (value >= 4)
                throw new Exception("Overflow");
        }

        public static implicit operator byte(Value4 i) => i.Value;
        public static implicit operator Value4(byte b) => new Value4(b);
    }

    public struct Value8
    {
        public byte Value { get; }

        public Value8(byte value)
        {
            Value = value;
            if (value >= 8)
                throw new Exception("Overflow");
        }

        public static implicit operator byte(Value8 i) => i.Value;
        public static implicit operator Value8(byte b) => new Value8(b);
    }

    public struct Value64 
    {
        public byte Value { get; }

        public Value64(byte offset)
        {
            Value = offset;
            if (offset >= 64)
                throw new Exception("Overflow");
        }

        public static implicit operator byte(Value64 i) =>i.Value;
        public static implicit operator Value64(byte b) => new Value64(b);

        public static implicit operator int(Value64 i) => i.Value;
        public static implicit operator Value64(int b) => new Value64((byte)b);

    }
}
