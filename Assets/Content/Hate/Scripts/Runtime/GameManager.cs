using System;
using Cf;
using UnityEngine;

namespace Hate
{
    public class GameManager : Util.Singleton.Mono<GameManager>
    {
        [SerializeField] private GameOption mGameOption;
        [SerializeField] private GameModel mGameModel;
        [SerializeField] private GameView mGameView;
        
        [RuntimeInitializeOnLoadMethod]
        private static void OnLoad()
        {
            Instance.mGameModel = new GameModel();
            Instance.mGameModel.Init();
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
