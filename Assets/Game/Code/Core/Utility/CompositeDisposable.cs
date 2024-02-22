using System;
using System.Collections.Generic;

namespace Game.Code
{
    public abstract class CompositeDisposable : IDisposable
    {
        protected List<IDisposable> _disposables;

        public bool isDisposed { get; private set; }

        protected void AddDisposable(IDisposable disposable)
        {
            if (_disposables == null)
            {
                isDisposed = false;
                _disposables = new List<IDisposable>();
            }

            _disposables.Add(disposable);
        }

        protected void AddDisposables(params IDisposable[] disposables)
        {
            for (int i = 0; i < disposables.Length; i++)
            {
                AddDisposable(disposables[i]);
            }
        }

        public void Dispose()
        {
            BeforeDispose();

            isDisposed = true;
            if (_disposables != null)
            {
                for (int i = 0; i < _disposables.Count; i++)
                {
                    _disposables[i].Dispose();
                }

                _disposables = null;
            }

            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void BeforeDispose()
        {
        }
    }
}