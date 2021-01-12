using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using SpatialSlur;
using UnityEngine.Events;
using XCharts;

namespace RC3
{
    namespace GameOfLifeStack
    {
        /// <summary>
        /// Rule for Conway's game of life
        /// </summary>
        [RequireComponent(typeof(StackModelManager))]
        [RequireComponent(typeof(StackAnalyser))]
        public class LuRule3 : MonoBehaviour, ICARule2D
        {
            //access to the stack model + analyser
            private StackModelManager _modelManager;
            private StackAnalyser _analyser;

            //setup some possible instruction sets
            private InstructionSet _instSetMO1 = new InstructionSet(1, 3, 4, 5);
            private InstructionSet _instSetMO2 = new InstructionSet(2, 3, 2, 2);
            private InstructionSet _instSetMO3 = new InstructionSet(1, 2, 3, 3);

            // analytic data
            public Text StackMeanDensity;
            public Text StackMeanAge;

            public Text LayerMaxDensity;
            public Text LayerMinDensity;

            public Text LayerMaxAge;

            public Text LevelDensity1;
            public Text LevelDensity2;
            public Text LevelDensity3;



            public Text Ins1;
            public Text Ins2;
            public Text Ins3;


            //chart

            public LineChart lineChart;
            private int layercount;

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




                /*
                if (currentlayerdensity < .17)
                {
                    instructionSet = _instSetMO3;
                }

                if (currentlayerdensity >= .17 && currentlayerdensity<.2)
                {
                    instructionSet = _instSetMO1;
                }

                if (currentlayerdensity >.2)
                {
                    instructionSet = _instSetMO2;
                }
                */

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


                // get the conditions where instructions change 
                int currentlevel = currentLayer;


                if (currentlevel <= 30)
                {
                    instructionSet = _instSetMO1;
                }

                if (currentlevel > 30 && currentlevel < 60)
                {
                    instructionSet = _instSetMO2;
                }

                if (currentlevel >= 60)
                {
                    instructionSet = _instSetMO3;
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



            private void Update()
            {
                ShowAnalyticData();
                layercount++;

                if (layercount <= 89)
                {
                    AddOneData();
                }

            }

            public void ShowAnalyticData()
            {
                //stack
                StackMeanDensity.text = _analyser.MeanStackDensity.ToString();
                StackMeanAge.text = _analyser.MeanStackAge.ToString();

                //layer
                LayerMaxDensity.text = "Layer " + (_modelManager.Stack.MaxLayerDensityNumber + 1).ToString() + "\n \n" +
                                       _modelManager.Stack.MaxLayerDensity.ToString();
                LayerMinDensity.text = "Layer " + (_modelManager.Stack.MinLayerDensityNumber + 1).ToString() + "\n \n" +
                                       _modelManager.Stack.MinLayerDensity.ToString();

                LayerMaxAge.text = "Layer " + (_modelManager.CurrentLayer + 1).ToString() + "\n \n" +
                                   _modelManager.Stack.MaxAge.ToString();

                //level
                LevelDensity1.text = _analyser.LevelMeanDensity[0].ToString();
                LevelDensity2.text = _analyser.LevelMeanDensity[1].ToString();
                LevelDensity3.text = _analyser.LevelMeanDensity[2].ToString();


                //rules
                Ins1.text = "[" + _instSetMO1.Instructions[0].ToString() + "," +
                            _instSetMO1.Instructions[1].ToString() + "," + _instSetMO1.Instructions[2].ToString() +
                            "," + _instSetMO1.Instructions[3].ToString() + "]";
                Ins2.text = "[" + _instSetMO2.Instructions[0].ToString() + "," +
                            _instSetMO2.Instructions[1].ToString() + "," + _instSetMO2.Instructions[2].ToString() +
                            "," + _instSetMO2.Instructions[3].ToString() + "]";
                Ins3.text = "[" + _instSetMO3.Instructions[0].ToString() + "," +
                            _instSetMO3.Instructions[1].ToString() + "," + _instSetMO3.Instructions[2].ToString() +
                            "," + _instSetMO3.Instructions[3].ToString() + "]";
            }

            //for chart
            public void AddOneData()
            {
                CellLayer[] layers = _modelManager.Stack.Layers;
                int currentLayer = _modelManager.CurrentLayer;
                
                var yvalue = layers[currentLayer].Density;

                if (currentLayer <= 89)
                {
                    lineChart.AddData(0, yvalue);
                    lineChart.AddXAxisData((currentLayer+1).ToString());
                }

            }
        }
    }
}