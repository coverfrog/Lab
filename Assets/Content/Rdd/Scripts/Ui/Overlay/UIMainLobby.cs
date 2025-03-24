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
        
        /// <summary>
        /// 컴포넌트 추가
        /// Button 이벤트 제거 및 비활성화
        /// </summary>
        private void Awake()
        {
            // 컴포넌트 추가
            if (!mUIRoomSlotGroup) mUIRoomSlotGroup = transform.GetComponentInChildren<UIRoomSlotGroup>(true);
            
            // Button 이벤트 제거 및 비활성화
            mCreateButton.onClick = new Button.ButtonClickedEvent();
            mCreateButton.interactable = false; 
            
            mJoinButton.onClick = new Button.ButtonClickedEvent();
            mJoinButton.interactable = false;
        }

        /// <summary>
        /// Steam Manager 대기
        /// Button 이벤트 추가 및 활성화
        /// </summary>
        private IEnumerator Start()
        {
            // Steam Manager 대기
            while (!SteamManager.Instance.IsInit)
            {
                yield return null;
            }
            
            // 버튼에 함수 추가
            mCreateButton.onClick.AddListener(SteamManager.Instance.CreateRoom);
            mCreateButton.interactable = true;
            
            mJoinButton.onClick.AddListener(SteamManager.Instance.JoinRoom);
            mJoinButton.interactable = true;
        }
    }
}
