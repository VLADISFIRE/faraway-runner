using System;
using Game.Client;
using UnityEngine;

namespace Game
{
    public class Player : IDisposable
    {
        private bool _enabled;

        private IGameInput _input;
        private Character _character;

        public Vector3 position { get { return _character?.position ?? Vector3.zero; } }

        public Player(IGameInput input, Character character, CameraController camera)
        {
            _character = character;
            _input = input;

            _input.onAction += HandleAction;

            camera.SetTarget(character.characterGameObject);
        }

        public void Dispose()
        {
            _input.onAction -= HandleAction;
        }

        public void SetEnable(bool value)
        {
            _enabled = value;

            if (value)
            {
                _character.Run();
            }
        }

        private void HandleAction(string key)
        {
            if (!_enabled)
                return;

            switch (key)
            {
                case GameActionInputType.UP:
                    _character.Jump();
                    break;
                case GameActionInputType.DOWN:
                    _character.Slide();
                    break;
                case GameActionInputType.LEFT:
                    _character.MoveToLeft();
                    break;
                case GameActionInputType.RIGHT:
                    _character.MoveToRight();
                    break;
            }
        }
    }
}