using System;
using System.Linq;
using System.Text;

namespace GlitchGame.GameMain.Helpers
{
    public static class BinaryHelper
    {
        public static string HexStringToBitString(string str, int bitsPerCharacter)
        {
            var sb = new StringBuilder();
            foreach(var chr in str)
            {
                var b = Convert.ToByte(chr.ToString(), 16);
                sb.Append(Convert.ToString(b, bitsPerCharacter).PadLeft(bitsPerCharacter, '0'));
            }

            return sb.ToString();
        }

        public static string BitStringToHexString(string str, int bitsPerCharacter)
        {
            var sb = new StringBuilder();
            for(int i =0; i < str.Length; i+= bitsPerCharacter)
            {
                var value = Convert.ToByte(str.Substring(i, 2), 2);
                sb.Append(Convert.ToString(value, 16));
            }
            return sb.ToString();
        }

        public static byte[] BitStringToBytes(string str)
        {
            byte[] bytes = new byte[str.Length/8];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(str.Substring(8 * i, 8), 2);
            }

            return bytes;
        }

        public static string ByteToBitString(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');

        }
        public static string BytesToBitString(byte[] b)
        {
            return string.Join("", b.Select(ByteToBitString).ToArray());
        }
    }
}
