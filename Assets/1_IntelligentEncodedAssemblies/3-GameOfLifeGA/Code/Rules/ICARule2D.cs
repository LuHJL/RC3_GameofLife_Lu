namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// 
        /// </summary>
        public interface ICARule2D
        {
            /// <summary>
            /// Calculates the next state at the given index
            /// </summary>
            int NextState(Index2 index, int[,] current);
        }
    }
}