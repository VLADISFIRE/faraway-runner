using Game.Core.Events;
using Game.InputManagement;
using Game.UI;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class GameInitializer
    {
        public const string CONFIG_PATH = "Assets/GameSettings.asset";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static async void Initialize()
        {
            var scrobject = AssetDatabase.LoadAssetAtPath<GameSettingsScrobject>(CONFIG_PATH);

            var eventsManager = new UnityEventsManager();

            IInputReader inputReader;

#if UNITY_EDITOR
            inputReader = new DesktopInputReader(eventsManager);
#else
            inputReader = new MobileInputReader(eventsManager);
#endif

            var input = new GameInput(inputReader);
            var camera = new CameraController();

            var character = new Character(scrobject.character);

            var player = new Player(input, character, camera);

            var controller = new GameController(player);

            //UI
            var startWindow = new StartWindow(scrobject.startWindow, controller);
        }
    }
}