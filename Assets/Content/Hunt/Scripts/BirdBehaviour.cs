using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rpg
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        private void Update()
        {
            if (InputManager.Data.MoveDirNormal.magnitude > 0)
            {
                transform.position += InputManager.Data.MoveDirNormal * (1.0f * Time.deltaTime);
            }
        }
    }
}
