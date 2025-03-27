using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rdd.CfUi
{
    public class UIPageLoading : UIPage
    {
        [Header("Reference")] 
        [SerializeField] private Slider mSlider;

        public event Action OnSliderComplete;
        
        /// <summary>
        /// 슬라이더 모든 이벤트 제거
        /// </summary>
        private void Awake()
        {
            mSlider.onValueChanged.RemoveAllListeners();
        }

        /// <summary>
        /// 슬라이더 초기화
        /// 슬라이더 이벤트 추가
        /// </summary>
        public override void OnEnable()
        {
            base.OnEnable();

            // 슬라이더 초기화
            mSlider.value = 0.0f;
            
            // 슬라이더 이벤트 추가
            mSlider.onValueChanged.AddListener(OnValueChanged);
        }

        /// <summary>
        /// 슬라이더 이벤트 제거
        /// </summary>
        private void OnDisable()
        {
            // 슬라이더 이벤트 추가
            mSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// 슬라이더 이벤트가 실제로 적용된 이후에 대한 이벤트 처리
        /// </summary>
        /// <param name="value"></param>
        private void OnValueChanged(float value)
        {
            if (value < 1.0f) return;
            
            OnSliderComplete?.Invoke();
        }
        
        /// <summary>
        /// 슬라이더 값 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValue(float value)
        {
            value = Mathf.Clamp01(value);
            
            mSlider.value = value;
        }
        
        
      
    }
}
