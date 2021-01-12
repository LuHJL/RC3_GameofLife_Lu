using UnityEngine;

namespace RC3
{
    /// <summary>
    /// Abstract class for any ModelInitializer
    /// The Initialize function takes a int[,] array of the state of 
    /// a 2d layer of cells and sets their initial state
    /// Scriptable objects architecture explained here:
    /// https://www.youtube.com/watch?v=raQ3iHhE_Kk
    /// https://docs.unity3d.com/Manual/class-ScriptableObject.html
    /// </summary>
    public abstract class ModelInitializer : ScriptableObject
    {
        public abstract void Initialize(int[,] state);
    }
}
