using UnityEngine;

namespace Cf.TouchGame.Template0
{
    public class ProgressPopup : Progress<ProgressType>
    {
        protected override ProgressType TypeName => ProgressType.Popup;
    }
}
