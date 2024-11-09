using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pu
{
    public class MonsterGroup : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private List<MonsterSpawnData> spawnDataList;

#if UNITY_EDITOR
        public void SpawnDataListUpdate(Object sender, List<MonsterSpawnData> newSpawnDataList)
        {
            spawnDataList = newSpawnDataList;
        }
#endif
        
        public bool SpawnBegin(MonsterSpawnDataKey key)
        {
            return false;
        }
    }
}
