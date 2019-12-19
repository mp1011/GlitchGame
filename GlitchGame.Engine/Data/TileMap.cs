using System;

namespace GlitchGame.Engine.Data
{
    public class TileMap : BitBlockGrid<TileIndex>
    {
        protected override TileIndex[,] Grid => throw new NotImplementedException();
    }
}
