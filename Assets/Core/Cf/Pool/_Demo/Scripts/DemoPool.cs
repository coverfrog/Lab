using System;
using UnityEngine;
using Cf;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace Cf.PoolDemo
{
    public class DemoPool : PoolBase<DemoBehaviour>
    {
        [Title("Demo")]
        [Button]
        private void Create()
        {
            int amount = Random.Range(1, 10);
                
            for (int i = 0; i < amount; ++i)
            {
                _ = Pool.Get();
            }
        }
    }
}
