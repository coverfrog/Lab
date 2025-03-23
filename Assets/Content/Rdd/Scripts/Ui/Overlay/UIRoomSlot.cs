using System;
using TMPro;
using UnityEngine;

namespace Rdd.CfUi
{
    public class UIRoomSlot : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private TMP_Text mIndexText;
        [SerializeField] private TMP_Text mRoomIdText;

        private Action<UIRoomSlot> _mSubAction;
        private Action<UIRoomSlot> _mUnSubAction;
        private Action<UIRoomSlot> _mOnClickAction;

        public int Index { get; private set; }

        public string RoomId { get; private set; }
        
        /// <summary>
        /// List에 추가
        /// </summary>
        private void OnEnable()
        {
            _mSubAction?.Invoke(this);
        }

        /// <summary>
        /// List에 제거
        /// </summary>
        private void OnDisable()
        {
            _mUnSubAction?.Invoke(this);
        }

        /// <summary>
        /// Click 
        /// </summary>
        public void OnClick()
        {
            _mOnClickAction?.Invoke(this);
        }

        /// <summary>
        /// 정보 갱신
        /// </summary>
        /// <param name="subAction"></param>
        /// <param name="unSubAction"></param>
        /// <param name="onClickAction"></param>
        /// <param name="index"></param>
        /// <param name="roomId"></param>
        public void Init(Action<UIRoomSlot> subAction, Action<UIRoomSlot> unSubAction, Action<UIRoomSlot> onClickAction, int index, string roomId)
        {
            _mSubAction = subAction;
            _mUnSubAction = unSubAction;
            _mOnClickAction = onClickAction;
            
            Index = index;
            RoomId = roomId;

            mIndexText.text = Index.ToString();
            mRoomIdText.text = RoomId;
        }
    }
}
