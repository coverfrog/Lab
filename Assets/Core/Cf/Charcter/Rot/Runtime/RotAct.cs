using UnityEngine;

namespace Cf
{
    public abstract class RotAct : ScriptableObject
    {
        public abstract void RotateBegin(Object sender, Rigidbody rBody, Vector3 dir, float speed);
        public abstract void Rotating(Object sender, Rigidbody rBody, Vector3 dir, float speed);
        public abstract void RotateEnd(Object sender, Rigidbody rBody);
    }
}
