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

        private bool _mIsUiPageLoadingSlideComplete;
        
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
        /// 이벤트 등록
        /// 값 초기화
        /// </summary>
        public override void OnEnable()
        {
            // Base
            base.OnEnable();
            
            // 이벤트 등록
            UIManager.Instance.GetPage(out UIPageLoading uiPageLoading);
            uiPageLoading.OnSliderComplete += OnLoadingSliderComplete;
            
            // 값 초기화
            _mIsUiPageLoadingSlideComplete = false;
        }

        /// <summary>
        /// 코루틴 초기화
        /// 이벤트 해제
        /// </summary>
        private void OnDisable()
        {
            // 코루틴 초기화
            if (_mCoGameStart != null) StopCoroutine(_mCoGameStart);
            _mCoGameStart = null;
            
            if (_mCoClose != null) StopCoroutine(_mCoClose);
            _mCoClose = null;
            
            // 이벤트 해제
            if (!UIManager.Instance) return;
            
            UIManager.Instance.GetPage(out UIPageLoading uiPageLoading);
            uiPageLoading.OnSliderComplete -= OnLoadingSliderComplete;
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
            // 버튼은 막기
            InteractableAll(false);
            
            // Loading 시작 
            UIManager.Instance.GetPage(out UIPageLoading uiPageLoading);
            uiPageLoading.SetActive(true);
            
            // Loading Data 요청
#if false
            // Debug 용도의 Loading Script
            
            for (float t = 0.0f; t < 5.0f; t += Time.deltaTime)
            {
                var percent = t / 5.0f;
                
                uiPageLoading.UpdateValue(percent);
                
                yield return null;
            }
            
            uiPageLoading.UpdateValue(1.0f);
            
#else
            // Steam Manager 에게 로드 요청
            SteamManager.Instance.LoadGameLevel();
#endif

            // Loading Ui 가 실제로 끝 점에 도달할 때 까지 대기
            while (!_mIsUiPageLoadingSlideComplete) yield return new WaitForEndOfFrame();
            
            // 다음 화면 보여주기
            Debug.Log("Loading End!");
            
            // Null
            _mCoGameStart = null;
        }

        /// <summary>
        /// 슬라이더가 실제로 UI 반영이 되는 시점을 얻기 위해 이벤트 추가
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnLoadingSliderComplete()
        {
            _mIsUiPageLoadingSlideComplete = true;
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
