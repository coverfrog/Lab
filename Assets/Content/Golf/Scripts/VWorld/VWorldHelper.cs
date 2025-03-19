using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cf.Yield;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


[Serializable]
public class VWorldMapSettingConst
{
    public const string ApiKey = "9BB69C3F-772F-36DA-AD38-4A2F7B3D90C4";
    public const string BaseUrl = "http://api.vworld.kr/req/image?service=image&request=getmap&key=";
    public const string Epsg = "4326";

    public const int InitZoomLevel = 14;
    public const int InitMapWidth = 512;
    public const int InitMapHeight = 512;
    
    public const int ZoomLevelMin = 7;
    public const int ZoomLevelMax = 18;
    public const int ZoomLevelRange = ZoomLevelMax - ZoomLevelMin + 1;
}

[Serializable]
public class VWorldMapSetting : VWorldMapSettingConst
{
    public int mapZoomLevel = InitZoomLevel;
    public int mapWidth = InitMapWidth;
    public int mapHeight = InitMapHeight;
}

public class VWorldHelper : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private VWorldMapSetting mMapSetting;
    
    [Header("Script")] 
    [SerializeField] private VWorldCursor vWorldCursor;
    
    [Header("Reference")] 
    [SerializeField] private RawImage mMapCenterRawImage;
    [SerializeField] private RawImage[] mMapNeighborRawImages;

    private static readonly Dictionary<string, Texture2D[]> Cache =
        new Dictionary<string, Texture2D[]>();
    
    private void OnEnable()
    {
        vWorldCursor.OnZoomInAction += OnZoomIn;
        vWorldCursor.OnZoomOutAction += OnZoomOut;
    }

    private void OnDisable()
    {
        vWorldCursor.OnZoomInAction -= OnZoomIn;
        vWorldCursor.OnZoomOutAction -= OnZoomOut;
    }

    private void Start()
    {
        OnMapUpdate(mMapSetting);
    }
    
    private void OnZoomIn()
    {
        var nextZoom = Mathf.Min(mMapSetting.mapZoomLevel + 1, VWorldMapSettingConst.ZoomLevelMax);

        if (nextZoom == mMapSetting.mapZoomLevel) return;

        mMapSetting.mapZoomLevel = nextZoom;
        
        OnMapUpdate(mMapSetting);
    }

    private void OnZoomOut()
    {
        var nextZoom = Mathf.Max(mMapSetting.mapZoomLevel - 1, VWorldMapSettingConst.ZoomLevelMin);

        if (nextZoom == mMapSetting.mapZoomLevel) return;
        
        mMapSetting.mapZoomLevel = nextZoom;
        
        OnMapUpdate(mMapSetting);
    }

    private void OnMapUpdate(VWorldMapSetting setting)
    {
        var requestLevel = setting.mapZoomLevel;

        VWorldUtil.ToMipIdx(requestLevel, out var mipIndex);

        vWorldCursor.OnCursorUpdate(mMapSetting);
        vWorldCursor.GetCenterKey(out var centerKey);
        vWorldCursor.GetNeighboringPoints(out var neighborPoints);
        vWorldCursor.GetNeighborKeys(neighborPoints, out var neighborKeys);

        if (!Cache.TryGetValue(centerKey, out var centerTextureMips))
        {
            // center
            vWorldCursor.GetCenterPoint(out var centerPoint);
            
            // dict key add
            Cache.Add(centerKey, new Texture2D[VWorldMapSettingConst.ZoomLevelRange]);
            foreach (var neighborKey in neighborKeys)
            {
                Cache.Add(neighborKey, new Texture2D[VWorldMapSettingConst.ZoomLevelRange]);
            }
            
            // self
            StartCoroutine(CoRequest(centerKey, mMapCenterRawImage, centerPoint, requestLevel, true));
            
            // mip
            for (var level = VWorldMapSettingConst.ZoomLevelMin; level <= VWorldMapSettingConst.ZoomLevelMax; level++)
            {
                if (level == requestLevel)
                {
                    continue;
                }

                StartCoroutine(CoRequest(centerKey, mMapCenterRawImage, centerPoint, level, false));
            }
            
            // neighbor
            for (var i = 0; i < neighborKeys.Length; i++)
            {
                for (var level = VWorldMapSettingConst.ZoomLevelMin; level <= VWorldMapSettingConst.ZoomLevelMax; level++)
                {
                    StartCoroutine(CoRequest(neighborKeys[i], mMapNeighborRawImages[i], neighborPoints[i], level, false));
                }
            }
        }
        
        else
        {
            mMapCenterRawImage.texture = centerTextureMips[mipIndex];
        }
        
        // neighbor

        for (var i = 0; i < neighborKeys.Length; i++)
        {
            var neighborKey = neighborKeys[i];
            
            if (Cache.TryGetValue(neighborKey, out var neighborTextureMips))
            {
                var requestNeighborTexture = neighborTextureMips[mipIndex];

                if (requestNeighborTexture)
                {
                    mMapNeighborRawImages[i].texture = requestNeighborTexture;
                }

                else
                {
                    StartCoroutine(CoApplyWait(neighborKey, mipIndex, mMapNeighborRawImages[i]));
                }
            }

            else
            {
                StartCoroutine(CoApplyWait(neighborKey, mipIndex, mMapNeighborRawImages[i]));
            }
        }
    }

    private IEnumerator CoRequest(string key, RawImage rawImage, VWorldCursorPoint point, int zoomLevel, bool apply)
    {
        VWorldUtil.ToUrl(point, zoomLevel, mMapSetting.mapWidth, mMapSetting.mapHeight, out var url, out var mipIndex);

        using var request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        var texture = DownloadHandlerTexture.GetContent(request);

        Cache[key][mipIndex] = texture;

        if (!apply)
        {
            yield break;
        }
        
        rawImage.texture = texture;
    }

    private IEnumerator CoApplyWait(string key, int mipIndex, RawImage rawImage)
    {
        Texture texture = null;
        
        while (true)
        {
            if (!Cache.TryGetValue(key, out var mips))
            {
                yield return YieldCache.WaitForEndOfFrame;
                
                continue;
            }

            texture = mips[mipIndex];

            if (texture)
            {
                break;
            }

            yield return YieldCache.WaitForEndOfFrame;
        }

        rawImage.texture = texture;
        
        Debug.Log("APPLY");
    }
}
