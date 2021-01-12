﻿namespace RC3
{
    namespace RC3_GameOfLife2D
    {
        /// <summary>
        /// Stores a Column / Row Index pair
        /// Convention is [i, j] -> [Row, Column]
        /// </summary>
        public struct Index2
        {
            public readonly int I; // Row
            public readonly int J; // Column

            /// <summary>
            /// 
            /// </summary>
            public Index2(int i, int j)
            {
                I = i;
                J = j;
            }
        }
    }
}