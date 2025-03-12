using System;
using System.Collections.Generic;
using Cf.Pattern;
using UnityEngine;

public class CamManager : Singleton<CamManager>
{
    private readonly Dictionary<CamType, CamHelper> _mCamHelperDict = new Dictionary<CamType, CamHelper>();
    
    public event Action<CamHelper> OnMainCamChanged, OnUiCamChanged, OnEffectCamChanged;

    public CamHelper GetCameraHelper(CamType camType)
    {
        return _mCamHelperDict.GetValueOrDefault(camType);
    }

    public void SetCameraHelper(CamHelper camHelper)
    {
        CamType camType = camHelper.GetCamType();
        
        if (!_mCamHelperDict.TryAdd(camType, camHelper))
        {
            _mCamHelperDict[camType] = camHelper;
        }

        Action<CamHelper> onCamChanged = camType switch
        {
            CamType.Main   => OnMainCamChange,
            CamType.Ui     => OnUiCamChange,
            CamType.Effect => OnEffectCamChange,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        onCamChanged?.Invoke(camHelper);
    }

    private void OnMainCamChange(CamHelper camHelper)
    {
        OnMainCamChanged?.Invoke(camHelper);
    }

    private void OnUiCamChange(CamHelper camHelper)
    {
        OnUiCamChanged?.Invoke(camHelper);
    }

    private void OnEffectCamChange(CamHelper camHelper)
    {
        OnEffectCamChanged?.Invoke(camHelper);
    }
}
