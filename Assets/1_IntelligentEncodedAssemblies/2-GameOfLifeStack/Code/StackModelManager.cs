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
        /// 
        /// </summary>
        [RequireComponent(typeof(ICARule2D))]
        [RequireComponent(typeof(StackAnalyser))]

        public class StackModelManager : MonoBehaviour
        {
            //the model initializer
            [SerializeField] private ModelInitializer _initializer;
            //input a prefab stack
            [SerializeField] private CellStack _stack;

            //the model
            private CAModel2D _model;

            //the stack analyzer
            private StackAnalyser _analyser;

            //store current layer
            private int _currentLayer = -1;

            //the rule used to update the model (requires a rule be attached, initialized in Awake() function) 
            private ICARule2D _rule;

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
            public CellStack Stack
            {
                get { return _stack; }
            }


            /// <summary>
            /// 
            /// </summary>
            public CAModel2D Model
            {
                get { return _model; }
            }


            /// <summary>
            /// Returns the index of the most recently processed layer
            /// </summary>
            public int CurrentLayer
            {
                get { return _currentLayer; }
            }


            /// <summary>
            /// 
            /// </summary>
            private void Awake()
            {
                // create model using the rule attached to this component
                _rule = GetComponent<ICARule2D>();
                _model = new CAModel2D(_rule, _stack.RowCount, _stack.ColumnCount);

                // initialize model
                _initializer.Initialize(_model.CurrentState);

                // update layer / cells in the stack to the seed image
                _currentLayer = 1;
                UpdateStack();

                //
                _analyser = GetComponent<StackAnalyser>();

            }


            /// <summary>
            /// 
            /// </summary>
            private void Update()
            {
                //do initial analysis including "seed image" cells
                if (_currentLayer == 1)
                {
                    _analyser.UpdateAnalysis();
                }

                // bail if stack is full
                if (_currentLayer == _stack.LayerCount - 1)
                    return;



                //execute the model - updates the state of the model
                if (_executionmode == ModelExecutionMode.Run)
                {
                    // advance layer
                    _currentLayer++;
                    _model.Execute();

                    // update cells in the stack
                    UpdateStack();
                    _analyser.UpdateAnalysis();
                }

                if (_executionmode == ModelExecutionMode.StepByStep && _hasstepped == false)
                {
                    // advance layer
                    _currentLayer++;

                    _model.Execute();

                    // update cells in the stack
                    UpdateStack();
                    _analyser.UpdateAnalysis();
                    _hasstepped = true;
                }
            }


            /// <summary>
            /// 
            /// </summary>
            public void ResetModel()
            {
                // reset cell states
                foreach (var layer in _stack.Layers)
                {
                    foreach (var cell in layer.Cells)
                        cell.State = 0;
                }

                // re-initialize model
                _initializer.Initialize(_model.CurrentState);

                // reset layer
                _currentLayer = -1;
            }


            /// <summary>
            /// 
            /// </summary>
            public void UpdateStack()
            {
                int[,] currState = _model.CurrentState;
                Cell[,] currCells = _stack.Layers[_currentLayer].Cells;

                int nrows = _stack.RowCount;
                int ncols = _stack.ColumnCount;

                // set cell state
                for (int i = 0; i < nrows; i++)
                {
                    for (int j = 0; j < ncols; j++)
                        currCells[i, j].State = currState[i, j];
                }

                // update cell age
                if (_currentLayer > 0)
                {
                    Cell[,] prevCells = _stack.Layers[_currentLayer - 1].Cells;

                    for (int i = 0; i < nrows; i++)
                    {
                        for (int j = 0; j < ncols; j++)
                            currCells[i, j].Age = currState[i, j] > 0 ? prevCells[i, j].Age + 1 : 0;
                    }
                }
            }
        }
    }
}
