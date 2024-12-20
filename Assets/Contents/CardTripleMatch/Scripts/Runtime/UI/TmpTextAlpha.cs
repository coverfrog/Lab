using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace CardTripleMatch.Ui
{
    [RequireComponent(typeof(TMP_Text))]
    public class TmpTextAlpha : MonoBehaviour
    {
        [Header("Options")] 
        [SerializeField] private bool actEnable = true;
        [SerializeField] private float duration = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float startAlpha = 0.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float endAlpha = 1.0f;

        [Header("Events")] 
        [SerializeField] private UnityEvent onAlphaEnd;
        
        private TMP_Text _tmpText;
        
        private IEnumerator _coAlpha;

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        public void OnEnable()
        {
            // todo : check
            if (!actEnable)
            {
                return;
            }

            // todo : start
            BeginAlpha(this, out _);
        }

        public void BeginAlpha()
        {
            BeginAlpha(this, duration, startAlpha, endAlpha, out _);
        }

        public void BeginAlpha(Object sender, out Func<bool> isRun)
        {
            BeginAlpha(sender, duration, startAlpha, endAlpha, out isRun);
        }

        public void BeginAlpha(Object sender, float dur, float start, float end, out Func<bool> isRun)
        {
            // todo : check
            if (_coAlpha != null)
            {
#if UNITY_EDITOR
                Debug.Log("Alpha is running");
#endif
                isRun = null;
                
                return;
            }

            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }

            // todo : start
            isRun = () => _coAlpha != null;
            
            _coAlpha = CoAlpha(sender, dur, start, end);
            StartCoroutine(_coAlpha);
        }

        private IEnumerator CoAlpha(Object sender, float dur, float start, float end)
        {
            // todo : alpha
            for (var t = 0.0f; t < dur; t += Time.deltaTime)
            {
                var percent = t / dur;
                var alpha = Mathf.Lerp(start, end, percent);

                _tmpText.alpha = alpha;
                
                yield return null;
            }

            _tmpText.alpha = end;

            // todo : null
            _coAlpha = null;
            
            // todo : events
            onAlphaEnd?.Invoke();
        }
    }
}
