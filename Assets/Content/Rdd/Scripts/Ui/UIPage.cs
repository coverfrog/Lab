using UnityEngine;

namespace Rdd.CfUi
{
    public abstract class UIPage : MonoBehaviour
    {
        /// <summary>
        /// UI Canvas Type 지정
        /// </summary>
        public abstract UICanvasType GetUICanvasType { get; }
        
        /// <summary>
        /// 매핑 함수
        /// </summary>
        /// <param name="isActive"></param>
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
