using System;
using UnityEngine;

namespace Hate
{
    [Serializable]
    public class GameOption
    {
        [SerializeField] private bool mIsSceneMoveAtStart;

        public bool IsSceneMoveAtStart => mIsSceneMoveAtStart;
    }
}
