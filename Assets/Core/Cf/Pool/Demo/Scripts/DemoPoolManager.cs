using System;
using UnityEngine;
using Cf;

namespace Cf.PoolDemo
{
    public class DemoPoolManager : MonoBehaviour
    {
        [SerializeField] private PoolInfo info;
        
        private DemoPool _demoPool;

        private void Start()
        {
            // _ = GenericPool<DemoBehaviour>.CreatePool(gameObject, info.Get(), info.GetOptions, out _demoPool);
        }
    }
}
