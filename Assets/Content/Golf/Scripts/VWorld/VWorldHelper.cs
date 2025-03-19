using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cf.Yield;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
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
    [Header("Option")] 
    [SerializeField] private VWorldMapSetting mMapSetting;
    
    [Header("Script")] 
    [SerializeField] private VWorldCursor vWorldCursor;
    
    [Header("Reference")] 
    [SerializeField] private RawImage mMapCenterRawImage;

    private static readonly Dictionary<string, Texture2D[]> Cache =
        new Dictionary<string, Texture2D[]>();
    
    private void OnEnable()
    {
        vWorldCursor.OnZoomInAction += OnZoomIn;
        vWorldCursor.OnZoomOutAction += OnZoomOut;
        vWorldCursor.OnMoveAction += OnMove;
    }

    private void OnDisable()
    {
        vWorldCursor.OnZoomInAction -= OnZoomIn;
        vWorldCursor.OnZoomOutAction -= OnZoomOut;
        vWorldCursor.OnMoveAction -= OnMove;
    }

    private void Start()
    {
        OnMapUpdate(mMapSetting);
    }

    private void OnMove(Vector2 prev, Vector2 now)
    {
        vWorldCursor.GetCenterPoint(out var mCenterPoint);
        vWorldCursor.GetSetting(out var setting);

        VWorldUtil.ToMipIdx(mMapSetting.mapZoomLevel, out var mipIndex);
        
        var dir = now - prev;

        var power = Mathf.Clamp01(dir.magnitude / setting.cursorMaxPower);

        var speedRange = setting.cursorMaxSpeed - setting.cursorMinSpeed;

        var speedPercent = 1.0f - Mathf.Clamp01((mipIndex + 1) / (float)VWorldMapSettingConst.ZoomLevelRange);

        var speed = setting.cursorMinSpeed + speedRange * Mathf.Sin(Mathf.PI * 0.5f * speedPercent);
        
        var newPoint =
            new Vector2(
                mCenterPoint.latitude - power * dir.normalized.y * speed,
                mCenterPoint.longitude - power * dir.normalized.x * speed);

        vWorldCursor.SetCenterPoint(newPoint);
        
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
        mMapCenterRawImage.rectTransform.sizeDelta = new Vector2(setting.mapWidth, setting.mapHeight);
        
        var requestLevel = setting.mapZoomLevel;

        VWorldUtil.ToMipIdx(requestLevel, out var mipIndex);

        vWorldCursor.GetCenterKey(out var centerKey);

        if (!Cache.TryGetValue(centerKey, out var centerTextureMips))
        {
            // center
            vWorldCursor.GetCenterPoint(out var centerPoint);
            
            // dict key add
            _ = Cache.TryAdd(centerKey, new Texture2D[VWorldMapSettingConst.ZoomLevelRange]);
            
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
        }
        
        else
        {
            mMapCenterRawImage.texture = centerTextureMips[mipIndex];
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
}
