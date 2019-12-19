using System.Linq;

namespace GlitchGame.Engine.Data
{
    public abstract class BitBlockArray<T> : IBitBlock
        where T : IBitBlock
    {
        protected abstract T[] Elements { get; }

        public int BitSize => Elements.Sum(p => p.BitSize);
    }
}
