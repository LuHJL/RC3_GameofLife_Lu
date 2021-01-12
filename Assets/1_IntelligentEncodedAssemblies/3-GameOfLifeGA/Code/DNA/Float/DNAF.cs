using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        [System.Serializable]

        /// <summary>
        /// 
        /// </summary>
        public class DNAF : IDNAF
        {

            private int _dnaLength = 4;
            private float[] _genes;
            private float[] _dnaMins;
            private float[] _dnaMaxs;
            private float _mutationRate = 0.05f;

            /// <summary>
            /// 
            /// </summary>
            public DNAF()
            {
                _genes = new float[_dnaLength];
                _dnaMins = new float[] { 0, 0, 0, 0 };
                _dnaMaxs = new float[] { 4, 4, 4, 4 }; //note max is number is included
                SetDNARandom();
            }

            /// <summary>
            /// Create a new DNA from genes
            /// </summary>
            /// <param name="genes"></param>
            /// <returns></returns>
            public static IDNAF CreateFromGenes(int[] genes)
            {
                var dna = new DNAF();
                for (int i = 0; i < genes.Length; i++)
                {
                    dna.SetGene(i, genes[i]);
                }
                return dna;
            }


            /// <summary>
            /// 
            /// </summary>
            public float[] Genes
            {
                get { return _genes; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dna1"></param>
            /// <param name="dna2"></param>
            public void Crossover(IDNAF dna1, IDNAF dna2)
            {
                CombineHalfRandom(dna1, dna2);
                Mutate();
            }

            /// <summary>
            /// 
            /// </summary>
            private void SetDNARandom()
            {
                for (int i = 0; i < _dnaLength; i++)
                {
                    _genes[i] = (float)UnityEngine.Random.Range(_dnaMins[i], _dnaMaxs[i]); //note max is number is included
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <param name="value"></param>
            public void SetGene(int index, float value)
            {
                _genes[index] = value;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public float GetGene(int index)
            {
                return _genes[index];
            }

            /// <summary>
            /// 
            /// </summary>
            private void Mutate()
            {
                for (int i = 0; i < _dnaLength; i++)
                {
                    _genes[i] = Random.Range(0f, 1f) < _mutationRate ? Random.Range(_dnaMins[i], _dnaMaxs[i]) : _genes[i];
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private void CombineHalf(IDNAF dna1, IDNAF dna2)
            {
                for (int i = 0; i < _dnaLength; i++)
                {
                    _genes[i] = i < (_dnaLength / 2) ? dna1.Genes[i] : dna2.Genes[i];
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private void CombineHalfRandom(IDNAF dna1, IDNAF dna2)
            {
                for (int i = 0; i < _dnaLength; i++)
                {
                    _genes[i] = Random.Range(0, 10) < 5 ? dna1.Genes[i] : dna2.Genes[i];
                }
            }
        }
    }
}
