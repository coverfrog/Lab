using System;
using System.Collections.Generic;
using Cf;
using UnityEngine;

namespace Hate
{
    public class UIManager : Util.Singleton.Resources<UIManager>
    {
        [Header(":: Overlay")]
        [SerializeField] private UIMainMenu mMainMenu;

        public UIMainMenu MainMenu => mMainMenu;
        
        // ::
        
        private Stack<GameObject> _mPopupStack;

        // ::
        
        protected override string ResourcesPath()
        {
            return "UI/UIManager";
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        // ::
        
        public void Init()
        {
   
        }
    }
}
