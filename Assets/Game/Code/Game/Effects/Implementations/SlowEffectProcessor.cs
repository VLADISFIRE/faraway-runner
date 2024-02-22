namespace Game.Effects.Implementations
{
    public class SlowEffectProcessor : EffectProcessor
    {
        public string id { get { return EffectType.SLOW; } }

        public override void Apply(IEffectable target, EffectModel effect)
        {
            if (target is IMovementable movementable)
            {
                var movement = movementable.movement;

                movement.SetSlow(true);
            }
        }
        
        public override void OnEnd(IEffectable target, EffectModel effect)
        {
            if (target is IMovementable movementable)
            {
                var movement = movementable.movement;

                movement.SetSlow(false);
            }
        }
    }
}