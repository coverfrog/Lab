using System;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

namespace Rdd.CfSteam
{
    public partial class SteamManager
    {
        public event Action<bool> OnLobbyCreated;
        
        /// <summary>
        /// 방 생성 요청에 대한 처리가 끝났을 때
        /// </summary>
        /// <param name="result"></param>
        /// <param name="lobby"></param>
        private void SteamMatchmakingOnLobbyCreated(Result result, Lobby lobby)
        {
            // Steam 요청 실패
            if (result != Result.OK)
            {
                OnLobbyCreated?.Invoke(false);
                return;
            }

            // Steam 요청 성공
            lobby.SetPublic();
            lobby.SetJoinable(true);

            // Net 요청 성공
            if (mNetworkManager.StartHost())
            {
                OnLobbyCreated?.Invoke(true);
                return;
            }
            
            // Net 요청 실패
            OnLobbyCreated?.Invoke(false);
        }
        
        /// <summary>
        /// 방 입장을 했을 때 
        /// </summary>
        /// <param name="lobby"></param>
        private void SteamMatchmakingOnLobbyEntered(Lobby lobby)
        {
            // 현재 방 기록
            _mCurrentLobby = lobby;
            
            // Host 는 이미 Lobby Id 로 동기화
            if (mNetworkManager.IsHost) return;
            
            // Steam Id 동기화
            mTransport.targetSteamId = lobby.Owner.Id;
            mNetworkManager.StartClient();
        }
        
        private void SteamMatchmakingOnLobbyMemberDisconnected(Lobby lobby, Friend friend)
        {
           Debug.Log("SteamMatchmakingOnLobbyMemberDisconnected");
        }
        
        private void SteamMatchmakingOnLobbyMemberLeave(Lobby lobby, Friend friend)
        {
            Debug.Log("SteamMatchmakingOnLobbyMemberLeave");
        }
        
        private void SteamMatchmakingOnLobbyInvite(Friend friend, Lobby lobby)
        {
            Debug.Log("SteamMatchmakingOnLobbyInvite");
        }
        
        /// <summary>
        /// 방을 찾아서 참가 요청을 보냈을 때
        /// </summary>
        /// <param name="lobby"></param>
        /// <param name="steamId"></param>
        private static async void SteamFriendsOnGameLobbyJoinRequested(Lobby lobby, SteamId steamId)
        {
            try
            {
                await lobby.Join();
            }
            catch 
            {
                // ignore
            }
        }
        
    
    }
}
