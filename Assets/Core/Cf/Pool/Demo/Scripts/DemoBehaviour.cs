using UnityEngine;
using UnityEngine.Pool;

namespace Cf.PoolDemo
{
    public class DemoBehaviour : MonoBehaviour, IReturnPool<DemoBehaviour>
    {
        public IObjectPool<DemoBehaviour> Pool { get; set; }
    }
}
