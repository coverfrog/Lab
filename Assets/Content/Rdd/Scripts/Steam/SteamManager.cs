using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cf.Pattern;
using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace Rdd.CfSteam
{
    public class SteamManager : Singleton<SteamManager>
    {
        [Header("Reference")]
        [SerializeField] private NetworkManager mNetworkManager;
        [SerializeField] private FacepunchTransport mTransport;

        /// <summary>
        /// 컴포넌트 추가
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (!mNetworkManager) mNetworkManager = gameObject.GetComponent<NetworkManager>();
            if (!mNetworkManager) mNetworkManager = gameObject.AddComponent<NetworkManager>();

            if (!mTransport) mTransport = gameObject.GetComponent<FacepunchTransport>();
            if (!mTransport) mTransport = gameObject.AddComponent<FacepunchTransport>();
        }

        /// <summary>
        /// 프리팹 로드
        /// </summary>
        private void Start()
        {
            mNetworkManager.NetworkConfig = new NetworkConfig()
            {
                NetworkTransport = mTransport,
                TickRate = 60,
            };
        }

        /// <summary>
        /// 방 정보 데이터 갱신
        /// </summary>
        public void UpdateRooms()
        {
            SteamMatchmaking.LobbyList.RequestAsync().ContinueWith(
                resultTask =>
                {
                    Lobby[] result = resultTask.Result;

                    int length = result.Length;
                    int[] indexes = new int[length];
                    string[] roomIds = new string[length];
                    
                    for (int idx = 0; idx < length; idx++)
                    {
                        Lobby lobby = result[idx];

                        indexes[idx] = idx;
                        roomIds[idx] = lobby.Id.ToString();
                    }
                    
                    OnUpdateRooms?.Invoke(length, indexes, roomIds);
                    
                }, 
                destroyCancellationToken, 
                TaskContinuationOptions.None, 
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public event Action<int, int[], string[]> OnUpdateRooms;
    }
}
