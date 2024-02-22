using Game.Effects;
using Game.Presenters;
using UnityEngine;

namespace Game
{
    public class GameInitializer
    {
        private const string CONFIG_PATH = "GameSettings";

        public static void Initialize()
        {
            var settings = Resources.Load<GameSettingsScrobject>(CONFIG_PATH);
            ServiceLocator.Register(settings);

            ServiceLocator.Register(new GameService());
            ServiceLocator.Register(new CameraFollow());
            ServiceLocator.Register(new PlayerService());
            ServiceLocator.Register(new RoadService());
            ServiceLocator.Register(new RoadElementService());
            
            ServiceLocator.Register(new EffectsService());
            ServiceLocator.Register(new EffectsRoadElementsTriggerReactor());

            ServiceLocator.Register(new PlayerCharacterViewPresenter());
        }
    }
}