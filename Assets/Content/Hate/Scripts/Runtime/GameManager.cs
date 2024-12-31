using System;
using Cf;
using UnityEngine;

namespace Hate
{
    public class GameManager : Util.Singleton.Mono<GameManager>
    {
        [SerializeField] private GameOption mGameOption;
        
        [RuntimeInitializeOnLoadMethod]
        private static void OnLoad()
        {
            Instance.Init();
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        private void Init()
        {
            DataHandler.Instance.Init(mGameOption.DebugLogin);
            SceneHandler.Instance.Init();
            
            transform.SetAsLastSibling();
            
            SceneHandler.Instance.Load(SceneType.UI, true);
        }
    }
}
