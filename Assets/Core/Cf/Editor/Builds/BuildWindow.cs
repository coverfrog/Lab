#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    public class BuildWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            _ = GetWindow<BuildWindow>();
        }

        private BuildScenes _buildScenes;

        private void OnEnable()
        {
            
        }

        private void OnGUI()
        {
            _buildScenes = EditorGUILayout.ObjectField("Select", _buildScenes, typeof(BuildScenes), false) as BuildScenes;
        }
    }
}

#endif