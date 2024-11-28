#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Cf
{
    public class SceneBuildListWindow : EditorWindow
    {
        private int _selectIndex;
        
        // todo : save key 
        private const string PathKey = "Cf_Resource_Save_Key";
        
        // todo : define resource folder
        
        // todo : resource scene list
        
        // todo : select scene
        
        // todo : save
        
        // todo : load
        
        
        // todo : gui call
        [MenuItem("Cf/Scene Build List")]
        public static void Init()
        {
            var window = GetWindow<SceneBuildListWindow>();
        }

        // todo : gui
        private void OnGUI()
        {
            GUILayout.Button("Save");
        }
    }
}
#endif