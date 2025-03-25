using UnityEngine;

namespace Rdd.CfUi
{
    public abstract class UIPage : MonoBehaviour
    {
        [Header("Option")]
        [SerializeField] private UIPageType mUIPageType;
        
        /// <summary>
        /// 매핑 함수
        /// </summary>
        /// <param name="isActive"></param>
        public void SetActive(bool isActive)
        {
            // Popup 인 경우에는 사용되면 안됨
            if (mUIPageType == UIPageType.Popup)
            {
                Debug.Assert(false, "[UI Page] Popup 타입은 UI Manager 의 CloseOverlayTop 호출");
                return;
            }

            // Active
            gameObject.SetActive(isActive);
        }
        
        /// <summary>
        /// UI 매니저의 Stack 에 등록
        /// 제일 Top 으로 Load
        /// 주의 : 닫을 떄는 무조건 UIManager 의 CloseOverlayTop 호출
        /// </summary>
        public virtual void OnEnable()
        {
            // UI 매니저의 Stack 에 등록
            if (mUIPageType == UIPageType.Popup)
            {
                UIManager.Instance.OnOpenPopup(this);
            }
            
            // 제일 Top 으로 Load
            transform.SetAsLastSibling();
        }

        /// <summary>
        /// Disable 과 다르게 Stack 에서 제외된 경우에만 호출
        /// </summary>
        public virtual void OnClosePopup()
        {
            // 공통 ( 끄기 )
            SetActive(false);
        }
    }
}
