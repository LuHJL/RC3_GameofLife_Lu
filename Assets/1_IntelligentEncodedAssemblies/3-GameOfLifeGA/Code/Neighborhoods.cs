namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// 
        /// </summary>
        public static class Neighborhoods
        {
            /// <summary>
            /// Convention is [i, j] -> [Row, Column]
            /// </summary>
            public static readonly Index2[] MooreR1 =
            {
            new Index2(-1, -1),
            new Index2(-1, 0),
            new Index2(-1, 1),
            new Index2(0, -1),

            new Index2(0, 1),
            new Index2(1, -1),
            new Index2(1, 0),
            new Index2(1, 1)
        };


            /// <summary>
            /// Convention is [i, j] -> [Row, Column]
            /// </summary>
            public static readonly Index2[] VonNeumannR1 =
            {
            new Index2(-1, 0),
            new Index2(0, -1),
            new Index2(0, 1),
            new Index2(1, 0)
        };


            /// <summary>
            /// Convention is [i, j] -> [Row, Column]
            /// </summary>
            public static readonly Index2[] VonNeumannPair1 =
            {
            new Index2(0, -1),
            new Index2(0, 1),
        };


            /// <summary>
            /// Convention is [i, j] -> [Row, Column]
            /// </summary>
            public static readonly Index2[] VonNeumannPair2 =
            {
            new Index2(-1, 0),
            new Index2(1, 0),
        };


            /// <summary>
            /// Convention is [i, j] -> [Row, Column]
            /// </summary>
            public static readonly Index2[] VonNeumannR2 =
            {
            new Index2(-2, 0),
            new Index2(-1, -1),
            new Index2(-1, 0),
            new Index2(-1, 1),

            new Index2(0, -2),
            new Index2(0, -1),
            new Index2(0, 1),
            new Index2(0, 2),

            new Index2(1, -1),
            new Index2(1, 0),
            new Index2(1, 1),
            new Index2(2, 0)
        };


            /// <summary>
            /// Convention is [i, j, k] -> [Layer, Row, Column]
            /// </summary>
            public static readonly Index3[] Moore3Cen =
            {
            new Index3(-1, -1, -1),
            new Index3(-1, -1, 0),
            new Index3(-1, -1, 1),
            new Index3(-1, 0, -1),
            new Index3(-1, 0, 0),
            new Index3(-1, 0, 1),
            new Index3(-1, 1, -1),
            new Index3(-1, 1, 0),
            new Index3(-1, 1, 1),

            new Index3(0, -1, -1),
            new Index3(0, -1, 0),
            new Index3(0, -1, 1),
            new Index3(0, 0, -1),
            new Index3(0, 0, 0),
            new Index3(0, 0, 1),
            new Index3(0, 1, -1),
            new Index3(0, 1, 0),
            new Index3(0, 1, 1),

            new Index3(1, -1, -1),
            new Index3(1, -1, 0),
            new Index3(1, -1, 1),
            new Index3(1, 0, -1),
            new Index3(1, 0, 0),
            new Index3(1, 0, 1),
            new Index3(1, 1, -1),
            new Index3(1, 1, 0),
            new Index3(1, 1, 1)
        };


            /// <summary>
            /// Convention is [i, j, k] -> [Layer, Row, Column]
            /// </summary>
            public static readonly Index3[] Moore3NoCen =
            {
            new Index3(-1, -1, -1),
            new Index3(-1, -1, 0),
            new Index3(-1, -1, 1),
            new Index3(-1, 0, -1),
            new Index3(-1, 0, 0),
            new Index3(-1, 0, 1),
            new Index3(-1, 1, -1),
            new Index3(-1, 1, 0),
            new Index3(-1, 1, 1),

            new Index3(0, -1, -1),
            new Index3(0, -1, 0),
            new Index3(0, -1, 1),
            new Index3(0, 0, -1),

            new Index3(0, 0, 1),
            new Index3(0, 1, -1),
            new Index3(0, 1, 0),
            new Index3(0, 1, 1),

            new Index3(1, -1, -1),
            new Index3(1, -1, 0),
            new Index3(1, -1, 1),
            new Index3(1, 0, -1),
            new Index3(1, 0, 0),
            new Index3(1, 0, 1),
            new Index3(1, 1, -1),
            new Index3(1, 1, 0),
            new Index3(1, 1, 1)
        };
        }
    }
}