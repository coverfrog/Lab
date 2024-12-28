using System;
using Unity.Netcode;
using UnityEngine;
namespace Cf.Steams
{
    [RequireComponent(typeof(NetworkManager))]
    public class SteamManager : Util.Singleton.Resources<SteamManager>
    {
        protected override string ResourcesPath()
        {
            return "SteamManager";
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
