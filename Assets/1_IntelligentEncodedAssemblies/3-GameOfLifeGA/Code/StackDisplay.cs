using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SpatialSlur;

namespace RC3
{
    namespace GameOfLifeGA
    {
        /// <summary>
        /// 
        /// </summary>
        [RequireComponent(typeof(StackModelManager))]
        [RequireComponent(typeof(StackAnalyser))]
        public partial class StackDisplay : MonoBehaviour
        {
            // TODO process OnModelReset event
            // TODO reference "AnalysisResults" scriptable object rather than "StackAnalyser"

            [SerializeField] private CellDisplayMode _displayMode = CellDisplayMode.Age;
            [SerializeField] private Material _baseMaterial;

            [Space(12)]
            [SerializeField] private Material _ageMaterial;
            [SerializeField] private int _ageDisplayMin = 0;
            [SerializeField] private int _ageDisplayMax = 10;
            [Space(12)]
            [SerializeField] private Material _densityMaterial;
            [SerializeField] private float _densityDisplayMin = 0.0f;
            [SerializeField] private float _densityDisplayMax = 1.0f;

            private StackModelManager _model;
            private StackAnalyser _analyser;
            private MaterialPropertyBlock _properties;
            private int _currentLayer; // index of the most recently updated layer


            /// <summary>
            /// 
            /// </summary>
            public CellDisplayMode DisplayMode
            {
                get { return _displayMode; }
                set
                {
                    if (_displayMode != value)
                        _currentLayer = -1;

                    _displayMode = value;
                }
            }


            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                _model = GetComponent<StackModelManager>();
                _analyser = GetComponent<StackAnalyser>();
                _properties = new MaterialPropertyBlock();
                ResetDisplay();
            }


            /// <summary>
            /// 
            /// </summary>
            private void LateUpdate()
            {
                // reset display if necessary
                if (_currentLayer > _model.CurrentLayer)
                    ResetDisplay();

                // update analysis if model has been updated
                if (_currentLayer < _model.CurrentLayer)
                    UpdateDisplay();
            }


            /// <summary>
            /// 
            /// </summary>
            private void UpdateDisplay()
            {
                switch (_displayMode)
                {
                    case CellDisplayMode.None:
                        DisplayNone();
                        break;

                    case CellDisplayMode.Alive:
                        DisplayAlive();
                        break;

                    case CellDisplayMode.Age:
                        DisplayAge();
                        break;
                    case CellDisplayMode.LayerDensity:
                        DisplayLayerDensity();
                        break;
                    case CellDisplayMode.MooreR1Density:
                        DisplayMooresR1Density();
                        break;

                    case CellDisplayMode.VNR1Density:
                        DisplayVNR1Density();
                        break;

                    case CellDisplayMode.VNR2Density:
                        DisplayVNR2Density();
                        break;

                    case CellDisplayMode.FunnyDisplay:
                        DisplayFunnyDisplay();
                        break;


                    case CellDisplayMode.OldCells:
                        DisplayOldCells();
                        break;
                }
            }


            /// <summary>
            /// 
            /// </summary>
            private void ResetDisplay()
            {
                _currentLayer = -1;
            }

