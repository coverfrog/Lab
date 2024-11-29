using System;
using UnityEngine;

namespace Rpg
{
    public class RotBehaviour : Cf.RotBehaviour
    {
        private InputData _inputData;

        private void Start()
        {
            _inputData = InputManager.Instance.Data;
        }

        protected override bool IsRotateInput => _inputData.IsMoveInput;
        
        protected override Vector3 GetRotateDirNormal => _inputData.MoveDirNormal;
        protected override float GetRotSpeed => 10.0f;
    }
}
