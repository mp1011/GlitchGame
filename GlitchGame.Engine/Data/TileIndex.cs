using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public struct TileIndex : IBitBlock
    {
        public int BitSize => Settings.VRAMTileColumns.BitsNeeded();
    }
}
