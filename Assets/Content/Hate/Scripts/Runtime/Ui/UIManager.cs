using System;
using Cf;
using UnityEngine;

namespace Hate
{
    public class UIManager : Util.Singleton.Mono<UIManager>
    {
        [SerializeField] private UIMainMenu mMainMenu;

        public UIMainMenu MainMenu => mMainMenu;
        
        protected override bool IsDontDestroyOnLoad()
        {
            return false;
        }
    }
}
