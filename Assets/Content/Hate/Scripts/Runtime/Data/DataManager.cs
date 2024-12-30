using System;
using System.Collections;
using UnityEngine;
using Cf;

namespace Hate
{
    public class DataManager : Util.Singleton.Mono<DataManager>
    {
        [SerializeField] private GameData mGameData;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
        
        public void Init(bool pIsGuestLogin)
        {
            mGameData = new GameData();
            
            if (pIsGuestLogin)
            {
                mGameData.Init(pIsGuestLogin);
            }
        }
    }
}
