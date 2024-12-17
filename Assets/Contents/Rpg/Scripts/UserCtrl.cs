using System;
using System.Collections;
using UnityEngine;

namespace Cf.Rpg
{
    public class UserCtrl : MonoBehaviour
    {
        [SerializeField] private ContentType startContent;
        [SerializeField] private UserInfo userInfo;

        private void Start()
        {
            ContentManager.Instance.ContentBegin(this, startContent, out _);
        }
    }
}
