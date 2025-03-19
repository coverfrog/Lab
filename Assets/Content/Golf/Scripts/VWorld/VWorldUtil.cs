using System;
using System.Text;
using UnityEngine;


public static class VWorldUtil
{
    public static void ToUrl(VWorldCursorPoint vWorldCursorPoint, int zoomLevel, int mapWidth, int mapHeight, out string url, out int mipIndex)
    {
        var sb = new StringBuilder();
        sb.Append(VWorldMapSettingConst.BaseUrl);
        sb.Append(VWorldMapSettingConst.ApiKey);
        sb.Append("&format=png");
        sb.Append("&basemap=GRAPHIC");
        sb.Append("&center=");
        sb.Append(vWorldCursorPoint.longitude);
        sb.Append(",");
        sb.Append(vWorldCursorPoint.latitude);
        sb.Append("&crs=epsg:");
        sb.Append(VWorldMapSettingConst.Epsg);
        sb.Append("&zoom=");
        sb.Append(zoomLevel);
        sb.Append("&type=");
        sb.Append("w");
        sb.Append("&size=");
        sb.Append(mapWidth);
        sb.Append(",");
        sb.Append(mapHeight);

        url = sb.ToString();

        ToMipIdx(zoomLevel, out mipIndex);
    }
    
    public static void ToMipIdx(int zoomLevel, out int mipIndex)
    {
        mipIndex = zoomLevel - VWorldMapSettingConst.ZoomLevelMin;
    }
    
    public static float LatitudeToMercator(float latitude)
    {
        latitude = Mathf.Clamp(latitude, -85.0f, 85.0f); // 제한된 범위 적용
        float latRad = latitude * Mathf.Deg2Rad;
        return Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latRad / 2)));
    }

    public static float MercatorToLatitude(float mercatorY)
    {
        float clampedValue = Mathf.Clamp(mercatorY, -3.0f, 3.0f); // 제한된 범위 내에서 변환
        return Mathf.Clamp(Mathf.Rad2Deg * (2 * Mathf.Atan(Mathf.Exp(clampedValue)) - Mathf.PI / 2), -85.0f, 85.0f);
    }

}
