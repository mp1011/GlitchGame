using System;
using System.Collections.Generic;
using System.Text;

namespace GlitchGame.GameMain.Extensions
{
    public static class ArrayExtensions
    {
        public static T GetFromCoordinates<T>(this T[] array, int x, int y, int columns)
        {
            return array[(y * columns) + x];
        }
    }
}
