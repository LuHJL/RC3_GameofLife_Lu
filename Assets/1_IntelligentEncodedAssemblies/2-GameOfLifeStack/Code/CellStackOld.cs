using System;
using UnityEngine;

using SpatialSlur.Collections;

namespace RC3
{
    namespace GameOfLifeStack
    {
        /// <summary>
        /// Stores the stack of layers of cells
        /// </summary>
        public class CellStackOld : MonoBehaviour
        {
            //prefabs for instantiation
            [SerializeField] CellLayer _layerPrefab;
            [SerializeField] Cell _cellPrefab;

            //grid size
            [SerializeField] private int _columnCount = 10;
            [SerializeField] private int _rowCount = 10;
            [SerializeField] private int _layerCount = 10;

            //array of the cell layers
            private CellLayer[] _layers;


            /// <summary>
            /// 
            /// </summary>
            private void Awake()
            {
                //instantiates the cells / cell layers
                InitializeCells();
            }


            /// <summary>
            /// Public property access to the layers array
            /// </summary>
            public CellLayer[] Layers
            {
                get { return _layers; }
            }


            /// <summary>
            /// Public property returns row count
            /// </summary>
            public int RowCount
            {
                get { return _rowCount; }
            }


            /// <summary>
            /// Public property returns column count
            /// </summary>
            public int ColumnCount
            {
                get { return _columnCount; }
            }


            /// <summary>
            /// Public property returns the layer count
            /// </summary>
            public int LayerCount
            {
                get { return _layerCount; }
            }


            /// <summary>
            /// Instantiates the cells / cell layers
            /// </summary>
            private void InitializeCells()
            {
                //initiate array with the number of layers 
                _layers = new CellLayer[_layerCount];

                // instantiate layers
                for (int i = 0; i < _layerCount; i++)
                {
                    //instantiate each layer and move to its correct location in the stack
                    CellLayer copy = Instantiate(_layerPrefab, transform);
                    copy.transform.localPosition = new Vector3(0.0f, i, 0.0f);

                    // create cell layer by running its initialize function
                    copy.Initialize(_cellPrefab, _rowCount, _columnCount);

                    //put the instantiated layer copy into the correct location in the array
                    _layers[i] = copy;
                }

                // place all layers at the center at the world origin
                transform.localPosition = new Vector3(_columnCount, _layerCount, _rowCount) * -0.5f;
            }
        }
    }
}
