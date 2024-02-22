using System;
using Game.Code.Core;
using Game.Core.Events;

namespace Game
{
    public class Initializer : IInitializer, IDisposable
    {
        public void Initialize()
        {
            ServiceLocator.Initialize(new SimpleServiceLocatorManager());
            EngineEvents.Initialize(new UnityEventsManager());

            InputInitializer.Initialize();
            GameInitializer.Initialize();
            UIInitializer.Initialize();
        }

        public void Dispose()
        {
            ServiceLocator.Dispose();
        }
    }
}