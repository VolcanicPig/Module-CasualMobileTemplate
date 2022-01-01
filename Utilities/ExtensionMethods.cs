using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig
{
    public static class ExtensionMethods
    {
        public static Vector3 RandomXZOffset(this Vector3 currVector, float min, float max)
        {
            currVector.x += Random.Range(min, max); 
            currVector.z += Random.Range(min, max);
            return currVector; 
        }
    }
}
