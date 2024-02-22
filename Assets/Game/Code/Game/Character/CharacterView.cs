using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    //Character view controller
    public class CharacterView : IDisposable
    {
        private GameObject _gameObject;

        private Animator _animator;
        private CharacterModel _model;

        private float _cacheAnimatorSpeed;
        public GameObject gameObject { get { return _gameObject; } }

        public CharacterView(CharacterModel model)
        {
            Initialize(model);

            model.movement.stateChanged += HandleStateChanged;
        }

        public void Dispose()
        {
            if (_model == null)
                return;

            _model.movement.stateChanged -= HandleStateChanged;
        }

        private void Initialize(CharacterModel model)
        {
            _model = model;
            _gameObject = Object.Instantiate(model.entry.view);

            _animator = _gameObject.GetComponent<Animator>();
            _cacheAnimatorSpeed = _animator.GetFloat("Speed");
        }

        private void HandleStateChanged(MovementState state)
        {
            var jumping = false;
            switch (state)
            {
                case MovementState.Idle:
                    _animator.SetTrigger("Idle");
                    break;
                case MovementState.Run:
                    _animator.SetTrigger("Start");
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
            _animator.SetBool("Jumping", jumping);
        }
    }
}