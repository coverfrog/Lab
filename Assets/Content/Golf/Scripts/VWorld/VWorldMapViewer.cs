using System;
using System.IO;
using UnityEngine;

public class VWorldMapViewer : MonoBehaviour
{
    [SerializeField] private VWorldMapSetting mSetting;
    
    private void Start()
    {
       Init();
    }

    /// <summary>
    /// 초기화 
    /// </summary>
    private void Init()
    {
        _ = InitStreamingAssetsPath();
        _ = InitHtml();
    }

    /// <summary>
    /// StreamingAssetsPath 검사
    /// </summary>
    /// <returns></returns>
    private static bool InitStreamingAssetsPath()
    {
        if (Directory.Exists(Application.streamingAssetsPath))
        {
            return true;
        }

        Directory.CreateDirectory(Application.streamingAssetsPath);

        return Directory.Exists(Application.streamingAssetsPath);
    }

    /// <summary>
    /// Html 경로 및 파일 검사
    /// </summary>
    /// <returns></returns>
    private bool InitHtml()
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, $"{mSetting.htmlFileName}.html");

        if (!File.Exists(filePath))
        {
            return false;
        }
        
        Application.OpenURL("file:///" + filePath);

        return true;
    }
}