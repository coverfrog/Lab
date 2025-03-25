using System;
using System.Collections;
using Rdd.CfSteam;
using UnityEngine;
using UnityEngine.UI;

namespace Rdd.CfUi
{
    public class UIPageRoom : UIPage
    {
        [Header("Reference")] 
        [SerializeField] private Button mStartButton;
        [SerializeField] private Button mCloseButton;

        /// <summary>
        /// Button 함수 제거 및 비활성화
        /// </summary>
        private void Awake()
        {
            // Button 함수 제거 및 비활성화
            mStartButton.onClick = new Button.ButtonClickedEvent();
            mCloseButton.onClick = new Button.ButtonClickedEvent();
            
            InteractableAll(false);
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
            mStartButton.onClick.AddListener(OnStart);
            mCloseButton.onClick.AddListener(OnClose);

            // 버튼 활성화
            InteractableAll(true);
        }
        
        /// <summary>
        /// 모든 버튼 활성화 컨트롤
        /// </summary>
        private void InteractableAll(bool interactable)
        {
            mStartButton.interactable = interactable;
            mCloseButton.interactable = interactable;
        }

        private void OnStart()
        {
            
        }

        /// <summary>
        /// UI 끄기
        /// </summary>
        private void OnClose()
        {
            
        }
    }
}
