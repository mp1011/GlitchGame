using FluentAssertions;
using GlitchGame.Engine.Data;
using NUnit.Framework;

namespace GlitchGame.Tests
{
    [TestFixture]
    public class ArchitectureTests 
    {
        [Test]
        public void RamFitsWithinSpace()
        {
            var ram = new RAM();
            var bitSize = ram.BitSize;
            bitSize.Should().Be(64000);
        }
    }
}
