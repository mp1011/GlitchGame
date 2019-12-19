using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchGame.Engine.Extensions
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T> EnumerateColumnsThenRows<T>(this T[,] grid)
        {
            foreach (var row in Enumerable.Range(0, grid.GetLength(1)))
            {
                foreach (var column in Enumerable.Range(0, grid.GetLength(0)))
                {
                    yield return grid[column, row];
                }
            }
        }

        public static T[] FillDefault<T>(this T[] array)
            where T : new()
        {
            foreach (var index in Enumerable.Range(0, array.Length))
                array[index] = new T();

            return array;
        }

        public static T[,] FillDefault<T>(this T[,] array)
            where T : new()
        {
            foreach (var column in Enumerable.Range(0, array.GetLength(0)))
            {
                foreach(var row in Enumerable.Range(0, array.GetLength(1)))
                {
                    array[column, row] = new T();
                }
            }

            return array;
        }
    }
}
