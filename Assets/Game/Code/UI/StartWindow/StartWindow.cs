using System;

namespace Game.UI
{
    public class StartWindow : IDisposable
    {
        private StartWindowLayout _layout;
        private GameService _service;

        public StartWindow(StartWindowLayout layout)
        {
            _layout = layout;

            _service = ServiceLocator.Get<GameService>();

            _layout.button.onClick.AddListener(HandleClick);
        }

        public void Dispose()
        {
            _layout.button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            _service.Play();
            _layout.gameObject.SetActive(false);
        }
    }
}