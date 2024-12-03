using System;
using System.Collections;
using Steamworks;
using Unity.Netcode;
using UnityEngine;

namespace Cf
{
    [RequireComponent(typeof(NetworkManager))]
    public class SteamManager : Util.Singleton.Mono<SteamManager>
    {
        public SteamId ClientUserId { get; private set; }
        
        protected override bool IsDontDestroyOnLoad()
        {
            return false;
        }
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => NetworkManager.Singleton != null);

            ClientUserId = SteamClient.SteamId;
        }
    }
}
