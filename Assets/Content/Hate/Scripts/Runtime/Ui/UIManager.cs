using System;
using System.Collections.Generic;
using Cf;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Hate
{
    public class UIManager : Util.Singleton.Mono<UIManager>
    {
        private Canvas _mOverlayCanvas;
        private CanvasScaler _mOverlayCanvasScaler;
        private GraphicRaycaster _mOverlayGraphicRaycaster;
        
        private UIOverlay _mOverlayCurrent;
        
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }

        public void Init(Object inSender)
        {
            _mOverlayCanvas = new GameObject("Overlay Canvas").AddComponent<Canvas>();
            _mOverlayCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _mOverlayCanvas.transform.SetParent(transform);
            
            _mOverlayCanvasScaler = _mOverlayCanvas.gameObject.AddComponent<CanvasScaler>();
            _mOverlayCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            _mOverlayCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            _mOverlayCanvasScaler.referenceResolution = new Vector2(1920, 1080);
            _mOverlayCanvasScaler.matchWidthOrHeight = 1.0f;

            _mOverlayGraphicRaycaster = _mOverlayCanvas.gameObject.AddComponent<GraphicRaycaster>();
        }

        public void SetOverlay(Object inSender, UIOverlay inOverlay)
        {
            if (_mOverlayCurrent)
            {
                Destroy(_mOverlayCurrent.gameObject);
            }

            _mOverlayCurrent = Instantiate(inOverlay, _mOverlayCanvas.transform);
            _mOverlayCurrent.transform.localPosition = Vector3.zero;
            _mOverlayCurrent.transform.localRotation = Quaternion.identity;
        }
    }
}
