namespace Game.Effects.Implementations
{
    public class FlyEffectProcessor : EffectProcessor
    {
        public string id { get { return EffectType.FLY; } }

        public override void Apply(IEffectable target, EffectModel effect)
        {
            if (target is IMovementable movementable)
            {
                var movement = movementable.movement;

                movement.SetFly();
            }
        }

        public override void OnEnd(IEffectable target, EffectModel effect)
        {
            if (target is IMovementable movementable)
            {
                var movement = movementable.movement;

                movement.ToRun();
            }
        }
    }
}