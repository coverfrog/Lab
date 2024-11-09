#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Cf.Scenes.Editor
{
    public class SceneFieldJsonEditor : EditorWindow
    {
        // < value >
        private static SerializedProperty _jsonPropertyCurrent;
        
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
            // todo : create select scene field 
        }
    }
}

#endif