using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Cf
{
    public static partial class Util
    {
        public static class Scenes
        {
            public static void Load(SceneField sceneField)
            {
                SceneManager.LoadScene(sceneField);
            }
        }
    }
}