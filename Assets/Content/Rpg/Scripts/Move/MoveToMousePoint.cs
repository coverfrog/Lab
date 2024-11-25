
using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Rpg/Move/Move To Mouse Point", fileName = "Move To Mouse Point")]
    public class MoveToMousePoint : MoveAction
    {
        public override void ToUpdate(ref Rigidbody rBody, ref Vector3? endPoint, float moveSpeedCurrent)
        {
            if (!rBody)
            {
                endPoint = null;
                return;
            }

            IsMoveInput = Input.GetMouseButtonDown(1);
            
            if (IsMoveInput)
            {
                MoveBegin(ref rBody, ref endPoint);
            }

            else
            {
                if (IsMoving)
                {
                    Moving(ref rBody, ref endPoint, moveSpeedCurrent);
                }

                else
                {
                    MoveEnd(ref rBody, ref endPoint);
                }
            }
        }

        public override void MoveBegin(ref Rigidbody rBody, ref Vector3? endPoint)
        {
            IsMoving = true;
            
            endPoint = new Vector3(100, 0, 100);
        }

        public override void Moving(ref Rigidbody rBody, ref Vector3? endPoint, float moveSpeedCurrent)
        {
            if (endPoint == null)
            {
                return;
            }

            Vector3 dirNormalized = (endPoint.Value - rBody.position).normalized;

            rBody.linearVelocity = dirNormalized * moveSpeedCurrent;
            rBody.rotation = Quaternion.LookRotation(dirNormalized);
            
            if (Vector3.Distance(rBody.position, endPoint.Value) < 2.0f)
            {
                MoveEnd(ref rBody, ref endPoint);
            }
        }

        public override void MoveEnd(ref Rigidbody rBody, ref Vector3? endPoint)
        {
            IsMoving = false;
            endPoint = null;
        }
    }
}
