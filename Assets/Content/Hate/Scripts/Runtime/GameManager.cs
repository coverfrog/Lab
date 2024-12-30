using System;
using Cf;
using UnityEngine;

namespace Hate
{
    public class GameManager : Util.Singleton.Mono<GameManager>
    {
        [Header("Runtime")]
        [SerializeField] private GameData mGameData;

        [Header("Mvvm")]
        [SerializeField] private GameModel mGameModel;
        [SerializeField] private GameView mGameView;

        [RuntimeInitializeOnLoadMethod]
        private static void OnLoad()
        {
            Instance.mGameModel = new GameModel();
            Instance.mGameModel.Init();
            Instance.mGameModel.OnDataLoadChanged += Instance.mGameView.OnDataLoadChanged;
            Instance.OnGameStart();
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        private void OnGameStart()
        {
            
        }
    }
}
