using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hate
{
    [Serializable]
    public class SceneMainMenuModel
    {
        public void Init()
        {
            
        }
    }

    [Serializable]
    public class SceneMainMenuView
    {
        [SerializeField] private Button mGameStartBtn;

        public Button GameStartBtn => mGameStartBtn;
    }

    public class SceneMainMenu : SceneBase
    {
        [SerializeField] private SceneMainMenuModel mModel;
        [SerializeField] private SceneMainMenuView mView;
        
        private void Start()
        {
            mModel = new SceneMainMenuModel();
            mModel.Init();
            
            mView.GameStartBtn.onClick.AddListener(OnGameStart);
        }

        private void OnGameStart()
        {
            Debug.Log("Game Start");
        }
    }
}
