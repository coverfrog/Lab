using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf
{
    public class PoolManager : Util.Singleton.Mono<PoolManager>
    {
        [Title("Info")]
        [SerializeField] private List<PoolInfo> infoList;
        
        [Title("")]
        [ShowInInspector] private Dictionary<string, PoolBase<Behaviour>> _poolDict;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
