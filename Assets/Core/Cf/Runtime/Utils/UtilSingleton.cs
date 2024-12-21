using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
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

                            if (_instance)
                            {
                                return _instance;
                            }
                            
                            _instance = FindAnyObjectByType<T>();
                            
                            if (_instance)
                            {
                                return _instance;
                            }
                            
                            return _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                        }
                    }
                }

                protected abstract bool IsDontDestroyOnLoad();
                
                protected virtual void Awake()
                {
                    if (_instance != null && _instance != this)
                    {
                        Destroy(gameObject);
                    }
                    
                    else
                    {
                        _instance = this as T;
                        
                        if (IsDontDestroyOnLoad())
                        {
                            DontDestroyOnLoad(gameObject);
                        }
                    }
                }

                protected virtual void OnApplicationQuit()
                {
                    _isClose = true;
                }

                protected virtual void OnDestroy()
                {
                    _isClose = true;
                }
            }
            
            public abstract class Resources<T> : MonoBehaviour where T : Behaviour
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
                            
                            if (_instance != null)
                            {
                                return _instance;
                            }
                            
                            _instance = FindAnyObjectByType<T>();

                            if (_instance != null)
                            {
                                return _instance;
                            }

                            var t = typeof(T);
                            var methodInfo =
                                t.GetMethod(nameof(ResourcesPath), BindingFlags.Instance | BindingFlags.NonPublic);

                            if (methodInfo == null)
                            {
#if UNITY_EDITOR
                                Debug.LogError("Method Is Null");
#endif
                                return null;
                            }

                            var invokedObj = methodInfo.Invoke(FormatterServices.GetUninitializedObject(typeof(T)), null);
                            var path = invokedObj.ToString();
                            
                            if (string.IsNullOrEmpty(path))
                            {
#if UNITY_EDITOR
                                Debug.LogError("Path Is Null");
#endif
                                return null;
                            }

                            var source = Resources.Load<T>(path);
                            
                            _instance = Instantiate(source, Vector3.zero, Quaternion.identity);
                            _instance.name = nameof(T);

                            return _instance;
                        }
                    }
                }

                protected abstract string ResourcesPath();
                
                protected abstract bool IsDontDestroyOnLoad();
                
                protected virtual void Awake()
                {
                    if (_instance != null && _instance != this)
                    {
                        Destroy(gameObject);
                    }
                    
                    else
                    {
                        _instance = this as T;
                        
                        if (IsDontDestroyOnLoad())
                        {
                            DontDestroyOnLoad(gameObject);
                        }
                    }
                }

                protected virtual void OnApplicationQuit()
                {
                    _isClose = true;
                }

                protected virtual void OnDestroy()
                {
                    _isClose = true;
                }
            }
        }
    }
}
