using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public class TileGrid : BitBlockGrid<TileIndex>
    {
        protected override TileIndex[,] Grid { get; }

        public TileGrid(int tiles) : this(tiles/2,tiles/2)
        {
        }

        public TileGrid(int tilesWidth, int tilesHeight)
        {
            Grid = new TileIndex[tilesWidth, tilesHeight]
                .FillDefault();
        }
    }
}
