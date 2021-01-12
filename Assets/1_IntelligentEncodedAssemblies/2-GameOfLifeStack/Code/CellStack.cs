using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SpatialSlur.Collections;

namespace RC3
{
    namespace GameOfLifeStack
    {
        /// <summary>
        /// Stores the stack of layers of cells
        /// </summary>
        public class CellStack : MonoBehaviour
        {
            [SerializeField] CellLayer _layerPrefab;
            [SerializeField] Cell _cellPrefab;

            [SerializeField] private int _columnCount = 10;
            [SerializeField] private int _rowCount = 10;
            [SerializeField] private int _layerCount = 10;

            private CellLayer[] _layers;

            private float _fitness = 1;
            private Texture2D _seed;

            private string _name = "";
            private float _meanStackDensity = 0;

            private float _maxLayerDensity = 0;
            private int _maxLayerDensityNumber = 0;

            private float _minLayerDensity = float.MaxValue;
            private int _minLayerDensityNumber = 0;


            private float _maxAge = 0;

            private float _avgAge = 0;

            private MaterialPropertyBlock _properties;

            public MaterialPropertyBlock MaterialProperties
            {
                get { return _properties; }
                set { _properties = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            private void Awake()
            {
                _properties = new MaterialPropertyBlock();
                InitializeCells();
            }


            /// <summary>
            /// 
            /// </summary>
            public void SetName(string name)
            {
                _name = name;
            }

            /// <summary>
            /// 
            /// </summary>
            public float MeanStackDensity
            {
                get { return _meanStackDensity; }
            }

            public void SetMeanStackDensity(float meanStackDensity)
            {
                _meanStackDensity = meanStackDensity;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public float MaxLayerDensity
            {
                get { return _maxLayerDensity; }
            }

            /// <summary>
            /// 
            /// </summary>
            public void SetMaxLayerDensity(float maxLayerDensity)
            {
                _maxLayerDensity = maxLayerDensity;
            }

            // get and return the layer number of MaxLayerDensity
            public int MaxLayerDensityNumber
            {
                get { return _maxLayerDensityNumber; }
            }

            public void SetMaxLayerDensityNumebr(int maxLayerDensityNumber)
            {
                _maxLayerDensityNumber = maxLayerDensityNumber;
            }


            
            /// <summary>
            /// 
            /// </summary>
            public float MinLayerDensity
            {
                get { return _minLayerDensity; }
            }

            // get and return the layer number of MaxLayerDensity
            public int MinLayerDensityNumber
            {
                get { return _minLayerDensityNumber; }
            }

            public void SetMinLayerDensityNumebr(int minLayerDensityNumber)
            {
                _minLayerDensityNumber = minLayerDensityNumber;
            }

            /// <summary>
            /// 
            /// </summary>
            public void SetMinLayerDensity(float minLayerDensity)
            {
                _minLayerDensity = minLayerDensity;
            }

            /// <summary>
            /// 
            /// </summary>
            public float MaxAge
            {
                get { return _maxAge; }
            }

            public void SetMaxAge(float maxAge)
            {
                _maxAge = maxAge;
            }

           
            public float AvgAge
            {
                get { return _avgAge; }
            }

            /// <summary>
            /// 
            /// </summary>
            public void SetAvgAge(float avgAge)
            {
                _avgAge = avgAge;
            }




            /// <summary>
            /// 
            /// </summary>
            public CellLayer[] Layers
            {
                get { return _layers; }
            }


            /// <summary>
            /// 
            /// </summary>
            public int RowCount
            {
                get { return _rowCount; }
            }


            /// <summary>
            /// 
            /// </summary>
            public int ColumnCount
            {
                get { return _columnCount; }
            }


            /// <summary>
            /// 
            /// </summary>
            public int LayerCount
            {
                get { return _layerCount; }
            }


            /// <summary>
            /// 
            /// </summary>
            private void InitializeCells()
            {
                _layers = new CellLayer[_layerCount];

                // instantiate layers
                for (int i = 0; i < _layerCount; i++)
                {
                    CellLayer copy = Instantiate(_layerPrefab, transform);
                    copy.transform.localPosition = new Vector3(0.0f, i, 0.0f);

                    // create cell layer
                    copy.Initialize(_cellPrefab, _rowCount, _columnCount);
                    _layers[i] = copy;
                }

                // center at the world origin
                //transform.localPosition = new Vector3(_columnCount, 0, _rowCount) * -0.5f;

                //place lower corner on origin
                transform.localPosition = new Vector3(0, 0, 0);

            }
        }
    }
}
