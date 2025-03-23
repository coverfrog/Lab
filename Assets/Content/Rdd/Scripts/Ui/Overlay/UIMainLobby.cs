using System;
using UnityEngine;

namespace Rdd.CfUi
{
    public class UIMainLobby : UIPage
    {
        [Header("Reference")] 
        [SerializeField] private UIRoomSlotGroup mUIRoomSlotGroup;

        /// <summary>
        /// 컴포넌트 추가
        /// </summary>
        private void Awake()
        {
            // 컴포넌트 추가
            if (!mUIRoomSlotGroup) mUIRoomSlotGroup = transform.GetComponentInChildren<UIRoomSlotGroup>(true);
        }
    }
}
