using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hate
{
    [Serializable]
    public class UserData
    {
        [SerializeField] private string mId;
        [SerializeField] private string mPw;
        
        public void Init(bool inDebugLogin)
        {
            if (inDebugLogin)
            {
                mId = "Debug";
                mPw = "Debug";
            }

            else
            {
                mId = string.Empty;
                mPw = string.Empty;
            }
        }
    }
}
