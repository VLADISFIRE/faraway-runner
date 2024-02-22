using System;
using UnityEngine;

namespace Game.Presenters
{
    public class PlayerCharacterViewPresenter : IDisposable
    {
        private GameService _service;
        private PlayerService _playerService;
        private CameraFollow _camera;

        private CharacterView _playerCharacterView;

        public PlayerCharacterViewPresenter()
        {
            ServiceLocator.Get(out _service);
            ServiceLocator.Get(out _playerService);
            ServiceLocator.Get(out _camera);

            _service.started += HandleStarted;

            CreatePlayerCharacter();
        }

        public void Dispose()
        {
            _playerCharacterView?.Dispose();

            _service.started -= HandleStarted;
        }

        private void HandleStarted()
        {
            _playerService.player.character.movement.ToRun();
        }

        private void CreatePlayerCharacter()
        {
            var playerCharacter = _playerService.player.character;
            _playerCharacterView = new CharacterView(playerCharacter);

            _playerCharacterView.gameObject.transform.SetParent(_playerService.player.character.movement.root.transform);
            _playerCharacterView.gameObject.transform.localPosition = Vector3.zero;

            _camera.SetTarget(_playerCharacterView.gameObject);
        }
    }
}