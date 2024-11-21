using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Cf.Scenes
{
    public sealed class SceneCtrl : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;

        [Title("Additive")] 
        [SerializeField] private bool isAdditiveLoad;
        [SerializeField] private List<SceneField> additiveSceneList;

        [Title("개발 중")] 
        [ShowInInspector] private List<Canvas> _canvasList;
        
        private static SceneField _mainSceneField;
        private IEnumerator _coAdditiveLoadStart;

        private void Awake()
        {
            // find other scene ctrl
            if (_mainSceneField == null)
            {
                
            }
        }

        private IEnumerator Start()
        {
            // option check
            if (!isAdditiveLoad)
            {
                yield break;
            }

            // get func
            _coAdditiveLoadStart = SceneLoader.AsyncLoad(additiveSceneList, null, null);
            
            // load
            yield return StartCoroutine(_coAdditiveLoadStart);

            // cash null
            _coAdditiveLoadStart = null;
        }
    }
}
