namespace RC3
{
    namespace GameOfLifeStack
    {
        /// <summary>
        /// Convention is [i, j, k] -> [Layer, Row, Column]
        /// </summary>

        public struct Index3
        {
            public readonly int I; // Layer
            public readonly int J; // Row
            public readonly int K; // Column


            /// <summary>
            /// 
            /// </summary>
            public Index3(int i, int j, int k)
            {
                I = i;
                J = j;
                K = k;
            }
        }
    }
}