using System;

namespace Game.Core.Events
{
    public enum EventsType
    {
        Update,
        FixedUpdate,
        LateUpdate,
        EachSecond,
        OnDrawGizmos,
        OnApplicationFocus,
        OnApplicationPause,
        OnApplicationQuit
    }
    
    public interface IEngineEventsManager
    {
        bool applicationFocused { get; }
        bool applicationPaused { get; }
        bool applicationQuitting { get; }

        void Subscribe(EventsType type, Action action);
        void Unsubscribe(EventsType type, Action action);
    }
}