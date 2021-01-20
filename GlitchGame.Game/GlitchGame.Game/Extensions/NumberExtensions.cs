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


        public static byte Increment(this byte b, int amount)
        {
            int newValue = b + amount;
            while (newValue > 255)
                newValue -= 255;
            while (newValue < 0)
                newValue += 255;

            return (byte)newValue;
        }

        public static int WrapInt(this int i, int max)
        {
            while (i >= max)
                i -= max;

            while (i < 0)
                i += max;

            return i;
        }

        public static byte WrapByte(this int i)
        {
            return (byte)(i.WrapInt(256));
        }
    }
}
