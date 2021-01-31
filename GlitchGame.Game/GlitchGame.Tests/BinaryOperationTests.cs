using FluentAssertions;
using GlitchGame.GameMain.Memory;
using NUnit.Framework;

namespace GlitchGame.Tests
{
    public class BinaryOperationTests
    {
        [SetUp]
        public void Setup()
        {
            SystemBinaryData.Reset();
        }

        [TestCase(0, 8, 255)]
        [TestCase(0, 6, 60)]
        [TestCase(0, 4, 15)]
        [TestCase(2, 4, 15)]
        [TestCase(2, 6, 60)]
        [TestCase(3, 8, 255)]
        [TestCase(3, 5, 31)]
        [TestCase(3, 7, 127)]
        [TestCase(5, 7, 127)]
        [TestCase(5, 8, 255)]
        [TestCase(6, 2, 3)]
        [TestCase(6, 2, 2)]
        [TestCase(6, 2, 1)]
        public void TestWriteBits(int bitOffset, int bitsToWrite, byte valueToWrite)
        {
            SystemBinaryData.SetIOPointer(0, bitOffset);
            SystemBinaryData.WriteBits(bitsToWrite, valueToWrite);

            SystemBinaryData.SetIOPointer(0, bitOffset);
            var data = SystemBinaryData.ReadBits(bitsToWrite);
            data.Should().Be(valueToWrite);
        }

        [TestCase(new byte[]{85,0},6,2,1)]
        [TestCase(new byte[] { 255, 255 }, 6, 4, 15)]
        [TestCase(new byte[] { 253, 175 }, 5, 6, 45)]

        public void TestReadBits(byte[] data, int offset, int bits, byte expected)
        {
            SystemBinaryData.SetIOPointer(0, 0);
            SystemBinaryData.WriteBytes(data);

            SystemBinaryData.SetIOPointer(0, offset);
            var result = SystemBinaryData.ReadBits(bits);

            result.Should().Be(expected);
        }

        [TestCase(7,0,3)]
        public void TestDoesNotOverwriteOtherBits(byte firstValue, byte secondValue, int bits)
        {
            SystemBinaryData.SetIOPointer(0, 0);

            for(int i = 0; i < 8; i++)          
                SystemBinaryData.WriteBits(bits, firstValue);

            for (int i = 0; i < 8; i++)
            {
                SystemBinaryData.SetIOPointer(0, bits * i);
                SystemBinaryData.WriteBits(bits, secondValue);

                for (int j = 0; i < 8; i++)
                {

                    SystemBinaryData.SetIOPointer(0, bits * j);
                    var read = SystemBinaryData.ReadBits(bits);
                    if (j <= i)
                        read.Should().Be(secondValue);
                    else
                        read.Should().Be(firstValue);
                }
            }


        }
    }
}