#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Cf.Editor.BuildWindow
{
    public partial class BuildWindow
    {
        /// Draw 
        
        #region < Base >

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

        #region < Common >

        private void DrawCommon(BuildWindowData data)
        {
            HeaderBase("Build Options");

            SelectBase("Build Mode",
                delegate { data.BuildMode = (BuildMode)EditorGUILayout.EnumPopup(data.BuildMode); });

            SelectBase("Build Target",
                delegate { data.BuildTarget = (BuildTarget)EditorGUILayout.EnumPopup(data.BuildTarget); });

            SelectBase("Build Target Group",
                delegate
                {
                    data.BuildTargetGroup = (BuildTargetGroup)EditorGUILayout.EnumPopup(data.BuildTargetGroup);
                });

            SelectBase("Build Path", delegate
            {
                data.BuildFolderPath = GUILayout.TextField(data.BuildFolderPath);

                if (GUILayout.Button("...", GUILayout.Width(30)))
                {
                    data.BuildFolderPath = EditorUtility.OpenFolderPanel("Select Build Folder Path", "", "");
                }
            });
        }

        #endregion

        #region < In Scene List >

        private Vector2 _drawSingleInSceneListBuildListScrollValue;

        private void DrawSingleInSceneList()
        {
            DrawSingleInSceneListTargetScenes();
            DrawSingleInSceneListMethods();
        }

        private void DrawSingleInSceneListTargetScenes()
        {
            var data = _dataSo.Data;
            var infos = _dataSo.BuildInfos;

            HeaderBase("Target Scenes");

            SelectBase("Edit Options",
                delegate { data.SingleInSceneListBoxEdit = EditorGUILayout.Toggle(data.SingleInSceneListBoxEdit); });

            if (data.SingleInSceneListBoxEdit)
            {
                SelectBase("    [] Vertical Mode",
                    delegate
                    {
                        data.SingleInSceneListVerticalMode = EditorGUILayout.Toggle(data.SingleInSceneListVerticalMode);
                    });

                if (data.SingleInSceneListVerticalMode)
                {
                    SelectBase("    [] Scroll Height", delegate
                    {
                        data.SingleInSceneListScrollHeight =
                            EditorGUILayout.IntSlider(data.SingleInSceneListScrollHeight, 40, 1000);
                    });
                }

                else
                {
                    SelectBase("    [] Col Length",
                        delegate
                        {
                            data.SingleInSceneListColLength =
                                EditorGUILayout.IntSlider(data.SingleInSceneListColLength, 1, 30);
                        });
                }

                SelectBase("    [] Box Size",
                    delegate
                    {
                        data.SingleInSceneListBoxSize =
                            EditorGUILayout.IntSlider(data.SingleInSceneListBoxSize, 25, 120);
                    });
            }

            if (data.SingleInSceneListBoxEdit)
            {
                SpaceBase();
            }

            SelectBase($"Build Index List", null);

            if (data.SingleInSceneListVerticalMode)
            {
                DrawSingleInSceneListVertical();
            }

            else
            {
                DrawSingleInSceneListHorizontal();
            }
        }

        private void DrawSingleInSceneListVertical()
        {
            var data = _dataSo.Data;
            var infos = _dataSo.BuildInfos;

            _drawSingleInSceneListBuildListScrollValue =
                GUILayout.BeginScrollView(
                    _drawSingleInSceneListBuildListScrollValue, 
                    GUILayout.Height(data.SingleInSceneListScrollHeight));

            try
            {
                for (int i = 0; i < infos.Length; i++)
                {
                    var info = infos[i];
                
                    GUILayout.BeginHorizontal();

                    try
                    {
                        DrawSingleInSceneListIdxBox(i);
                        EditorGUILayout.LabelField(info.Name, GUILayout.Height(data.SingleInSceneListBoxSize));

                    }

                    finally
                    {
                        GUILayout.EndHorizontal();
                    }
                }
            }
            
            finally
            {
                GUILayout.EndScrollView();
            }
        }
        
        private void DrawSingleInSceneListHorizontal()
        {
            var data = _dataSo.Data;
            var infos = _dataSo.BuildInfos;
            
            int colLength = data.SingleInSceneListColLength;
            int infosLength = infos.Length;
                
            int quo = infosLength / colLength;
            int remain = infosLength % colLength;

            for (int i = 0; i < quo; i++)
            {
                GUILayout.BeginHorizontal();

                try
                {
                    for (int j = 0; j < colLength; j++)
                    {
                        int idx = colLength * i + j;
                        DrawSingleInSceneListIdxBox(idx);
                    }
                }

                finally
                {
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.BeginHorizontal();

            try
            {
                for (int i = 0; i < remain; i++)
                {
                    int idx = colLength * quo + i;
                    DrawSingleInSceneListIdxBox(idx);
                }
            }

            finally
            {
                GUILayout.EndHorizontal();
            }
        }

        private void DrawSingleInSceneListIdxBox(int idx)
        {
            var data = _dataSo.Data;
            var style = new GUIStyle(GUI.skin.button)
            {

            };
            
            data.SingleInSceneListTargets[idx] = 
                GUILayout.Toggle(data.SingleInSceneListTargets[idx], 
                $"{idx}",
                style, 
                GUILayout.Width(data.SingleInSceneListBoxSize), 
                GUILayout.Height(data.SingleInSceneListBoxSize));
        }

        private void DrawSingleInSceneListMethods()
        {
            var data = _dataSo.Data;
            var infos = _dataSo.BuildInfos;
            
            HeaderBase("Methods");
            
            BtnBase("Remove Missing", delegate
            {
                var list = EditorBuildSettings.scenes.ToList();
                var removeCount = 0;

                for (var i = infos.Length - 1; i >= 0; i--)
                {
                    if (!File.Exists(list[i].path))
                    {
                        list.RemoveAt(i);
                    }
                }

                EditorBuildSettings.scenes = list.ToArray();
 
                Debug.Log($"Remove Count : {removeCount}");
            });

            var isAllSelect = data.SingleInSceneListTargets.All(b => b);
            var selectStr = isAllSelect ? "UnSelect All" : "Select All";
            
            BtnBase(selectStr, delegate
            {
                for (var i = 0; i < data.SingleInSceneListTargets.Length; i++)
                {
                    data.SingleInSceneListTargets[i] = !isAllSelect;
                }
            });
            
            BtnBase("Build", delegate
            {
                // 
                if (string.IsNullOrEmpty(data.BuildFolderPath))
                {
                    Debug.LogError("Build Path Null");
                    return;
                }
                
                if (infos.Any(i => !i.IsExist))
                {
                    Debug.LogError("Build List Contain Missing Scenes");
                    return;
                }

                if (!Directory.Exists(data.BuildFolderPath))
                {
                    Directory.CreateDirectory(data.BuildFolderPath);
                }

                //
                
                var targetInfos = new List<BuildSceneInfo>();
                
                for (var i = 0; i < data.SingleInSceneListTargets.Length; i++)
                {
                    if (data.SingleInSceneListTargets[i])
                    {
                        targetInfos.Add(infos[i]);
                    }
                }

                if (targetInfos.Count <= 0)
                {
                    return;
                }

                foreach (var info in targetInfos)
                {
                    if (!Directory.Exists(info.LocationPathDirectory))
                    {
                        Directory.CreateDirectory(info.LocationPathDirectory);
                    }
                    
                    var options = new BuildPlayerOptions()
                    {
                        scenes = new string[1] { info.AssetPath },
                        locationPathName = info.LocationPathName,
                        target = data.BuildTarget,
                        targetGroup = data.BuildTargetGroup,
                        options = BuildOptions.None,
                    };
                    
                    BuildPipeline.BuildPlayer(options);
                }
                
                //
                
                OnEnable();
            });
        }

        #endregion
    }
}

#endif