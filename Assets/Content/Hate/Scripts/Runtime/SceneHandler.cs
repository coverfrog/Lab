using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hate
{
    public enum SceneType
    {
        MainMenu = 0,
        Game = 1,
        Loading = 100,
    }
    
    public class SceneHandler : Cf.Util.Singleton.Mono<SceneHandler>
    {
        private Dictionary<SceneType, string> _mSceneNameDict;
        
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
        
        public void Init()
        {
            // :: get type by dict
            _mSceneNameDict = new Dictionary<SceneType, string>()
            {
                { SceneType.MainMenu, "0_MainMenu" },
                { SceneType.Game, "1_Game" },
                { SceneType.Loading, "100_Loading" },
            };
        }

        public void Load(SceneType inSceneType)
        {
            var sceneName = _mSceneNameDict[inSceneType];

            SceneManager.LoadScene(sceneName);
        }
    }
}
