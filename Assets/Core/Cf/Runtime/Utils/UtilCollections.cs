using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cf
{
    public static partial class Util
    {
        public static class Collections 
        {
            public static void Shuffle<T>(T[] array)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    var randIdx = Random.Range(0, array.Length);
                    (array[i], array[randIdx]) = (array[randIdx], array[i]);
                }
            }
            
            public static void Shuffle<T>(List<T> list)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    var randIdx = Random.Range(0, list.Count);
                    (list[i], list[randIdx]) = (list[randIdx], list[i]);
                }
            }
        }
    }
}

