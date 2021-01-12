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
        /// Rule for Conway's game of life
        /// </summary>
        [RequireComponent(typeof(StackModelManager))]
        [RequireComponent(typeof(StackAnalyser))]
        public class BonnyRule : MonoBehaviour, ICARule2D
        {
            //access to the stack model + analyser
            private StackModelManager _modelManager;
            private StackAnalyser _analyser;

            //setup some possible instruction sets
            //Age
            private InstructionSet _instSetMO1 = new InstructionSet(2, 3, 3, 3);
            private InstructionSet _instSetMO2 = new InstructionSet(2, 4, 3, 4);
            private InstructionSet _instSetMO3 = new InstructionSet(2, 3, 4, 4);

            /*original backup
            private InstructionSet _instSetMO1 = new InstructionSet(2, 3, 3, 3);
            private InstructionSet _instSetMO2 = new InstructionSet(3, 4, 3, 4);
            private InstructionSet _instSetMO3 = new InstructionSet(2, 5, 2, 6);
            */

            /*
            //Density
            private InstructionSet _instSetMO4 = new InstructionSet(2, 5, 2, 6);
            private InstructionSet _instSetMO5 = new InstructionSet(2, 5, 2, 6);
            private InstructionSet _instSetMO6 = new InstructionSet(2, 5, 2, 6);

            //Level
            private InstructionSet _instSetMO7 = new InstructionSet(2, 5, 2, 6);
            private InstructionSet _instSetMO8 = new InstructionSet(2, 5, 2, 6);
            private InstructionSet _instSetMO9 = new InstructionSet(2, 5, 2, 6);
            */

            //Input for only age
            public int changeConAge01 = new int();
            public int changeConAge02 = new int();

            //Input for only density
            public float changeConDensity01 = new float();
            public float changeConDensity02 = new float();

            //Input for only level
            public int changeConLevel01 = new int();
            public int changeConLevel02 = new int();

            //Input for combination
            public int changeConAge03 = new int();
            public float changeConDensity03 = new float();
            public int changeConLevel03 = new int();

            /// <summary>
            /// 
            /// </summary>
            private void Start()
            {
                //access to the stack model + analyser as components of the same gameObject "this" script is attached to
                _modelManager = GetComponent<StackModelManager>();
                _analyser = GetComponent<StackAnalyser>();
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="i"></param>
            /// <param name="j"></param>
            /// <param name="current"></param>
            /// <returns></returns>
            public int NextState(Index2 index, int[,] current)
            {
                //get current state
                int state = current[index.I, index.J];

                //get local neighborhood data for this index using the current model
                int sumMO = GetNeighborSum(index, current, Neighborhoods.MooreR1);
                int sumVNPair = GetNeighborSum(index, current, Neighborhoods.VonNeumannPair1);

                //choose an instruction set
                InstructionSet instructionSet = _instSetMO1;

                // collect relevant analysis results
                CellLayer[] layers = _modelManager.Stack.Layers;
                int currentLayer = _modelManager.CurrentLayer;

                float prevLayerDensity;
                int prevCellAge;
                //int currentLevel;


                // get attributes of corresponding cell on the previous layer (if it exists)
                if (currentLayer > 0)
                {
                    var prevLayer = layers[currentLayer - 1];
                    prevLayerDensity = prevLayer.Density;
                    prevCellAge = prevLayer.Cells[index.I, index.J].Age;
                }
                else
                {
                    prevLayerDensity = 1.0f;
                    prevCellAge = 0;
                }


                //Set change condition: Only CellAge
                if (changeConAge01 > 0 && changeConAge02 > 0)
                {
                    if (prevCellAge <= changeConAge01)
                    {
                        instructionSet = _instSetMO1;
                    }

                    if (prevCellAge > changeConAge01 && prevCellAge <= changeConAge02)
                    {
                        instructionSet = _instSetMO2;
                    }

                    if (prevCellAge > changeConAge02)
                    {
                        instructionSet = _instSetMO3;
                    }
                }


                //Set change condition: Only Density
                if (changeConDensity01 > 0 && changeConDensity02 > 0)
                {
                    if (prevLayerDensity < changeConDensity01)
                    {
                        instructionSet = _instSetMO3;
                    }

                    if (prevLayerDensity >= changeConDensity01 && prevLayerDensity < changeConDensity02)
                    {
                        instructionSet = _instSetMO1;
                    }

                    if (prevLayerDensity > changeConDensity02)
                    {
                        instructionSet = _instSetMO2;
                    }
                }
                /*
               


                /*
                if(state==0 && sumVNPair == 2)
                {
                    return 1;
                }

                if (state == 1 && sumVNPair == 2)
                {
                    return 0;
                }
                */



                //Set change condition: Only Level
                if (changeConLevel01 > 0 && changeConLevel02 > 0)
                {
                    if (currentLayer <= changeConLevel01)
                    {
                        instructionSet = _instSetMO1;
                    }

                    if (currentLayer > changeConLevel01 && currentLayer < changeConLevel02)
                    {
                        instructionSet = _instSetMO2;
                    }

                    if (currentLayer >= changeConLevel02)
                    {
                        instructionSet = _instSetMO3;
                    }
                }

                //Set Change Condition: 01.Age, 02.Density 03.Level
                if (changeConAge03 > 0 && changeConDensity03 > 0 && changeConLevel03 > 0)
                {
                    if (prevCellAge <= changeConAge03)
                    {
                        instructionSet = _instSetMO1;
                    }

                    if (prevCellAge > changeConAge03)
                    {
                        instructionSet = _instSetMO2;
                    }

                    if (prevLayerDensity > changeConDensity03)
                    {
                        instructionSet = _instSetMO3;
                    }

                    if (currentLayer > changeConLevel03)
                    {
                        instructionSet = _instSetMO1;
                    }
                }


                int output = 0;

                //if current state is "alive"
                if (state == 1)
                {
                    if (sumMO < instructionSet.getInstruction(0))
                    {
                        output = 0;
                    }

                    if (sumMO >= instructionSet.getInstruction(0) && sumMO <= instructionSet.getInstruction(1))
                    {
                        output = 1;
                    }

                    if (sumMO > instructionSet.getInstruction(1))
                    {
                        output = 0;
                    }
                }

                //if current state is "dead"
                if (state == 0)
                {
                    if (sumMO >= instructionSet.getInstruction(2) && sumMO <= instructionSet.getInstruction(3))
                    {
                        output = 1;
                    }
                    else
                    {
                        output = 0;
                    }
                }

                return output;

            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="i0"></param>
            /// <param name="j0"></param>
            /// <returns></returns>
            private int GetNeighborSum(Index2 index, int[,] current, Index2[] neighborhood)
            {
                int nrows = current.GetLength(0);
                int ncols = current.GetLength(1);
                int sum = 0;

                foreach (Index2 offset in neighborhood)
                {
                    int i1 = Wrap(index.I + offset.I, nrows);
                    int j1 = Wrap(index.J + offset.J, ncols);

                    if (current[i1, j1] > 0)
                        sum++;
                }

                return sum;
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