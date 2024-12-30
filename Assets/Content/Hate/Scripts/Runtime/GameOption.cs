using System;
using UnityEngine;

namespace Hate
{
    [Serializable]
    public class GameOption
    {
        [SerializeField] private bool mGuestLogin;

        public bool GuestLogin => mGuestLogin;
    }
}
