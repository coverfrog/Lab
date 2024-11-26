using System;
using UnityEngine;

namespace Cf.PoolDemo
{
    public class Caller : MonoBehaviour
    {
        private void Start()
        {
            _ = PoolManager.Instance;
        }
    }
}
