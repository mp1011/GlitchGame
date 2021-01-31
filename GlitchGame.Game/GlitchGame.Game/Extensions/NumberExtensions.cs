using Microsoft.Xna.Framework;
using System;
using System.Text;

namespace GlitchGame.GameMain.Extensions
{
    public static class NumberExtensions
    {
        public static byte ShiftRight(this byte b, int places)
        {
            if (places == 0)
                return b;
            else if (places > 0)
                return (byte)(b >> places);
            else
                return (byte)(b << -places);
        }

        public static byte ShiftLeft(this byte b, int places)
        {
            return b.ShiftRight(-places);
        }

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

        public static string HexToBinary(this char hexChar)
        {
            var b = Convert.ToByte(hexChar.ToString(), 16);
            return Convert.ToString(b, 2).PadLeft(4, '0');
        }

        public static string BinaryToHex(this string binary)
        {
            var sb = new StringBuilder();

            for(int i = 0; i < binary.Length; i += 4)
            {
                var subString = i+4 >= binary.Length ? binary.Substring(i) : binary.Substring(i, 4);
                var b = Convert.ToByte(subString.ToString(), 2);
                sb.Append(Convert.ToString(b, 16));
            }

            return sb.ToString();
        }

        public static string ToBinary(this byte value, int bits)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0')
                .Substring(8 - bits, bits);
        }

        public static byte BinaryToByte(this string binary)
        {
            return Convert.ToByte(binary, 2);
        }
    }
}
