using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class Character
    {
        private GameObject _characterGameObject;
        private Animator _animator;
        private CharacterSettings _settings;
        private readonly GameObject _root;
        private readonly GameObject _y;

        public Vector3 position { get { return _root != null ?_root.transform.position : Vector3.zero; } }

        public GameObject CharacterGameObject { get { return _characterGameObject; } }

        public Character(CharacterSettings settings)
        {
            _settings = settings;
            _root = new GameObject("Player");
            _y = new GameObject("Y");
            _y.transform.SetParent(_root.transform);
            
            _characterGameObject = Object.Instantiate(_settings.prefab, _y.transform);

            _animator = _characterGameObject.GetComponent<Animator>();
            _animator.SetFloat("JumpSpeed", _settings.jumpSpeed);
        }

        public void Jump()
        {
            _animator.SetBool("Jumping", true);
            _y.transform.DOJump(Vector3.zero, _settings.jumpPower, 1, _settings.jumpDuration).OnComplete(DisableJump);
        }

        public void Slide()
        {
            //_animator.SetBool("Sliding", true);
        }

        public void MoveToRight()
        {
            var x = position.x;

            if (x >= _settings.step)
                return;

            var newX = 0f;

            if (x >= 0 && x < _settings.step)
            {
                newX = _settings.step;
            }

            DoMoveX(newX);
        }

        public void MoveToLeft()
        {
            var x = position.x;

            if (x <= -_settings.step)
                return;

            var newX = 0f;

            if (x < _settings.step && x >= 0)
            {
                newX = -_settings.step;
            }

            DoMoveX(newX);
        }

        private void DoMoveX(float x, float duration = 0.3f)
        {
            x = Mathf.Clamp(x, -_settings.step, _settings.step);
            _root.transform.DOMoveX(x, duration).OnComplete(
                () =>
                {
                    var position = _root.transform.position;
                    _root.transform.position = new Vector3(x, position.y, position.z);
                });
        }

        public void Run()
        {
            _animator.SetTrigger("Start");
        }

        private void DisableJump()
        {
            _animator.SetBool("Jumping", false);
        }
    }

    [Serializable]
    public class CharacterSettings
    {
        public GameObject prefab;

        public float jumpPower = 2;
        public float jumpDuration = 1.5f;
        public float jumpSpeed = 1f;

        public float step = 1.5f;
        public float speed = 1;
    }
}