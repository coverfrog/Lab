using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf.Structs
{
    [Serializable]
    public class SceneField
    {
        [SerializeField] private Object mSceneAsset;
        [SerializeField] private string mSceneName;

        public string SceneName => mSceneName;

        public static implicit operator string(SceneField obj)
        {
            return obj.SceneName;
        }
    }
}
