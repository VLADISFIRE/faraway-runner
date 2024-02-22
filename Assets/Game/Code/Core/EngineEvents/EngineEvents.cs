using System;

namespace Game.Core.Events
{
    public class EngineEvents : StaticWrapper<IEngineEventsManager>
    {
        public static bool applicationPaused { get { return _instance.applicationPaused; } }
        public static bool applicationFocused { get { return _instance.applicationFocused; } }
        public static bool applicationQuitting { get { return _instance != null && _instance.applicationQuitting; } }

        public static void Subscribe(EventsType type, Action action)
        {
            if (InitializationCheck())
            {
                _instance.Subscribe(type, action);
            }
        }

        public static void Unsubscribe(EventsType type, Action action)
        {
            if (InitializationCheck())
            {
                _instance.Unsubscribe(type, action);
            }
        }
    }
}