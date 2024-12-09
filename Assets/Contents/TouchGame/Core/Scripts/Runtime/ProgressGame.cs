using UnityEngine;

namespace Cf.TouchGame.Template0
{
    public class ProgressGame : Progress<ProgressType>
    {
        protected override ProgressType TypeName => ProgressType.Game;
    }
}
