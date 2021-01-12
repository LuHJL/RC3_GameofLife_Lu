using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        public class CellLayer : MonoBehaviour
        {
            private Cell[,] _cells;

            // Additional custom per-layer attributes
            private float _density;
            private float _avgAge;
            private float _maxAge;

            // ...
            // ...
            // ...


            /// <summary>
            /// 
            /// </summary>
            public Cell[,] Cells
            {
                get { return _cells; }
            }


            /// <summary>
            /// 
            /// </summary>
            public float Density
            {
                get { return _density; }
                set { _density = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public float AvgAge
            {
                get { return _avgAge; }
                set { _avgAge = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public float MaxAge
            {
                get { return _maxAge; }
                set { _maxAge = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int RowCount
            {
                get { return _cells.GetLength(0); }
            }


            /// <summary>
            /// 
            /// </summary>
            public int ColumnCount
            {
                get { return _cells.GetLength(1); }
            }


            /// <summary>
            /// 
            /// </summary>
            public int CellCount
            {
                get { return _cells.Length; }
            }


            /// <summary>
            /// 
            /// </summary>
            public void Initialize(Cell cellPrefab, int rows, int columns)
            {
                // create cell array
                _cells = new Cell[rows, columns];

                // instantiate cell prefabs and store in array
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Cell cell = Instantiate(cellPrefab, transform);

                        cell.transform.localPosition = new Vector3(j, 0.0f, i);
                        _cells[i, j] = cell;
                    }
                }
            }
        }
    }
}
