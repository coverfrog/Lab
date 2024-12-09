using System;
using System.Collections.Generic;
using UnityEngine;
using Cf;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Cf.TouchGame.Template0
{
    public class ProgressManager : Utils.Util.Singleton.Mono<ProgressManager>
    {
        [SerializeField] private bool useStart = true;
        [SerializeField] private Transform progressesRootTr;
        
        private Dictionary<ProgressType, Progress<ProgressType>> _progressDict;

        private static bool _progressRunning;
        
        protected override bool IsDontDestroyOnLoad()
        {
            return false;
        }

        protected override void Awake()
        {
            base.Awake();

            // todo : root null check
            if (progressesRootTr == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Root Is Null");
#endif
                return;
            }

            // todo : get progress child
            _progressDict = new Dictionary<ProgressType, Progress<ProgressType>>();
            
            foreach (Transform tr in progressesRootTr)
            {
                var p = tr.GetComponent<Progress<ProgressType>>();
                
                if (p == null) continue;

                if (_progressDict.TryAdd(p, p)) continue;

#if UNITY_EDITOR
                Debug.LogError("Key Is Overlap");
#endif
                return;
            }

            // todo : check get all
            var dictCount = _progressDict.Count;
            var enumLength = Utils.Util.Enums.GetLength<ProgressType>();
            
            if (dictCount != enumLength)
            {
#if UNITY_EDITOR
                Debug.LogError($"Key Is Missing : {enumLength - dictCount}");
                return;
#endif
            }

            // todo : progress start
#if UNITY_EDITOR
            Debug.Log($"Progress Start");
#endif

            _progressRunning = true;
        }

        private void Start()
        {
            // todo : check start
            if (!_progressRunning || !useStart)
            {
                return;
            }
            
            // todo : off all progress
            foreach (var progress in _progressDict.Values)
            {
                progress.gameObject.SetActive(false);
            }
            
            // todo : start first step
            _progressDict[ProgressType.Title].Begin(this);
        }

        public void ToNext(Object sender, ProgressType nextType)
        {
            _progressDict[nextType].Begin(sender);
        }
    }
}
