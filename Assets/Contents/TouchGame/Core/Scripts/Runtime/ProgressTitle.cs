using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cf.TouchGame.Template0
{
    public class ProgressTitle : Progress<ProgressType>
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip actClip;

        private bool _animExist;
        
        protected override ProgressType TypeName => ProgressType.Title;

        private void OnEnable()
        {
            _animExist = false;
            
            if (animator == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Animator Is Null");
#endif
                return;
            }

            if (animator.runtimeAnimatorController == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Animator Controller Is Null");
#endif
                return;
            }

            if (actClip == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Clip Is Null");
#endif
                return;
            }

            _animExist = true;
        }

        private IEnumerator Start()
        {
            if (!_animExist)
            {
                yield break;
            }

            for (float _ = 0.0f; _ < actClip.length; _ += Time.deltaTime)
            {
                yield return null;
            }
            
            ProgressManager.Instance.ToNext(this, ProgressType.ConceptVideo);
            
            gameObject.SetActive(false);
        }
    }
}
