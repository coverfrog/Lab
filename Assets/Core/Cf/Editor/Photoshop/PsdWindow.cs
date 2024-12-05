#if UNITY_EDITOR
using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    public class PsdWindow : EditorWindow
    {
        private string _path;

        [MenuItem("Cf/Window/Psd")]
        private static void ShowWindow()
        {
            _ = GetWindow<PsdWindow>();
        }

        private void OnGUI()
        {
            GUILayout.Label(_path);
            
            if (GUILayout.Button("Select"))
            {
                _path = EditorUtility.OpenFilePanel("", "", "psd");
            }

            if (GUILayout.Button("Parse"))
            {
                // todo : get stream

                FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read);

                byte[] bytes = new byte[fileStream.Length];

                _ = fileStream.Read(bytes, 0, (int)fileStream.Length);

                Stream stream = new MemoryStream(bytes);

                BinaryReader reader = new BinaryReader(stream, Encoding.Default);

                // todo : psd file
                // offset   byte    subject             description
                /*
                    0	     4	    Signature (8BPS)	PSD 파일임을 나타내는 시그니처
                    4	     2	    Version	            1: PSD, 2: PSB (대용량 포맷)
                    6	     6	    Reserved	        예약 공간 (항상 0)
                    12	     2	    Channels	        채널 수 (1~56)
                    14	     4	    Height	            이미지 높이 (픽셀)
                    18	     4	    Width	            이미지 폭 (픽셀)
                    22	     2	    Depth	            비트 깊이 (1, 8, 16, 32)
                    24	     2	    Color Mode	        색상 모드 (예: 3 = RGB)
                 */

                Parse0(reader);
            }
        }

        private void Parse0(BinaryReader reader)
        {
            
            byte[] bytes = reader.ReadBytes(2);
            Array.Reverse(bytes); // 빅 엔디안으로 변환
            var i = BitConverter.ToInt16(bytes, 0);
            
            Debug.Log(i);
        }
    }
}

#endif