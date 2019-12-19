using GlitchGame.Engine.Extensions;

namespace GlitchGame.Engine.Data
{
    public class TileDefinition : BitBlockGrid<Pixel>
    {
        protected override Pixel[,] Grid { get; }

        public TileDefinition()
        {
            Grid = new Pixel[Settings.TileSize, Settings.TileSize].FillDefault();
        }
    }
}
