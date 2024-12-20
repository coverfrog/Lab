using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Cf.CardTripleMatch
{
    public enum ContentType
    {
        Intro,
        Game,
    }
    
    public class ContentManager : Util.Singleton.Mono<ContentManager>
    {
        private IEnumerator _coRunner;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        #region < Content >

        public void ContentBegin(Object sender, ContentType contentType, out Func<bool> isRun)
        {
            if (_coRunner != null)
            {
#if UNITY_EDITOR
                Debug.Log($"Content Is Running");
#endif
            }

#if UNITY_EDITOR
            Debug.Log($"Content Begin : {contentType}");
#endif
            Func<IEnumerator> co = contentType switch
            {
                ContentType.Intro => CoIntro,
                ContentType.Game => CoGame,
                _ => null,
            };

            if (co == null)
            {
#if UNITY_EDITOR
                Debug.Log($"Error");
#endif
                isRun = null;
                
                return;
            }

            _coRunner = CoRunner(co());
            StartCoroutine(_coRunner);
            
            isRun = () => _coRunner != null;
        }

        private IEnumerator CoRunner(IEnumerator method)
        {
            yield return StartCoroutine(method);

            _coRunner = null;
        }

        private IEnumerator CoIntro()
        {
            yield return null;
        }

        private IEnumerator CoGame()
        {
            yield return null;
        }
        
        #endregion
    }
}
