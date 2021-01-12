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
        public class StackAnalyser : MonoBehaviour
        {
            private StackModelManager _model;
            private float _densitySum;
            private float _ageSum;

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
                }

                //update stack min layer density
                if (layer.Density < _model.Stack.MinLayerDensity)
                {
                    _model.Stack.SetMinLayerDensity(layer.Density);
                }

                //update stack max age
                if (layer.MaxAge > _model.Stack.MaxAge)
                {
                    _model.Stack.SetMaxAge(layer.MaxAge);
                }
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


            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public void ResetAnalysis()
            {
                _densitySum = 0.0f;
                _ageSum = 0.0f;
                _currentLayer = -1;
            }

            /// <summary>
            /// 
            /// </summary>
            public void Fitness()
            {
                float fitness = 1;

                //get my density / number of voxels

                //check stability

                //get max deflection 

                //if max deflection is greater than 300mm, return 0;

                //max deflection is <300mm

                //





                //calculate separate objective fitness values
                float structuralFit = StructuralFitness();
                float massFit = MassFitness();

                //setup fitness function to combine and weight these factors
                fitness = (structuralFit + massFit) / 2;

                fitness = MeanStackDensity;

                //set stack fitness value
                _model.Stack.SetFitness(fitness);
            }

            /// <summary>
            /// 
            /// </summary>
            public float RemapFitness(float fitness)
            {
                float remappedFitness = 0;

                return remappedFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float PatternFitness()
            {
                float patternFitness = 1;
                //calculate assign pattern fitness value

                return patternFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float DensityFitness()
            {
                float densityFitness = 1;
                //calculate assign density fitness value

                return densityFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float MassFitness()
            {
                float massFitness = 1;
                //calculate overall mass - assign fitness value

                return massFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float StructuralFitness()
            {
                float structuralFitness = 1;
                //calculate structural forces and return fitness value
                return structuralFitness;
            }
        }


        /*
        /// <summary>
        /// 
        /// </summary>
        [RequireComponent(typeof(StackModel))]
        public class StackAnalyser : MonoBehaviour
        {
            private StackModel _model;
            private float _densitySum;
            private float _ageSum;

            private int _currentLayer; // index of the most recently analysed layer

            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                _model = GetComponent<StackModel>();
                ResetAnalysis();
            }


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
                }

                //update stack min layer density
                if (layer.Density < _model.Stack.MinLayerDensity)
                {
                    _model.Stack.SetMinLayerDensity(layer.Density);
                }

                //update stack max age
                if (layer.MaxAge > _model.Stack.MaxAge)
                {
                    _model.Stack.SetMaxAge(layer.MaxAge);
                }


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


            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private void ResetAnalysis()
            {
                _densitySum = 0.0f;
                _currentLayer = -1;
            }

            /// <summary>
            /// 
            /// </summary>
            public void Fitness()
            {
                float fitness = 1;
                //calculate separate objective fitness values
                float structuralFit = StructuralFitness();
                float massFit = MassFitness();

                //setup fitness function to combine and weight these factors
                fitness = (structuralFit + massFit) / 2;

                fitness = MeanStackDensity;

                //set stack fitness value
                _model.Stack.SetFitness(fitness);
            }

            /// <summary>
            /// 
            /// </summary>
            public float RemapFitness(float fitness)
            {
                float remappedFitness = 0;

                return remappedFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float PatternFitness()
            {
                float patternFitness = 1;
                //calculate assign pattern fitness value

                return patternFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float DensityFitness()
            {
                float densityFitness = 1;
                //calculate assign density fitness value

                return densityFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float MassFitness()
            {
                float massFitness = 1;
                //calculate overall mass - assign fitness value

                return massFitness;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private float StructuralFitness()
            {
                float structuralFitness = 1;
                //calculate structural forces and return fitness value
                return structuralFitness;
            }
        }
        
         */
    }


}
