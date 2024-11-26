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
        private static InputData _data;

        public static InputData Data
        {
            get
            {
                if (!_data)
                {
                    _ = Instance;
                }

                return _data;
            }
        }

        private PlayerInput _playerInput;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        private bool IsNull => Data == null || _playerInput == null;

        protected override void Awake()
        {
            base.Awake();

            _data = 
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
            
            Data.Bind(ref _playerInput);
        }

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();

            if (IsNull)
            {
                return;
            }
            
            Data.UnBind();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (IsNull)
            {
                return;
            }
            
            Data.UnBind();
        }
    }
}