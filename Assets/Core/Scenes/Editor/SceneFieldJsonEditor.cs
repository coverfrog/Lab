#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Cf.Scenes.Editor
{
    public class SceneFieldJsonEditor : EditorWindow
    {
        // < value >
        private static SerializedProperty _jsonPropertyCurrent;
        private static string _json;
        
        // < open by menu item >
        [MenuItem("Cf/Scene/Json Editor")]
        public static void EditorOpen()
        {
            SceneFieldJsonEditor window = GetWindow<SceneFieldJsonEditor>();

        }
        
        // < open by scene field edit >
        public static void EditorOpen(SerializedProperty jsonProperty)
        {
            _jsonPropertyCurrent = jsonProperty;
            
            EditorOpen();
        }

        // < gui >
        private void OnGUI()
        {
            
        }
    }
}

#endif