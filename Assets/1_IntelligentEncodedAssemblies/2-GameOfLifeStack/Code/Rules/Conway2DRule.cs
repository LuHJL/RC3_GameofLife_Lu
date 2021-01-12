
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur;

namespace RC3
{
    namespace GameOfLifeStack
    {
        /// <summary>
        /// Rule for Conway's game of life
        /// </summary>
        public class Conway2DRule : MonoBehaviour, ICARule2D
        {
            private Index2[] _offsets = Neighborhoods.MooreR1;


            /// <summary>
            /// 
            /// </summary>
            /// <param name="offsets"></param>
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

                if (state == 0)
                    return (sum == 3) ? 1 : 0;
                else
                    return (sum < 2 || sum > 3) ? 0 : 1;
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

                foreach (Index2 offset in _offsets)
                {
                    int i1 = Wrap(index.I + offset.I, nrows);
                    int j1 = Wrap(index.J + offset.J, ncols);

                    if (current[i1, j1] > 0)
                        sum++;
                }

                return sum;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="i"></param>
            /// <param name="n"></param>
            /// <returns></returns>
            private static int Wrap(int i, int n)
            {
                i %= n;
                return (i < 0) ? i + n : i;
            }
        }
    }
}