namespace Game.Effects
{
    public abstract class EffectProcessor
    {
        public abstract void Apply(IEffectable target, EffectModel effect);

        public virtual void OnEnd(IEffectable target, EffectModel effect)
        {
        }
    }
}