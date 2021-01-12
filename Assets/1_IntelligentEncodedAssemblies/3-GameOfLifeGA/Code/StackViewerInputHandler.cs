using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// 
        /// </summary>
        public class StackViewerInputHandler : MonoBehaviour
        {
            [SerializeField]
            private CellStack _stack;
            private StackDataViewer _stackViewer;

            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                _stackViewer = GetComponent<StackDataViewer>();
            }


            /// <summary>
            /// 
            /// </summary>
            private void Update()
            {
                HandleKeyPress();
            }


            /// <summary>
            /// 
            /// </summary>
            private void HandleKeyPress()
            {
                // Reset model
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _stack.ResetStackFromStackData();

                }

                // Update display mode
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {

                    if (_stackViewer.DisplayMode != CellDisplayMode.None)
                    {
                        _stackViewer.DisplayMode = CellDisplayMode.None;
                    }
                    else
                    {
                        _stackViewer.DisplayMode = CellDisplayMode.Alive;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.Alpha1))
                    _stackViewer.DisplayMode = CellDisplayMode.Age;
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    _stackViewer.DisplayMode = CellDisplayMode.LayerDensity;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    _stackViewer.DisplayMode = CellDisplayMode.MooreR1Density;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    _stackViewer.DisplayMode = CellDisplayMode.VNR1Density;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    _stackViewer.DisplayMode = CellDisplayMode.VNR2Density;
            }
        }
    }
}
