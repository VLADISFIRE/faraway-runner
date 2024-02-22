namespace Game.Effects.Implementations
{
    public class SpeedBoostEffectProcessor : EffectProcessor
    {
        public string id { get { return EffectType.SPEED_BOOST; } }

        public override void Apply(IEffectable target, EffectModel effect)
        {
            if (target is IMovementable movementable)
            {
                var movement = movementable.movement;

                movement.SetBoost(true);
            }
        }

        public override void OnEnd(IEffectable target, EffectModel effect)
        {
            if (target is IMovementable movementable)
            {
                var movement = movementable.movement;

                movement.SetBoost(false);
            }
        }
    }
}