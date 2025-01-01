using System;
using UnityEngine;

namespace Hate
{
    public class SceneMainMenu : SceneBase
    {
        [SerializeField] private UIOverlayMainMenu mUiOverlay;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            UIManager.Instance.SetOverlay(this, mUiOverlay);
        }
    }
}
