using UnityEngine;

namespace RC3
{

    namespace GameOfLifeGA
    {
        [CreateAssetMenu(menuName = "RC3/GameOfLifeGA/ImageInitializer")]
        public class ImageInitializer : ModelInitializer
        {
            [SerializeField] private Texture2D _texture;
            [SerializeField] private float _threshold = 0.5f;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="state"></param>
            public override void Initialize(int[,] state)
            {

                int nrows = state.GetLength(0);
                int ncols = state.GetLength(1);

                float ti = 1.0f / (nrows - 1);
                float tj = 1.0f / (ncols - 1);

                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        Color color = _texture.GetPixelBilinear(j * tj, i * ti);

                        if (color.grayscale > _threshold)
                            state[i, j] = 1;
                        else
                            state[i, j] = 0;
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="state"></param>
            /// <param name="texture"></param>
            public override void Initialize(int[,] state, Texture2D texture)
            {
                int nrows = state.GetLength(0);
                int ncols = state.GetLength(1);

                float ti = 1.0f / (nrows - 1);
                float tj = 1.0f / (ncols - 1);

                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        Color color = texture.GetPixelBilinear(j * tj, i * ti);

                        if (color.grayscale > _threshold)
                            state[i, j] = 1;
                        else
                            state[i, j] = 0;
                    }
                }
            }

        }
    }
}
