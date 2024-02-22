using System;

namespace Game.Effects
{
    public class EffectModel
    {
        private EffectEntry _entry;
        private float _time;
        public EffectEntry entry { get { return _entry; } }

        public float time { get { return _time; } }

        public event Action<EffectModel> ended;

        public EffectModel(EffectEntry entry)
        {
            _entry = entry;
            SetTime(_entry.duration);
        }

        public void Tick(float delta)
        {
            _time -= delta;

            if (_time <= 0)
            {
                ended?.Invoke(this);
            }
        }

        private void SetTime(float time)
        {
            _time = time;
        }
    }
}