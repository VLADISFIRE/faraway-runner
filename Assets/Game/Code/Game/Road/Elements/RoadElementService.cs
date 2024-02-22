using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Game
{
    public class RoadElementService : IDisposable
    {
        private RoadService _service;
        private GameService _gameService;
        private RoadElementFactory _factory;

        private Dictionary<RoadSegment, List<RoadElementModel>> _elements = new Dictionary<RoadSegment, List<RoadElementModel>>();

        public event Action<RoadElementModel> cleared;
        public event Action<RoadElementModel> created;
        public event Action<RoadElementModel, ITriggerSource> triggered;

        public RoadElementService()
        {
            ServiceLocator.Get(out _service);
            ServiceLocator.Get(out _gameService);

            _factory = new RoadElementFactory();

            _service.segmentUpdated += HandleSegmentUpdated;
            _gameService.started += HandleStarted;
        }

        public void Dispose()
        {
            _service.segmentUpdated -= HandleSegmentUpdated;
            _gameService.started -= HandleStarted;
        }

        private void HandleStarted()
        {
            var skip = 3;
            foreach (var segment in _service.road.segments)
            {
                skip--;
                if (skip >= 0) continue;
                FillElementsToSegment(_service.road, segment);
            }
        }

        private void HandleSegmentUpdated(RoadModel model, RoadSegment segment)
        {
            TryClear(segment);

            FillElementsToSegment(model, segment);
        }

        private void FillElementsToSegment(RoadModel model, RoadSegment segment)
        {
            var sockets = segment.sockets.ToList();
            _elements[segment] = new List<RoadElementModel>();

            var amount = Random.Range(0, 3);
            amount = Math.Clamp(amount, 0, sockets.Count);
            for (int i = 0; i < amount; i++)
            {
                var socketIndex = Random.Range(0, sockets.Count);
                var socket = sockets[socketIndex];

                var elementIndex = Random.Range(0, model.entry.elements.Count);
                var target = model.entry.elements[elementIndex];

                var element = _factory.Create(target.entry);
                element.SetSocket(socket);
                element.triggered += HandleElementTriggered;
                created?.Invoke(element);

                _elements[segment].Add(element);

                sockets.Remove(socket);
            }
        }

        private void TryClear(RoadSegment segment)
        {
            if (_elements.TryGetValue(segment, out var list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Clear(list[i]);
                }

                list.Clear();
            }
        }

        private void Clear(RoadElementModel element)
        {
            element.Dispose();
            cleared?.Invoke(element);
            element.triggered -= HandleElementTriggered;
        }

        private void HandleElementTriggered(RoadElementModel element, ITriggerSource source)
        {
            triggered?.Invoke(element, source);
        }
    }
}