using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hate
{
    public class UiLoading : MonoBehaviour
    {
        [Header(":: Option")] 
        [SerializeField] private bool useWhiteBox;
        
        [Header(":: Reference")]
        [SerializeField] private Image fillImage;
        
        [Header(":: Event")]
        [SerializeField] private UnityEvent<float> onPercent;

        public UnityEvent<float> OnPercent => onPercent;
        
        private void Awake()
        {
            if (!fillImage)
            {
                return;
            }

            // :: white box exist -> create
            if (useWhiteBox)
            {
                GetWhiteSprite(out Sprite whiteSprite);

                fillImage.sprite = whiteSprite;
            }

            // : white box sprite exist -> image option check
            if (fillImage.sprite != null)
            {
                SetImageOption(ref fillImage);
            }
        }

        private static void GetWhiteSprite(out Sprite sprite)
        {
            // :: option
            const int width = 8;
            const int height = 8;
            
            // :: create
            var pixels = new Color[width * height];
            Array.Fill(pixels, Color.white);

            var texture = new Texture2D(width, height);
            texture.SetPixels(pixels);
            texture.Apply();

            var rect = new Rect(0, 0, width, height);

            var pivot = new Vector2(0.5f, 0.5f);

            sprite = Sprite.Create(texture, rect, pivot);
            sprite.name = "White";
        }

        private static void SetImageOption(ref Image image)
        {
            image.fillMethod = Image.FillMethod.Horizontal;
        }
    }
}
