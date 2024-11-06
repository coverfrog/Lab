using UnityEngine;

namespace Cf.AirPass
{
    public abstract class Game : MonoBehaviour
    {
        // common
        private GameResources _resources;
        
        // common
        public virtual void Play()
        {
            
        }

        // common 
        public virtual void Init(ref GameResources resources)
        {
            _resources = resources;
        }


        public virtual void Stop()
        {
            
        }

        public virtual void Pause()
        {
            
        }

        public virtual void UnPause()
        {
            
        }
    }
}
