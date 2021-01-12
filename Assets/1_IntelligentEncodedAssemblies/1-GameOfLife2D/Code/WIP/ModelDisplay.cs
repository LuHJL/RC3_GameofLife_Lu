using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SpatialSlur;

namespace RC3
{
    namespace RC3_GameOfLife2D
    {

        public class ModelDisplay : MonoBehaviour
        {
            //mode for display, gets potentially changed by InputHandler
            private CellDisplayMode _displaymode = CellDisplayMode.Alive;

            //stores whether the display has changed since last step
            //so we don't update every frame if not necessary
            private bool _displaychanged;

            //stores the cells to change their visual attributes
            private Cell[,] _cells;

            //input size of the grid
            private int _countX;
            private int _countY;

            private bool _isdisplayed = true;

            public bool IsDisplayed
            {
                get { return _isdisplayed; }
                set { _isdisplayed = value; }
            }

            public CellDisplayMode DisplayMode
            {
                get { return _displaymode; }
                set { _displaymode = value; }
            }

            /// <summary>
            /// Initialize the display
            /// </summary>
            /// <param name="countX"></param>
            /// <param name="countY"></param>
            /// <param name="cells"></param>
            public void Initialize(int countX, int countY, Cell[,] cells)
            {
                _cells = cells;
                _countX = countX;
                _countY = countY;
                _displaychanged = true;
            }

            private void Start()
            {
                

            }
            private void Update()
            {
                
            }
            public void UpdateDisplay()
            {
                if (_isdisplayed == false)
                    {
                        for (int y = 0; y < _countY; y++)
                        {
                            for (int x = 0; x < _countX; x++)
                            {
                                //disable cell
                                _cells[y, x].Renderer.enabled = false;
                            }
                        }
                    }

                    if (_isdisplayed == true)
                    {
                        for (int y = 0; y < _countY; y++)
                        {
                            for (int x = 0; x < _countX; x++)
                            {
                                if (_cells[y, x].State == 1)
                                {
                                    _cells[y, x].Renderer.enabled = true;
                                }
                                else
                                {
                                    _cells[y, x].Renderer.enabled = false;

                                }
                            }
                        }
                    }


                    //reset display mode has changed to false
                    _displaychanged = false;
                
            }
        }
    }
}
