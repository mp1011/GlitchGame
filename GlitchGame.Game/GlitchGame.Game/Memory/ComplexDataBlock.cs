using System;
using System.Linq;

namespace GlitchGame.GameMain.Memory
{
    public abstract class ComplexDataBlock<T1> : IDataBlock
        where T1 : IDataBlock
    {
        public int Address => Block1.Address;


        protected T1 Block1 { get; }

        public int BitWidth => InternalBitWidth;

        protected abstract int InternalBitWidth { get; }

        public ComplexDataBlock(T1 block1)
        {
            Block1 = block1;
        }
    }

    public abstract class ComplexDataBlock<T1,T2> : ComplexDataBlock<T1>
        where T1 : IDataBlock
        where T2: IDataBlock
    {
        protected T2 Block2 { get; }

        protected override int InternalBitWidth => Block1.BitWidth + Block2.BitWidth;

        public ComplexDataBlock(T1 block1, T2 block2) : base(block1)
        {
            Block2 = block2;
        }
    }

    public abstract class ComplexDataBlock<T1, T2, T3> : ComplexDataBlock<T1, T2>
         where T1 : IDataBlock
         where T2 : IDataBlock
         where T3 : IDataBlock

    {
        protected T3 Block3 { get; }

        protected override int InternalBitWidth => Block1.BitWidth + Block2.BitWidth + Block3.BitWidth;

        public ComplexDataBlock(T1 block1, T2 block2, T3 block3) :base(block1,block2)
        {
            Block3 = block3;
        }
    }

    public abstract class ComplexDataBlock<T1, T2, T3, T4> : ComplexDataBlock<T1, T2,T3>
      where T1 : IDataBlock
      where T2 : IDataBlock
      where T3 : IDataBlock
      where T4 : IDataBlock
    { 
        protected T4 Block4 { get; }

        protected override int InternalBitWidth => Block1.BitWidth + Block2.BitWidth + Block3.BitWidth + Block4.BitWidth;

        public ComplexDataBlock(T1 block1, T2 block2, T3 block3, T4 block4) :base(block1,block2,block3)
        {
            Block4 = block4;
        }

        public void BeginWriteBlock1()
        {
            SystemBinaryData.SetIOPointer(Address, 0);
        }

        public void BeginWriteBlock2()
        {
            SystemBinaryData.SetIOPointer(Address, Block1.BitWidth);
        }

        public void BeginWriteBlock3()
        {
            SystemBinaryData.SetIOPointer(Address, Block1.BitWidth + Block2.BitWidth);
        }

        public void BeginWriteBlock4()
        {
            SystemBinaryData.SetIOPointer(Address, Block1.BitWidth + Block2.BitWidth + Block3.BitWidth);
        }
    }

}
