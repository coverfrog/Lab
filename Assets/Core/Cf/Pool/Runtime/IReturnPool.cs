using UnityEngine;
using UnityEngine.Pool;

namespace Cf
{
    public interface IReturnPool<T> where T : Behaviour
    {
        public IObjectPool<T> Pool { get; set; }
    }
}
