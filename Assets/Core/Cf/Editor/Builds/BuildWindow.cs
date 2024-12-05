#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    public class BuildWindow : EditorWindow
    {
        #region < Prefabs Key >
        public class PrefabsKey
        {
            public const string ModeKey = "BuildWindow_Mode_Key";
            public const string TargetKey = "BuildWindow_Target_Key";
            public const string TargetGroupKey = "BuildWindow_Target_Group_Key";
            public const string BuildPathKey = "BuildWindow_BuildPath_Key";
            
            public const string InSceneListIsBuildAllKey = "BuildWindow_InSceneList_IsBuildAll_Key";
            public const string InSceneListBuildIndexListStr = "BuildWindow_InSceneList_BuildIndexListStr_Key";
            public const string InSceneListColLength = "BuildWindow_InSceneList_ColLength_Key";
            public const string InSceneListBoxSize = "BuildWindow_InSceneList_BoxSize_Key";
        }

        #endregion
        
        // common
        private BuildMode _buildMode = BuildMode.SingleInSceneList;
        private BuildTarget _buildTarget = BuildTarget.StandaloneWindows64;
        private BuildTargetGroup _buildTargetGroup = BuildTargetGroup.Standalone;
        private string _buildFolderPath = "";

        // SingleInSceneList
        private int _inSceneListColLength = 4;
        private int _inSceneListBoxSize = 30;
        private bool _inSceneListIsBuildAll = true;
        private bool[] _inSceneListBuildTargetIndexes;
        
        
        [MenuItem("Cf/Window/Build")]
        public static void ShowWindow()
        {
            _ = GetWindow<BuildWindow>("Build Window");
        }

        private void OnEnable()
        {
            // todo : load mode
            
            if (EditorPrefs.HasKey(PrefabsKey.ModeKey))
            {
                _buildMode = (BuildMode)EditorPrefs.GetInt(PrefabsKey.ModeKey);
            }

            if (EditorPrefs.HasKey(PrefabsKey.TargetKey))
            {
                _buildTarget = (BuildTarget)EditorPrefs.GetInt(PrefabsKey.TargetKey);
            }
            
            if (EditorPrefs.HasKey(PrefabsKey.TargetGroupKey))
            {
                _buildTargetGroup = (BuildTargetGroup)EditorPrefs.GetInt(PrefabsKey.TargetGroupKey);
            }
            
            if (EditorPrefs.HasKey(PrefabsKey.BuildPathKey))
            {
                _buildFolderPath = EditorPrefs.GetString(PrefabsKey.BuildPathKey);
            }
            
            //
            
            if (EditorPrefs.HasKey(PrefabsKey.InSceneListIsBuildAllKey))
            {
                _inSceneListIsBuildAll = EditorPrefs.GetBool(PrefabsKey.InSceneListIsBuildAllKey);
            }

            if (EditorPrefs.HasKey(PrefabsKey.InSceneListColLength))
            {
                _inSceneListColLength = EditorPrefs.GetInt(PrefabsKey.InSceneListColLength);
            }

            if (EditorPrefs.HasKey(PrefabsKey.InSceneListBoxSize))
            {
                _inSceneListBoxSize = EditorPrefs.GetInt(PrefabsKey.InSceneListBoxSize);
            }

            if (EditorPrefs.HasKey(PrefabsKey.InSceneListBuildIndexListStr))
            {
                string str = EditorPrefs.GetString(PrefabsKey.InSceneListBuildIndexListStr);

                if (!string.IsNullOrEmpty(str))
                {
                    string[] split = str.Split('_');

                    _inSceneListBuildTargetIndexes = new bool[split.Length];

                    for (var i = 0; i < split.Length; i++)
                    {
                        string s = split[i];
                        bool isSelect = s.ToLower() == "true";

                        _inSceneListBuildTargetIndexes[i] = isSelect;
                    }
                }
            }
        }

        private void OnGUI()
        {
            // todo : common
            HeaderBase("Build Options");
            
            SelectWindowMode();
            SelectBuildTarget();
            SelectBuildTargetGroup();
            SelectBuildFolderPath();

            // todo : mode
            SpaceBase();

            switch (_buildMode)
            {
                case BuildMode.SingleInSceneList:
                    SingleInSceneList();
                    break;
                
                case BuildMode.Multiple:
                    break;
          
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region < Base Gui >

        private void HeaderBase(string str)
        {
            GUILayout.Label(str, new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
            });    
        }
        
        private void SpaceBase(uint i = 1)
        {
            EditorGUILayout.Space(5 * i);
        }

        private void SelectBase(string str, Action onAction)
        {
            GUILayout.BeginHorizontal();

            try
            {
                GUILayout.Label(str, GUILayout.Width(115));

                onAction?.Invoke();
            }

            finally
            {
                GUILayout.EndHorizontal();
            }
        }

        private void RunBase(Action onAction)
        {
            SpaceBase();
            
            HeaderBase("Methods");
            
            onAction?.Invoke();
        }

        private void BtnBase(string str, Action onAction)
        {
            var btnStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 200,
                fixedHeight = 30,
                alignment = TextAnchor.MiddleLeft,
            };
            
            if (!GUILayout.Button($" > {str}", btnStyle))
            {
                return;
            }
            
            onAction?.Invoke();
        }

        #endregion

        #region < Build Options >

        private void SelectWindowMode()
        {
            SelectBase("Build Mode", delegate
            {
                // todo : show enum popup
                
                _buildMode = (BuildMode)EditorGUILayout.EnumPopup(_buildMode);
                
                // todo : save data at editor
            
                EditorPrefs.SetInt(PrefabsKey.ModeKey, (int)_buildMode);
            });
        }

        private void SelectBuildTarget()
        {
            SelectBase("Build Target", delegate
            {
                // todo : show enum popup
                
                _buildTarget = (BuildTarget)EditorGUILayout.EnumPopup(_buildTarget);
                
                // todo : save data at editor
            
                EditorPrefs.SetInt(PrefabsKey.TargetKey, (int)_buildTarget);
            });
        }

        private void SelectBuildTargetGroup()
        {
            SelectBase("Build Target Group", delegate
            {
                // todo : show enum popup
                
                _buildTargetGroup = (BuildTargetGroup)EditorGUILayout.EnumPopup(_buildTargetGroup);
                
                // todo : save data at editor
            
                EditorPrefs.SetInt(PrefabsKey.TargetGroupKey, (int)_buildTargetGroup);
            });
        }

        private void SelectBuildFolderPath()
        {
            SelectBase("Build Path", delegate
            {
                // todo : text view and edit
                
                _buildFolderPath = GUILayout.TextField(_buildFolderPath);
                
                if (GUILayout.Button("...", GUILayout.Width(30)))
                {
                    // todo : select folder

                    _buildFolderPath = EditorUtility.OpenFolderPanel("Select Build Folder Path", "", "");
                    
                    // todo : save data at editor
            
                    EditorPrefs.SetString(PrefabsKey.BuildPathKey, _buildFolderPath);
                }
            });
        }

        #endregion

        #region < Single In Scene List >

        private void SingleInSceneList()
        {
            SingleInSceneListData(out BuildSceneInfo[] buildSceneInfos);

            SingleInSceneListOptions(buildSceneInfos);
            SingleInSceneListMethods(buildSceneInfos);
            SingleInSceneListView(buildSceneInfos);
        }

        private void SingleInSceneListData(out BuildSceneInfo[] buildSceneInfos)
        {
            // todo : build setting info get convert user data
            // todo : convert all data, contains null
    
            // scenes in build settings
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            BuildSceneInfo[] infos = new BuildSceneInfo[scenes.Length];

            // parse
            for (int index = 0; index < scenes.Length; index++)
            {
                // target
                EditorBuildSettingsScene settingsScene = scenes[index];

                // original data get assetPath
                string assetPath = settingsScene.path;
                
                // check exist by path
                // null  => add 'null' ( is not empty ) ( why ? optional check auto clean )
                // exist => add 'data' 
                bool exist = File.Exists(assetPath);
                
                // parse
                BuildSceneInfo info = new BuildSceneInfo(settingsScene, _buildFolderPath, exist);
                infos[index] = info;
            }
            
            // todo : apply

            buildSceneInfos = infos;

            if (_inSceneListBuildTargetIndexes == null || _inSceneListBuildTargetIndexes.Length != infos.Length)
            {
                bool[] newTargetIndexes = new bool[infos.Length];

                if (_inSceneListBuildTargetIndexes != null)
                {
                    for (int i = 0; i < Mathf.Min(_inSceneListBuildTargetIndexes.Length, newTargetIndexes.Length); i++)
                    {
                        newTargetIndexes[i] = _inSceneListBuildTargetIndexes[i];
                    }
                }

                _inSceneListBuildTargetIndexes = newTargetIndexes;
            }
        }

        private void SingleInSceneListOptions(BuildSceneInfo[] buildSceneInfos)
        {
            // draw header
            HeaderBase("Single In Scene List");
            
            // null check
            if (buildSceneInfos == null)
            {
                SelectBase("None Register In Build Scene List", null);

                return;
            }
            
            // draw options
            
            // option - build all
            SelectBase("Build All", delegate
            {
                // todo : show option
                _inSceneListIsBuildAll = EditorGUILayout.Toggle(_inSceneListIsBuildAll);
                    
                // todo : save
                EditorPrefs.SetBool(PrefabsKey.InSceneListIsBuildAllKey, _inSceneListIsBuildAll);
            });
            
            // option - define target
            
            if (_inSceneListIsBuildAll)
            {
                return;
            }
            
            SelectBase("Col Length", delegate
            {
                // todo : show option
                _inSceneListColLength = EditorGUILayout.IntSlider(_inSceneListColLength, 1, 30);
                
                // todo : save
                EditorPrefs.SetInt(PrefabsKey.InSceneListColLength, _inSceneListColLength);
            });
            
            SelectBase("Box Size", delegate
            {
                // todo : show option
                
                _inSceneListBoxSize = EditorGUILayout.IntSlider(_inSceneListBoxSize, 25, 120);
                
                // todo : save
                EditorPrefs.SetInt(PrefabsKey.InSceneListBoxSize, _inSceneListBoxSize);
            });
            
            SelectBase("Build Index List", null);

            int colLength = _inSceneListColLength;
            int quo = buildSceneInfos.Length / colLength;
            int remain = buildSceneInfos.Length % colLength;
            
            for (int i = 0; i < quo; i++)
            {
                GUILayout.BeginHorizontal();

                try
                {
                    for (int j = 0; j < colLength; j++)
                    {
                        int idx = colLength * i + j;
                        SingleInSceneListDrawIndexBox(idx);
                    }

                }
                
                finally
                {
                    GUILayout.EndHorizontal();

                }
            }

            if (quo > 0)
            {
                for (int i = 0; i < remain; i++)
                {
                    GUILayout.BeginHorizontal();

                    try
                    {
                        int idx = colLength * quo + i;
                        SingleInSceneListDrawIndexBox(idx);
                    }

                    finally
                    {
                        GUILayout.EndHorizontal();
                    }
                }
            }

            else
            {
                GUILayout.BeginHorizontal();

                try
                {
                    for (int i = 0; i < remain; i++)
                    {
                        int idx = colLength * quo + i;
                        SingleInSceneListDrawIndexBox(idx);
                    }
                }
                
                finally
                {
                    GUILayout.EndHorizontal();
                }
            }

            if (_inSceneListBuildTargetIndexes.Length <= 0)
            {
                return;
            }

            string str = _inSceneListBuildTargetIndexes
                .Select(b => b.ToString())
                .Aggregate((s0, s) => s0 += $"_{s}")
                .ToString();
            
            EditorPrefs.SetString(PrefabsKey.InSceneListBuildIndexListStr, str);
        }

        private void SingleInSceneListDrawIndexBox(int idx)
        {
            _inSceneListBuildTargetIndexes[idx] = 
                GUILayout.Toggle(_inSceneListBuildTargetIndexes[idx], 
                    $"{idx}",
                    new GUIStyle(GUI.skin.button), 
                    GUILayout.Width(_inSceneListBoxSize), 
                    GUILayout.Height(_inSceneListBoxSize));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void SingleInSceneListMethods(BuildSceneInfo[] buildSceneInfos)
        {
            // draw header
            SpaceBase();
            HeaderBase("Methods");
            
            // draw methods
            BtnBase("Remove None Exists", delegate
            {
                var origin = EditorBuildSettings.scenes;
                var newScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes.Length);
                var removeCount = 0;
                
                for (var i = buildSceneInfos.Length - 1; i >= 0; i--)
                {
                    var info = buildSceneInfos[i];

                    if (!info.IsExist)
                    {
                        removeCount++;
                        continue;
                    }
                    
                    newScenes.Add(origin[i]);
                }

                EditorBuildSettings.scenes = newScenes.ToArray();
                
                Debug.Log($"Remove Count : {removeCount}");
            });
            
            if (_inSceneListIsBuildAll)
            {
                BtnBase("Build All", delegate
                {
                    SingleInSceneListBuild(buildSceneInfos);
                });
            }

            else
            {
                BtnBase("Build From Targets", delegate
                {
                    SingleInSceneListBuild(buildSceneInfos);
                });
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void SingleInSceneListBuild(BuildSceneInfo[] buildSceneInfos)
        {
            // null check
            
            if (string.IsNullOrEmpty(_buildFolderPath))
            {
                return;
            }

            if (!Directory.Exists(_buildFolderPath))
            {
                Directory.CreateDirectory(_buildFolderPath);
            }

            // parse index
            
            List<int> targetIndexList;

            if (_inSceneListIsBuildAll)
            {
                targetIndexList = Enumerable.Range(0, buildSceneInfos.Length).ToList();
            }

            else
            {
                targetIndexList = new List<int>();
                
                for (var i = 0; i < _inSceneListBuildTargetIndexes.Length; i++)
                {
                    bool isTarget = _inSceneListBuildTargetIndexes[i];

                    if (!isTarget)
                    {
                        continue;
                    }
                    
                    targetIndexList.Add(i);
                }
            }
            
            // targets

            List<BuildSceneInfo> targetInfos = new List<BuildSceneInfo>();
            
            foreach (int i in targetIndexList)
            {
                var info = buildSceneInfos[i];
                
                targetInfos.Add(info);
            }
            
            // null check

            if (targetInfos.Any(info => !info.IsExist))
            {
                Debug.Log("Is Contain None Exist Scene");
                
                return;
            }
            
            // build

            var scenes = EditorBuildSettings.scenes;
            
            foreach (var scene in scenes)
            {
                scene.enabled = false;
            }
            
            for (var i = 0; i < targetInfos.Count; i++)
            {
                if (i > 0)
                {
                    scenes[i - 1].enabled = false;
                }

                var info = buildSceneInfos[i];

                if (!Directory.Exists(info.LocationPathDirectory))
                {
                    Directory.CreateDirectory(info.LocationPathDirectory);
                }

                scenes[i].enabled = true;

                var options = new BuildPlayerOptions()
                {
                    scenes = new string[1] { info.AssetPath },
                    locationPathName = info.LocationPathName,
                    target = _buildTarget,
                    targetGroup = _buildTargetGroup,
                    options = BuildOptions.None,
                };

                BuildPipeline.BuildPlayer(options);

                EditorBuildSettings.scenes = scenes;
            }
        }

        private void SingleInSceneListView(BuildSceneInfo[] buildSceneInfos)
        {
            // draw header
            SpaceBase();
            HeaderBase("View");
            
            var strStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                richText = true,
            };

            // draw in header
            
            const float width0 = 50;
            const float width1 = 50;

            EditorGUILayout.BeginHorizontal();
            try
            {
                EditorGUILayout.LabelField("[ index ]", strStyle, GUILayout.Width(width0));
                EditorGUILayout.LabelField("[ Exist ]", strStyle, GUILayout.Width(width1));
                EditorGUILayout.LabelField("[ Name ]", strStyle, GUILayout.ExpandWidth(true));
            }
            
            finally
            {
                EditorGUILayout.EndHorizontal();    
            }
            
            // draw body

            for (var index = 0; index < buildSceneInfos.Length; index++)
            {
                BuildSceneInfo info = buildSceneInfos[index];
                EditorGUILayout.BeginHorizontal();
                try
                {
                    EditorGUILayout.LabelField($"{index}", strStyle, GUILayout.Width(width0));
                    EditorGUILayout.LabelField($"{(info.IsExist ? "True" : $"<color=#FF0000>False</color>")}", strStyle, GUILayout.Width(width1));
                    EditorGUILayout.LabelField($"{info.SceneName}", strStyle, GUILayout.ExpandWidth(true));
                }
                
                finally
                {
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        #endregion
    }
}

#endif