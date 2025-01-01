using System;
using System.Collections;
using UnityEngine;
using Cf;
using Object = UnityEngine.Object;

namespace Hate
{
    public class DataHandler : Util.Singleton.Mono<DataHandler>
    {
        [SerializeField] private GameData mGameData;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
        
        public void Init(Object inSender, bool inDebugLogin)
        {
            mGameData = new GameData();
            mGameData.Init(inDebugLogin);
        }
    }
}
