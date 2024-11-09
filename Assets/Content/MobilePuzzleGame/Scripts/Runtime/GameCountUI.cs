using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Pu
{
    public class GameCountUI : MonoBehaviour
    {
        [SerializeField] [Min(0.01f)] private float segment = 1.0f;
        [SerializeField] [Min(0.0f)] private float minScale = 1.0f;
        [SerializeField] [Min(1.0f)] private float maxScale = 3.0f;
        [Space]
        [SerializeField] private Sprite[] spriteArr;
        
        private Image _img;

        private IEnumerator _coCountDown;
        
        private void Awake()
        {
            // get
            _img = GetComponent<Image>();
            
            // null check
            if (spriteArr == null)
            {
                return;
            }

            // order by sprite name
            spriteArr = spriteArr.OrderByDescending(sprite => sprite.name).ToArray();
        }

        public IEnumerator CountDown(out Func<bool> countDownRun)
        {
            // set
            countDownRun = () => _coCountDown != null;
            
            // active 
            gameObject.SetActive(true);

            // start func
            _coCountDown = CoCountDown();
            StartCoroutine(_coCountDown);

            // return
            return _coCountDown;
        }

        private IEnumerator CoCountDown()
        {
            // define
            int count = spriteArr.Length;
            float calcScale = maxScale - minScale;
            Color color = new Color(1, 1, 1, 0);
            
            // run
            for (int i = 0; i < count; ++i)
            {
                Sprite sprite = spriteArr[i];

                _img.sprite = sprite;
                _img.SetNativeSize();
                
                for (float t = 0.0f; t <= segment; t += Time.deltaTime)
                {
                    float percent = t / segment;
                    Vector3 scale = Vector3.one * Mathf.Lerp(maxScale, minScale, percent);
                    color.a = percent;

                    _img.color = color;
                    transform.localScale = scale;
                    
                    yield return null;
                }

                color.a = 1.0f;

                _img.color = color;
                transform.localScale = Vector3.one * minScale;
            }
            
            // last segment wait
            for (float t = 0.0f; t <= segment; t += Time.deltaTime)
            {
                yield return null;
            }

            // init
            transform.localScale = Vector3.zero;
            gameObject.SetActive(false);

            // cash null
            _coCountDown = null;
        }
    }
}
