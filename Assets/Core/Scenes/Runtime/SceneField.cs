using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf.Scenes
{
    [Serializable]
    public class SceneField
    {
        // base info this
        [SerializeField] private Object sceneAsset;
        [SerializeField] private string sceneName;
        
        // get name 
        public string SceneName => sceneName;
        
        // implicit : change Type , sceneField -> string
        public static implicit operator string(SceneField obj)
        {
            return obj.sceneName;
        }
    }
}
