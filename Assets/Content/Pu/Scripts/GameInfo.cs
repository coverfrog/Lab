using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pu
{
    public enum GameType
    {
        Debug,
    }

    [CreateAssetMenu(menuName = "Fu/Game/Game Info", fileName = "Game Info")]
    public class GameInfo : ScriptableObject
    {
        [Title("")]
        [SerializeField] private GameType gameType;

        public GameInfo Clone()
        {
            GameInfo info = CreateInstance<GameInfo>();

            ReferenceCopy(ref info);
            
            return info;
        }
        
        private void ReferenceCopy(ref GameInfo info)
        {
            // text
            info.gameType = gameType;       
            info.name = $"cloned game info : {gameType}";
        }
    }
}
