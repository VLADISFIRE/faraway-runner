using System;

namespace Game.Effects
{
    public class EffectsRoadElementsTriggerReactor : IDisposable
    {
        private RoadElementService _roadElements;
        private EffectsService _effects;

        public EffectsRoadElementsTriggerReactor()
        {
            ServiceLocator.Get(out _roadElements);
            ServiceLocator.Get(out _effects);

            _roadElements.triggered += HandleTriggered;
        }

        public void Dispose()
        {
            _roadElements.triggered -= HandleTriggered;
        }

        private void HandleTriggered(RoadElementModel element, ITriggerSource source)
        {
            if( element.entry.effect == null) return;
            
            if (source is IEffectable effectable)
            {
                _effects.ApplyEffect(effectable, element.entry.effect.entry);
            }
        }
    }
}