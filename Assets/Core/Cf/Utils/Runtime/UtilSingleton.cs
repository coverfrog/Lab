using System;
using UnityEngine;

namespace Cf
{
    public static partial class Util
    {
        public static class Singleton
        {
            public abstract class Mono<T> : MonoBehaviour where T : Behaviour
            {
                private static object _lock = new object();
                private static bool _isClose;
                
                private static T _instance;

                public static T Instance
                {
                    get
                    {
                        lock (_lock)
                        {
                            if (_isClose)
                            {
                                return null;
                            }

                            if (_instance == null)
                            {
                                _instance = FindAnyObjectByType<T>();
                            
                                if (_instance == null)
                                {
                                    GameObject obj = new GameObject(typeof(T).Name);
                                    _instance = obj.AddComponent<T>();
                                }
                            }
                        
                            return _instance;
                        }
                    }
                }

                protected abstract bool IsDontDestroyOnLoad();

                protected virtual void Awake()
                {
                    if (_instance == null)
                    {
                        _instance = this as T;

                        if (IsDontDestroyOnLoad())
                        {
                            DontDestroyOnLoad(gameObject);
                        }
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }

                protected void OnApplicationQuit()
                {
                    _isClose = true;
                }

                private void OnDestroy()
                {
                    _isClose = true;
                }
            }
        }
    }
}
