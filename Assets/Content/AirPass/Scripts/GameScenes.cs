using System;
using System.Collections.Generic;
using System.Linq;
using Cf;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace AirPass
{
    [Serializable]
    public class GameScenesGroup
    {
        [SerializeField] private GameScenes common;
        [SerializeField] private GameScenes content;

        public bool IsNullContain => common.IsNullContain || content.IsNullContain;
        
        public SceneField Get(SceneType sceneType)
        {
            SceneField sceneField;
            
            if (common.Get(sceneType, out sceneField))
            {
                return sceneField;
            }

            if (content.Get(sceneType, out sceneField))
            {
                return sceneField;
            }

            return sceneField;
        }

        public List<SceneField> GetAll()
        {
            List<SceneField> all = new List<SceneField>();
            all.AddRange(common.GetAll());
            all.AddRange(content.GetAll());

            return all;
        }
    }

    [CreateAssetMenu(menuName = "AirPass/Game Scenes")]
    public class GameScenes : SerializedScriptableObject
    {
        [Title("")] 
        [SerializeField] private SerializedDictionary<SceneType, SceneField> sceneDict;

        public bool IsNullContain => sceneDict.Any(scene => scene.Value == null);

        public bool Get(SceneType sceneType, out SceneField sceneField)
        {
            return sceneDict.TryGetValue(sceneType, out sceneField);
        }

        public List<SceneField> GetAll()
        {
            return sceneDict.Values.ToList();
        }
    }
}