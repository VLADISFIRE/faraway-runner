namespace Game
{
    public class GameController
    {
        private Player _player;

        public GameController(Player player)
        {
            _player = player;
        }

        public void Start()
        {
            _player.SetEnable(true);
        }

        public void GameOver()
        {
            _player.SetEnable(false);
        }
    }
}