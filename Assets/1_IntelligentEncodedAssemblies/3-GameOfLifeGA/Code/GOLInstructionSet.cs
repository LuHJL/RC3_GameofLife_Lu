using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3
{
    namespace GameOfLifeGA
    {
        public class GOLInstructionSet
        {
            private int[] _instructions = new int[4];

            public GOLInstructionSet()
            {
            }

            public GOLInstructionSet(int[] instructions)
            {
                _instructions = instructions;
            }

            // Use this for initialization
            public GOLInstructionSet(int inst0, int inst1, int inst2, int inst3)
            {
                _instructions[0] = inst0;
                _instructions[1] = inst1;
                _instructions[2] = inst2;
                _instructions[3] = inst3;
            }

            public void Setup(int inst0, int inst1, int inst2, int inst3)
            {
                _instructions[0] = inst0;
                _instructions[1] = inst1;
                _instructions[2] = inst2;
                _instructions[3] = inst3;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="_index"></param>
            /// <returns></returns>
            public int getInstruction(int _index)
            {
                return _instructions[_index];
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="inst"></param>
            /// <param name="index"></param>
            public void setInstruction(int inst, int index)
            {
                _instructions[index] = inst;
            }

            /// <summary>
            /// 
            /// </summary>
            public int[] Instructions
            {
                get { return _instructions; }
            }
        }
    }
}