using UnityEngine;

namespace Rdd.CfUi
{
    public abstract class UIPage : MonoBehaviour
    {
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
