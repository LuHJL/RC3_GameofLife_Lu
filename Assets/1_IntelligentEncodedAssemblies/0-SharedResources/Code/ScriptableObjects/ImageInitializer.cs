using UnityEngine;

namespace RC3
{
    /// <summary>
    /// Creates a Scriptable Object containing image used to initialize
    /// a model from a seed image
    /// 
    /// The Initialize function takes a int[,] array of the state of 
    /// a 2d layer of cells and sets their initial state to match an input image
    /// 
    /// Scriptable objects architecture explained here:
    /// https://www.youtube.com/watch?v=raQ3iHhE_Kk
    /// https://docs.unity3d.com/Manual/class-ScriptableObject.html
    /// </summary>

    [CreateAssetMenu(menuName = "RC3/WS1/ImageInitializer")]
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
                for(int j = 0; j < ncols; j++)
                {
                    Color color = _texture.GetPixelBilinear(j * tj, i * ti);

                    if (color.grayscale > _threshold)
                        state[i, j] = 1;
                    else
                        state[i, j] = 0;
                }
            }
        }
    }
}
