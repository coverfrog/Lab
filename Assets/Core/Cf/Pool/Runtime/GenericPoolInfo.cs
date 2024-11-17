using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf
{
    public abstract class GenericPoolInfo<TBehaviour> : ScriptableObject where TBehaviour : Behaviour
    {
        [Title("")] 
        [SerializeField] private TBehaviour prefab;

        public TBehaviour Prefab => prefab;
    }
}
