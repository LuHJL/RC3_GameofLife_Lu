
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;

namespace RC3
{

    namespace GameOfLifeGA
    {

        /// <summary>
        /// Static methods for importing from and exporting to external formats.
        /// </summary>
        public static class Interop
        {
            /// <summary>
            /// Binary serialization
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="item"></param>
            /// <param name="path"></param>
            public static void SerializeBinary<T>(T item, string path)
            {
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, item);
                }
            }


            /// <summary>
            /// Binary deserialization
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static object DeserializeBinary(string path)
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream);
                }
            }


            /// <summary>
            /// Binary deserialization
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            public static T DeserializeBinary<T>(string path)
            {
                return (T)DeserializeBinary(path);
            }


            /// <summary>
            /// Json serialization
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="item"></param>
            /// <param name="path"></param>
            public static void SerializeJson<T>(T item, string path)
            {
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    serializer.WriteObject(stream, item);
                }
            }


            /// <summary>
            /// Json deserialization
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="path"></param>
            /// <returns></returns>
            public static T DeserializeJson<T>(string path)
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var serializer = new DataContractJsonSerializer(typeof(T));
                    return (T)serializer.ReadObject(stream);
                }
            }



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
