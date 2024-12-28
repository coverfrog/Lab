using Cf;
using UnityEngine;

namespace Hate
{
    public class GameManager : Util.Singleton.Mono<GameManager>
    {
        [SerializeField] private GameData gameData;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            Instance.gameData = new GameData();
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
