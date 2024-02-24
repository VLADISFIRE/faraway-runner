using System;
using Game.Core.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class WorldCurve : IDisposable
    {
        private const string GLOBAL_PROPERTY_X = "_Curve_X";
        private const string GLOBAL_PROPERTY_Y = "_Curve_Y";
        
        private const float TIME = 3;

        private const float CURVE_MIN_X = -1f;
        private const float CURVE_MAX_X = 1f;

        private const float CURVE_MIN_Y = -1f;
        private const float CURVE_MAX_Y = 0f;

        private float _targetX;
        private float _targetY;

        private float _nextX;
        private float _nextY;

        private float _speed;

        private float _t = TIME;

        public WorldCurve()
        {
            SetStartCurveWorld();
            RandomNextCurveWorld();
            RandomSpeed();

            EngineEvents.Subscribe(EventsType.Update, HandleUpdate);
        }

        public void Dispose()
        {
            EngineEvents.Unsubscribe(EventsType.Update, HandleUpdate);
        }

        private void HandleUpdate()
        {
            var x = Mathf.Lerp(_targetX, _nextX, Time.deltaTime * _speed);
            var y = Mathf.Lerp(_targetY, _nextY, Time.deltaTime * _speed);

            UpdateCurveWorld(x, y);

            _t -= Time.deltaTime;

            if (_t < 0)
            {
                _t = TIME;
                RandomNextCurveWorld();
                RandomSpeed();
            }
        }

        private void UpdateCurveWorld(float x, float y)
        {
            if (Mathf.Abs(_targetX - x) > float.Epsilon)
            {
                _targetX = Mathf.Clamp(x, CURVE_MIN_X, CURVE_MAX_X);

                Shader.SetGlobalFloat(GLOBAL_PROPERTY_X, _targetX);
            }

            if (Mathf.Abs(_targetY - y) > float.Epsilon)
            {
                _targetY = Mathf.Clamp(y, CURVE_MIN_Y, CURVE_MAX_Y);
                Shader.SetGlobalFloat(GLOBAL_PROPERTY_Y, _targetY);
            }
        }

        private void SetStartCurveWorld()
        {
            var x = Random.Range(CURVE_MIN_X, CURVE_MAX_X);
            var y = Random.Range(CURVE_MIN_Y, CURVE_MAX_Y);

            UpdateCurveWorld(x, y);
        }

        private void RandomNextCurveWorld()
        {
            _nextX = Random.Range(CURVE_MIN_X, CURVE_MAX_X);
            _nextY = Random.Range(CURVE_MIN_Y, CURVE_MAX_Y);
        }

        private void RandomSpeed()
        {
            _speed = Random.Range(0.05f, 0.3f);
        }
    }
}