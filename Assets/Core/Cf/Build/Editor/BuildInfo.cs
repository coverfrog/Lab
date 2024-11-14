#if UNITY_EDITOR
using System.Linq;
using Cf.Scenes;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Test")]
    public class BuildInfo : ScriptableObject
    {
        // < baes info >
        [SerializeField] private BuildTarget target;
        [SerializeField] private BuildOptions options;
        [SerializeField] private string assetBundleManifestPath;
        [SerializeField] private BuildTargetGroup targetGroup;
        [SerializeField] private string localPathName;
        [SerializeField] private int subTarget;
        [SerializeField] private string[] extraScriptingDefines;
        [SerializeField] private SceneField[] sceneFields;

        // < implicit >
        public static implicit operator BuildPlayerOptions(BuildInfo info)
        {
            return new BuildPlayerOptions()
            {
                target = info.target,
                options = info.options,
                assetBundleManifestPath = info.assetBundleManifestPath,
                targetGroup = info.targetGroup,
                extraScriptingDefines = info.extraScriptingDefines,
                locationPathName = info.localPathName,
                subtarget = info.subTarget,
                scenes = info.sceneFields.Select(s => s.SceneName).ToArray(),
            };
        }
    }
}

#endif