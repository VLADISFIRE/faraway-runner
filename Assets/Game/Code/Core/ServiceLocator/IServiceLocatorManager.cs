public interface IServiceLocatorManager
{
    void Register<T>(T service) where T : class;
    T Get<T>() where T : class;
}