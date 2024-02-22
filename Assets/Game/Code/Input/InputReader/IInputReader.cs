using System;

namespace Game.InputManagement
{
    public interface IInputReader
    {
        public event Action<SwipeInfo> onSwipe;
        public event Action<TapInfo> onTap;
        public event Action onDoubleTap;

        public bool isDoubleTap { get; }

        public void EnableInput(bool enable);
    }
}