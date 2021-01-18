using System;
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

        public static byte[] BitStringToBytes(string str)
        {
            byte[] bytes = new byte[str.Length/8];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(str.Substring(8 * i, 8), 2);
            }

            return bytes;
        }
    }
}
