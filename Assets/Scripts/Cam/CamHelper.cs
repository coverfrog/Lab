using System;
using UnityEngine;

public enum CamType
{
    Main,
    Ui,
    Effect,
}

public class CamHelper : MonoBehaviour
{
    [SerializeField] private CamType camType;

    public CamType GetCamType() => camType;
    
    private void Start()
    {
        CamManager.Instance.SetCameraHelper(this);
    }
}
