using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cf.Scenes
{
    public static class SceneLoader
    {
        // load multi scene list
        public static IEnumerator AsyncLoad(List<SceneField> sceneFieldList, Action<float> onUnitProgress,
            Action<float> onTotalProgress)
        {
            // to readonly
            IReadOnlyList<SceneField> fieldList = sceneFieldList;
            
            // sceneField to string
            int count = sceneFieldList.Count;
            List<string> sceneNameList = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                SceneField sceneField = fieldList[i];
                if (sceneField == null)
                    continue;
                
                sceneNameList.Add(sceneField.SceneName);
            }

            // call from string
            return AsyncLoad(sceneNameList, onUnitProgress, onTotalProgress);
        }

        // load multi scene list
        public static IEnumerator AsyncLoad(List<string> sceneNameList, Action<float> onUnitProgress, Action<float> onTotalProgress)
        {
            // check list count
            if (sceneNameList == null || sceneNameList.Count <= 0)
            {
                yield break;
            }

            // to read only
            IReadOnlyList<string> loadSceneList = sceneNameList;
            
            // value
            int loadIndex = 0;
            float segment = 1.0f / loadSceneList.Count;
            
            foreach (string sceneField in loadSceneList)
            {
                // get operation
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneField, LoadSceneMode.Additive);
                if (loadOperation == null)
                {
                    yield break;
                }
                
                // null check
                bool isUnitActionExist = onUnitProgress != null;
                bool isTotalActionExist = onTotalProgress != null;
                
                // run operation
                while (!loadOperation.isDone)
                {
                    if (isUnitActionExist)
                    {
                        float unitPercent = loadOperation.progress / 0.9f;
                        onUnitProgress.Invoke(unitPercent);
                    }

                    if (isTotalActionExist)
                    {
                        float totalPercent = segment * loadIndex + segment * loadOperation.progress / 0.9f;
                        onTotalProgress.Invoke(totalPercent);
                    }

                    yield return null;
                }
                
                // idx
                ++loadIndex;
            }
            
            // active
            foreach (string sceneName in loadSceneList)
            {
                Scene scene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(scene);
            }
        }
    }
}
