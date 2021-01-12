namespace RC3
{
    namespace GameOfLifeStack
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