using System;
using Game.Client;

namespace Game.InputManagement
{
    public class GameInput : IGameInput, IDisposable
    {
        private SwipeHandler _swipeHandler;

        public event Action<string> onAction;

        public GameInput()
        {
            var inputReader = ServiceLocator.Get<IInputReader>();
            _swipeHandler = new SwipeHandler(inputReader);

            _swipeHandler.swiped += HandleSwiped;
        }

        public void Dispose()
        {
            _swipeHandler.swiped -= HandleSwiped;

            _swipeHandler.Dispose();
        }

        private void HandleSwiped(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.None:
                    break;
                case SwipeDirection.Up:
                    onAction?.Invoke(GameActionInputType.UP);
                    break;
                case SwipeDirection.Left:
                    onAction?.Invoke(GameActionInputType.LEFT);
                    break;
                case SwipeDirection.Right:
                    onAction?.Invoke(GameActionInputType.RIGHT);
                    break;
                case SwipeDirection.Down:
                    onAction?.Invoke(GameActionInputType.DOWN);
                    break;
            }
        }
    }
}