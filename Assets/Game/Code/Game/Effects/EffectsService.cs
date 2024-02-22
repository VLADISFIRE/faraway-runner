using System;
using System.Collections.Generic;
using System.Linq;
using Game.Core.Events;
using UnityEngine;

namespace Game.Effects
{
    public class EffectsService : IDisposable
    {
        private EffectsSystem _system;
        private EffectModelFactory _factory;

        private Dictionary<EffectModel, IEffectable> _models = new Dictionary<EffectModel, IEffectable>();

        public EffectsService()
        {
            _system = new EffectsSystem();
            _factory = new EffectModelFactory();

            EngineEvents.Subscribe(EventsType.Update, HandleUpdate);
        }

        public void Dispose()
        {
            EngineEvents.Unsubscribe(EventsType.Update, HandleUpdate);

            foreach (var model in _models.Keys)
            {
                model.ended -= HandleEnded;
            }
        }

        private void HandleUpdate()
        {
            if (_models == null || _models.Count <= 0) return;

            var models = _models.Keys.ToList();
            foreach (var model in models)
            {
                model.Tick(Time.deltaTime);
            }

            _system.Tick();
        }

        public void ApplyEffect(IEffectable target, EffectEntry entry)
        {
            var model = _factory.Create(entry);
            model.ended += HandleEnded;
            _models.Add(model, target);

            _system.Add(target, model);
        }

        private void HandleEnded(EffectModel model)
        {
            var target = _models[model];
            _system.Remove(target, model);

            _models.Remove(model);
        }
    }
}