using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur;

namespace RC3
{
    namespace GameOfLifeGA
    {

        /// <summary>
        /// 
        /// </summary>
        [RequireComponent(typeof(ICARule2D))]
        [RequireComponent(typeof(StackAnalyser))]

        public class StackModelManager : MonoBehaviour
        {
            [SerializeField] private ModelInitializer _initializer;
            [SerializeField] private CellStack _stack;

            private CAModel2D _model;
            private StackAnalyser _analyser;

            //store current layer
            private int _currentLayer = -1;

            //track if the build has completed
            private bool _buildComplete = false;


            //mode for execution, gets potentially changed by InputHandler
            [SerializeField]
            private ModelExecutionMode _executionmode = ModelExecutionMode.Run;

            //keeps track of stepping in StepByStep execution mode
            private bool _hasstepped = false;

            //enables control of initialization from population manager
            private bool _initialized = false;


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
                set { _stack = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public CAModel2D Model
            {
                get { return _model; }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool BuildComplete
            {
                get { return _buildComplete; }
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
            private void Start()
            {
                if (_initialized == false)
                {
                    // create model
                    _model = new CAModel2D(GetComponent<ICARule2D>(), _stack.RowCount, _stack.ColumnCount);

                    // initialize model
                    _initializer.Initialize(_model.CurrentState);

                    // update layer / cells in the stack to the seed image
                    _currentLayer = 1;
                    UpdateStack();

                    //
                    _analyser = GetComponent<StackAnalyser>();
                    _initialized = true;

                }
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
                if (_currentLayer == _stack.LayerCount - 1 && _buildComplete == false)
                {
                    _buildComplete = true;
                    _analyser.UpdateAnalysis();
                    return;
                }

                if (_buildComplete == true)
                {
                    return;

                }



                //execute the model - updates the state if the model
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
                if (_initialized == false)
                {
                    Start();
                    _initialized = true;
                }
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
                _buildComplete = false;

                //_stack.SetSeed(_initializer.Texture);
            }

            /// <summary>
            /// 
            /// </summary>
            public void ResetModel(Texture2D texture)
            {
                if (_initialized == false)
                {
                    Start();
                    _initialized = true;
                }

                // reset cell states
                foreach (var layer in _stack.Layers)
                {
                    foreach (var cell in layer.Cells)
                        cell.State = 0;
                }

                if (_model == null)
                {
                    _model = new CAModel2D(GetComponent<ICARule2D>(), _stack.RowCount, _stack.ColumnCount);

                }

                // re-initialize model
                _initializer.Initialize(_model.CurrentState, texture);

                // reset layer
                _currentLayer = -1;

                _buildComplete = false;
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

        /*
        
        /// <summary>
        /// 
        /// </summary>
        [RequireComponent(typeof(ICARule2D))]
        public class StackModel : MonoBehaviour
        {
            // TODO fire OnModelReset event0.

            [SerializeField] private ModelInitializer _initializer;
            [SerializeField] private CellStack _stack;

            private CAModel2D _model;
            private StackAnalyser _analyser;

            private int _currentLayer = -1;
            private bool _buildComplete = false;

            private bool _pause = false;


            /// <summary>
            /// 
            /// </summary>
            public bool Pause
            {
                get { return _pause; }
                set { _pause = value; }
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
            /// <param name="stack"></param>
            public void SetStack(CellStack stack)
            {
                _stack = stack;
            }


            /// <summary>
            /// 
            /// </summary>
            public CAModel2D Model
            {
                get { return _model; }
            }

            /// <summary>
            /// 
            /// </summary>
            public ModelInitializer Initializer
            {
                get { return _initializer; }
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
            public bool BuildComplete
            {
                get { return _buildComplete; }
            }

            /// <summary>
            /// 
            /// </summary>
            private void Awake()
            {
                // create model
                _model = new CAModel2D(GetComponent<ICARule2D>(), _stack.RowCount, _stack.ColumnCount);

                _stack = Instantiate(_stack);

                // initialize model
                _initializer.Initialize(_model.CurrentState);
            }


            /// <summary>
            /// 
            /// </summary>
            private void Update()
            {
                if (_pause == false)
                {


                    // bail if stack is full
                    if (_currentLayer == _stack.LayerCount - 1 && _buildComplete == false)
                    {
                        _buildComplete = true;
                        return;
                    }

                    if (_buildComplete == true)
                    {
                        return;

                    }


                    // advance later
                    _currentLayer++;

                    // advance model
                    _model.Step();
                    //_model.StepParallel();

                    // update cells in the stack
                    UpdateStack();
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

                _buildComplete = false;

            }

            /// <summary>
            /// 
            /// </summary>
            public void ResetModel(Texture2D texture)
            {
                // reset cell states
                foreach (var layer in _stack.Layers)
                {
                    foreach (var cell in layer.Cells)
                        cell.State = 0;
                }

                // re-initialize model
                _initializer.Initialize(_model.CurrentState, texture);

                // reset layer
                _currentLayer = -1;

                _buildComplete = false;
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
                    {
                        currCells[i, j].State = currState[i, j];
                        _stack.CellStates[(_currentLayer, i, j)] = currState[i, j];
                    }
                }

                //Debug.Log("Test1= " + currState[10, 10]);
                //Debug.Log("Test2= " + _stack.CellStates[(_currentLayer, 10, 10)]);


                int maxage = 0;

                // update cell age
                if (_currentLayer > 0)
                {
                    Cell[,] prevCells = _stack.Layers[_currentLayer - 1].Cells;

                    for (int i = 0; i < nrows; i++)
                    {
                        for (int j = 0; j < ncols; j++)
                        {
                            currCells[i, j].Age = currState[i, j] > 0 ? prevCells[i, j].Age + 1 : 0;

                            if (currCells[i, j].Age > maxage)
                            {
                                maxage = currCells[i, j].Age;
                            }
                        }
                    }
                }

                //set layer max age
                _stack.Layers[_currentLayer].MaxAge = maxage;
            }
        }

        */
    }
}
