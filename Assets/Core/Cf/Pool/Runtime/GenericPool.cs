using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace Cf
{
    public abstract class GenericPool<T> : MonoBehaviour where T : Behaviour
    { 
        protected enum PoolType
        {
            Stack,
            LinkedList,
        }

        [Title("T")] 
        [SerializeField] protected T prefab;
        
        [Title("Option")] 
        [SerializeField] protected PoolType poolType;
        [SerializeField] protected bool collectionChecks;
        [SerializeField] [Range(0, 100_000)] [ShowIf("IsTypeStack")] protected int defaultCapacity = 10;
        [SerializeField] [Range(0, 100_000)] protected int maxPoolSize = 10_000;

        #region < Condition >

        private bool IsTypeStack => poolType == PoolType.Stack;
        
        private bool IsTypeLinkedList => poolType == PoolType.LinkedList;

        private bool IsPrefabExist => prefab != null;

        #endregion

        #region < Pool >

        private IObjectPool<T> _pool;

        public IObjectPool<T> Pool
        {
            get
            {
                if (_pool == null)
                {
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

        #region < Method >

        private T CreatePooledItem()
        {
            T item = Instantiate(prefab);
            IReturnPool<T> returnPool = item.GetComponent<IReturnPool<T>>();

            if (returnPool != null)
            {
                returnPool.Pool = Pool;
            }

            else
            {
#if UNITY_EDITOR
                Debug.LogError($"{prefab.name} Is Not Contain {nameof(IReturnPool<T>)} !!!");
#endif
            }

            return item;
        }
        
        private void OnReturnedToPool(T t)
        {
            t.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(T t)
        {
            t.gameObject.SetActive(transform);
        }

        private void OnDestroyPoolObject(T t)
        {
            Destroy(t.gameObject);
        }

        #endregion

    }
}
