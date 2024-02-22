using Game.UI;
using UnityEngine;

namespace Game
{
    public class UIInitializer
    {
        public const string CONFIG_PATH = "UISettings";

        private const string CANVAS_TAG = "Canvas";

        public static void Initialize()
        {
            //Rough implementation of settings
            var settings = Resources.Load<UISettingsScrobject>(CONFIG_PATH);

            ServiceLocator.Register(settings);
            
            var canvas = GameObject.FindGameObjectWithTag(CANVAS_TAG);

            var startWindowLayout = Object.Instantiate(settings.windows.start, canvas.transform);
            var startWindow = new StartWindow(startWindowLayout);

            ServiceLocator.Register(startWindow);
        }
    }
}