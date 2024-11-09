using System;
using UnityEngine;

namespace Pu
{
    public enum MonsterSpawnDataKey
    {
        Level0,
        Level1,
        Level2,
    }

    [CreateAssetMenu(menuName = "Pu/Monster/Spawn Data", fileName = "Monster Spawn Data")]
    public class MonsterSpawnData : ScriptableObject
    {
        [SerializeField] private int spawnCount = 3;
    }
}
