using System;
using System.Collections.Generic;
using System.Text;

namespace GlitchGame.GameMain.Extensions
{
    public static class DebugExtensions
    {
        public static T DebugCheck<T>(this T obj, Func<T,bool> check)
        {
            if (check(obj))
            {
                throw new Exception("Check failed");
            }

            return obj;
        }
    }
}
