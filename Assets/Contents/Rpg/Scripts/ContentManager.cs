using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf.Rpg
{
    public class ContentManager : Util.Singleton.Mono<ContentManager>
    {
        private IEnumerator _coContent;

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        public void Init()
        {
            if (_coContent != null) StopCoroutine(_coContent);

            _coContent = null;
        }

        public void ContentBegin(Object sender, ContentType contentType, out Func<bool> isRun)
        {
            if (_coContent != null)
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
                ContentType.Game => CoContentGame,
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

            _coContent = co();
            StartCoroutine(_coContent);
            
            isRun = () => _coContent != null;
        }

        private IEnumerator CoContentGame()
        {
            yield return null;
        }
    }
}
