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
            _demoPool = GenericPool<DemoBehaviour>.CreatePool<DemoPool>(this, info, transform);
        }
    }
}
