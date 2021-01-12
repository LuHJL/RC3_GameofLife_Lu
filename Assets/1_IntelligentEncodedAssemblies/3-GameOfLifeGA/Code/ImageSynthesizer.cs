using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// 
        /// </summary>
        public static class ImageSynthesizer
        {

            /// <summary>
            /// Combine 4 images as quarters of 1 image
            /// </summary>
            /// <param name="texture1"></param>
            /// <param name="texture2"></param>
            /// <param name="texture3"></param>
            /// <param name="texture4"></param>
            public static Texture2D CombineFour(Texture2D texture1, Texture2D texture2, Texture2D texture3, Texture2D texture4, int rows, int cols)
            {
                var output = new Texture2D(rows, cols, TextureFormat.ARGB32, false);

                int t1rows = rows / 2;
                int t1cols = cols / 2;
                float ti = 1.0f / (t1rows - 1);
                float tj = 1.0f / (t1cols - 1);

                for (int i = 0; i < t1rows; i++)
                {
                    for (int j = 0; j < t1cols; j++)
                    {

                        output.SetPixel(i, j, texture1.GetPixelBilinear(i * ti, j * tj));
                    }
                }

                for (int i = 0; i < t1rows; i++)
                {
                    for (int j = 0; j < t1cols; j++)
                    {

                        output.SetPixel(i + t1rows, j, texture2.GetPixelBilinear(i * ti, j * tj));
                    }
                }

                for (int i = 0; i < t1rows; i++)
                {
                    for (int j = 0; j < t1cols; j++)
                    {

                        output.SetPixel(i, j + t1cols, texture3.GetPixelBilinear(i * ti, j * tj));
                    }
                }

                for (int i = 0; i < t1rows; i++)
                {
                    for (int j = 0; j < t1cols; j++)
                    {

                        output.SetPixel(i + t1rows, j + t1cols, texture4.GetPixelBilinear(i * ti, j * tj));
                    }
                }

                return output;
            }




            /*
            /// <summary>
            /// Sample half of each image and combine to a new image (left,right)
            /// </summary>
            /// <param name="texture1"></param>
            /// <param name="texture2"></param>
            public static void CombineHalf1(Texture2D texture1, Texture2D texture2)
            {
                var output = new Texture2D((texture1.width / 2) + (texture2.width / 2), texture1.height, TextureFormat.ARGB32, false);

                for (int i = 0; i < texture1.width / 2; i++)
                {
                    for (int j = 0; j < texture1.height; j++)
                    {
                        output.SetPixel(i, j, texture1.GetPixel(i, j));
                    }
                }
                for (int i = texture2.width / 2; i < texture2.width; i++)
                {
                    for (int j = 0; j < texture1.height; j++)
                    {
                        output.SetPixel(i, j, texture2.GetPixel(i, j));
                    }
                }
            }


            /// <summary>
            /// Sample half of each image and combine to a new image (bottom,top)
            /// </summary>
            public static void CombineHalf2(Texture2D texture1, Texture2D texture2)
            {
                var output = new Texture2D(texture1.width, (texture1.height / 2) + (texture2.height / 2), TextureFormat.ARGB32, false);

                for (int i = 0; i < texture1.width; i++)
                {
                    for (int j = 0; j < texture1.height / 2; j++)
                    {
                        output.SetPixel(i, j, texture1.GetPixel(i, j));
                    }
                }
                for (int i = 0; i < texture1.width; i++)
                {
                    for (int j = texture2.height / 2; j < texture2.height; j++)
                    {
                        output.SetPixel(i, j, texture2.GetPixel(i, j));
                    }
                }
            }
            */


        }
    }
}