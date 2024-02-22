using System;

namespace Game
{
    public class PlayerModel
    {
        private CharacterModel _character;

        private int _score;

        public int _hp;

        public int score { get { return _score; } }
        public int hp { get { return _hp; } }

        public CharacterModel character { get { return _character; } }

        public void SetCharacter(CharacterModel character)
        {
            _character = character;
        }
    }
}