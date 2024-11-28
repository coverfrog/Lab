using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Move/To Dir")]
    public class MoveToDir : MoveAct
    {
        public override void MoveBegin(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            rBody.linearVelocity = dir;
        }

        public override void Moving(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            Quaternion lookRt = Quaternion.LookRotation(dir);
            
            rBody.rotation = Quaternion.Slerp(rBody.rotation, lookRt, speed * Time.deltaTime);
            rBody.linearVelocity = dir;
        }

        public override void MoveEnd(Object sender, Rigidbody rBody)
        {
            rBody.linearVelocity = Vector3.zero;
        }
    }
}
