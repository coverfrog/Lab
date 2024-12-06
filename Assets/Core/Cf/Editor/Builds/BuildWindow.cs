#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Cf.Editor.BuildWindow
{
    public partial class BuildWindow : EditorWindow
    {
        private const string MenuName = "Cf/Window/Build";
        private const string PrefabsKey = "Build_Window_Data_Key";
  
        private static BuildWindowDataSo _dataSo;
        
        [MenuItem(MenuName)]
        private static void Init()
        {
            _ = GetWindow<BuildWindow>();
        }

        private void OnEnable()
        {
            if (_dataSo == null)
            {
                _dataSo = CreateInstance<BuildWindowDataSo>();
                
                if (EditorPrefs.HasKey(PrefabsKey))
                {
                    string json = EditorPrefs.GetString(PrefabsKey);
                    _dataSo.Data = JsonUtility.FromJson<BuildWindowData>(json);
                }
            }
        }

        private void OnDisable()
        {
            if (_dataSo != null)
            {
                DestroyImmediate(_dataSo);
            }
        }

        private void OnDestroy()
        {
            if (_dataSo != null)
            {
                DestroyImmediate(_dataSo);
            }
        }

        private void OnGUI()
        {
            if (!_dataSo)
            {
                return;
            }

            DrawCommon(_dataSo.Data);

            switch (_dataSo.Data.BuildMode)
            {
                case BuildMode.SingleInSceneList:
                    UpdateSingleInSceneList(_dataSo.Data);
                    DrawSingleInSceneList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            string json = JsonUtility.ToJson(_dataSo.Data);
            
            EditorPrefs.SetString(PrefabsKey, json);
        }
    }
}

#endif