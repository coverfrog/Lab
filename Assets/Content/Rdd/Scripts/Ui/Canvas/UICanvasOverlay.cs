using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rdd.CfUi
{
    public class UICanvasOverlay : UICanvas
    {
        [Header("Reference")]
        [SerializeField] private UILoading mUILoading; 
        [SerializeField] private UITitle mUITitle; 
        [SerializeField] private UIMainLobby mUIMainLobby;

        public UILoading UILoading => mUILoading;
        public UITitle UITitle => mUITitle;
        public UIMainLobby UIMainLobby => mUIMainLobby; 
        
        /// <summary>
        /// 컴포넌트 추가
        /// 모든 컴포넌트 끄기
        /// </summary>
        protected void Awake()
        {
            // 컴포넌트 추가
            if (!mUILoading) mUILoading = transform.GetComponentInChildren<UILoading>(true);
            if (!mUITitle) mUITitle = transform.GetComponentInChildren<UITitle>(true);
            if (!mUIMainLobby) mUIMainLobby = transform.GetComponentInChildren<UIMainLobby>(true);
            
            // 모든 컴포넌트 끄기
            mUILoading.SetActive(false);
            mUITitle.SetActive(false);
            mUIMainLobby.SetActive(false);
        }
    }
}
