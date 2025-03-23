using Cf.Pattern;
using UnityEngine;

namespace Rdd.CfUi
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Reference")]
        [SerializeField] private UICanvasOverlay mUICanvasOverlay;

        public UICanvasOverlay UICanvasOverlay => mUICanvasOverlay;
        
        /// <summary>
        /// 컴포넌트 추가
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (!mUICanvasOverlay) mUICanvasOverlay = transform.GetComponentInChildren<UICanvasOverlay>(true);
        }
    }
}
