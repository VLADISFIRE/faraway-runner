using System;
using UnityEngine;

namespace Game.UI
{
    public class StartWindow : IDisposable
    {
        private const string CANVAS_TAG = "Canvas";
        
        private StartWindowLayout _layout;
        private GameController _controller;

        public StartWindow(StartWindowLayout prefab, GameController controller)
        {
            var canvas = GameObject.FindGameObjectWithTag(CANVAS_TAG);

            _layout = GameObject.Instantiate(prefab, canvas.transform);

            _controller = controller;

            _layout.button.onClick.AddListener(HandleClick);
        }

        public void Dispose()
        {
            _layout.button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            _controller.Start();
            _layout.gameObject.SetActive(false);
        }
    }
}