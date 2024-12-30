using UnityEngine;

namespace Hate
{
    public abstract class SceneBase<TModel, TView> : MonoBehaviour where TModel : ModelBase where TView : ViewBase
    {
        [SerializeField] protected TModel mModel;
        [SerializeField] protected TView mView;
    }
}
