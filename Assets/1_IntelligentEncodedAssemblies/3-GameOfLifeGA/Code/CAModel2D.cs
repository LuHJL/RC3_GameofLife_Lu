
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// Controls the updating of the CellModel 2d layer
        /// </summary>
        public class CAModel2D
        {
            //2d array stores the current state of the 2d model
            private int[,] _currentState;

            //2d array stores the next state of the 2d model
            private int[,] _nextState;

            //Rule that is applied to the model to update its state
            private ICARule2D _rule;


            /// <summary>
            /// Public property provides access to current state of the model
            /// </summary>
            public int[,] CurrentState
            {
                get { return _currentState; }
            }


            /// <summary>
            /// Public property provides access to get and set the Rule
            /// </summary>
            public ICARule2D Rule
            {
                get { return _rule; }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException();

                    _rule = value;
                }
            }


            /// <summary>
            /// Constructor for CAModel2d
            /// </summary>
            /// <param name="rows"></param>
            /// <param name="columns"></param>
            public CAModel2D(ICARule2D rule, int rows, int columns)
            {
                _rule = rule;
                _currentState = new int[rows, columns];
                _nextState = new int[rows, columns];
            }


            /// <summary>
            /// 
            /// </summary>
            public void Execute()
            {
                int nrows = _currentState.GetLength(0);
                int ncols = _currentState.GetLength(1);

                // calculate next state at each location
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                        //calculate the next state using the rule
                        _nextState[i, j] = _rule.NextState(new Index2(i, j), _currentState);
                }

                // swap state buffers
                var temp = _currentState;
                _currentState = _nextState;
                _nextState = temp;
            }


        }

        /*

        /// <summary>
        /// 
        /// </summary>
        public class CAModel2D
        {
            private int[,] _currentState;
            private int[,] _nextState;
            private ICARule2D _rule;


            /// <summary>
            /// 
            /// </summary>
            public int[,] CurrentState
            {
                get { return _currentState; }
            }


            /// <summary>
            /// 
            /// </summary>
            public ICARule2D Rule
            {
                get { return _rule; }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException();

                    _rule = value;
                }
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="rows"></param>
            /// <param name="columns"></param>
            public CAModel2D(ICARule2D rule, int rows, int columns)
            {
                Rule = rule;
                _currentState = new int[rows, columns];
                _nextState = new int[rows, columns];
            }


            /// <summary>
            /// 
            /// </summary>
            public void Execute()
            {
                int nrows = _currentState.GetLength(0);
                int ncols = _currentState.GetLength(1);

                // calculate next state at each location
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                        _nextState[i, j] = _rule.NextAt(new Index2(i, j), _currentState);
                }

                // swap state buffers
                var temp = _currentState;
                _currentState = _nextState;
                _nextState = temp;
            }


            /// <summary>
            /// 
            /// </summary>
            public void StepParallel()
            {
                Parallel.ForEach(Partitioner.Create(0, _currentState.Length), range =>
                {
                    int n = _currentState.GetLength(1);

                    for (int index = range.Item1; index < range.Item2; index++)
                    {
                        int i = index / n;
                        int j = index - i * n;
                        _nextState[i, j] = _rule.NextAt(new Index2(i, j), _currentState);
                    }
                });

                // swap state buffers
                var temp = _currentState;
                _currentState = _nextState;
                _nextState = temp;
            }
        }

        */
    }
}