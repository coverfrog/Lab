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
    public partial class SteamManager : Singleton<SteamManager>
    {
        [Header("Reference")]
        [SerializeField] private NetworkManager mNetworkManager;
        [SerializeField] private FacepunchTransport mTransport;

        private Lobby? _mCurrentLobby;
        
        /// <summary>
        /// 어떠한 접속 상태에도 속한다면 방에 있는 것으로 간주
        /// </summary>
        public bool GetIsInRoom => mNetworkManager.IsHost || mNetworkManager.IsServer || mNetworkManager.IsClient;
        
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
            // 프리팹 로드
            mNetworkManager.NetworkConfig = new NetworkConfig()
            {
                NetworkTransport = mTransport,
                TickRate = 60,
            };
        }

        /// <summary>
        /// 콜백 등록 ( SteamCallbackManager.cs , Partial Class )
        /// </summary>
        private void OnEnable()
        {
            // 콜백 등록
            SteamMatchmaking.OnLobbyCreated += SteamMatchmakingOnLobbyCreated;
            SteamMatchmaking.OnLobbyEntered += SteamMatchmakingOnLobbyEntered;
            SteamMatchmaking.OnLobbyMemberDisconnected += SteamMatchmakingOnLobbyMemberDisconnected;
            SteamMatchmaking.OnLobbyMemberLeave += SteamMatchmakingOnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyInvite += SteamMatchmakingOnLobbyInvite;
            
            SteamFriends.OnGameLobbyJoinRequested += SteamFriendsOnGameLobbyJoinRequested;
        }




        /// <summary>
        /// 콜백 해제 ( SteamCallbackManager.cs , Partial Class )
        /// </summary>
        private void OnDisable()
        {
            // 콜백 해제
            SteamMatchmaking.OnLobbyCreated -= SteamMatchmakingOnLobbyCreated;
            SteamMatchmaking.OnLobbyEntered -= SteamMatchmakingOnLobbyEntered;
            SteamMatchmaking.OnLobbyMemberDisconnected -= SteamMatchmakingOnLobbyMemberDisconnected;
            SteamMatchmaking.OnLobbyMemberLeave -= SteamMatchmakingOnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyInvite -= SteamMatchmakingOnLobbyInvite;
            
            SteamFriends.OnGameLobbyJoinRequested -= SteamFriendsOnGameLobbyJoinRequested;
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

        /// <summary>
        /// 혹시 모를 Null 대비
        /// </summary>
        public bool IsInit => mTransport != null && mNetworkManager != null && mNetworkManager.NetworkConfig != null;
        
        /// <summary>
        /// 방 생성 시작
        /// </summary>
        public static async void CreateRoom(int maxMembers = 10)
        {
            try
            {
                await SteamMatchmaking.CreateLobbyAsync(maxMembers);
            }
            catch 
            {
                // ignore
            }
        }
        
        /// <summary>
        /// 방 입장 시작
        /// </summary>
        public void JoinRoom()
        {
            if (!IsInit)
            {
                return;
            }
            
            Debug.Log("Joining room");
        }

        /// <summary>
        /// 방 떠나기
        /// </summary>
        public void LeaveRoom()
        {
            _mCurrentLobby?.Leave();
            
            mNetworkManager.Shutdown();
        }

        /// <summary>
        /// 게임 레벨을 로드 시작
        /// </summary>
        public void LoadGameLevel()
        {
            // todo : scene manager 로 특정 scene 로 이동 시켜주기
        }
    }
}
