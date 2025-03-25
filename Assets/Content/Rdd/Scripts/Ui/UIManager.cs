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
        [SerializeField] private UIPage[] mUIPages = Array.Empty<UIPage>();


        /// <summary>
        /// UI Page 를 직렬화
        /// </summary>
        private Dictionary<string, UIPage> _mUIPageDict;

        private Dictionary<string, UIPage> UIPageDict
        {
            get
            {
                if (_mUIPageDict != null) return _mUIPageDict;

                _mUIPageDict = new Dictionary<string, UIPage>();
                
                foreach (UIPage uiPage in mUIPages)
                {
                    string typeName = uiPage.GetType().Name;

                    if (_mUIPageDict.TryAdd(typeName, uiPage))
                    {
                        
                    }
                }
                
                return _mUIPageDict;
            }
        }

        /// <summary>
        /// 열린 Popup 페이지 관리를 할 자료 구조
        /// </summary>
        private readonly Stack<UIPage> _mUIPageOverlayOpenedStack = new Stack<UIPage>();

        /// <summary>
        /// Popup 페이지 열리면 Stack 에 등록
        /// </summary>
        /// <param name="uiPage"></param>
        public void OnOpenPopup(UIPage uiPage)
        {
            _mUIPageOverlayOpenedStack.Push(uiPage);
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
            
            uiPage.OnClosePopup();
        }

        /// <summary>
        /// Type 이름 기반으로 조회
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public bool GetPage<T>(out T t) where T : UIPage
        {
            string typeName = typeof(T).Name;

            bool isGet = UIPageDict.TryGetValue(typeName, out UIPage uiPage);

            t = isGet ? 
                (T)uiPage : 
                null;
            
            return isGet;
        }

        /// <summary>
        /// 컴포넌트 추가
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
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
