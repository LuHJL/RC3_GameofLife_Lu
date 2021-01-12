using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

using SpatialSlur;

namespace RC3
{
    namespace GameOfLifeStack
    {
        /// <summary>
        /// 
        /// </summary>
        [RequireComponent(typeof(StackModelManager))]
        public class StackAnalyser : MonoBehaviour
        {
            private StackModelManager _model;
            private float _densitySum;
            private float _ageSum;

            private float[] _densityLevels = new float[3];

            private int _currentLayer; // index of the most recently analysed layer

            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                _model = GetComponent<StackModelManager>();
                ResetAnalysis();
            }


            /*
            /// <summary>
            /// 
            /// </summary>
            private void LateUpdate()
            {
                // reset analysis if necessary
                if (_currentLayer > _model.CurrentLayer)
                    ResetAnalysis();

                // update analysis if model has been updated
                if (_currentLayer < _model.CurrentLayer)
                    UpdateAnalysis();
            }
            */

            /// <summary>
            /// Returns the current mean density of the stack
            /// </summary>
            public float MeanStackDensity
            {
                get { return _densitySum / (_model.CurrentLayer + 1); }
            }

            /// <summary>
            /// Returns the current mean age of the stack
            /// </summary>
            public float MeanStackAge
            {
                get { return _ageSum / (_model.CurrentLayer + 1); }
            }

            // level density by lu
             public float[] LevelMeanDensity
             {
                 get { return _densityLevels; }
             }


            /// <summary>
            /// 
            /// </summary>
            public void UpdateAnalysis()
            {
                int currentLayer = _model.CurrentLayer;
                CellLayer layer = _model.Stack.Layers[currentLayer];

                //update layer current density
                var density = CalculateDensity(layer);
                layer.Density = density;
                
                _densitySum += density; // add to running sum

                //update stack mean density
                _model.Stack.SetMeanStackDensity(MeanStackDensity); //update the stack


                //update layer avg age
                var avgage = CalculateAverageAge(layer);
                layer.AvgAge = avgage;
                _ageSum += avgage; // add to running sum

                //update stack avg age
                _model.Stack.SetAvgAge(MeanStackAge);//update the stack

                //update stack max layer density
                if (layer.Density > _model.Stack.MaxLayerDensity)
                {
                    _model.Stack.SetMaxLayerDensity(layer.Density);
                    _model.Stack.SetMaxLayerDensityNumebr(currentLayer);
                }

                //update stack min layer density
                if (layer.Density < _model.Stack.MinLayerDensity)
                {
                    _model.Stack.SetMinLayerDensity(layer.Density);
                    _model.Stack.SetMinLayerDensityNumebr(currentLayer);
                }

                //update max age in current layer
                var maxage = CalculateMaxAge(layer);
                layer.MaxAge = maxage;
                _model.Stack.SetMaxAge(maxage);//update the stack

              
                //update level density
                CalculateEachLevelDensity(_model );

                _currentLayer = currentLayer;
            }


            /// <summary>
            /// Calculate the density of alive cells for the given layer
            /// </summary>
            /// <returns></returns>
            private float CalculateDensity(CellLayer layer)
            {
                var cells = layer.Cells;
                int aliveCount = 0;

                foreach (var cell in cells)
                    aliveCount += cell.State;

                return (float)aliveCount / cells.Length;
            }

            /// <summary>
            /// Calculate the average age of live cells for the given layer
            /// </summary>
            /// <returns></returns>
            private float CalculateAverageAge(CellLayer layer)
            {
                var cells = layer.Cells;
                int aliveCount = 0;
                int ageCount = 0;

                foreach (var cell in cells)
                {
                    aliveCount += cell.State;
                    ageCount += cell.Age;
                }


                return (float)((float)ageCount) / ((float)aliveCount);
            }
            
            // Calculate MaxAge (written by Lu)
            private int CalculateMaxAge(CellLayer layer)
            {
                var cells = layer.Cells;
                int maxAge = 0;

                foreach (var cell in cells)
                {
                    //// skip dead cells
                    //if (cell.State == 0)
                    //    continue;

                    if (cell.Age > maxAge)
                    {
                        maxAge = cell.Age;
                    }
                }

                return maxAge;
            }

            //calculate max age in the stack (by lu)
           


            // Calculate mean density in each level (written by Lu)
            private void CalculateEachLevelDensity(StackModelManager model)
            {

                //get current layer
                int currentLayer = _model.CurrentLayer;
                CellLayer layer = _model.Stack.Layers[currentLayer];

                int aliveCount = 0;

                // get cells in layers
                var cells = layer.Cells;


                //outcome
                float _densityLevel1;
                float _densityLevel2;
                float _densityLevel3;

                

                if (currentLayer <= 30)
                {
                    foreach (var cell in cells)
                    {aliveCount += cell.State; 
                        _densityLevel1 = (float) aliveCount / cells.Length;
                        _densityLevels[0] = _densityLevel1;
                    }

                }

                if (currentLayer> 30 && currentLayer < 60)

                    foreach (var cell in cells)
                    { aliveCount += cell.State;  
                        _densityLevel2 = (float)aliveCount / cells.Length;
                        _densityLevels[1] = _densityLevel2;
                    }

                if (currentLayer >= 60)

                    foreach (var cell in cells)
                    {
                        aliveCount += cell.State; 
                        _densityLevel3 = (float)aliveCount / cells.Length;
                        _densityLevels[2] = _densityLevel3;
                    }
                ;
            }


           
            
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private void ResetAnalysis()
            {
                _densitySum = 0.0f;
                _currentLayer = -1;
            }
        }
    }
}
