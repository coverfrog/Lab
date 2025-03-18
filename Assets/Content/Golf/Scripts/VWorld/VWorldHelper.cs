using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;


[Serializable]
public class VWorldMapSettingConst
{
    protected const int InitZoomLevel = 14;
    protected const int InitMapWidth = 512;
    protected const int InitMapHeight = 512;
    
    public const int ZoomLevelMin = 7;
    public const int ZoomLevelMax = 18;
    public const int ZoomLevelRange = ZoomLevelMax - ZoomLevelMin;
}

[Serializable]
public class VWorldMapSetting : VWorldMapSettingConst
{
    private const string ApiKey = "9BB69C3F-772F-36DA-AD38-4A2F7B3D90C4";
    public const string BaseUrl = "http://api.vworld.kr/req/image?service=image&request=getmap&key=";

    public int zoomLevel = InitZoomLevel;
    public int mapWidth = InitMapWidth;
    public int mapHeight = InitMapHeight;

    public string ToUrl(VWorldCursorPoint vWorldCursorPoint)
    {
        var sb = new StringBuilder();
        sb.Append(BaseUrl);
        sb.Append(ApiKey);
        sb.Append("&format=png");
        sb.Append("&basemap=GRAPHIC");
        sb.Append("&center=");
        sb.Append(vWorldCursorPoint.longitude);
        sb.Append(",");
        sb.Append(vWorldCursorPoint.latitude);
        sb.Append("&crs=epsg:4326");
        sb.Append("&zoom=");
        sb.Append(zoomLevel);
        sb.Append("&size=");
        sb.Append(mapWidth);
        sb.Append(",");
        sb.Append(mapHeight);

        return sb.ToString();
    }
}

public class VWorldHelper : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private VWorldMapSetting mMapSetting;
    
    [Header("Script")] 
    [SerializeField] private VWorldCursor vWorldCursor;
    
    [Header("Reference")] 
    [SerializeField] private RawImage mMapRawImage;

    private static readonly Dictionary<VWorldCursorPoint, Texture2D[]> ZoomCacheDict =
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
        OnZoom(mMapSetting);
    }
    
    private void OnZoomIn()
    {
        var nextZoom = Mathf.Min(mMapSetting.zoomLevel + 1, VWorldMapSettingConst.ZoomLevelMax);

        if (nextZoom == mMapSetting.zoomLevel) return;

        mMapSetting.zoomLevel = nextZoom;
        
        OnZoom(mMapSetting);
    }

    private void OnZoomOut()
    {
        var nextZoom = Mathf.Max(mMapSetting.zoomLevel - 1, VWorldMapSettingConst.ZoomLevelMin);

        if (nextZoom == mMapSetting.zoomLevel) return;
        
        mMapSetting.zoomLevel = nextZoom;
        
        OnZoom(mMapSetting);
    }

    private void OnZoom(VWorldMapSetting setting)
    {
        var point = vWorldCursor.GetPoint();

        var zoomLevel = setting.zoomLevel;
        
        if (!ZoomCacheDict.TryGetValue(point, out var textures))
        {
            ZoomCacheDict.Add(point, new Texture2D[VWorldMapSettingConst.ZoomLevelRange]);

            setting.zoomLevel = zoomLevel;
            
            StartCoroutine(CoZoomCacheRequest(setting, point, true));
            
            for (var level = VWorldMapSettingConst.ZoomLevelMin; level < VWorldMapSettingConst.ZoomLevelMax; level++)
            {
                if (level == zoomLevel) continue;

                var newSetting = new VWorldMapSetting()
                {
                    zoomLevel = level,
                    mapHeight = setting.mapHeight,
                    mapWidth = setting.mapWidth,
                };
                    
                StartCoroutine(CoZoomCacheRequest(newSetting, point, false));
            }
        }
        
        else
        {
            Debug.Log("Aplly?" + $"{zoomLevel}");
            mMapRawImage.texture = textures[setting.zoomLevel - VWorldMapSettingConst.ZoomLevelMin];
        }
    }

    private IEnumerator CoZoomCacheRequest(VWorldMapSetting setting, VWorldCursorPoint point, bool apply)
    {
        var url = setting.ToUrl(point);

        using var request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        var texture = DownloadHandlerTexture.GetContent(request);
        
        ZoomCacheDict[point][setting.zoomLevel - VWorldMapSettingConst.ZoomLevelMin] = texture;
        
        if (apply) mMapRawImage.texture = texture;
    }
}
