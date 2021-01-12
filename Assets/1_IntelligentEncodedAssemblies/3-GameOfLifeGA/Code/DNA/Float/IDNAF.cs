using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        public interface IDNAF
        {

            void Crossover(IDNAF dna1, IDNAF dna2);
            void SetGene(int index, float value);
            float GetGene(int index);
            float[] Genes { get; }

        }
    }
}
