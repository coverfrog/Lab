using System;
using System.Collections;
using Cf;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rpg
{
 
    [RequireComponent(typeof(Animator))]
    public class BirdBehaviour : MonoBehaviour
    {
        private Rigidbody _rBody;
        private Animator _animator;
        
        private MoveBehaviour _moveBehaviour;

        private void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _moveBehaviour = GetComponent<MoveBehaviour>();
        }
    }
}
