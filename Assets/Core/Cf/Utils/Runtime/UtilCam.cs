using UnityEngine;

namespace Cf
{
    public static partial class Util
    {
        public static class Cam
        {
            private static Camera MainCam { get; set; }

            public static void SetMainCam(Camera cam)
            {
                MainCam = cam;
            }

            private static bool MainCamExist()
            {
                if (MainCam == null)
                {
                    MainCam = Camera.main;

                    if (MainCam == null)
                    {
                        return false;
                    }
                }

                return true;
            }

            public static void RayScreen()
            {
                
            }
        }
    }
}
