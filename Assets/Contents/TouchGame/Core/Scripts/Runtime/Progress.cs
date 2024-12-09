using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf.TouchGame.Template0
{
    public abstract class Progress<TEnum> : MonoBehaviour where TEnum : Enum
    {
        protected abstract TEnum TypeName { get; }
        
        public static implicit operator TEnum(Progress<TEnum> progress)
        {
            return progress.TypeName;
        }

        public void Begin(Object sender)
        {
            gameObject.SetActive(true);
        }

        public void End(Object sender)
        {
            gameObject.SetActive(false);
        }
    }
}
