using System;
using Game.Core.Events;
using UnityEngine;

namespace Game.InputManagement
{
    public abstract class BaseInputReader : IDisposable, IInputReader
    {
        private IEngineEventsManager _engineEvents;
        
        private float DOUBLE_TAP_TIMEOUT = 0.5f;

        protected bool _enabled = true;

        private bool _isDoubleTap;
        private float _doubleTapDelay;
        private int _tapCount;

        private bool _blockSwipe;

        public event Action<SwipeInfo> onSwipe;
        public event Action<TapInfo> onTap;
        public event Action onDoubleTap;

        public bool isDoubleTap { get { return _isDoubleTap; } }

        public BaseInputReader()
        {
            EngineEvents.Subscribe(EventsType.Update, Update);
        }

        public void Dispose()
        {
            EngineEvents.Unsubscribe(EventsType.Update, Update);
        }

        protected virtual void InvokeTap(TapInfo info)
        {
            onTap?.Invoke(info);
        }

        protected void BeginSwipe()
        {
            _blockSwipe = true;
        }

        protected virtual void InvokeSwipe(SwipeInfo info)
        {
            if (_blockSwipe)
            {
                _blockSwipe = false;
                onSwipe?.Invoke(info);
            }
        }

        public void EnableInput(bool enable)
        {
            _enabled = enable;
        }

        protected abstract void ReadInput();

        private void Update()
        {
            if (!_enabled) return;

            TapCountdown();
            ReadInput();
        }

        protected void TapCountdown()
        {
            if (_tapCount > 0)
            {
                _doubleTapDelay += Time.deltaTime;
            }

            if (_doubleTapDelay > DOUBLE_TAP_TIMEOUT)
            {
                _tapCount = 0;
                _doubleTapDelay = 0;
            }
        }

        protected void RegisterTaps()
        {
            _tapCount++;

            if (_tapCount == 2)
            {
                _tapCount = 0;
                _doubleTapDelay = 0;

                _isDoubleTap = true;
                onDoubleTap?.Invoke();
            }
        }

        protected void ClearDoubleTap()
        {
            if (_isDoubleTap)
            {
                _isDoubleTap = false;
            }
        }
    }
}