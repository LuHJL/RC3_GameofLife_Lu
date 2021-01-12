﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// Takes keypresses / inputs to trigger changes the game
        /// </summary>
        public class InputHandler : MonoBehaviour
        {
            //provides access to the stackmodel and stackdisplay for running their functions
            private StackModelManager _model;
            private StackDisplay _display;


            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                //provides access to the stackmodel and stackdisplay for running their function
                _model = GetComponent<StackModelManager>();
                _display = GetComponent<StackDisplay>();
            }


            /// <summary>
            /// Updates every frame the keypresses
            /// </summary>
            private void Update()
            {
                //run the keypress function each frame
                HandleKeyPress();
            }


            /// <summary>
            /// Keypresses that change the behaviour or visualisation of the game
            /// </summary>
            private void HandleKeyPress()
            {
                // Reset model
                if (Input.GetKeyDown(KeyCode.X))
                {
                    _model.ResetModel();
                }

                // Pause model
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    _model.ExecutionMode = ModelExecutionMode.Pause;
                }

                // Step incrementally through execution of model
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    _model.ExecutionMode = ModelExecutionMode.StepByStep;
                }

                //Run the model continuously
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    _model.ExecutionMode = ModelExecutionMode.Run;
                }

                //Run the model continuously
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    _model.HasStepped = false;
                }

                // Update display mode
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    
                    if (_display.DisplayMode!= CellDisplayMode.None)
                    {
                        _display.DisplayMode = CellDisplayMode.None;
                    }
                    else
                    {
                        _display.DisplayMode = CellDisplayMode.Alive;
                    }
                }

                // Update display mode
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _display.DisplayMode = CellDisplayMode.Age;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _display.DisplayMode = CellDisplayMode.LayerDensity;
                }

                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _display.DisplayMode = CellDisplayMode.MooreR1Density;
                }

                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    _display.DisplayMode = CellDisplayMode.VNR1Density;
                }

                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    _display.DisplayMode = CellDisplayMode.VNR2Density;
                }

                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    _display.DisplayMode = CellDisplayMode.FunnyDisplay;
                }

                else if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    _display.DisplayMode = CellDisplayMode.OldCells;
                }



            }
        }
    }
}
