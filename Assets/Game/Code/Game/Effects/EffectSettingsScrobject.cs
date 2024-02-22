using UnityEngine;

namespace Game.Effects
{
    [CreateAssetMenu(menuName = "Game/Effect", fileName = "New Effect")]
    public class EffectSettingsScrobject : ScriptableObject
    {
        public EffectEntry entry;
    }
}