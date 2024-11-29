using UnityEngine;

namespace Cf
{
    public class NewMonoBehaviourScript : Util.Singleton.Resources<NewMonoBehaviourScript>
    {
        [SerializeField] protected string test;
        
        protected override string ResourcesPath()
        {
            return "NewMonoBehaviourScript";
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
