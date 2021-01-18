using Microsoft.Xna.Framework;

namespace GlitchGame.GameMain.Extensions
{
    public static class NumberExtensions
    {
        public static Point IndexToPoint(this int number, int columns)
        {
            int column = 0;
            while(number >= columns)
            {
                column++;
                number -= columns;
            }

            return new Point(number, column);
        }

        public static Point IndexToPoint(this byte number, int columns)
        {
            return ((int)number).IndexToPoint(columns);
        }
    }
}
