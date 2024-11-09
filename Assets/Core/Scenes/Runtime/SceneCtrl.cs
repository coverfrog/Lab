using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Cf.Scenes
{
    public abstract class SceneCtrl : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;

        [Header("Additive")] 
        [SerializeField] private bool isAdditiveLoad;
        [SerializeField] private List<SceneField> additiveSceneList;
        
        private IEnumerator _coAdditiveLoadStart;
        
        protected virtual IEnumerator Start()
        {
            // option check
            if (!isAdditiveLoad)
                yield break;
            
            // get func
            _coAdditiveLoadStart = SceneLoader.AsyncLoad(additiveSceneList, null, null);
            
            // load
            yield return StartCoroutine(_coAdditiveLoadStart);

            // cash null
            _coAdditiveLoadStart = null;
        }
    }
}
