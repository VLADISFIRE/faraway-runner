using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Events
{
    public class UnityEventsManager : IEngineEventsManager, IDisposable
    {
        private UnityEventsComponent _events;

        private Dictionary<EventsType, EventSubscriptionsContainer> _containers;

        public bool applicationFocused { get; private set; } = true;
        public bool applicationPaused { get; private set; }
        public bool applicationQuitting { get; private set; }

        public UnityEventsManager()
        {
            var componentsHolder = new GameObject("[UnityEngineEvents]");

            _events = componentsHolder.AddComponent<UnityEventsComponent>();

            _events.eventUpdated += HandleEventUpdated;
        }

        public void Dispose()
        {
            _events.eventUpdated -= HandleEventUpdated;
        }

        private void HandleEventUpdated(EventsType type, bool value)
        {
            switch (type)
            {
                case EventsType.OnApplicationFocus:
                    applicationFocused = value;
                    break;

                case EventsType.OnApplicationPause:
                    applicationPaused = value;
                    break;

                case EventsType.OnApplicationQuit:
                    applicationQuitting = value;
                    break;
            }

            TryDispatchUpdate(type);
        }

        private void TryDispatchUpdate(EventsType type)
        {
            if (_containers == null) return;
            if (_containers != null && _containers.TryGetValue(type, out EventSubscriptionsContainer container))
            {
                container.Update();
            }
        }

        public void Subscribe(EventsType type, Action action)
        {
            if (_containers == null)
            {
                _containers = new Dictionary<EventsType, EventSubscriptionsContainer>();
            }

            if (_containers.TryGetValue(type, out EventSubscriptionsContainer container))
            {
                container.Add(action);
            }
            else
            {
                EventSubscriptionsContainer newContainer = new EventSubscriptionsContainer(type);
                newContainer.Add(action);

                _containers[type] = newContainer;
            }
        }

        public void Unsubscribe(EventsType type, Action action)
        {
            if (_containers == null)
            {
                Debug.LogWarning($"Could not unsubscribe from unexisting container type: {type}");
                return;
            }

            if (_containers.TryGetValue(type, out EventSubscriptionsContainer container))
            {
                container.TryRemove(action);
            }
        }

        public void ForceClearSubscriptions()
        {
            foreach (var container in _containers.Values)
            {
                container.ClearAll();
            }
        }

        public void Clear()
        {
            ForceClearSubscriptions();
        }
    }
}