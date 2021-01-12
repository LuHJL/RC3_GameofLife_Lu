
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace RC3
{
    namespace GameOfLifeStack
    {

        public static class Interop
        {
            /// <summary>
            /// 
            /// </summary>

            public static string ToString(IEnumerable<CellLayer> layers)
            {

                return ToString(
                    layers.SelectMany(layer => ToEnumerable(layer.Cells)),
                    cell => $"{cell.State}, ");

            }


            /// <summary>
            /// 
            /// </summary>
            public static string ToString(IEnumerable<CellLayer> layers, Func<Cell, string> formatter)
            {
                return ToString(layers.SelectMany(layer => ToEnumerable(layer.Cells)), formatter);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="cells"></param>
            /// <returns></returns>
            public static string ToString<T>(IEnumerable<T> items, Func<T, string> formatter)
            {
                StringBuilder text = new StringBuilder();

                foreach (var item in items)
                    text.Append(formatter(item));

                return text.ToString();
            }


            /// <summary>
            /// 
            /// </summary>
            private static IEnumerable<T> ToEnumerable<T>(T[,] source)
            {
                int nrows = source.GetLength(0);
                int ncols = source.GetLength(1);

                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                        yield return source[i, j];
                }
            }
        }
    }
}
