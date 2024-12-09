using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cf.Utils
{
    public static partial class Util
    {
        public static class Math 
        {
            public static class Ran
            {
                public static int[] GetNoneOverlapNumberArray(int n)
                {
                    return GetNoneOverlapNumberArray(n, n);
                }

                public static int[] GetNoneOverlapNumberArray(int maxCount, int n)
                {
                    var source = new int[maxCount];
                    for (var i = 0; i < maxCount; i++) { source[i] = i; }

                    var result = new int[n];

                    for (var i = 0; i < n; i++)
                    {
                        var pick = Random.Range(0, maxCount);

                        result[i] = source[pick];
                        source[pick] = source[maxCount - 1];
                        maxCount -= 1;
                    }

                    return result;
                }
            }
        }
    }

 
}
