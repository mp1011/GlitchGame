using GlitchGame.GameMain.Extensions;
using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.Graphics
{
    public class TileLayer
    {
        public TileMap TileMap { get; }
        public Value2 Palette { get; set; }

        public byte XOffset { get; set; }

        public byte YOffset { get; set; }

        public TileLayer()
        {
            TileMap = new TileMap(new ByteValue(), new ByteValue(), new BackgroundTileGrid(new TileIndex()));
        }

        public byte GetColorAtScreenPoint(TileSet tileSet, byte screenX, byte screenY)
        {
            var x = screenX + XOffset;
            var y = screenY + YOffset;
            return TileMap.Tiles.GetColorAtPoint(tileSet, x, y);
        }
    }

    public class BackgroundTileGrid : Grid<TileIndex>
    {
        public BackgroundTileGrid(params TileIndex[] items) : base(items)
        {
        }

        public override int Columns => 34;
        public override int Rows => 32;


    }
}
