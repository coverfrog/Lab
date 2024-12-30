using System;
using UnityEngine;

namespace Hate
{
    [Serializable]
    public class GameModel
    {
        // :: Init
        public void Init()
        {
            mIsLoad = false;
            mId = "";
        }

        // :: isDataLoad
        public event Action<bool> OnDataLoadChanged;
        
        [SerializeField] private bool mIsLoad;

        public bool IsLogin
        {
            get => mIsLoad;
            set
            {
                if (mIsLoad == value)
                    return;
                mIsLoad = value;
                OnDataLoadChanged?.Invoke(mIsLoad);
            }
        }


        // :: id
        public event Action<string> OnIdChanged;  
        
        [SerializeField] private string mId;

        public string Id
        {
            get => mId;
            set
            {
                if (mId == value) 
                    return;
                mId = value;
                OnIdChanged?.Invoke(mId);
            }
        }
    }
}
