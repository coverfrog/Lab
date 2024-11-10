using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cf.Scenes;

namespace Pu
{
    public class SceneCtrlGame : SceneCtrl
    {
        [Header("Debug")] 
        [SerializeField] private MonsterSpawnDataKey debugMonsterSpawnDataKey;
        
        [Header("User")]
        [SerializeField] private GameCountUI gameCountUI;
        [SerializeField] private MonsterGroup monsterGroup;

        protected override IEnumerator Start()
        {
            yield return base.Start();

            _ = gameCountUI.CountDownBegin();
            _ = monsterGroup.SpawnBegin(debugMonsterSpawnDataKey);
        }
    }
}
