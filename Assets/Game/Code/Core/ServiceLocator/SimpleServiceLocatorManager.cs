using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple locator implementation without context
/// </summary>
public class SimpleServiceLocatorManager : IServiceLocatorManager, IDisposable
{
    public Dictionary<Type, object> _dictionary;

    public SimpleServiceLocatorManager()
    {
        _dictionary = new Dictionary<Type, object>();
    }

    public void Register<T>(T service) where T : class
    {
        var type = typeof(T);

        if (!_dictionary.TryAdd(type, service))
        {
            Debug.LogError($"Error on register service by type [ {type} ]");
        }
    }

    public T Get<T>() where T : class
    {
        var type = typeof(T);
        if (!_dictionary.TryGetValue(type, out var result))
        {
            Debug.LogError($"Not found service by type [ {type} ]");
            return null;
        }

        return result as T;
    }

    public void Dispose()
    {
        foreach (var service in _dictionary.Values)
        {
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}