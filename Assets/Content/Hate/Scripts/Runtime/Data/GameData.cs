using System;
using UnityEngine;

namespace Hate
{
    [Serializable]
    public class GameData
    {
        
        [SerializeField] private UserData mUserData;
        
        public void Init(bool inDebugLogin)
        {
            mUserData = new UserData();
            mUserData.Init(inDebugLogin);
        }
    }
}
