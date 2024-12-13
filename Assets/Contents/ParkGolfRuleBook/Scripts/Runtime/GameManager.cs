using System;
using UnityEngine;

namespace ParkGolf
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreGroup scoreGroup;

        [SerializeField] [Range(3, 5)] private int totalCount = 3;
        [SerializeField] [Range(1, 10)] private int hitCount = 1;
        
        private void Awake()
        {
            scoreGroup.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
             
            }
        }
    }
}
