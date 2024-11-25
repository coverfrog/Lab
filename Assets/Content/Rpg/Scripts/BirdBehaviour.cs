using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cf
{
    [RequireComponent(typeof(Rigidbody))]
    public class BirdBehaviour : MonoBehaviour
    {
        [Title("Action")]
        [SerializeField] private MoveAction moveAction;

        private Rigidbody _rBody;
        private Vector3? _moveEndPoint;

        private void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            moveAction.ToUpdate(ref _rBody, ref _moveEndPoint, 1.0f);
        }
    }
}
