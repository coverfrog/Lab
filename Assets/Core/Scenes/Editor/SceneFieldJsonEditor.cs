#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Cf.Scenes.Editor
{
    public class SceneFieldJsonEditor : EditorWindow
    {
        // < open by menu item >
        [MenuItem("Cf/Scene/Json Editor")]
        public static void EditorOpen()
        {
            _ = GetWindow<SceneFieldJsonEditor>();
        }
        
        // < open by scene field edit >
        public static void EditorOpen(SceneField sceneField)
        {
            EditorOpen();
        }

        // < gui >
        private void OnGUI()
        {
           
        }
    }
}

#endif