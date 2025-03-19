using System.Text;
using UnityEngine;

public static class VWorldUtil
{
    public static void ToUrl(VWorldCursorPoint vWorldCursorPoint, int zoomLevel, int mapWidth, int mapHeight, out string url, out int cacheIndex)
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
        sb.Append("&size=");
        sb.Append(mapWidth);
        sb.Append(",");
        sb.Append(mapHeight);

        url = sb.ToString();
        cacheIndex = zoomLevel - VWorldMapSettingConst.ZoomLevelMin;
    }
}
