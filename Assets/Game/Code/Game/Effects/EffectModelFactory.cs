namespace Game.Effects
{
    public class EffectModelFactory
    {
        public EffectModel Create(EffectEntry entry)
        {
            return new EffectModel(entry);
        }
    }
}