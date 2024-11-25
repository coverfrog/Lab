using UnityEngine;

namespace Rpg
{
    public class BirdManager : Cf.Util.Singleton.Mono<BirdManager>
    {
        protected override bool IsDontDestroyOnLoad()
        {
            return true;
        }
    }
}
