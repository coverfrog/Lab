using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hate
{
    [Serializable]
    public class SceneMainMenuModel
    {
        public event Action<bool> OnGameStartClickedChanged; 
        
        [SerializeField] private bool mGameStartClicked;

        public bool GameStartClicked
        {
            get => mGameStartClicked;
            set
            {
                if (mGameStartClicked == value) return;

                mGameStartClicked = value;
                
                OnGameStartClickedChanged?.Invoke(mGameStartClicked);
            }
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
            mModel.OnGameStartClickedChanged += OnOnGameStartClickedChanged;
            
            mView.GameStartBtn.onClick.AddListener(() => mModel.GameStartClicked = true);
        }

        private void OnOnGameStartClickedChanged(bool inTrue)
        {
            if (!inTrue) 
                return;
            
            SceneHandler.Instance.Load(SceneType.Game);
        }
    }
}