            /// <summary>
            /// 
            /// </summary>
            private void DisplayAlive()
            {
                CellLayer[] layers = _model.Stack.Layers;
                int layer1 = _model.CurrentLayer;

                for (int i = 0; i <= layer1; i++)
                {
                    foreach (var cell in layers[i].Cells)
                    {
                        // skip dead cells
                        if (cell.State == 0)
                            continue;

                        // update cell material
                        MeshRenderer renderer = cell.Renderer;
                        renderer.enabled = true;
                        renderer.sharedMaterial = _baseMaterial;
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private void DisplayNone()
            {
                CellLayer[] layers = _model.Stack.Layers;
                int layer1 = _model.CurrentLayer;

                for (int i = 0; i <= layer1; i++)
                {
                    foreach (var cell in layers[i].Cells)
                    {
                        // update cell material
                        MeshRenderer renderer = cell.Renderer;
                        renderer.enabled = false;
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private void DisplayAge()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                for (int i = layer0; i <= layer1; i++)
                {
                    foreach (var cell in layers[i].Cells)
                    {
                        // skip dead cells
                        if (cell.State == 0)
                            continue;

                        // update cell material
                        MeshRenderer renderer = cell.Renderer;
                        renderer.enabled = true;
                        renderer.sharedMaterial = _ageMaterial;

                        // set material properties
                        {
                            renderer.GetPropertyBlock(_properties);

                            // normalize age
                            float value = SlurMath.Normalize(cell.Age, _ageDisplayMin, _ageDisplayMax);
                            _properties.SetFloat(propName, value);

                            renderer.SetPropertyBlock(_properties);
                        }
                    }
                }

                _currentLayer = layer1;
            }

            private void DisplayOldCells()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                for (int i = layer0; i <= layer1; i++)
                {
                    foreach (var cell in layers[i].Cells)
                    {
                        // skip dead cells
                        if (cell.State == 0)
                            continue;

                        // update cell material
                        MeshRenderer renderer = cell.Renderer;
                        if (cell.Age < 10)
                        {
                            renderer.enabled = false;

                        }

                        if (cell.Age >= 10)
                        {
                            renderer.enabled = true;

                        }
                        renderer.sharedMaterial = _ageMaterial;

                        // set material properties
                        {
                            renderer.GetPropertyBlock(_properties);

                            // normalize age
                            float value = SlurMath.Normalize(cell.Age, _ageDisplayMin, _ageDisplayMax);
                            _properties.SetFloat(propName, value);

                            renderer.SetPropertyBlock(_properties);
                        }
                    }
                }

                _currentLayer = layer1;
            }


            /// <summary>
            /// 
            /// </summary>
            private void DisplayLayerDensity()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                for (int i = layer0; i <= layer1; i++)
                {
                    CellLayer layer = layers[i];
                    float value = SlurMath.Normalize(layer.Density, _densityDisplayMin, _densityDisplayMax);

                    foreach (var cell in layer.Cells)
                    {
                        // skip dead cells
                        if (cell.State == 0)
                            continue;

                        // update cell material
                        Renderer renderer = cell.Renderer;
                        renderer.enabled = true;
                        renderer.sharedMaterial = _densityMaterial;

                        // set material properties
                        renderer.GetPropertyBlock(_properties);
                        _properties.SetFloat(propName, value);
                        renderer.SetPropertyBlock(_properties);
                    }
                }

                _currentLayer = layer1;
            }


            /// <summary>
            /// 
            /// </summary>
            private void DisplayMooresR1Density()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                //apply material props to each obj renderer
                for (int k = layer0; k <= layer1; k++)
                {
                    Cell[,] cells = layers[k].Cells;
                    int nrows = cells.GetLength(0);
                    int ncols = cells.GetLength(1);

                    for (int i = 0; i < nrows; i++)
                    {
                        for (int j = 0; j < ncols; j++)
                        {
                            Cell cell = cells[i, j];

                            // skip dead cells
                            if (cell.State == 0)
                                continue;

                            // update cell material
                            Renderer renderer = cell.Renderer;
                            renderer.enabled = true;
                            renderer.sharedMaterial = _densityMaterial;

                            // set material properties
                            renderer.GetPropertyBlock(_properties);

                            // normalize density
                            float density = GetNeighborDensity(cells, new Index2(i, j), Neighborhoods.MooreR1);
                            float value = SlurMath.Normalize(density, _densityDisplayMin, _densityDisplayMax);

                            _properties.SetFloat(propName, value);
                            cell.Renderer.SetPropertyBlock(_properties);
                        }
                    }
                }

                _currentLayer = layer1;
            }

            /// <summary>
            /// 
            /// </summary>
            private void DisplayVNR1Density()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                //apply material props to each obj renderer
                for (int k = layer0; k <= layer1; k++)
                {
                    Cell[,] cells = layers[k].Cells;
                    int nrows = cells.GetLength(0);
                    int ncols = cells.GetLength(1);

                    for (int i = 0; i < nrows; i++)
                    {
                        for (int j = 0; j < ncols; j++)
                        {
                            Cell cell = cells[i, j];

                            // skip dead cells
                            if (cell.State == 0)
                                continue;

                            // update cell material
                            Renderer renderer = cell.Renderer;
                            renderer.enabled = true;
                            renderer.sharedMaterial = _densityMaterial;

                            // set material properties
                            renderer.GetPropertyBlock(_properties);

                            // normalize density
                            float density = GetNeighborDensity(cells, new Index2(i, j), Neighborhoods.VonNeumannR1);
                            float value = SlurMath.Normalize(density, _densityDisplayMin, _densityDisplayMax);

                            _properties.SetFloat(propName, value);
                            cell.Renderer.SetPropertyBlock(_properties);
                        }
                    }
                }

                _currentLayer = layer1;
            }

            /// <summary>
            /// 
            /// </summary>
            private void DisplayVNR2Density()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                //apply material props to each obj renderer
                for (int k = layer0; k <= layer1; k++)
                {
                    Cell[,] cells = layers[k].Cells;
                    int nrows = cells.GetLength(0);
                    int ncols = cells.GetLength(1);

                    for (int i = 0; i < nrows; i++)
                    {
                        for (int j = 0; j < ncols; j++)
                        {
                            Cell cell = cells[i, j];

                            // skip dead cells
                            if (cell.State == 0)
                                continue;

                            // update cell material
                            Renderer renderer = cell.Renderer;
                            renderer.enabled = true;
                            renderer.sharedMaterial = _densityMaterial;

                            // set material properties
                            renderer.GetPropertyBlock(_properties);

                            // normalize density
                            float density = GetNeighborDensity(cells, new Index2(i, j), Neighborhoods.VonNeumannR2);
                            float value = SlurMath.Normalize(density, _densityDisplayMin, _densityDisplayMax);

                            _properties.SetFloat(propName, value);
                            cell.Renderer.SetPropertyBlock(_properties);
                        }
                    }
                }

                _currentLayer = layer1;
            }

