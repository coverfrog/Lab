using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Cf
{
    public abstract class GenericPool<T> : MonoBehaviour where T : Behaviour
    { 
        [TitleGroup("T")] 
        [SerializeField] protected T prefab;
        [TitleGroup("T")] 
        [SerializeField] protected PoolOptions options;

        #region < Unity >

        private void Awake()
        {
            _ = Pool;
        }

        #endregion

        #region < Check Prefab >

#if UNITY_EDITOR
        [TitleGroup("T")]
        [ShowInInspector]
        public bool PrefabCanUse =>  PrefabCanUseT(prefab);
        
        private bool PrefabCanUseT(T t)
        {
            bool isCaneUse;
            
            if (t == null)
            {
                isCaneUse = false;
            }

            else if (t.GetComponent<IReturnPool<T>>() == null)
            {
                isCaneUse = false;
            }

            else
            {
                isCaneUse = true;
            }

            return isCaneUse;
        }
        
        [HideIf("PrefabCanUse")]
        [TitleGroup("T")]
        [Button]
        public void ErrorReport()
        {
            ErrorReportT(prefab);
        }
        
        private void ErrorReportT(T t)
        {
            string message;
            
            if (t == null)
            {
                message = $"<color=red>Prefab is null.</color>";
            }

            else if (t.GetComponent<IReturnPool<T>>() == null)
            {
                message = $"<color=red>Prefab is not inherit _IReturnPool<T>.</color>";
            }

            else
            {
                message = $"<color=green>Good!</color>";
            }

            Debug.Log(message);
        }
#endif
        #endregion

        #region < Pool >

        private IObjectPool<T> _pool;

        public IObjectPool<T> Pool
        {
            get
            {
                if (options == null)
                {
                    options = ScriptableObject.CreateInstance<PoolOptions>();
                    options.name = "Default Stack";
                }

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

        #region < Factory >

        private void SetPrefab(T t)
        {
            prefab = t;
        }

        private void SetOptions(PoolOptions pOptions)
        {
            options = pOptions;
        }

        public static TPool CreatePool<TPool>(Object sender, PoolInfo info, Transform parent) where TPool : GenericPool<T>
        {
            TPool pool = new GameObject("").AddComponent<TPool>();
            pool.name = $"Pool_{info.CodeName}";

            if (pool == null)
            {
#if UNITY_EDITOR
                Debug.Log("Pool is null");
#endif
                Destroy(pool.gameObject);
                
                return pool;
            }

            T t = info.Prefab.GetComponent<T>();
            PoolOptions options = info.Options; 

            if (!pool.PrefabCanUseT(t) || options == null)
            {
#if UNITY_EDITOR
                Debug.Log("Prefab is null IReturnPool or Options or Type Miss");
#endif
                Destroy(pool.gameObject);
                
                return pool;
            }
            
            pool.SetPrefab(t);
            pool.SetOptions(options);
            pool.transform.SetParent(parent);

            return pool;
        }

        #endregion

        #region < Interface >

        protected virtual T CreatePooledItem()
        {
            if (!PrefabCanUse)
            {
#if UNITY_EDITOR
                Debug.LogError($"{prefab.name} Is Not Contain {nameof(IReturnPool<T>)} !!!");
#endif
                return null;
            }

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
