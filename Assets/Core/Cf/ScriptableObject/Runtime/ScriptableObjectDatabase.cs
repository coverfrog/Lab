using System.Collections.Generic;
using UnityEngine;

namespace Cf
{
    public abstract class ScriptableObjectDatabase<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField] private List<T> list = new List<T>();
        [SerializeField] private List<string> searchDirectoryList = new List<string>();
    }
}
