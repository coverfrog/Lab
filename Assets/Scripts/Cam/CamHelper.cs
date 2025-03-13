using System;
using UnityEngine;

public enum CamType
{
    Main,
    Ui,
    Effect,
}

[RequireComponent(typeof(Camera))]
public class CamHelper : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private CamType mCamType;
    
    [Header("Reference")]
    [SerializeField] private Camera mCam;
    
    public CamType GetCamType() => mCamType;

    public bool GetCamera(out Camera c)
    {
        c = mCam;

        return c != null;
    }

    private void Awake()
    {
        if (!mCam) mCam = gameObject.GetComponent<Camera>();
        if (!mCam) mCam = gameObject.AddComponent<Camera>();
    }

    private void OnEnable()
    {
        CamManager.Instance.SetCameraHelper(this);
    }
}
