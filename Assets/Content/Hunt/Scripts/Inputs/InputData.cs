using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rpg
{
    public class InputData : ScriptableObject
    {
        public Vector3 MoveDirNormal { get; private set; }

        private PlayerInput _playerInput;

        public void Bind(ref PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.ac

            int missCount = 0;
            
            if (_playerInput.actions["Move"] is { name: "Move" } moveAct)
            {
                Debug.Log("Bind Success");
            }

            else
            {
                missCount++;
            }

            if (missCount > 0)
            {
                Debug.LogError($"Bind Miss Count");
            }
        }

        public void UnBind()
        {
            
        }
    }
}
