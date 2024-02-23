using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    //Character view controller
    public class CharacterView : IDisposable
    {
        private const string ANIMATOR_PROPERTY_FLOAT_SPEED = "Speed";
        private const string ANIMATOR_TRIGGER_IDLE = "Idle";
        private const string ANIMATOR_TRIGGER_START = "Start";
        private const string ANIMATOR_PROPERTY_BOOL_JUMPING = "Jumping";

        private GameObject _gameObject;

        private Animator _animator;
        private CharacterModel _model;

        private float _cacheAnimatorSpeed;

        public GameObject gameObject { get { return _gameObject; } }

        public CharacterView(CharacterModel model)
        {
            Initialize(model);

            model.movement.stateChanged += HandleStateChanged;
            model.movement.speedUpdated += HandleSpeedUpdated;
        }

        public void Dispose()
        {
            if (_model == null)
                return;

            _model.movement.stateChanged -= HandleStateChanged;
            _model.movement.speedUpdated -= HandleSpeedUpdated;
        }

        private void Initialize(CharacterModel model)
        {
            _model = model;
            _gameObject = Object.Instantiate(model.entry.view);

            _animator = _gameObject.GetComponent<Animator>();
            _cacheAnimatorSpeed = _animator.GetFloat(ANIMATOR_PROPERTY_FLOAT_SPEED);
        }

        private void HandleStateChanged(MovementState state)
        {
            var jumping = false;
            switch (state)
            {
                case MovementState.Idle:
                    _animator.SetTrigger(ANIMATOR_TRIGGER_IDLE);
                    break;
                case MovementState.Run:
                    _animator.SetTrigger(ANIMATOR_TRIGGER_START);
                    break;
                case MovementState.Jump:
                case MovementState.Fly:
                    jumping = true;
                    break;
            }

            SetJumping(jumping);
        }

        private void SetJumping(bool jumping)
        {
            _animator.SetBool(ANIMATOR_PROPERTY_BOOL_JUMPING, jumping);
        }

        private void HandleSpeedUpdated()
        {
            var multiplier = _model.movement.speed / _model.entry.movement.speed;
            _animator.SetFloat(ANIMATOR_PROPERTY_FLOAT_SPEED, _cacheAnimatorSpeed * multiplier);
        }
    }
}