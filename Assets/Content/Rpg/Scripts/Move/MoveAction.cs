using UnityEngine;

namespace Cf
{
    public abstract class MoveAction : ScriptableObject
    {
        public bool IsMoveInput { get; protected set; }
        
        public bool IsMoving { get; protected set; }

        public abstract void ToUpdate(ref Rigidbody rBody, ref Vector3? endPoint, float moveSpeedCurrent);

        public abstract void MoveBegin(ref Rigidbody rBody, ref Vector3? endPoint);

        public abstract void Moving(ref Rigidbody rBody, ref Vector3? endPoint, float moveSpeedCurrent);
        
        public abstract void MoveEnd(ref Rigidbody rBody, ref Vector3? endPoint);
    }
}
