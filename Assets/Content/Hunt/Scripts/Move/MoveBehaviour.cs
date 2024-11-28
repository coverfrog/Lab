using UnityEngine;

namespace Rpg
{
    public class MoveBehaviour : Cf.MoveBehaviour
    {
        private InputData _inputData;

        protected override bool IsMoveInput => _inputData.IsMoveInput;

        protected override Vector3 GetMoveDirNormal => _inputData.MoveDirNormal;

        protected void Start()
        {
            _inputData = InputManager.Instance.Data;
        }
    }
}
