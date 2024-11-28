using UnityEngine;

namespace Cf
{
    public abstract class MoveAct : ScriptableObject
    {
        public abstract void MoveBegin(Object sender, Rigidbody rBody, Vector3 dir, float speed);

        public abstract void Moving(Object sender, Rigidbody rBody, Vector3 dir, float speed);

        public abstract void MoveEnd(Object sender, Rigidbody rBody);
    }
}
