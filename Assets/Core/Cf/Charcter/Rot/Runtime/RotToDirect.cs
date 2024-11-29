using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Character/Rot/To Direct", fileName = "Rot To Direct")]
    public class RotToDirect : RotAct   
    {
        public override void RotateBegin(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            
        }

        public override void Rotating(Object sender, Rigidbody rBody, Vector3 dir, float speed)
        {
            rBody.rotation = Quaternion.LookRotation(dir);
        }

        public override void RotateEnd(Object sender, Rigidbody rBody)
        {
            
        }
    }
}
