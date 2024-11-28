using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf
{
    [Serializable]
    public class SceneField
    {
        // < base info this >
        [SerializeField] private Object sceneAsset;
        [SerializeField] private string sceneName;
        
        // < custom user json >
        [SerializeField] private string sceneInfoJson;

        // < get >  
        public string SceneName => sceneName;

        public SceneAsset SceneAsset => (SceneAsset)sceneAsset;
        
        // < implicit >
        public static implicit operator string(SceneField obj)
        {
            return obj.sceneName;
        }
        
        // write
        private void Write()
        {
            
        }
    }
}
