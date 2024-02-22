using Game.Code;

namespace Game
{
    public class PlayerService : CompositeDisposable
    {
        private GameSettingsScrobject _settings;

        private PlayerModel _player;

        public PlayerModel player { get { return _player; } }

        public PlayerService()
        {
            ServiceLocator.Get(out _settings);

            var model = new PlayerModel();
            var character = new CharacterModel(_settings.character);
            model.SetCharacter(character);

            _player = model;

            var inputHandler = new PlayerCharacterInputHandler();
            inputHandler.SetPlayer(model);

            AddDisposable(inputHandler);
        }
    }
}