            private void DisplayFunnyDisplay()
            {
                const string propName = "_Value";

                CellLayer[] layers = _model.Stack.Layers;
                int layer0 = _currentLayer + 1;
                int layer1 = _model.CurrentLayer;

                //apply material props to each obj renderer
                for (int k = layer0; k <= layer1; k++)
                {
                    Cell[,] cells = layers[k].Cells;
                    int nrows = cells.GetLength(0);
                    int ncols = cells.GetLength(1);

                    for (int i = 0; i < nrows; i++)
                    {
                        for (int j = 0; j < ncols; j++)
                        {
                            Cell cell = cells[i, j];

                            // skip dead cells
                            if (cell.State == 0)
                                continue;

                            // update cell material
                            Renderer renderer = cell.Renderer;
                            renderer.enabled = true;
                            renderer.sharedMaterial = _densityMaterial;

                            // set material properties
                            renderer.GetPropertyBlock(_properties);

                            // normalize density
                            float density = GetNeighborDensity(cells, new Index2(i, j), Neighborhoods.VonNeumannPair1);
                            float value = SlurMath.Normalize(density, _densityDisplayMin, _densityDisplayMax);

                            _properties.SetFloat(propName, value);
                            cell.Renderer.SetPropertyBlock(_properties);
                        }
                    }
                }

                _currentLayer = layer1;
            }


            /// <summary>
            /// 
            /// </summary>
            private float GetNeighborDensity(Cell[,] cells, Index2 index, Index2[] neighborhood)
            {
                int nrows = cells.GetLength(0);
                int ncols = cells.GetLength(1);
                int sum = 0;

                foreach (Index2 offset in neighborhood)
                {
                    int i1 = Wrap(index.I + offset.I, nrows);
                    int j1 = Wrap(index.J + offset.J, ncols);

                    if (cells[i1, j1].State > 0)
                        sum++;
                }

                return (float)sum / neighborhood.Length;
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
