using System;
using System.Collections;
using System.Collections.Generic;
using CardTripleMatch.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cf.CardTripleMatch
{
    public class ContentIntro : MonoBehaviour
    {
        [Header("Options")] 
        [SerializeField] private float duration = 2.0f;
        
        private IEnumerator _coUI;
        
        private IEnumerator Start()
        {
            // todo : start ui
            _coUI = CoUI();
            StartCoroutine(_coUI);
            
            // todo : start data
            ContentManager.Instance.ContentBegin(this, ContentType.Intro, out var beginRun);

            // todo : wait
            yield return new WaitWhile(() => beginRun() || _coUI != null);
            
            // todo : to next scene
            SceneManager.LoadScene("Contents/CardTripleMatch/Scenes/Game");
        }

        private IEnumerator CoUI()
        {
            // todo : wait
            for (var t = 0.0f; t < duration; t += Time.deltaTime)
            {
                yield return null;
            }

            // todo : null
            _coUI = null;
        }
    }
}

