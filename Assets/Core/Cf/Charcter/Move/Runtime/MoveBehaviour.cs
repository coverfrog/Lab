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
        
        protected abstract float GetMoveSpeed { get; }

        protected virtual void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            if (!moveAct || !_rBody)
            {
                return;
            }
            
            if (IsMoveInput)
            {
                if (_isMoving)
                {
                    moveAct.Moving(this, _rBody, GetMoveDirNormal, GetMoveSpeed);
                }

                else
                {
                    _isMoving = true;
                    
                    moveAct.MoveBegin(this, _rBody, GetMoveDirNormal, GetMoveSpeed);
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
