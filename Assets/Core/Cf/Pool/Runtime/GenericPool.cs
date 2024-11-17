using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Pool;

namespace Cf
{
    public abstract class GenericPool<TInfo, TBehavior> : MonoBehaviour where TInfo : GenericPoolInfo<TBehavior> where TBehavior : Behaviour
    {
        [Title("")]
        [SerializeField] private bool collectionChecks;

        [SerializeField] [Range(10, int.MaxValue)] private int defaultCapacity = 10;
        [SerializeField] [Range(1, int.MaxValue)] private int maxPoolSize = 20;
        
        [Title("")]
        [ShowInInspector] [ReadOnly] private ObjectPool<TBehavior> _pool;
        
        private IObjectPool<TBehavior> Pool
        {
            get
            {
                if (_pool != null)
                {
                    return _pool;
                }

                _pool = new ObjectPool<TBehavior>(
                    CreatePooledItem, 
                    OnTakeFromPool, 
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    collectionChecks, 
                    defaultCapacity, 
                    maxPoolSize);
                
                return _pool;
            }
        }
        
        private TBehavior CreatePooledItem()
        {
            // var go = new GameObject("Pooled Particle System");
            // var ps = go.AddComponent<ParticleSystem>();
            // ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            //
            // var main = ps.main;
            // main.duration = 1;
            // main.startLifetime = 1;
            // main.loop = false;
            //
            // // This is used to return ParticleSystems to the pool when they have stopped.
            // var returnToPool = go.AddComponent<ReturnToPool>();
            // returnToPool.pool = Pool;

            return null;
        }

        // Called when an item is returned to the pool using Release
        private void OnReturnedToPool(TBehavior tBehavior)
        {
            tBehavior.gameObject.SetActive(false);
        }

        // Called when an item is taken from the pool using Get
        private void OnTakeFromPool(TBehavior tBehavior)
        {
            tBehavior.gameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(TBehavior system)
        {
            Destroy(system.gameObject);
        }
    }
}
