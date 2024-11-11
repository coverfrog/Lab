using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf
{
    [Serializable]
    public class PoolHelperField<TPrefab> where TPrefab : Object
    {
        // base info
        [SerializeField] private TPrefab prefab;
    }
}
