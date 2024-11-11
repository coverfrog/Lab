using System;
using System.Collections;
using System.Collections.Generic;
using Cf;
using UnityEngine;
using Cf.Scenes;

namespace Pu
{
    public class SceneCtrlGame : SceneCtrl
    {
        [Header("Debug")] 
        [SerializeField] private MonsterSpawnDataKey debugMonsterSpawnDataKey;

        [Header("Test")] 
        [SerializeField] private PoolHelperField<Monster> poolHelperField;
        
        protected override IEnumerator Start()
        {
            yield return base.Start();
        }
    }
}
