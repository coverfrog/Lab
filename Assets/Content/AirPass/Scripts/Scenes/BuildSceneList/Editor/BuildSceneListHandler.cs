#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cf;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace AirPass
{
    public static class BuildSceneListHandler
    {
        [MenuItem("AirPass/Scenes/List Update")]
        private static void ListUpdate()
        {
            GameManager gameManager = null;

            foreach (GameObject o in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                gameManager = o.GetComponent<GameManager>();

                if (gameManager != null)
                {
                    break;
                }
            }

            if (gameManager == null)
            {
                return;
            }

            GameScenesGroup scenesGroup = gameManager.GetSceneGroup(null);

            if (scenesGroup == null)
            {
                // Debug.Log("Game Manager Scene Group Is Null");
                return;
            }

            if (scenesGroup.IsNullContain)
            {
                // Debug.Log("Game Manager Scene Group Is Null Contain");
                return;
            }

            List<SceneField> sceneFieldList = scenesGroup.GetAll();
            List<EditorBuildSettingsScene> buildList = new List<EditorBuildSettingsScene>();
            
            foreach (SceneField sceneField in sceneFieldList)
            {
                string path = AssetDatabase.GetAssetPath(sceneField.SceneAsset);
                
                EditorBuildSettingsScene setting = new EditorBuildSettingsScene()
                {
                    path = path,
                    enabled = true,
                };
                
                buildList.Add(setting);
            }

            EditorBuildSettings.scenes = buildList.ToArray();

            return;
        }
    }
}

#endif