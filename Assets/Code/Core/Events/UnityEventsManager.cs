using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Events
{
    public class UnityEventsManager : IEventsManager, IDisposable
    {
        private UnityEventsComponent _events;

        private Dictionary<EventsType, EventSubscriptionsContainer> _containers;

        public bool applicationFocused { get; private set; } = true;
        public bool applicationPaused { get; private set; }
        public bool applicationQuitting { get; private set; }

        public UnityEventsManager()
        {
            var componentsHolder = new GameObject("[UnityEngineEvents]").MakePermanent();

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

        private IEnumerator DelayAndExecuteRoutine(Action action, float delay = 0, int repeat = 1)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return null;
            }

            if (repeat > 1)
            {
                for (int i = 0; i < repeat; i++)
                {
                    action?.Invoke();
                    yield return null;
                }
            }
            else
            {
                action?.Invoke();
            }
        }

        private IEnumerator WaitUntilAndExecuteRoutine(Action action, Func<bool> condition, int repeat = 1)
        {
            yield return new WaitUntil(condition);

            if (repeat > 1)
            {
                for (int i = 0; i < repeat; i++)
                {
                    action?.Invoke();
                    yield return null;
                }
            }
            else
            {
                action?.Invoke();
            }
        }

        private IEnumerator ExecuteForPeriodRoutine(Action action, float period, float delay = 0)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }

            if (period == 0)
            {
                action?.Invoke();
            }
            else
            {
                float time = 0;
                while (time < period)
                {
                    time += Time.deltaTime;
                    action?.Invoke();
                    yield return null;
                }
            }
        }

        public void Clear()
        {
            ForceClearSubscriptions();
        }
    }
}