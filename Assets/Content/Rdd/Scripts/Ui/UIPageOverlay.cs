using System;
using UnityEngine;

namespace Rdd.CfUi
{
    public abstract class UIPageOverlay : UIPage
    {
        public override UICanvasType GetUICanvasType => UICanvasType.Overlay;

        /// <summary>
        /// UI 매니저의 Stack 에 등록
        /// 제일 Top 으로 Load
        /// 주의 : 닫을 떄는 무조건 UIManager 의 CloseOverlayTop 호출
        /// </summary>
        public virtual void OnEnable()
        {
            // UI 매니저의 Stack 에 등록
            UIManager.Instance.OnOpenOverlay(this);
            
            // 제일 Top 으로 Load
            transform.SetAsLastSibling();
        }

        /// <summary>
        /// Disable 과 다르게 Stack 에서 제외된 경우에만 호출
        /// </summary>
        public virtual void OnPop()
        {
            // 공통 ( 끄기 )
            SetActive(false);
        }
    }
}
