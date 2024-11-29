using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Character/Rot/To Dir", fileName = "Rot To Dir")]
    public class RotToDir : RotAct
    {
        public override void RotateBegin(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            
        }

        public override void Rotating(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            Quaternion lookQuaternion = Quaternion.LookRotation(dir);

            rBody.rotation = Quaternion.Slerp(rBody.rotation, lookQuaternion, speed * Time.deltaTime);
        }

        public override void RotateEnd(Object sender, Rigidbody rBody)
        {
            
        }
    }
}
