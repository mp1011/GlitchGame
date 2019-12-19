using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public struct Coordinate : IBitBlock
    {
        public int BitSize { get; }

        public Coordinate(int range)
        {
            BitSize = range.BitsNeeded();
        }

    }
}
