#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Cf.Editor.BuildWindow
{
    /// <summary>
    /// Mode 
    /// </summary>
    public enum BuildMode
    {
        SingleInSceneList,
    }

    /// <summary>
    /// Save Build Info
    /// </summary>
    public class BuildSceneInfo
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Is Exist
        /// </summary>
        public bool IsExist { get; private set; }
        
        /// <summary>
        /// Call 
        /// </summary>
        public string AssetPath { get; private set; }
        
        /// <summary>
        /// Use This When Create Directory
        /// </summary>
        public string LocationPathDirectory { get; private set; }
        
        /// <summary>
        /// Use This When BuildPlayerOptions LocationPathName 
        /// </summary>
        public string LocationPathName { get; private set; }
        
        public BuildSceneInfo(EditorBuildSettingsScene settingsScene, string buildFolderPath)
        {
            // original data
            _ = settingsScene.enabled;
            _ = settingsScene.guid;

            // parse
            AssetPath = settingsScene.path;
            Name = Path.GetFileNameWithoutExtension(AssetPath);
            IsExist = File.Exists(AssetPath);;
            LocationPathDirectory = Path.Combine(buildFolderPath, Name);
            LocationPathName = Path.Combine(LocationPathDirectory, Name) + ".exe";
        }
    }

    /// <summary>
    /// Data Edit On Editor Prefabs
    /// </summary>
    public class BuildWindowData
    {
        public BuildMode BuildMode = BuildMode.SingleInSceneList;
        public BuildTarget BuildTarget = BuildTarget.StandaloneWindows64;
        public BuildTargetGroup BuildTargetGroup = BuildTargetGroup.Standalone;
        public string BuildFolderPath = "";

        public bool SingleInSceneListBoxEdit = false;
        public bool SingleInSceneListVerticalMode = true;
        public int SingleInSceneListScrollHeight = 100;
        public int SingleInSceneListColLength = 4;
        public int SingleInSceneListBoxSize = 20;
        public bool[] SingleInSceneListTargets;
    }

    /// <summary>
    /// Data Controller
    /// </summary>
    public class BuildWindowDataSo : ScriptableObject
    {
        public BuildWindowData Data { get; set; } = new BuildWindowData();
        
        public BuildSceneInfo[] BuildInfos { get; set; }
        
        private void OnEnable()
        {
            Data ??= new BuildWindowData();
            BuildInfos ??= Array.Empty<BuildSceneInfo>();
        }
    }
    
    /// <summary>
    /// Data Update 
    /// </summary>
    public partial class BuildWindow
    {
        private void UpdateSingleInSceneList(BuildWindowData data)
        {
            var scenes = EditorBuildSettings.scenes;
            var infos = scenes
                .Select(s => new BuildSceneInfo(s, _dataSo.Data.BuildFolderPath))
                .ToArray();
            
            var targets = data.SingleInSceneListTargets;
            if (targets == null || targets.Length != infos.Length)
            {
                var newTargets = new bool[infos.Length];

                if (targets != null)
                {
                    for (int i = 0; i < Mathf.Min(targets.Length, newTargets.Length); i++)
                    {
                        newTargets[i] = targets[i];
                    }
                }

                data.SingleInSceneListTargets = newTargets;
            }

            _dataSo.BuildInfos = infos;
        }
    }
}

#endif