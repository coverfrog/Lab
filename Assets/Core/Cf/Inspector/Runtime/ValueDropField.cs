using System;
using UnityEngine;

namespace Cf
{
    [Serializable]
    public class ValueDropField<T>
    {
        [SerializeField] private T t;
    }
}
