using System;
using UnityEngine;

namespace Game.Code.Core
{
    public class LifecycleComponent : MonoBehaviour
    {
        private IInitializer _initializer;

        public void Initialize(IInitializer initializer)
        {
            _initializer = initializer;
        }

        private void Start()
        {
            _initializer.Initialize();
        }

        private void OnDestroy()
        {
            if (_initializer is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}