using System;

namespace GlitchGame.GameMain.Memory
{
    public abstract class Sequence<T> : IDataBlock
        where T:IDataBlock, new()
    {
        public abstract int Length { get; }

        public int Address => First.Address;

        public T First { get; }

        public int BitWidth => First.BitWidth * Length;


        public PrecisionAddress GetAddress(int index)
        {
            return new PrecisionAddress(Address, First.BitWidth * index);
        }

        public Sequence(T first)
        {
            First = first;
            SystemBinaryData.SetIOPointer(Address, First.BitWidth * Length);

            if ((BitWidth % 8) > 0)
                throw new Exception("Sequences must be byte aligned");
        }

        public Sequence()
        {
            First = new T();
            SystemBinaryData.SetIOPointer(Address, First.BitWidth * Length);
        }

        public T Get(int index)
        {
            SystemBinaryData.SetIOPointer(Address, First.BitWidth * index);
            return new T();
        }

        public void SetWritePointer(int index)
        {
            SystemBinaryData.SetIOPointer(Address, First.BitWidth * index);
        }
    }
}
