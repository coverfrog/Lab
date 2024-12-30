using System;
using UnityEngine;

namespace Hate
{
    [Serializable]
    public class GameData
    {
        
        [SerializeField] private UserData mUserData;
        
        public void Init(bool pIsGuestLogin)
        {
            mUserData = new UserData();
            mUserData.Init(pIsGuestLogin);
        }
    }
}
