using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hate
{
    [Serializable]
    public class GameOption
    {
        [SerializeField] private bool mDebugLogin = true;

        public bool DebugLogin => mDebugLogin;
    }
}
