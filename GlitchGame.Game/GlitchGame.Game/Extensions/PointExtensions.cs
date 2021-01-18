using Microsoft.Xna.Framework;

namespace GlitchGame.GameMain.Extensions
{
    public static class PointExtensions
    {
        public static Rectangle ToRectangle(this Point p, int tileSize)
        {
            return new Rectangle(p.X * tileSize, p.Y * tileSize, tileSize, tileSize);
        }
    }
}
