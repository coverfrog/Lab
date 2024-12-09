using UnityEngine;

namespace Cf.TouchGame.Template0
{
    public class ProgressResult : Progress<ProgressType>
    {
        protected override ProgressType TypeName => ProgressType.Result;
    }
}
