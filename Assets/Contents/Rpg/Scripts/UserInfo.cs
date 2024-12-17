using UnityEngine;
using System;

namespace Cf.Rpg
{
    [Serializable]
    public class UserInfo
    {
        [SerializeField] private string guid;
        [SerializeField] private string id;
        [SerializeField] private string pw;

        public UserInfo()
        {
            
        }
    }
}
