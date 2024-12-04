using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cf
{
    public static partial class Util
    {
        public static class Enums
        {
            private static Dictionary<Type, int> _enumLengthDictionary;
        
            public static int GetLength<T>() where T : Enum
            {
                if (_enumLengthDictionary != null)
                {
                    Type key = typeof(T);

                    if (!_enumLengthDictionary.TryGetValue(key, out var length))
                    {
                        length = Enum.GetNames(key).Length;
                    }

                    return length;
                }

                _enumLengthDictionary = new Dictionary<Type, int>();

                return GetLength<T>();
            }
        }
    }
}
