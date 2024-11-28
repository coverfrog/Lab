using System;
using UnityEngine;
using Cf;

namespace Cf
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class MoveBehaviour : MonoBehaviour
    {
        [SerializeField] private MoveAct moveAct;

        private Rigidbody _rBody;

        private bool _isMoving;
        
        protected abstract bool IsMoveInput { get; }
        
        protected abstract Vector3 GetMoveDirNormal { get; }

        protected virtual void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            if (IsMoveInput)
            {
                if (_isMoving)
                {
                    moveAct.Moving(this, _rBody, GetMoveDirNormal, 1.0f);
                }

                else
                {
                    _isMoving = true;
                    
                    moveAct.MoveBegin(this, _rBody, GetMoveDirNormal, 1.0f);
                }
            }

            else
            {
                if (_isMoving)
                {
                    _isMoving = false;
                    
                    moveAct.MoveEnd(this, _rBody);
                }
            }
        }
    }
}
