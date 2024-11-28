using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cf;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AirPass
{
    public class GameManager : Util.Singleton.MonoSerialized<GameManager>
    {
        [Title("")]
        [SerializeField] private bool run;
        [ValueDropdown(nameof(CanStartSceneNames))] [SerializeField] private string startSceneName = SceneType.Title.ToString();
        
        [Title("")] 
        [SerializeField] private GameScenesGroup scenesGroup;

        public GameScenesGroup GetSceneGroup(Object sender) => scenesGroup;
        
        [Title("")]
        [ShowInInspector] [ListDrawerSettings(ShowIndexLabels = true)] [ReadOnly] private static List<CanvasBehaviour> _activeCanvasList;

        private IEnumerator _coNext;

        private static readonly object OnCanvasBehaviourLock = new object();

        public bool IsBuildListUpdate { get; set; }
        
        public static void OnCanvasBehaviour(Object sender, CanvasBehaviour behaviour, bool active)
        {
            lock (OnCanvasBehaviourLock)
            {
                _activeCanvasList ??= new List<CanvasBehaviour>(Util.Enums.GetLength<SceneType>());

                if (active)
                {
                    _activeCanvasList.Add(behaviour);
                }

                else
                {
                    for (int index = _activeCanvasList.Count - 1; index >= 0; index--)
                    {
                        CanvasBehaviour org = _activeCanvasList[index];

                        if (org == behaviour)
                        {
                            _activeCanvasList.RemoveAt(index);
                            break;
                        }
                    }
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            IsBuildListUpdate = true;
        }

        private IEnumerator Start()
        {
            if (!run)
            {
                yield break;
            }

            if (string.IsNullOrEmpty(startSceneName))
            {
#if UNITY_EDITOR
                Debug.Log("Start Scene Name Is Empty");
#endif
                yield break;
            }

            if (!Enum.TryParse(startSceneName, out SceneType startSceneType))
            {
#if UNITY_EDITOR
                Debug.Log("Start Scene Name Is Not Scene Type");
#endif 
                yield break;
            }

            if (scenesGroup == null)
            {
#if UNITY_EDITOR
                Debug.Log("Scenes Is Null");
#endif 
                yield break;
            }

            if (scenesGroup.IsNullContain)
            {
#if UNITY_EDITOR
                Debug.Log("Scenes Dictionary Contain Null");
#endif 
                yield break;
            }

            while (!IsBuildListUpdate)
            {
                yield return null;
            }
            
            Next(this, startSceneType, true);
        }
        
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        private IEnumerable<string> CanStartSceneNames()
        {
            if (Enum.GetValues(typeof(SceneType)) is SceneType[] sceneTypes)
            {
                return sceneTypes
                    .Where(type => (int)type >= 10_000 && (int)type < 20_000)
                    .Select(type => type.ToString());
            }

            return null;
        }

        private void Next(Object sender, SceneType sceneType, bool logPrint = false)
        {
            if (_coNext != null)
            {
#if UNITY_EDITOR
                Debug.Log("Next Is Running");
#endif
                return;
            }

            _coNext = CoNext(sender, sceneType, logPrint);
            StartCoroutine(_coNext);
        }

        private IEnumerator CoNext(Object sender, SceneType sceneType, bool logPrint = false)
        {
#if UNITY_EDITOR
            if (logPrint)
            {
                Debug.Log($"Sender: {sender.name}, SceneType: {sceneType}");
            }
#endif
            SceneField sceneField = scenesGroup.Get(sceneType);
            
            Util.Scenes.Load(sceneField);
            
            yield return null;
            
            _coNext = null;
        }
    }
}
