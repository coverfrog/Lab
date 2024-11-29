using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Character/Move/To Dir", fileName = "Move To Dir")]
    public class MoveToDir : MoveAct
    {
        public override void MoveBegin(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            rBody.linearVelocity = dir;
        }

        public override void Moving(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            rBody.linearVelocity = dir;
        }

        public override void MoveEnd(Object sender, Rigidbody rBody)
        {
            rBody.linearVelocity = Vector3.zero;
        }
    }
}
