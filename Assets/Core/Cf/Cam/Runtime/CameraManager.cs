using System.Linq;
using UnityEngine;

namespace Cf
{
    public class CameraManager : Util.Singleton.Mono<CameraManager>
    {
        public Camera MainCam { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();

            MainCam = Camera.main;

            if (MainCam == null)
            {
                MainCam = FindObjectsByType<Camera>(FindObjectsSortMode.None).FirstOrDefault();
            }
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
        
        
    }
}
