using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Presenters
{
    public class RoadElementsViewPresenter : IDisposable
    {
        private RoadElementService _roadElements;

        public Dictionary<RoadElementModel, GameObject> _views = new Dictionary<RoadElementModel, GameObject>();

        public RoadElementsViewPresenter()
        {
            ServiceLocator.Get(out _roadElements);

            _roadElements.created += HandleCreated;
            _roadElements.cleared += HandleCleared;
            _roadElements.triggered += HandleTriggered;
        }

        public void Dispose()
        {
            _roadElements.created -= HandleCreated;
            _roadElements.cleared -= HandleCleared;
            _roadElements.triggered -= HandleTriggered;
        }

        private void HandleCreated(RoadElementModel model)
        {
            var view = Object.Instantiate(model.entry.view, model.root.transform);
            _views.Add(model, view);
        }

        private void HandleCleared(RoadElementModel model)
        {
            _views.Remove(model);
        }

        private void HandleTriggered(RoadElementModel model, ITriggerSource _)
        {
            if (_views.TryGetValue(model, out var go))
            {
                if (go.TryGetComponent(out DestroyComponent _))
                {
                    _views.Remove(model);
                    Object.Destroy(go);
                }
            }
        }
    }
}