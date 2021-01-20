using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.Graphics
{
    public class TileLayer
    {
        public TileMap TileMap { get; }
        public Value4 Palette { get; set; }

        public byte XOffset { get; set; }

        public byte YOffset { get; set; }

        public TileLayer()
        {
            TileMap = new TileMap(0, 0, new TileIndex[3840*2], 64);
        }

        public Value4 GetColorAtScreenPoint(TileSet tileSet, byte screenX, byte screenY)
        {
            var x = screenX + XOffset;
            var y = screenY + YOffset;
            return TileMap.GetColorAtPoint(tileSet, x, y);
        }
    }
}
