using System;
using System.Collections;
using System.Collections.Generic;
using Rdd.CfSteam;
using UnityEngine;
using UnityEngine.UI;

namespace Rdd.CfUi
{
    public class UIPageMainLobby : UIPage
    {
        [Header("Reference")] 
        [SerializeField] private UIRoomSlotGroup mUIRoomSlotGroup;
        [Space]
        [SerializeField] private Button mCreateButton;
        [SerializeField] private Button mJoinButton;

        private IEnumerator _mCoInteractWait;
        private IEnumerator _mCoCreateRoom;
        private IEnumerator _mCoJoinRoom;
        private IEnumerator _mCoEnterRoom;

        private bool _misCreateRoomResponse;
        private bool _misCreateRoomSuccess;

        private bool _misJoinRoomResponse;
        
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
            mCreateButton.onClick.AddListener(OnClickCreateRoom);

            mJoinButton.onClick = new Button.ButtonClickedEvent();
            mJoinButton.onClick.AddListener(OnClickJoinRoom);
        }

        /// <summary>
        /// 전역 이벤트 등록
        /// 버튼 Interact 대기
        /// </summary>
        public override void OnEnable()
        {
            // Base
            base.OnEnable();
            
            // 전역 이벤트 등록
            SteamManager.Instance.OnLobbyCreated += OnLobbyCreated;
            
            // 버튼 Interact 대기
            InteractWait();
        }

        /// <summary>
        /// 전역 이벤트 해제
        /// 코루틴 초기화
        /// Response Flag 초기화
        /// </summary>
        private void OnDisable()
        {
            // 전역 이벤트 해제
            if (SteamManager.Instance) SteamManager.Instance.OnLobbyCreated -= OnLobbyCreated;
            
            // 코루틴 초기화
            if (_mCoInteractWait != null) StopCoroutine(_mCoInteractWait);
            _mCoInteractWait = null;
            
            if (_mCoCreateRoom != null) StopCoroutine(_mCoCreateRoom);
            _mCoCreateRoom = null;
            
            if (_mCoJoinRoom != null) StopCoroutine(_mCoJoinRoom);
            _mCoJoinRoom = null;
            
            if (_mCoEnterRoom != null) StopCoroutine(_mCoEnterRoom);
            _mCoEnterRoom = null;
            
            // Response Flag 초기화
            _misCreateRoomResponse = false;
            _misCreateRoomSuccess = false;
            
            _misJoinRoomResponse = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InteractWait()
        {
            if (_mCoInteractWait != null) return;

            _mCoInteractWait = CoInteractWait();
            StartCoroutine(_mCoInteractWait);
        }

        /// <summary>
        /// Steam Manager 대기 후 Button 활성화
        /// </summary>
        private IEnumerator CoInteractWait()
        {
            // 버튼 off
            InteractableAll(false);
            
            // Steam Manager 대기
            while (!SteamManager.Instance) yield return null;
            while (!SteamManager.Instance.IsInit) yield return null;
            while (SteamManager.Instance.GetIsInRoom) yield return null;
            
            // 버튼 on
            InteractableAll(true);
        }

        /// <summary>
        /// 모든 버튼 활성화 컨트롤
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
        /// 방 만들기 전역 콜백
        /// </summary>
        /// <param name="isSuccess"></param>
        private void OnLobbyCreated(bool isSuccess)
        {
            _misCreateRoomSuccess = isSuccess;
            _misCreateRoomResponse = false;
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
            SteamManager.CreateRoom();
            
            // 요청 대기
            _misCreateRoomResponse = true;
            
            while (_misCreateRoomResponse)
            {
                yield return null;
            }
            
            // 콜백 
            if (_misCreateRoomSuccess)
            {
                OnEnterRoom();
            }

            else
            {
                _mCoCreateRoom = null;
            }
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
            _misJoinRoomResponse = true;

            while (_misJoinRoomResponse)
            {
                yield return null;
            }
            
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
            StartCoroutine(_mCoEnterRoom);
        }

        /// <summary>
        /// 방 입장
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoEnterRoom()
        {
            // UI Get
            if (!UIManager.Instance.GetPage(out UIPageRoom uiPageRoom))
            {
                _mCoEnterRoom = null;
                yield break;
            }

            // UI 열기
            uiPageRoom.SetActive(true);

            // 요청 대기
            yield return null;
            
            // 자기 자신 종료
            // Null 은 OnDisable 에서 진행
            SetActive(false);
        }
    }
}
