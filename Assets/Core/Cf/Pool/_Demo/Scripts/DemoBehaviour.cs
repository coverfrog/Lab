using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace Cf.PoolDemo
{
    public class DemoBehaviour : MonoBehaviour, IReturnPool<DemoBehaviour>
    {
        public IObjectPool<DemoBehaviour> Pool { get; set; }

        [Title("Demo")]
        [Button]
        private void Release()
        {
            Pool?.Release(this);
        }
    }
}
