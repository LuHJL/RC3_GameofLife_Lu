using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace RC3_GameOfLife2D
    {

        /// <summary>
        /// 
        /// </summary>
        public class Cell : MonoBehaviour
        {
            private MeshRenderer _renderer;

            // Additional custom per-cell attributes
            private int _state = 0;
            private int _age = 0;

            // analytic data
            private int alive = 0;

            /// <summary>
            /// 
            /// </summary>
            private void Awake()
            {
                _renderer = GetComponent<MeshRenderer>();
                _state = 0; // set dead by default
            }


            /// <summary>
            /// 
            /// </summary>
            public int State
            {
                get { return _state; }
                set
                {
                    _state = value;
                    //_renderer.enabled = (value == 1);
                }
            }


            /// <summary>
            /// 
            /// </summary>
            public int Age
            {
                get { return _age; }
                set { _age = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public MeshRenderer Renderer
            {
                get { return _renderer; }
            }
        }
    }
}
