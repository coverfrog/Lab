using System;
using System.Collections;
using System.Collections.Generic;
using Cf;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AirPass
{
    public class GameManager : Util.Singleton.Mono<GameManager>
    {
        [SerializeField] private bool run;
        [SerializeField] private SceneType startSceneType = SceneType.Title;
        
        private IEnumerator _coNext;
        
        private void Start()
        {
            if (!run)
            {
                return;
            }

            Next(this, startSceneType, true);
        }
        
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
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
            
            yield return null;
            
            _coNext = null;
        }
    }
}
