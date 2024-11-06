using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cf.AirPass
{
    [Serializable]
    public class GameField 
    {
        // base info this
        [SerializeField] private GameResources resources;
        [SerializeField] private List<Game> gameList;
        
        // get
        public GameResources Resources => resources;
        public List<Game> GameList => gameList;
        
        // base method
        public void Init()
        {
            Stop();
            
            foreach (Game game in gameList)
            {
                game.Init(ref resources);
            }
        }

        public void Stop()
        {
            Pause();
            
            foreach (Game game in gameList)
            {
                game.Stop();
            }
        }

        public void Pause()
        {
            foreach (Game game in gameList)
            {
                game.Pause();
            }
        }

        public void UnPause()
        {
            foreach (Game game in gameList)
            {
                game.UnPause();
            }
        }
        
        // play
        public void Play(int level)
        {
            gameList[level].Play();
        }
    }
}