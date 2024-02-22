using System;

public abstract class StaticWrapper<T> where T : class
{
    protected static T _instance;

    public static bool initialized { get { return _instance != null; } }

    public static void Initialize(T instance)
    {
        Dispose();

        _instance = instance;
    }

    protected static bool InitializationCheck()
    {
        return initialized;
    }

    public static void Dispose()
    {
        if (_instance == null) return;

        if (_instance is IDisposable disposable)
        {
            disposable.Dispose();
        }

        _instance = null;
    }
}