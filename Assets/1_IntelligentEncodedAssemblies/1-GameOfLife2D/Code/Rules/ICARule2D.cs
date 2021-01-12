namespace RC3
{
    namespace RC3_GameOfLife2D
    {
        /// <summary>
        /// 
        /// </summary>
        public interface ICARule2D
        {
            /// <summary>
            /// Calculates the next state at the given index with the current state
            /// </summary>
            int NextState(Index2 index, int[,] current);
        }
    }
}