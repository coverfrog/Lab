using System;
using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Pool/Pool Info/Info")]
    public class PoolInfo : ScriptableObject
    {
        [SerializeField] private Behaviour prefab;
        [SerializeField] private PoolOptions options;
    }

    [Serializable]
    public class PoolInfoValue<T>
    {
        public T Prefab { get; private set; }
        
        public PoolOptions Options { get; private set; }
    }
}

