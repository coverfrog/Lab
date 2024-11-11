using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using Cf;

namespace Pu
{
    public class MonsterGroup : MonoBehaviour
    {
        public IEnumerator SpawnBegin(MonsterSpawnDataKey key)
        {
            // co start
            IEnumerator coSpawnLoop = CoSpawnLoop();
            StartCoroutine(coSpawnLoop);
            
            return coSpawnLoop;
        }

        private IEnumerator CoSpawnLoop()
        {
            yield return null;
        }
    }
}
