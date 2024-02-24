using System;
using Game.Core.Events;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public enum MovementState
    {
        Idle,
        Run,
        Jump,
        Fly
    }

    public class CharacterMovement
    {
        private const float LINE_OFFSET = 1.5f;
        private const float MOVE_ERROR = 0.01f;

        private CharacterMovementSettings _settings;

        private GameObject _root;

        private MovementState _state;
        private float _speed;

        private float _focusX;
        private float _x;
        private float _y;

        private bool _isSlow;
        private bool _isBoost;

        private CharacterController _controller;

        public CharacterMovementSettings settings { get { return _settings; } }

        public float speed
        {
            get
            {
                if (_state == MovementState.Fly)
                    return _settings.flySpeed;

                if (_isSlow && !_isBoost)
                    return _speed / 2;

                if (_isBoost && !_isSlow)
                    return _speed * 2;

                return _speed;
            }
        }

        public float jumpForce { get { return _settings.jumpForce; } }

        public GameObject root { get { return _root; } }

        public event Action<MovementState> stateChanged;

        public event Action<Vector3> positionChanged;

        public event Action speedUpdated;

        public CharacterMovement(CharacterMovementSettings settings)
        {
            _settings = settings;

            _speed = _settings.speed;

            _root = Object.Instantiate(_settings.controller);
            _controller = _root.GetComponent<CharacterController>();

            EngineEvents.Subscribe(EventsType.Update, HandleUpdated);
        }

        public void Dispose()
        {
            EngineEvents.Unsubscribe(EventsType.Update, HandleUpdated);
        }

        private void HandleUpdated()
        {
            if (_controller == null) return;
            if (_state == MovementState.Idle) return;

            var transform = root.transform;
            var position = transform.position;

            var y = _y * Time.deltaTime;

            var positionY = position.y;
            if (_state == MovementState.Fly && positionY >= _settings.flyY)
            {
                _y = 0;
                transform.position = new Vector3(position.x, _settings.flyY, position.z);
            }

            var motion = new Vector3(_x - position.x, y, Time.deltaTime * speed);
            _x = Mathf.Lerp(_x, _focusX, Time.deltaTime * _settings.dodgeSpeed);
            _controller.Move(motion);

            Fall();

            position = transform.position;
            positionChanged?.Invoke(position);
        }

        public void ToRun()
        {
            _y = 0;
            SetState(MovementState.Run);
            NotifySpeedUpdated();
        }

        public void SetFly()
        {
            if (_state == MovementState.Fly) return;

            _y = _settings.flyY;
            SetState(MovementState.Fly);
            NotifySpeedUpdated();
        }

        public void Jump()
        {
            if (!_controller.isGrounded)
                return;

            _y = jumpForce;
            SetState(MovementState.Jump);
        }

        public void Down()
        {
            if (_state != MovementState.Jump)
                return;

            _y -= 10;
        }

        public void MoveToLeft()
        {
            if (_focusX <= -LINE_OFFSET)
                return;

            var newX = 0f;

            if (_focusX <= MOVE_ERROR && _focusX >= -LINE_OFFSET)
            {
                newX = -LINE_OFFSET;
            }

            _focusX = newX;
        }

        public void MoveToRight()
        {
            if (_focusX >= LINE_OFFSET)
                return;

            var newX = 0f;

            if (_focusX >= -MOVE_ERROR && _focusX < LINE_OFFSET)
            {
                newX = LINE_OFFSET;
            }

            _focusX = newX;
        }

        public void SetState(MovementState state)
        {
            if (_state == state) return;

            _state = state;
            stateChanged?.Invoke(state);
        }

        public void SetSlow(bool value)
        {
            _isSlow = value;
            NotifySpeedUpdated();
        }

        public void SetBoost(bool value)
        {
            _isBoost = value;
            NotifySpeedUpdated();
        }

        private void NotifySpeedUpdated()
        {
            speedUpdated?.Invoke();
        }

        private void Fall()
        {
            if (_state == MovementState.Fly) return;

            if (_controller.isGrounded)
            {
                SetState(MovementState.Run);
                return;
            }

            _y -= Time.deltaTime * _settings.gravity;
        }
    }

    [Serializable]
    public struct CharacterMovementSettings
    {
        public GameObject controller;

        public float jumpForce;
        public float speed;
        public float dodgeSpeed;
        public float gravity;
        public float flySpeed;
        public float flyY;
    }
}