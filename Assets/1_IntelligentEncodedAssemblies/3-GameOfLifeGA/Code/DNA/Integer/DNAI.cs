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
        public class DNAI : IDNAI
        {
            private int _dnaLength = 6; //important this matches the gene arrays below!!
            private int[] _genes;
            private int[] _dnaMins;
            private int[] _dnaMaxs;
            private float _mutationRate = 0.05f;

            /// <summary>
            /// 
            /// </summary>
            public DNAI()
            {
                _genes = new int[_dnaLength];
                _dnaMins = new int[] { 0, 0, 0, 0, 10, 20 }; //important these match in size length to the _dnaLength variable above
                _dnaMaxs = new int[] { 4, 4, 4, 4, 20, 40 };
                SetDNARandom();
            }

            /// <summary>
            /// Create a new DNA from genes
            /// </summary>
            /// <param name="genes"></param>
            /// <returns></returns>
            public static IDNAI CreateFromGenes(int[] genes)
            {
                var dna = new DNAI();
                for (int i = 0; i < genes.Length; i++)
                {
                    dna.SetGene(i, genes[i]);
                }
                return dna;
            }

            /// <summary>
            /// 
            /// </summary>
            public int[] Genes
            {
                get { return _genes; }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="dna1"></param>
            /// <param name="dna2"></param>
            public void Crossover(IDNAI dna1, IDNAI dna2)
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
                    _genes[i] = UnityEngine.Random.Range(_dnaMins[i], _dnaMaxs[i]);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <param name="value"></param>
            public void SetGene(int index, int value)
            {
                _genes[index] = value;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public int GetGene(int index)
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
            private void CombineHalf(IDNAI dna1, IDNAI dna2)
            {
                for (int i = 0; i < _dnaLength; i++)
                {
                    _genes[i] = i < (_dnaLength / 2) ? dna1.Genes[i] : dna2.Genes[i];
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private void CombineHalfRandom(IDNAI dna1, IDNAI dna2)
            {
                for (int i = 0; i < _dnaLength; i++)
                {
                    _genes[i] = Random.Range(0, 10) < 5 ? dna1.Genes[i] : dna2.Genes[i];
                }
            }
        }
    }
}
