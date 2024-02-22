using System;
using Game.Client;

namespace Game
{
    public class PlayerCharacterInputHandler : IDisposable
    {
        private IGameInput _gameInput;

        private PlayerModel _player;

        public PlayerCharacterInputHandler()
        {
            ServiceLocator.Get(out _gameInput);

            _gameInput.onAction += HandleInput;
        }

        public void Dispose()
        {
            _gameInput.onAction -= HandleInput;
        }

        public void SetPlayer(PlayerModel player)
        {
            _player = player;
        }

        private void HandleInput(string key)
        {
            if (_player == null) return;

            switch (key)
            {
                case GameActionInputType.UP:
                    _player.character.movement.Jump();
                    break;
                case GameActionInputType.DOWN:
                    _player.character.movement.Down();
                    break;
                case GameActionInputType.LEFT:
                    _player.character.movement.MoveToLeft();
                    break;
                case GameActionInputType.RIGHT:
                    _player.character.movement.MoveToRight();
                    break;
            }
        }
    }
}