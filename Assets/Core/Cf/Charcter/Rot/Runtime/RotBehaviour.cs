using UnityEngine;

namespace Cf
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class RotBehaviour : MonoBehaviour
    {
        [SerializeField] private RotAct rotAct;
        
        private Rigidbody _rBody;

        private bool _isRotating;
        
        protected abstract bool IsRotateInput { get; }
        
        protected abstract Vector3 GetRotateDirNormal { get; }
        
        protected abstract float GetRotSpeed { get; }

        protected virtual void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            if (!rotAct || !_rBody)
            {
                return;
            }

            if (IsRotateInput)
            {
                if (_isRotating)
                {
                    rotAct.Rotating(this, _rBody, GetRotateDirNormal, GetRotSpeed);
                }

                else
                {
                    _isRotating = true;
                    
                    rotAct.RotateBegin(this, _rBody, GetRotateDirNormal, GetRotSpeed);
                }
            }

            else
            {
                if (_isRotating)
                {
                    _isRotating = false;
                    
                    rotAct.RotateEnd(this, _rBody);
                }
            }
        }
    }
}
