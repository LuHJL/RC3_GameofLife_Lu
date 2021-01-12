using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace RC3_GameOfLife2D
    {
        /// <summary>
        /// Takes keypresses / inputs to trigger changes the game
        /// </summary>
        public class InputHandler : MonoBehaviour
        {
            //provides access to the stackmodel and stackdisplay for running their functions
            private ModelManager2D _manager;

            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                //provides access to the stackmodel and stackdisplay for running their function
                _manager = GetComponent<ModelManager2D>();
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
                    _manager.ResetModel();
                }

                // Pause model
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    _manager.ExecutionMode = ModelExecutionMode.Pause;
                }

                // Step incrementally through execution of model
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    _manager.ExecutionMode = ModelExecutionMode.StepByStep;
                }

                //Run the model continuously
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    _manager.ExecutionMode = ModelExecutionMode.Run;
                }

                //Run the model continuously
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    _manager.HasStepped=false;
                }

            }
        }
    }
}
