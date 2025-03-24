using System;
using System.Collections.Generic;
using Cf.Pattern;
using UnityEngine;

namespace Rdd.CfUi
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Option")]
        [SerializeField] private UIPage[] mUIStartPages = Array.Empty<UIPage>();
        
        [Header("Reference")]
        [SerializeField] private UICanvasOverlay mUICanvasOverlay;

        /// <summary>
        /// 열린 Overlay 페이지 관리를 할 자료 구조
        /// </summary>
        private readonly Stack<UIPageOverlay> _mUIPageOverlayOpenedStack = new Stack<UIPageOverlay>();

        /// <summary>
        /// Overlay 페이지 열리면 Stack 에 등록
        /// </summary>
        /// <param name="uiPageOverlay"> Overlay 상속받은 페이지 </param>
        public void OnOpenOverlay(UIPageOverlay uiPageOverlay)
        {
            _mUIPageOverlayOpenedStack.Push(uiPageOverlay);
        }

        /// <summary>
        /// Overlay Stack 중에 가장 위에 있는 Page를 Stack 에서 제외
        /// 해당 Page 에게 이벤트 전달
        /// </summary>
        [ContextMenu("[Debug] Close Overlay Top")]
        public void CloseOverlayTop()
        {
            if (!_mUIPageOverlayOpenedStack.TryPop(out var uiPage))
            {
                return;
            }
            
            uiPage.OnPop();
        }

        /// <summary>
        /// 컴포넌트 추가
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (!mUICanvasOverlay) mUICanvasOverlay = transform.GetComponentInChildren<UICanvasOverlay>(true);
        }

        /// <summary>
        /// 시작 페이지 목록들 켜기
        /// </summary>
        private void Start()
        {
            if (mUIStartPages is not { Length: > 0 })
            {
                return;
            }
            
            foreach (UIPage uiPage in mUIStartPages)
            {
                if (!uiPage)
                {
                    continue;
                }
                    
                uiPage.SetActive(true);
            }
        }
    }
}
