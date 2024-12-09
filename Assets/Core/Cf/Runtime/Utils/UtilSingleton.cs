using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Cf.Utils
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
                            
                            if (_instance == null)
                            {
                                _instance = FindAnyObjectByType<T>();

                                if (_instance == null)
                                {
                                    Type t = typeof(T);
                                    MethodInfo methodInfo =
                                        t.GetMethod(nameof(ResourcesPath), BindingFlags.Instance | BindingFlags.NonPublic);

                                    if (methodInfo != null)
                                    {
                                        _instance = new GameObject("Resources Load Dummy").AddComponent<T>();

                                        object valueObj = methodInfo.Invoke(_instance, null);
                                        string valueStr = (string)valueObj;

                                        DestroyImmediate(_instance.gameObject);
                                        
                                        T resources = Resources.Load<T>(valueStr);
                                        if (resources != null)
                                        {
                                            _instance = Instantiate(original: resources);
                                            _instance.transform.position = Vector3.zero;
                                            _instance.name = $"{typeof(T)}";
                                        }
                                    }
                                }
                            }
                        
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
