using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cf
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : Util.Singleton.Mono<InputManager>
    {
        [ShowInInspector] InputAction moveAction;

        private PlayerInput InputSys { get; set; }
        
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        protected override void Awake()
        {
            base.Awake();

            InputSys = GetComponent<PlayerInput>();
        }

        void Start()
        {
            // InputAction 생성
            moveAction = new InputAction("Move", InputActionType.PassThrough);

            // 개별 키에 바인딩 추가
            moveAction.AddBinding("<Keyboard>/w").WithProcessor("scaleVector2(x=0,y=1)");
            moveAction.AddBinding("<Keyboard>/s").WithProcessor("scaleVector2(x=0,y=-1)");
            moveAction.AddBinding("<Keyboard>/a").WithProcessor("scaleVector2(x=-1,y=0)");
            moveAction.AddBinding("<Keyboard>/d").WithProcessor("scaleVector2(x=1,y=0)");

            // Gamepad 입력 추가
            moveAction.AddBinding("<Gamepad>/leftStick");

            // 액션 활성화
            moveAction.Enable();
        }

        void Update()
        {
            // 키보드 입력을 개별적으로 읽어 Vector2로 계산
            Vector2 moveInput = Vector2.zero;
            if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
            if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
            if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
            if (Keyboard.current.dKey.isPressed) moveInput.x += 1;

            // Gamepad 입력 병합
            moveInput += moveAction.ReadValue<Vector2>();

            Debug.Log($"Move Input: {moveInput}");
        }

        void OnDestroy()
        {
            // 리소스 정리
            moveAction.Disable();
            moveAction.Dispose();
        }
    }
}
