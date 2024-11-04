using System.IO;
using UnityEngine;
using UnityEditor;

namespace Cf.Scenes.Editor
{
    public static class SceneAssetEditor
    {
        [MenuItem("Cf/Action/Scene Log Edit", true)]
        private static bool ValidateProcessSceneAssetFile()
        {
            return Selection.activeObject is SceneAsset;
        }

        [MenuItem("Cf/Action/Scene Log Edit")]
        private static void ProcessSceneAssetFile()
        {
            // get select file path
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            
            // check file exist
            if (!File.Exists(path))
            {
                return;
            }
            
            // open asset
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            
            // check asset is scene asset file
            if (sceneAsset == null)
            {
                return;
            }
            
            // file edit
        }
    }
}
