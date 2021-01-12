using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using SpatialSlur;


namespace RC3
{
    namespace GameOfLifeGA
    {

        public class PopulationManager : MonoBehaviour
        {
            [SerializeField]
            private int _genSize = 3;

            [SerializeField]
            private bool _SeedFromGenes = true;

            private int _curCount = 0;
            int generations = 0;
            private List<IDNAI> _matingPool = new List<IDNAI>();
            CellStackData[] _currentGenerationData;
            private CellStack _currentStack;
            private bool _fitnessComplete = false;

            [SerializeField] private StackModelManager _model;
            [SerializeField] private StackAnalyser _analyser;
            [SerializeField] private CellStack _stackPrefab;
            [SerializeField] private SharedTextures _seeds;
            [SerializeField] private StackPopulation _population;

            private string filePath;


            bool _pause = false;
            bool _hideCurGen = false;

            /// <summary>
            /// 
            /// </summary>
            private void Awake()
            {
                _currentStack = _model.Stack;
                _currentGenerationData = new CellStackData[_genSize];
                _population.Reset();
                InitializeMatingPool();

                string filepath = Application.dataPath + "/00-DataOutput";
                string dateAndTimeVar = System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                filepath += "/" + dateAndTimeVar;
                filePath = filepath;
                System.IO.Directory.CreateDirectory(filePath);

                //ONLY IF USING GENES FOR PARTIAL IMAGE SEEDS - synthesize 4 images from child genes - don't use this if you dont have genes for image textures 
                if (_SeedFromGenes == true)
                {
                    Texture2D texture1 = _seeds[_currentStack.DNA.GetGene(0)];
                    Texture2D texture2 = _seeds[_currentStack.DNA.GetGene(1)];
                    Texture2D texture3 = _seeds[_currentStack.DNA.GetGene(2)];
                    Texture2D texture4 = _seeds[_currentStack.DNA.GetGene(3)];
                    Texture2D combined = ImageSynthesizer.CombineFour(texture1, texture2, texture3, texture4, _currentStack.RowCount, _currentStack.ColumnCount);

                    //place the synthesized image into the stack
                    _currentStack.SetSeed(combined);

                    //resets/initializes the model using the synthesized image
                    _model.ResetModel(combined);

                    //place the synthesized image into the stack
                    _currentStack.SetSeed(combined);

                    _analyser.ResetAnalysis();
                }
            }


