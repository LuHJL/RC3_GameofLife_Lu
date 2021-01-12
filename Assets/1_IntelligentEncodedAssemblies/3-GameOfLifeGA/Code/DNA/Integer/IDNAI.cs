using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        public interface IDNAI
        {
            void Crossover(IDNAI dna1, IDNAI dna2);
            void SetGene(int index, int value);
            int GetGene(int index);
            int[] Genes { get; }
        }
    }
}
