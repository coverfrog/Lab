using UnityEngine;

namespace Cf.TouchGame.Template0
{
    public class ProgressTutorial : Progress<ProgressType>
    {
        protected override ProgressType TypeName => ProgressType.Tutorial;
    }
}
