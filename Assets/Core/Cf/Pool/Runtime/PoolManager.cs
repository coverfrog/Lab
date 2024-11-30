using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cf
{
    public class PoolManager : Util.Singleton.Mono<PoolManager>
    {
        [Header("")]
        [SerializeField] private List<PoolInfo> infoList;
        
        [Header("")]
        private Dictionary<string, PoolBase<Behaviour>> _poolDict;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
