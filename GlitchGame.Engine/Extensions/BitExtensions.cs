using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchGame.Engine.Extensions
{
    public static class BitExtensions
    {
        public static int BitsNeeded(this int number)
        {
            int bitsNeeded = 1;
            while (Math.Pow(2, bitsNeeded) < number)
                bitsNeeded++;

            return bitsNeeded;
        }
    }
}
