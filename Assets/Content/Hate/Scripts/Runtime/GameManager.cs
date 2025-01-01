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
            _ = DataHandler.Instance;
            _ = SceneHandler.Instance;
            _ = UIManager.Instance;

        }

        protected override void Awake()
        {
            base.Awake();
            
            DataHandler.Instance.Init(this, mGameOption.DebugLogin);
            SceneHandler.Instance.Init(this);
            UIManager.Instance.Init(this);
        }
    }
}
