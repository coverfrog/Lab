using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf
{
    public class CALL : MonoBehaviour
    {
        [Button]
        private void Test()
        {
            string[] subClassNames = Util.Class.FindSubClassFullNames<MoveAct>();

            foreach (var derviedName in subClassNames)
            {
                Debug.Log(derviedName);
            }
        }
    }
}
