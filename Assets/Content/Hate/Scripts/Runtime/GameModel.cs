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
            mIsLogin = false;
            mId = "";
        }

        // :: isLogin
        public event Action<bool> OnLoginChanged;
        
        [SerializeField] private bool mIsLogin;

        public bool IsLogin
        {
            get => mIsLogin;
            set
            {
                if (mIsLogin == value)
                    return;
                mIsLogin = value;
                OnLoginChanged?.Invoke(mIsLogin);
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
