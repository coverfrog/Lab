using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : Util.Singleton.Mono<InputManager>
    {
        private PlayerInput _playerInput;
        
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        protected override void Awake()
        {
            base.Awake();

            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            
        }
    }
}
