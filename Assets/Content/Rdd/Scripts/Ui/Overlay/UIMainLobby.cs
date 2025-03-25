using System;
using System.Collections;
using System.Collections.Generic;
using Rdd.CfSteam;
using UnityEngine;
using UnityEngine.UI;

namespace Rdd.CfUi
{
    public class UIMainLobby : UIPageOverlay
    {
        [Header("Reference")] 
        [SerializeField] private UIRoomSlotGroup mUIRoomSlotGroup;
        [Space]
        [SerializeField] private Button mCreateButton;
        [SerializeField] private Button mJoinButton;

        private IEnumerator _mCoCreateRoom;
        private IEnumerator _mCoJoinRoom;
        private IEnumerator _mCoEnterRoom;
        
        /// <summary>
        /// 컴포넌트 추가
        /// Button 함수 제거 및 비활성화
        /// </summary>
        private void Awake()
        {
            // 컴포넌트 추가
            if (!mUIRoomSlotGroup) mUIRoomSlotGroup = transform.GetComponentInChildren<UIRoomSlotGroup>(true);
            
            // Button 함수 제거 및 비활성화
            mCreateButton.onClick = new Button.ButtonClickedEvent();
            mJoinButton.onClick = new Button.ButtonClickedEvent();
            
            InteractableAll(false);
        }

        /// <summary>
        /// 코루틴 초기화
        /// </summary>
        private void OnDisable()
        {
            // 코루틴 초기화
            if (_mCoCreateRoom != null) StopCoroutine(_mCoCreateRoom);
            _mCoCreateRoom = null;
            
            if (_mCoJoinRoom != null) StopCoroutine(_mCoJoinRoom);
            _mCoJoinRoom = null;
            
            if (_mCoEnterRoom != null) StopCoroutine(_mCoEnterRoom);
            _mCoEnterRoom = null;
        }

        /// <summary>
        /// Steam Manager 대기
        /// 버튼 함수 추가
        /// 버튼 활성화
        /// </summary>
        private IEnumerator Start()
        {
            // Steam Manager 대기
            while (!SteamManager.Instance) yield return null;
            while (!SteamManager.Instance.IsInit) yield return null;
            
            // 버튼 함수 추가
            mCreateButton.onClick.AddListener(OnClickCreateRoom);
            mJoinButton.onClick.AddListener(OnClickJoinRoom);

            // 버튼 활성화
            InteractableAll(true);
        }

        /// <summary>
        /// 모든 버튼 활성화
        /// </summary>
        private void InteractableAll(bool interactable)
        {
            mCreateButton.interactable = interactable;
            mJoinButton.interactable = interactable;
        }

        /// <summary>
        /// 방 만들기 ( 코루틴 호출 )
        /// </summary>
        private void OnClickCreateRoom()
        {
            if (_mCoCreateRoom != null) return;
            
            _mCoCreateRoom = CoOnClickCreateRoom();
            StartCoroutine(_mCoCreateRoom);
        }

        /// <summary>
        /// 방 만들기
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoOnClickCreateRoom()
        {
            // 버튼 비활성화
            InteractableAll(false);
            
            // 요청
            SteamManager.Instance.CreateRoom(out Func<bool> isRun, out Func<bool> isSuccess);

            // 요청 대기
            while (isRun()) yield return null;
            
            // 결과에 따라서 Null
            if (isSuccess())
            {
                OnEnterRoom();
                yield break;
            }

            _mCoCreateRoom = null;
        }

        /// <summary>
        /// 방 찾기 ( 코루틴 호출 )
        /// </summary>
        private void OnClickJoinRoom()
        {
            if (_mCoJoinRoom != null) return;
            
            _mCoJoinRoom = CoOnClickJoinRoom();
            StartCoroutine(_mCoJoinRoom);
        }

        /// <summary>
        /// 방 찾기
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoOnClickJoinRoom()
        {
            // 버튼 비활성화
            InteractableAll(false);
            
            // 요청
            SteamManager.Instance.JoinRoom();

            // 요청 대기
            yield return null;
            
            // 결과에 따라서 Null
            _mCoJoinRoom = null;
        }

        /// <summary>
        /// 방 입장 ( 코루틴 호출 )
        /// </summary>
        private void OnEnterRoom()
        {
            if (_mCoEnterRoom != null) return;
            
            _mCoEnterRoom = CoEnterRoom();
            StartCoroutine(_mCoCreateRoom);
        }

        /// <summary>
        /// 방 입장
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoEnterRoom()
        {
            // Room Scene 요청
            // todo : ui loading 화면 애니메이션 추가 필요
            Debug.Log("Entering room");
            
            // 요청 대기
            yield return null;
            
            // Null
            _mCoEnterRoom = null;
        }
    }
}
