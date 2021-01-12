using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using SpatialSlur.Collections;
using System.Linq;

namespace RC3
{
    namespace GameOfLifeGA
    {
        [CreateAssetMenu(menuName = "RC3/GameOfLifeGA/StackPopulation")]

        /// <summary>
        /// 
        /// </summary>
        public class StackPopulation : ScriptableObject
        {
            private List<CellStackData> _population;
            private float _maxFitness = float.MinValue;
            private float _minFitness = float.MaxValue;
            List<FitnessDNA> _fitnessData;

            /// <summary>
            /// 
            /// </summary>
            public float MaxFitness
            {
                get { return _maxFitness; }
                set { _maxFitness = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public float MinFitness
            {
                get { return _minFitness; }
                set { _minFitness = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public List<CellStackData> Population
            {
                get { return _population; }
            }
            public List<FitnessDNA> FitnessData
            {
                get { return _fitnessData; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="generation"></param>
            public void AddGeneration(CellStackData[] generation)
            {
                //_population.AddRange(generation);
                _fitnessData.AddRange(generation.Select(cellstackdata => new FitnessDNA(cellstackdata)));
            }

            public void Reset()
            {
                _population = new List<CellStackData>();
                _fitnessData = new List<FitnessDNA>();
                _maxFitness = float.MinValue;
                _minFitness = float.MaxValue;
            }
        }

        public struct FitnessDNA
        {
            public float fitness;
            public IDNAI DNA;
            public FitnessDNA(CellStackData data)
            {
                fitness = data.Fitness;
                DNA = DNAI.CreateFromGenes(data.DNAGENES);
            }
        }

    }
}
