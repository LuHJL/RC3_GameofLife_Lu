
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RC3
{
    namespace RC3_GameOfLife2D
    {
        /// <summary>
        /// 
        /// </summary>
        public class CAModel2D
        {
            //store current state in 2d array
            private int[,] _currentState;

            //store next state in 2d array
            private int[,] _nextState;

            //define which neighborhood to operate in
            private Index2[] _neighborhood = Neighborhoods.MooreR1;

            //the rule we will execute to change the model state
            private ICARule2D _rule;


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
            /// Public property provides access to neighborhood
            /// </summary>
            public Index2[] Neighborhood
            {
                get { return _neighborhood; }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException();

                    _neighborhood = value;
                }
            }



            /// <summary>
            /// Main Execution run by the model manager each frame
            /// </summary>
            public void Execute()
            {
                //get number of rows and columns
                int nrows = _currentState.GetLength(0);
                int ncols = _currentState.GetLength(1);

                // calculate next state
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        //current state of the model
                        int state = _currentState[i, j];

                        //sum of "living" neighbors
                        int sum = GetNeighborSum(i, j);

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

                        //set the next state to the output based on rules
                        _nextState[i, j] = output;

                    }
                }

                // swap state buffers once the entire model state has been updated
                var temp = _currentState;
                _currentState = _nextState;
                _nextState = temp;
                
                //Swap(ref _currentState, ref _nextState);
            }

            /// <summary>
            /// 
            /// </summary>
            public void ExecuteNew()
            {
                int nrows = _currentState.GetLength(0);
                int ncols = _currentState.GetLength(1);

                // calculate next state at each location
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                    {
                        //calculate the next state using the rule
                        _nextState[i, j] = _rule.NextState(new Index2(i, j), _currentState);
                    }
                }

                // swap state buffers
                var temp = _currentState;
                _currentState = _nextState;
                _nextState = temp;

            }


            /// <summary>
            /// Function returns sum of living cells in the neighborhood
            /// </summary>
            /// <param name="i0"></param>
            /// <param name="j0"></param>
            /// <returns></returns>
            private int GetNeighborSum(int i0, int j0)
            {
                //get current state (2d array)
                var current = _currentState;
                int nrows = current.GetLength(0);
                int ncols = current.GetLength(1);

                //sum is a counter variable starting at zero
                int sum = 0;

                //foreach loop steps through the cells in the neighborhood
                //neighborhood contains Index2 "offsets" as a series of
                //integers that are offset from the current cell defining 
                //the neighbors
                foreach (Index2 offset in _neighborhood)
                {
                    //wrap is a function that takes each neighbor relative to
                    //the current cell and checks if it is out of the grid
                    //and if so wraps it around within the grid
                    int i1 = Wrap(i0 + offset.I, nrows);
                    int j1 = Wrap(j0 + offset.J, ncols);

                    //checks if the neighbor cell is alive
                    //if it is it adds 1 to the "sum" counter
                    if (current[i1, j1] > 0)
                        sum++;
                }

                //finally returns the sum of living neighbors
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

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="t0"></param>
            /// <param name="t1"></param>
            private static void Swap<T>(ref T t0, ref T t1)
            {
                var temp = t0;
                t0 = t1;
                t1 = temp;
            }
        }
    }
}