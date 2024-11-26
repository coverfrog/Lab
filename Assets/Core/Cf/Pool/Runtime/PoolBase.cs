using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Cf
{
    public abstract class PoolBase<T> : MonoBehaviour where T : Behaviour
    { 
        [TitleGroup("T")] 
        [SerializeField] protected T prefab;
        [TitleGroup("T")] 
        [SerializeField] protected PoolOptions options;

        public bool IsInit { get; private set; }
        
        #region < Unity >

        private void Awake()
        {
            IsInit = options != null && prefab.GetComponent<IReturnPool<T>>() != null;
        }

        private void Start()
        {
            if (!IsInit)
            {
                return;
            }
            
            _ = Pool;
        }

        #endregion

        #region < Pool >

        private IObjectPool<T> _pool;

        public IObjectPool<T> Pool
        {
            get
            {
                if (_pool == null)
                {
                    PoolType poolType = options.GetPoolType;
                    bool collectionChecks = options.GetCollectionChecks;
                    int defaultCapacity = options.GetDefaultCapacity;
                    int maxPoolSize = options.GetMaxPoolSize;
                    
                    switch (poolType)
                    {
                        case PoolType.Stack:
                            _pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultCapacity, maxPoolSize);
                            break;
                        case PoolType.LinkedList:
                            _pool = new LinkedPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
                            break;
                        default:
                            _pool = null;
                            break;
                    }
                }

                return _pool;
            }
        }

        #endregion

        #region < Interface >

        protected virtual T CreatePooledItem()
        {
            T item = Instantiate(original: prefab, parent: transform);
            IReturnPool<T> returnPool = item.GetComponent<IReturnPool<T>>();

            returnPool.Pool = Pool;

            return item;
        }
        
        protected virtual void OnReturnedToPool(T t)
        {
            t.gameObject.SetActive(false);
        }

        protected virtual void OnTakeFromPool(T t)
        {
            t.gameObject.SetActive(transform);
        }

        protected virtual void OnDestroyPoolObject(T t)
        {
            Destroy(t.gameObject);
        }

        #endregion
    }
}
