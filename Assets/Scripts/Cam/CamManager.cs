using System;
using System.Collections.Generic;
using Cf.Pattern;
using UnityEngine;

public class CamManager : Singleton<CamManager>
{
    private readonly Dictionary<CamType, CamHelper> _mCamHelperDict = new Dictionary<CamType, CamHelper>();

    public bool GetCameraHelper(CamType camType, out CamHelper helper)
    {
        return _mCamHelperDict.TryGetValue(camType, out helper);
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
        
        onCamChanged.Invoke(camHelper);
    }
    
    // ---

    public event Action<CamHelper> OnMainCamChanged;

    private void OnMainCamChange(CamHelper camHelper)
    {
        OnMainCamChanged?.Invoke(camHelper);
    }
    
    // ---

    public event Action<CamHelper>  OnUiCamChanged;

    private void OnUiCamChange(CamHelper camHelper)
    {
        OnUiCamChanged?.Invoke(camHelper);
    }

    // ---
    
    public event Action<CamHelper> OnEffectCamChanged;

    private void OnEffectCamChange(CamHelper camHelper)
    {
        OnEffectCamChanged?.Invoke(camHelper);
    }
}
