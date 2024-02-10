using System;

namespace Game.InputManagement
{
    public enum SwipeDirection
    {
        None,

        Up,
        Left,
        Right,
        Down,
    }

    public class SwipeHandler : IDisposable
    {
        private IInputReader _reader;

        public event Action<SwipeDirection> swiped;

        public SwipeHandler(IInputReader reader)
        {
            _reader = reader;

            _reader.onSwipe += HandleSwiped;
        }

        public void Dispose()
        {
            _reader.onSwipe -= HandleSwiped;
        }

        private void HandleSwiped(SwipeInfo info)
        {
            var swipe = info.delta;

            swipe.Normalize();

            if (swipe.y > 0 && swipe.x > -0.5f && swipe.x < 0.5f)
            {
                swiped?.Invoke(SwipeDirection.Up);
            }
            else if (swipe.y < 0 && swipe.x > -0.5f && swipe.x < 0.5f)
            {
                swiped?.Invoke(SwipeDirection.Down);
            }
            else if (swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f)
            {
                swiped?.Invoke(SwipeDirection.Left);
            }
            else if (swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f)
            {
                swiped?.Invoke(SwipeDirection.Right);
            }
        }
    }
}