using System;
using Rpg;
using UnityEngine;
using UnityEngine.InputSystem;
using Cf;

namespace Rpg
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : Util.Singleton.Mono<InputManager>
    {
        private PlayerInput _playerInput;
        
        public InputData Data { get; private set; }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        private bool IsNull => Data == null || _playerInput == null;

        protected override void Awake()
        {
            base.Awake();

            Data = 
                ScriptableObject.CreateInstance<InputData>();

            _playerInput = 
                GetComponent<PlayerInput>();
        }

        private void Start()
        {
            if (IsNull)
            {
                return;
            }

            Data?.Bind(ref _playerInput);
        }

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();

            if (IsNull)
            {
                return;
            }
            
            Data?.UnBind();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (IsNull)
            {
                return;
            }
            
            Data?.UnBind();
        }
    }
}