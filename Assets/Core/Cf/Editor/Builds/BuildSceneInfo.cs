#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    [Serializable]
    public class BuildSceneInfo 
    {
        public bool IsExist { get; private set; }
        
        public bool IsEnabled { get; private set; }
        
        public GUID Guid { get; private set; }
        
        public string AssetPath { get; private set; }
        
        public string BuildFolderPath { get; private set; }
        
        public string SceneName { get; private set; }
        
        /// <summary>
        /// Use This When Create Directory
        /// </summary>
        public string LocationPathDirectory { get; private set; }
        
        /// <summary>
        /// Use Thiw When BuildPlayerOptions LocationPathName 
        /// </summary>
        public string LocationPathName { get; private set; }
        
        public BuildSceneInfo(EditorBuildSettingsScene settingsScene, string buildFolderPath, bool isExist)
        {
            // todo : copy data
            
            // optional data
            IsExist = isExist;
            
            // original data
            IsEnabled = settingsScene.enabled;
            Guid = settingsScene.guid;
            AssetPath = settingsScene.path;
            BuildFolderPath = buildFolderPath;
            
            // parse
            SceneName = Path.GetFileNameWithoutExtension(AssetPath);
            LocationPathDirectory = Path.Combine(BuildFolderPath, SceneName);
            LocationPathName = Path.Combine(LocationPathDirectory, SceneName) + ".exe";
        }
    }
}

#endif