using System;
using System.Collections;
using Rdd.CfSteam;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Rdd.CfUi
{
    public class UIPageRoom : UIPage
    {
        [Header("Reference")] 
        [SerializeField] private Button mGameStartButton;
        [SerializeField] private Button mCloseButton;

        private IEnumerator _mCoGameStart;
        private IEnumerator _mCoClose;
        
        /// <summary>
        /// Button 함수 제거 및 비활성화
        /// </summary>
        private void Awake()
        {
            // Button 함수 제거 및 비활성화
            mGameStartButton.onClick = new Button.ButtonClickedEvent();
            mCloseButton.onClick = new Button.ButtonClickedEvent();
            
            InteractableAll(false);
        }

        /// <summary>
        /// 코루틴 초기화
        /// </summary>
        private void OnDisable()
        {
            // 코루틴 초기화
            if (_mCoGameStart != null) StopCoroutine(_mCoGameStart);
            _mCoGameStart = null;
            
            if (_mCoClose != null) StopCoroutine(_mCoClose);
            _mCoClose = null;
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
            mGameStartButton.onClick.AddListener(OnGameStart);
            mCloseButton.onClick.AddListener(OnClose);

            // 버튼 활성화
            InteractableAll(true);
        }
        
        /// <summary>
        /// 모든 버튼 활성화 컨트롤
        /// </summary>
        private void InteractableAll(bool interactable)
        {
            mGameStartButton.interactable = interactable;
            mCloseButton.interactable = interactable;
        }

        /// <summary>
        /// 게임 시작 ( 코루틴 호출 )
        /// </summary>
        private void OnGameStart()
        {
            if (_mCoGameStart != null) return;

            _mCoGameStart = CoGameStart();
            StartCoroutine(_mCoGameStart);
        }

        /// <summary>
        /// 게임 시작
        /// </summary>
        private IEnumerator CoGameStart()
        {
            yield return null;
            
            _mCoGameStart = null;
        }

        /// <summary>
        /// UI 끄기 ( 코루틴 호출 )
        /// </summary>
        private void OnClose()
        {
            if (_mCoClose != null) return;
            
            _mCoClose = CoClose();
            StartCoroutine(_mCoClose);
        }

        /// <summary>
        /// 닫기
        /// </summary>
        private IEnumerator CoClose()
        {
            SteamManager.Instance.LeaveRoom();
            UIManager.Instance.GetPage(out UIPageMainLobby uiPageMainLobby);
            
            uiPageMainLobby.SetActive(true);

            yield return new WaitForEndOfFrame();

            gameObject.SetActive(false);
        }
    }
}
