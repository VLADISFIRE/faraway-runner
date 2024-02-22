using System;

namespace Game
{
    public class GameService
    {
        public event Action started;

        public void Play()
        {
            started?.Invoke();
        }
    }
}