namespace Game
{
    public class CharacterModel : ITriggerSource,IEffectable, IMovementable
    {
        private CharacterEntry _entry;
        private CharacterMovement _movement;

        public CharacterMovement movement { get { return _movement; } }

        public CharacterEntry entry { get { return _entry; } }

        public CharacterModel(CharacterEntry entry)
        {
            _entry = entry;

            _movement = new CharacterMovement(entry.movement);

            _movement.root.AddComponent<TriggerSourceComponent>().SetSource(this);
        }
    }
}