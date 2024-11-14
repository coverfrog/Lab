#if UNITY_EDITOR
using Cf.Scenes;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    [CreateAssetMenu(menuName = "Cf/Test")]
    public class CustomBuildInfo : ScriptableObject
    {
        [Header("Player Options")] 
        [SerializeField] private SceneField[] sceneFields;
        [SerializeField] private string localPathName;
        [SerializeField] private string assetBundleManifestPath;
        [SerializeField] private BuildTargetGroup targetGroup;
        [SerializeField] private BuildTarget target;
        [SerializeField] private int subTarget;
        [SerializeField] private BuildOptions options;
        [SerializeField] private string[] extraScriptingDefines;
    }
}

#endif