using System;
using UnityEngine;

namespace Game
{
    public class RoadService : IDisposable
    {
        private const float OFFSET = 9f;

        private PlayerService _playerService;

        private float _x;
        private RoadModel _road;

        public RoadModel road { get { return _road; } }

        public event Action<RoadModel, RoadSegment> segmentUpdated;

        public RoadService()
        {
            ServiceLocator.Get(out _playerService);
            var settings = ServiceLocator.Get<GameSettingsScrobject>();

            _road = new RoadModel(settings.road);
            _road.segmentUpdated += HandleUpdated;

            _x = OFFSET;
            _playerService.player.character.movement.positionChanged += HandlePositionChanged;
        }

        public void Dispose()
        {
            _playerService.player.character.movement.positionChanged -= HandlePositionChanged;
            
            _road.segmentUpdated -= HandleUpdated;
        }

        private void HandlePositionChanged(Vector3 position)
        {
            if (position.z > _x)
            {
                _x += OFFSET;
                _road.Next();
            }
        }

        private void HandleUpdated(RoadModel model, RoadSegment segment)
        {
            segmentUpdated?.Invoke(model, segment);
        }
    }
}