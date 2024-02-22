using System.Collections.Generic;

namespace Game.Effects
{
    public class EffectsSystem
    {
        private EffectsProcessors _processors;

        private Dictionary<IEffectable, List<EffectModel>> _dictionary;

        public EffectsSystem()
        {
            _dictionary = new Dictionary<IEffectable, List<EffectModel>>();
            _processors = new EffectsProcessors();
        }

        public void Add(IEffectable target, EffectModel model)
        {
            if (!_dictionary.TryGetValue(target, out var effects))
            {
                _dictionary[target] = new List<EffectModel>();
            }

            _dictionary[target].Add(model);
        }

        public void Remove(IEffectable target, EffectModel model)
        {
            if (_dictionary.TryGetValue(target, out var effects))
            {
                effects.Remove(model);

                var processor = _processors.GetProcessor(model.entry);
                processor.OnEnd(target, model);
            }
        }

        public void Tick()
        {
            if (_dictionary == null || _dictionary.Count <= 0) return;

            foreach (var pair in _dictionary)
            {
                foreach (var effect in pair.Value)
                {
                    var processor = _processors.GetProcessor(effect.entry);
                    processor.Apply(pair.Key, effect);
                }
            }
        }
    }
}