            /// <summary>
            /// 
            /// </summary>
            private void Update()
            {
                if (_pause == false)
                {
                    //check if stack is finished building

                    //if build not complete, leave function
                    if (_model.BuildComplete == false)
                    {
                        return;
                    }

                    //if stack building is complete, get fitness / update
                    if (_model.BuildComplete == true)
                    {
                        //calculate fitness
                        _analyser.Fitness();

                        //add stack to current generation
                        _currentStack.SetName("GEN " + generations + "_STACK " + _curCount);
                        _currentStack.UpdateDataText(); //updates the datatext

                        //add the stack to the generation
                        AddStackToGeneration(_currentStack);
                        
                        _curCount++;

                        //if count == popsize recalculate the mating pool
                        if (_curCount == _genSize)
                        {
                            //add generation to the population history
                            AddGenToPopulation(_currentGenerationData);

                            //run natural selection to generate breeding pool - threshold value: include best performers above threshhold value (between 0,1))
                            UpdateMatingPool(0.5f);

                            SerializeData(_currentGenerationData);

                            //reset current population array
                            _currentGenerationData = new CellStackData[_genSize];

                            //reset popcounter
                            _curCount = 0;
                            generations++;

                        }

                        //breed new dna from mating pool
                        IDNAI childdna = Breed();

                        //reset the stack and insert new dna
                        _currentStack.Restore();
                        _currentStack.SetDNA(childdna);
                        _model.Stack = _currentStack;

                        //ONLY IF USING GENES FOR PARTIAL IMAGE SEEDS - synthesize 4 images from child genes - don't use this if you dont have genes for image textures 
                        if (_SeedFromGenes == true)
                        {
                            Texture2D texture1 = _seeds[childdna.GetGene(0)];
                            Texture2D texture2 = _seeds[childdna.GetGene(1)];
                            Texture2D texture3 = _seeds[childdna.GetGene(2)];
                            Texture2D texture4 = _seeds[childdna.GetGene(3)];
                            Texture2D combined = ImageSynthesizer.CombineFour(texture1, texture2, texture3, texture4, _currentStack.RowCount, _currentStack.ColumnCount);

                            //place the synthesized image into the stack
                            _currentStack.SetSeed(combined);

                            //resets/initializes the model using the synthesized image
                            _model.ResetModel(combined);
                            _analyser.ResetAnalysis();
                        }

                        else
                        {
                            //resets/initializes the model using original seedimage
                            _model.ResetModel();
                            _analyser.ResetAnalysis();
                        }
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool Pause
            {
                get { return _pause; }
                set { _pause = value; }
            }

            /// <summary>
            /// Adds a stack to the current generation of stacks
            /// </summary>
            private void AddStackToGeneration(CellStack stack)
            {
                //add stack to current generation of stacks
                _currentGenerationData[_curCount] = stack.CopyData();
            }

            /// <summary>
            /// Adds the generation of stacks to the population and updates max and min population fitness
            /// </summary>
            private void AddGenToPopulation(CellStackData[] generation)
            {
                //update min & max fitness of the population
                foreach (var stack in generation)
                {
                    if (stack.Fitness > _population.MaxFitness)
                    {
                        _population.MaxFitness = stack.Fitness;
                    }

                    if (stack.Fitness < _population.MinFitness)
                    {
                        _population.MinFitness = stack.Fitness;
                    }
                }
                //add generation of stacks to the population
                _population.AddGeneration(generation);

            }

            public void SerializeData(CellStackData[] generation)
            {
                foreach (var c in generation)
                {
                    Interop.SerializeBinary(c, $"{filePath}/{generations}_{c.Fitness}_{c.Name}.stackdata");
                }
            }

            /// <summary>
            /// Initializes the mating pool
            /// </summary>
            private void InitializeMatingPool()
            {
                //initialize mating pool with random instances of dna
                _matingPool = new List<IDNAI>();
                for (int i = 0; i < _genSize; i++)
                {
                    _matingPool.Add(new DNAI());
                }
            }

            /// <summary>
            /// Updates the mating pool
            /// </summary>
            private void UpdateMatingPool(float threshhold)
            {
                //reset mating pool
                _matingPool.Clear();

                //get flattened stack population
                var population = _population.FitnessData;
                //sort by fitness value (highest first - descending)
                var sortedList = population.OrderByDescending(f => f.fitness).ToList();

                if (sortedList.Count < _genSize * 2)
                {
                    //add DNA to mating pool weighted by fitness value
                    int quantity = sortedList.Count;
                    float totalfitness = TotalFitness(sortedList, quantity);
                    for (int i = 0; i < quantity; i++)
                    {
                        int weightedQuantity = (int)((sortedList[i].fitness / totalfitness) * 1000);
                        for (int j = 0; j < weightedQuantity; j++)
                        {
                            _matingPool.Add(sortedList[i].DNA);
                        }
                    }
                }

                else
                {
                    //add DNA to mating pool weighted by fitness value
                    int quantity = Mathf.RoundToInt(sortedList.Count * threshhold);
                    float totalfitness = TotalFitness(sortedList, quantity);
                    for (int i = 0; i < quantity; i++)
                    {
                        int weightedQuantity = (int)((sortedList[i].fitness / totalfitness) * 1000);
                        for (int j = 0; j < weightedQuantity; j++)
                        {
                            _matingPool.Add(sortedList[i].DNA);
                        }
                    }
                }
            }




            /// <summary>
            /// Calculate total fitness from list of stacks and quantity of that list to include
            /// </summary>
            /// <param name="sortedfitnesslist"></param>
            /// <param name="quantity"></param>
            /// <returns></returns>
            private float TotalFitness(List<CellStack> sortedfitnesslist, int quantity)
            {
                float totfitness = 0;
                for (int i = 0; i < quantity; i++)
                {
                    totfitness += sortedfitnesslist[i].Fitness;
                }

                return totfitness;
            }
            private float TotalFitness(List<FitnessDNA> sortedfitnesslist, int quantity)
            {
                float totfitness = 0;
                for (int i = 0; i < quantity; i++)
                {
                    totfitness += sortedfitnesslist[i].fitness;
                }

                return totfitness;
            }

            private float TotalFitness(List<CellStackData> sortedfitnesslist, int quantity)
            {
                float totfitness = 0;
                for (int i = 0; i < quantity; i++)
                {
                    totfitness += sortedfitnesslist[i].Fitness;
                }

                return totfitness;
            }

            /// <summary>
            /// Create child dna by breeding two parents from the mating pool
            /// </summary>
            /// <param name="dna1"></param>
            /// <param name="dna2"></param>
            /// <returns></returns>
            private IDNAI Breed()
            {
                IDNAI child = new DNAI();
                IDNAI parent1 = _matingPool[UnityEngine.Random.Range(0, _matingPool.Count)];
                IDNAI parent2 = _matingPool[UnityEngine.Random.Range(0, _matingPool.Count)];
                child.Crossover(parent1, parent2);
                return child;
            }

            /// <summary>
            /// 
            /// </summary>
            public StackPopulation Population
            {
                get { return _population; }
            }

            /// <summary>
            /// 
            /// </summary>
            public int GenSize
            {
                get { return _genSize; }
            }

        }

    }
}
