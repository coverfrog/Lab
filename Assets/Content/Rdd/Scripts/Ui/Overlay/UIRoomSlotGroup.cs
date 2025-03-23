using System;
using System.Collections.Generic;
using Rdd.CfSteam;
using UnityEngine;

namespace Rdd.CfUi
{
    public interface IUIRoomSlotGroup
    {

    }

    public class UIRoomSlotGroup : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private RectTransform mContentRt;
        
        [Header("Prefab")] 
        [SerializeField] private UIRoomSlot mUIRoomSlotPrefab;

        private readonly List<UIRoomSlot> _mUIRoomSlotList = new List<UIRoomSlot>();
        
        /// <summary>
        /// 이벤트 등록
        /// </summary>
        private void OnEnable()
        {
            SteamManager.Instance.OnUpdateRooms += OnUpdateRooms;
        }

        /// <summary>
        /// 이벤트 해제
        /// </summary>
        private void OnDisable()
        {
            if (!SteamManager.Instance)
            {
                return;
            }

            SteamManager.Instance.OnUpdateRooms -= OnUpdateRooms;
        }
        
        /// <summary>
        /// Click 
        /// </summary>
        public void OnClick(UIRoomSlot uiRoomSlot)
        {
            int index = uiRoomSlot.Index;
            string roomId = uiRoomSlot.RoomId;
            
            Debug.Log(index + ":" + roomId);
        }

        /// <summary>
        /// 방 업데이트 이벤트
        /// </summary>
        /// <param name="length"> 길이 </param>
        /// <param name="indexes"> 인덱스 배열 </param>
        /// <param name="roomIds"> 방 번호 배열 </param>
        private void OnUpdateRooms(int length, int[] indexes, string[] roomIds)
        {
            for (int i = 0; i < length; i++)
            {
                int index = indexes[i];
                string roomId = roomIds[i];

                UIRoomSlot uiRoomSlot = Instantiate(original: mUIRoomSlotPrefab, parent: mContentRt);
                uiRoomSlot.Init(
                    slot => { _mUIRoomSlotList.Add(slot); },
                    slot => { _mUIRoomSlotList.Remove(slot); },
                    OnClick, 
                    index, 
                    roomId);
            }
        }
    }
}
