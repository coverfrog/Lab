using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    [SerializeField] private RawImage mMapRawImage;

    private static readonly Dictionary<VWorldCursorPoint, Texture2D[]> Cache =
        new Dictionary<VWorldCursorPoint, Texture2D[]>();
    
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
        var point = vWorldCursor.GetPoint();

        if (!Cache.TryGetValue(point, out var textures))
        {
            Cache.Add(point, new Texture2D[VWorldMapSettingConst.ZoomLevelRange]);

            var requestLevel = setting.mapZoomLevel;
            
            StartCoroutine(CoRequest(point, requestLevel, true));
            
            for (var level = VWorldMapSettingConst.ZoomLevelMin; level <= VWorldMapSettingConst.ZoomLevelMax; level++)
            {
                if (level == requestLevel)
                {
                    continue;
                }

                StartCoroutine(CoRequest(point, level, false));
            }
        }
        
        else
        {
            mMapRawImage.texture = textures[setting.mapZoomLevel - VWorldMapSettingConst.ZoomLevelMin];
        }
    }

    private IEnumerator CoRequest(VWorldCursorPoint point, int zoomLevel, bool apply)
    {
        VWorldUtil.ToUrl(point, zoomLevel, mMapSetting.mapWidth, mMapSetting.mapHeight, out var url, out var cacheIndex);

        using var request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        var texture = DownloadHandlerTexture.GetContent(request);

        Cache[point][cacheIndex] = texture;

        if (!apply)
        {
            yield break;
        }
        
        mMapRawImage.texture = texture;
    }
}
