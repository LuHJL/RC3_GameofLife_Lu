using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using SpatialSlur;

namespace RC3
{
    namespace RC3_GameOfLife2D
    {
        /// <summary>
        /// Manages the game of life model
        /// </summary>
        public class ModelManager2D : MonoBehaviour
        {
            //input different types of initializers to initialize the model (random, seed image, etc.)
            [SerializeField] private ModelInitializer _initializer;

            //instance of model display for displaying the model
            private ModelDisplay _display;

            //input a cell prefab to be instantiated
            [SerializeField] private Cell _cellPrefab;

            //input size of the grid
            [SerializeField] private int _countX = 10;
            [SerializeField] private int _countY = 10;

            //stores 2d array of cell instances
            private Cell[,] _cells;

            //stores the model
            private CAModel2D _model;

            //the rule used to update the model (requires a rule be attached, initialized in Awake() function) 
            private ICARule2D _rule;

            //counter variable stores how many times we have stepped
            //in the model
            private int _stepCount;

            //mode for execution, gets potentially changed by InputHandler
            [SerializeField]
            private ModelExecutionMode _executionmode = ModelExecutionMode.Run;

            //keeps track of stepping in StepByStep execution mode
            private bool _hasstepped = false;


            /// <summary>
            /// 
            /// </summary>
            public ModelExecutionMode ExecutionMode
            {
                get { return _executionmode; }
                set { _executionmode = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool HasStepped
            {
                get { return _hasstepped; }
                set { _hasstepped = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                // create cell array
                _cells = new Cell[_countY, _countX];

                // instantiate cell prefabs and store in array
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        //instantiate a new cell using the cell prefab
                        Cell cell = Instantiate(_cellPrefab, transform);

                        //move the cell's position to correct location based on the index
                        cell.transform.localPosition = new Vector3(x, 0, y);

                        //store the newly created cell in the cells 2d array
                        _cells[y, x] = cell;
                    }
                }


                // create model using the rule attached to this component
                _rule = GetComponent<ICARule2D>();
                _model = new CAModel2D(_rule, _countY, _countX);

                // initialize model
                _initializer.Initialize(_model.CurrentState);

                // create display
                _display = new ModelDisplay();
                _display.Initialize(_countX, _countY, _cells);
            }


            /// <summary>
            /// 
            /// </summary>
            private void Update()
            {
                //execute the model - updates the state if the model
                if (_executionmode == ModelExecutionMode.Run)
                {
                    _model.Execute();

                }

                if (_executionmode == ModelExecutionMode.StepByStep && _hasstepped==false)
                {
                    _model.Execute();
                    _hasstepped = true;
                }

                //increase the step counter
                _stepCount++;

                //get a copy of the model current state
                int[,] state = _model.CurrentState;

                // update cells based on current state of model
                // nested for loops steps through each location in
                // 2d array of the model's current state, sets the 
                // state of the corresponding cell accordingly
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        _cells[y, x].State = state[y, x];
                    }
                }

                //update the display
                _display.UpdateDisplay();
            }

            /// <summary>
            /// Resets the model
            /// </summary>
            public void ResetModel()
            {

                // initialize model
                _initializer.Initialize(_model.CurrentState);

                // create display
                _display = new ModelDisplay();
                _display.Initialize(_countX, _countY, _cells);
            }
        }
    }
}
