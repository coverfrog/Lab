using UnityEngine;

namespace Cf.CardTripleMatch
{
    public class A : Util.Singleton.Resources<A>
    {
        public float a;
        
        protected override string ResourcesPath()
        {
            return "A";
        }

        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
