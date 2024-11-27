using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rpg
{
    public class InputData : ScriptableObject
    {
        public Vector3 MoveDirNormal { get; private set; }

        public bool IsMoveInput => MoveDirNormal.magnitude > 0;

        private PlayerInput _playerInput;

        public void Bind(ref PlayerInput playerInput)
        {
            _playerInput = playerInput;

            if (_playerInput.actions["Move"] is { name: "Move" } moveAct)
            {
                moveAct.performed -= OnMovePerformed;
                moveAct.performed += OnMovePerformed;

                moveAct.canceled -= OnMoveCanceled;
                moveAct.canceled += OnMoveCanceled;
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            
            MoveDirNormal = new Vector3(dir.x, 0, dir.y);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveDirNormal = Vector3.zero;
        }

        public void UnBind()
        {
            if (_playerInput == null)
            {
                return;
            }

            foreach (InputAction ia in _playerInput.actions)
            {
                ia.started -= null;
                ia.performed -= null;
                ia.canceled -= null;
            }
        }
    }
}
