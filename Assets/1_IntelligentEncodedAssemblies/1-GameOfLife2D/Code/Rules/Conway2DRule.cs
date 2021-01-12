
using System;
using UnityEngine;

namespace RC3
{
    namespace RC3_GameOfLife2D
    {
        /// <summary>
        /// Rule for Conway's game of life
        /// </summary>
        public class Conway2DRule : MonoBehaviour, ICARule2D
        {
            private Index2[] _neighborhood = Neighborhoods.MooreR1;

            /// <summary>
            /// Constructor for this rule
            /// </summary>
            public Conway2DRule()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            /// <param name="current"></param>
            /// <returns></returns>
            public int NextState(Index2 index, int[,] current)
            {
                int sum = GetNeighborSum(index, current);

                int state = current[index.I, index.J];
                //the state that will be output based on rules
                int output = 0;

                //if current state is "alive"
                if (state == 1)
                {
                    //loneliness
                    if (sum < 2)
                    {
                        output = 0;
                    }

                    //balance
                    if (sum >= 2 && sum <= 3)
                    {
                        output = 1;
                    }

                    //overcrowding
                    if (sum > 3)
                    {
                        output = 0;
                    }
                }

                //if current state is "dead"
                if (state == 0)
                {
                    //procreation
                    if (sum == 3)
                    {
                        output = 1;
                    }

                    //no procreation
                    else
                    {
                        output = 0;
                    }
                }

                return output;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="i0"></param>
            /// <param name="j0"></param>
            /// <returns></returns>
            private int GetNeighborSum(Index2 index, int[,] current)
            {
                int nrows = current.GetLength(0);
                int ncols = current.GetLength(1);
                int sum = 0;

                foreach (Index2 offset in _neighborhood)
                {
                    int i1 = Wrap(index.I + offset.I, nrows);
                    int j1 = Wrap(index.J + offset.J, ncols);

                    if (current[i1, j1] > 0)
                        sum++;
                }

                return sum;
            }

            /// <summary>
            /// Deals with boundary conditions and wraps neighborhood calculation from one side to another
            /// </summary>
            /// <param name="i"></param>
            /// <param name="n"></param>
            /// <returns></returns>
            private static int Wrap(int i, int n)
            {
                i %= n;

                if (i < 0)
                {
                    return i + n;
                }
                else
                {
                    return i;
                }
                //return (i < 0) ? i + n : i;
            }
        }
    }
}