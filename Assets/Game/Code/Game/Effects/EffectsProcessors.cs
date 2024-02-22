using System.Collections.Generic;
using Game.Effects.Implementations;
using UnityEngine;

namespace Game.Effects
{
    public class EffectsProcessors
    {
        private Dictionary<string, EffectProcessor> _processors = new Dictionary<string, EffectProcessor>();

        public EffectsProcessors()
        {
            //The easiest way to set processors, there are better options...
            _processors.Add(EffectType.FLY, new FlyEffectProcessor());
            _processors.Add(EffectType.SLOW, new SlowEffectProcessor());
            _processors.Add(EffectType.SPEED_BOOST, new SpeedBoostEffectProcessor());
        }

        public EffectProcessor GetProcessor(EffectEntry effect)
        {
            if (!_processors.TryGetValue(effect.type, out var processor))
            {
                Debug.LogError($"$Not found effect processor by type [ {effect.type} ]");
            }

            return processor;
        }

        // private void Update()
        // {
        //     if (effects == null || effects.Count <= 0) return;
        //
        //     List<EffectEntry> list = null;
        //
        //     var target = effects.Keys.ToList();
        //     foreach (var key in target)
        //     {
        //         var left = effects[key];
        //         effects[key] -= Time.deltaTime;
        //
        //         if (left <= 0)
        //         {
        //             ToRemove(key);
        //         }
        //     }
        //
        //     if (target != null)
        //     {
        //         foreach (var effect in target)
        //         {
        //             effects.Remove(effect);
        //         }
        //     }
        //
        //     void ToRemove(EffectEntry effect)
        //     {
        //         if (target == null)
        //             target = new List<EffectEntry>();
        //
        //         target.Add(effect);
        //     }
        // }
    }
}