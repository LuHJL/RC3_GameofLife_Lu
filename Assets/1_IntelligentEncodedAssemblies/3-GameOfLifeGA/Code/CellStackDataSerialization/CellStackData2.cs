using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SpatialSlur.Collections;

namespace RC3
{
    namespace GameOfLifeGA
    {

        [System.Serializable]
        /// <summary>
        /// 
        /// </summary>
        public struct CellStackData2
        {
            private int _columnCount;
            private int _rowCount;
            private int _layerCount;

            float[] _position;

            //Stack Genetic Data
            private int[] _dnaGenes;
            private float _fitness;

            //Stack Data
            private string _name;
            private float _meanStackDensity;
            private float _maxLayerDensity;
            private float _minLayerDensity;
            private float _maxAge;
            private float _avgAge;

            //Layer Data
            private float[] _layerDensities;
            private float[] _layerAvgAges;
            private float[] _layerMaxAges;


            //Cell Data
            private int[,,] _cellStates;
            private int[,,] _cellAges;

            /// <summary>
            /// 
            /// </summary>


            public float[] Position
            {
                get { return _position; }
                set { _position = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public float MeanStackDensity
            {
                get { return _meanStackDensity; }
                set { _meanStackDensity = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public float MaxLayerDensity
            {
                get { return _maxLayerDensity; }
                set { _maxLayerDensity = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public float MinLayerDensity
            {
                get { return _minLayerDensity; }
                set { _minLayerDensity = value; }
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
            public float AvgAge
            {
                get { return _avgAge; }
                set { _avgAge = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public int[] DNAGENES
            {
                get { return _dnaGenes; }
                set { _dnaGenes = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public float Fitness
            {
                get { return _fitness; }
                set { _fitness = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int RowCount
            {
                get { return _rowCount; }
                set { _rowCount = value; }

            }


            /// <summary>
            /// 
            /// </summary>
            public int ColumnCount
            {
                get { return _columnCount; }
                set { _columnCount = value; }

            }


            /// <summary>
            /// 
            /// </summary>
            public int LayerCount
            {
                get { return _layerCount; }
                set { _layerCount = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public float[] LayerAvgAges
            {
                get { return _layerAvgAges; }
                set { _layerAvgAges = value; }

            }

            /// <summary>
            /// 
            /// </summary>
            public float[] LayerDensities
            {
                get { return _layerDensities; }
                set { _layerDensities = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public float[] LayerMaxAges
            {
                get { return _layerMaxAges; }
                set { _layerMaxAges = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public int[,,] CellStates
            {
                get { return _cellStates; }
                set { _cellStates = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int[,,] CellAges
            {
                get { return _cellAges; }
                set { _cellAges = value; }
            }
        }

    }
}
