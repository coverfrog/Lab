#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Cf
{
    public static class CustomBuild 
    {
        [MenuItem("Cf/Build/Pc")]
        public static void BuildPc()
        {
            // 빌드 대상 설정
            string buildPath = "Builds/MyGame";
            BuildTarget buildTarget = BuildTarget.StandaloneWindows64; // 예: Windows 64비트

            // 빌드 옵션 설정
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = new[] { "Assets/Scenes/MainScene.unity" }; // 빌드할 씬 지정
            buildPlayerOptions.locationPathName = buildPath;
            buildPlayerOptions.target = buildTarget;
            buildPlayerOptions.options = BuildOptions.None;

            // 빌드 실행
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            // 빌드 결과 확인
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }
            else if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("Build failed");
            }
        }
    }
}

#endif