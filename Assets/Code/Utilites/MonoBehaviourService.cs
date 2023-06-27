using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public abstract class MonoBehaviourService<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    protected void Awake()
    {
        if (_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _instance = this as T;
        
        OnCreateService();
    }

    protected void OnDestroy()
    {
        if (_instance != null)
        {
            OnDestroyService();
            _instance = null;
        }
    }

    public static T Get(bool createIfNotExist = false)
    {
        if (_instance == null)
        {
            if (!createIfNotExist)
                throw new Exception($"Service {typeof(T).Name} not found");

            new GameObject($"Service {typeof(T).Name}").AddComponent<T>();
            Debug.LogWarning($"{typeof(T).Name} service was created");
        }
        return _instance;
    }

    
    protected abstract void OnCreateService();
    
    protected abstract void OnDestroyService();
}
