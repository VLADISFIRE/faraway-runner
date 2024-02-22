namespace Game
{
    public class ServiceLocator : StaticWrapper<IServiceLocatorManager>
    {
        public static void Register<T>(T obj) where T : class
        {
            if (InitializationCheck())
            {
                _instance.Register<T>(obj);
            }
        }

        public static T Get<T>() where T : class
        {
            if (InitializationCheck())
            {
                return _instance.Get<T>();
            }

            return null;
        }

        public static void Get<T>(out T service) where T : class
        {
            service = Get<T>();
        }
    }
}