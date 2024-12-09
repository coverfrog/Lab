using UnityEngine;

namespace Cf.TouchGame.Template0
{
    public class ProgressLevelSelect : Progress<ProgressType>
    {
        protected override ProgressType TypeName => ProgressType.LevelSelect;
    }
}
