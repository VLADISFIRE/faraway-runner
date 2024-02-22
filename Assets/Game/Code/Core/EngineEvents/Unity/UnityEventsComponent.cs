using System;
using UnityEngine;

namespace Game.Core.Events
{
    public class UnityEventsComponent : MonoBehaviour
    {
        private const float INTERVAL = 1f;

        private float _nextSecondTick;

        public event Action<EventsType, bool> eventUpdated;

        private void Update()
        {
            eventUpdated?.Invoke(EventsType.Update, true);
        }

        private void LateUpdate()
        {
            eventUpdated?.Invoke(EventsType.LateUpdate, true);
        }

        private void FixedUpdate()
        {
            eventUpdated?.Invoke(EventsType.FixedUpdate, true);

            if (Time.time >= _nextSecondTick)
            {
                _nextSecondTick = Time.time + INTERVAL;
                eventUpdated?.Invoke(EventsType.EachSecond, true);
            }
        }

        private void OnDrawGizmos()
        {
            eventUpdated?.Invoke(EventsType.OnDrawGizmos, true);
        }

        private void OnApplicationQuit()
        {
            eventUpdated?.Invoke(EventsType.OnApplicationQuit, true);
        }

        private void OnApplicationFocus(bool focus)
        {
            eventUpdated?.Invoke(EventsType.OnApplicationFocus, focus);
        }

        private void OnApplicationPause(bool pause)
        {
            eventUpdated?.Invoke(EventsType.OnApplicationPause, pause);
        }
    }
}