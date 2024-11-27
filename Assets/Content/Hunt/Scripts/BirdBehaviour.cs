using System;
using Cf;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rpg
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        [SerializeField] private ValueDropField<MoveAct> moveAct;
        
        private InputData _inputData;
        
        private void Start()
        {
            _inputData = InputManager.Data;
        }

        private void Update()
        {
            if (_inputData.IsMoveInput)
            {
                transform.position += _inputData.MoveDirNormal * (1.0f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(_inputData.MoveDirNormal), 10.0f * Time.deltaTime);
            }
        }
    }
}
