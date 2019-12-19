using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public class VRAMTileData : BitBlockGrid<TileDefinition>
    {
        protected override TileDefinition[,] Grid { get; }

        public VRAMTileData()
        {
            Grid = new TileDefinition[Settings.VRAMTileColumns, Settings.VRAMTileRows]
                .FillDefault();
        }

    }
}
