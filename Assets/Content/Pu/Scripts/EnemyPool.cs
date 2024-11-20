using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Cf;

namespace Fu
{
    public class EnemyPool : GenericPool<EnemyPoolInfo, Enemy>
    {
        public enum Test
        {
            A,B,C,D
        }
        
        private void Start()
        {
            Debug.Log(Util.Enums.GetLength<Test>());
        }
    }
}
