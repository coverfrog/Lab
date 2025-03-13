using System;
using System.Collections;
using Cf.Yield;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class InputMoveToPointNav : InputAct
{
    private const int HitsMaxCount = 10;
    private const int HitLayer = 11;
    private const float HitsLength = 1000.0f;

    [Header("Reference")] 
    [SerializeField] private NavMeshAgent mAgent;
    
    [Header("Data")]
    [SerializeField] private Vector3 mPoint;

    private Ray _mRay;
    private RaycastHit _mNearHit;
    private readonly RaycastHit[] _mHits = new RaycastHit[HitsMaxCount];

    private InputManager _mInputManager;
    private CamManager _mCamManager;

    private void Awake()
    {
        if (!mAgent) mAgent = gameObject.GetComponent<NavMeshAgent>();
        if (!mAgent) mAgent = gameObject.AddComponent<NavMeshAgent>();
    }

    private IEnumerator Start()
    {
        while (!InputManager.Instance) 
            yield return YieldCache.WaitForEndOfFrame;
        while (!CamManager.Instance) 
            yield return YieldCache.WaitForEndOfFrame;
        
        _mInputManager = InputManager.Instance;
        _mCamManager = CamManager.Instance;
    }

    private void Update()
    {
        if (!_mInputManager)
        {
            return;
        }

        if (!_mInputManager.Data.isMouseRightClick)
        {
            return;
        }
        
        SetPoint();
    }

    private void SetPoint()
    {
        if (!_mCamManager)
        {
            return;
        }

        if (!_mCamManager.GetCameraHelper(CamType.Main, out CamHelper camHelper))
        {
            return;
        }

        if (!camHelper.GetCamera(out Camera cam))
        {
            return;
        }
        
        _mRay = cam.ScreenPointToRay(Input.mousePosition);

        int hitCount = Physics.RaycastNonAlloc(_mRay, _mHits, HitsLength, 1 << HitLayer);

        if (hitCount == 0)
        {
            return;
        }
        
        _mNearHit = _mHits[0];

        if (hitCount > 1)
        {
            for (var i = 1; i < hitCount; i++)
            {
                if (_mHits[i].distance < _mNearHit.distance)
                {
                    _mNearHit = _mHits[i];
                }
            }
        }
   
        mPoint = _mNearHit.point;

        if (!mAgent)
        {
            return;
        }

        mAgent.SetDestination(mPoint);
    }
}
