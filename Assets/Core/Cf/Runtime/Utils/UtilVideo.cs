using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Cf
{
    public static partial class Util
    {
        public static class Video
        {
            public static void Play(ref RawImage screen, ref VideoPlayer player, int width = 1920, int height = 1080)
            {
                var tex = CreateRenderTexture(width, height);

                screen.texture = tex;
                player.targetTexture = tex;
                
                player.Play();
            }

            private static RenderTexture CreateRenderTexture(int width, int height)
            {
                var tex = new RenderTexture(width, height, default);
                
                return tex;
            }

            private static void PlayComplete(VideoPlayer player)
            {
                
            }
        }
    }
}
