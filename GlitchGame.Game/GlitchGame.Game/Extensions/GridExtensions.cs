using GlitchGame.GameMain.Graphics;
using GlitchGame.GameMain.Memory;

namespace GlitchGame.GameMain.Extensions
{
    public static class GridExtensions
    {
        public static byte GetColorAtPoint(this Grid<TileIndex> grid, TileSet tileSet, int pixelX, int pixelY)
        {
            byte tileX = (byte)(pixelX / SystemConstants.TileSize);
            byte tileY = (byte)(pixelY / SystemConstants.TileSize);

            pixelX = pixelX % SystemConstants.TileSize;
            pixelY = pixelY % SystemConstants.TileSize;

            var tileIndexAddress = grid.GetAddressFromCoordinates(tileX, tileY);
            var tileValue = TileIndex.GetValue(tileIndexAddress);

            var flip = TileIndex.GetFlip(tileIndexAddress);
            var color = tileSet.GetColorAtPoint(tileValue, (byte)pixelX, (byte)pixelY, flip);
            return color;
        }
    }
}
