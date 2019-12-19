using GlitchGame.Engine.Extensions;
using System.Linq;

namespace GlitchGame.Engine.Data
{
    public abstract class BitBlockGrid<T> : IBitBlock
          where T:IBitBlock
    {
        public int BitSize => Grid
            .EnumerateColumnsThenRows()
            .Sum(p => p.BitSize);

        protected abstract T[,] Grid { get; }


    }
}
