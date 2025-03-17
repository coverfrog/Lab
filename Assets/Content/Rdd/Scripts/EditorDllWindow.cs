#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Cf.Editors;
using NPOI.Util;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EditorDllWindow : EditorWindow
{
    [MenuItem("Cf/Window/Dll")]
    public static void Init()
    {
        _ = GetWindow<EditorDllWindow>();
    }

    private void OnGUI()
    {
        CfEditorUtil.Gui.ShowScript(this);

        EditorGUILayout.Space();
        
        if (GUILayout.Button("Update", GUILayout.Height(25)))
        {
            UpdateDll();
        }
        
        if (GUILayout.Button("Open", GUILayout.Height(25)))
        {
            OpenFolder();
        }
    }

    private string GetOrgDllDirectory()
    {
        DirectoryInfo dllParentDirectory = Directory.GetParent(Application.dataPath);

        if (dllParentDirectory == null)
        {
            return null;
        }
        
        return Path.Combine(dllParentDirectory.FullName, "Library", "ScriptAssemblies");
    }

    private string GetNewDllDirectory()
    {
        DirectoryInfo dllParentDirectory = Directory.GetParent(Application.dataPath);

        if (dllParentDirectory == null)
        {
            return null;
        }
        
        return Path.Combine(dllParentDirectory.FullName, "Cf Dlls");
    }

    private List<string> GetFilePaths()
    {
        DirectoryInfo dllParentDirectory = Directory.GetParent(Application.dataPath);

        if (dllParentDirectory == null)
        {
            return null;
        }

        string dllDirectory = GetOrgDllDirectory();

        string[] dllFilePaths = Directory.GetFiles(dllDirectory, "*.dll");

        List<string> cfDllFilePaths = new List<string>();
            
        foreach (string dllFilePath in dllFilePaths)
        {
            string dllFileName = Path.GetFileNameWithoutExtension(dllFilePath);

            if (!dllFileName.StartsWith("Cf"))
            {
                continue;
            }

            cfDllFilePaths.Add(dllFilePath);
        }
        
        return cfDllFilePaths;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateDll()
    {
        string dllNewDirectory = GetNewDllDirectory();

        if (Directory.Exists(dllNewDirectory))
        {
            Directory.Delete(dllNewDirectory, true);
        }

        Directory.CreateDirectory(dllNewDirectory);

        List<string> cfDllFilePaths = GetFilePaths();
        int copyCount = 0;
        
        foreach (string cfDllFilePath in cfDllFilePaths)
        {
            try
            {
                File.Copy(cfDllFilePath, $"{dllNewDirectory}/{Path.GetFileName(cfDllFilePath)}");
                ++copyCount;
            }
            catch 
            {
                throw;
            }
        }
        
        Debug.Log($"[Dll] Copy File : {copyCount}");
    }

    private void OpenFolder()
    {
        string dllNewDirectory = GetNewDllDirectory();

        Process.Start(dllNewDirectory);
    }
}

#endif