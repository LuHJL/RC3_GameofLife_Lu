using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    /// <summary>
    /// Randomly assigns % of living cells to initialize the model
    /// The Initialize function takes a int[,] array of the state of 
    /// a 2d layer of cells and sets their initial state to match the 
    /// random pattern
    /// 
    /// Scriptable objects architecture explained here:
    /// https://www.youtube.com/watch?v=raQ3iHhE_Kk
    /// https://docs.unity3d.com/Manual/class-ScriptableObject.html
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/WS1/RandomInitializer")]
    public class RandomInitializer : ModelInitializer
    {
        //threshold for approximate percentage of cells that start alive
        [SerializeField] float _threshold = 0.75f;


        /// <summary>
        /// Function for initializing the model starting state
        /// </summary>
        /// <param name="state"></param>
        public override void Initialize(int[,] state)
        {
            int nrows = state.GetLength(0);
            int ncols = state.GetLength(1);

            for(int i = 0; i < nrows; i++)
            {
                for(int j = 0; j < ncols; j++)
                {
                    //randomly assign starting cell value based on threshold
                    if (Random.Range(0.0f, 1.0f) > _threshold)
                        state[i, j] = 1;
                    else
                        state[i, j] = 0;
                }
            }
        }
    }
}
