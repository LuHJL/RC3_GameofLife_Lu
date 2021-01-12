using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {

        /// <summary>
        /// 
        /// </summary>
        public abstract class ModelInitializer : ScriptableObject
        {
            public abstract void Initialize(int[,] state);
            public abstract void Initialize(int[,] state, Texture2D texture);

        }
    }
}
