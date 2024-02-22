using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RoadModel
    {
        private const string TAG = "Road";
        private const float OFFSET = 9f;

        private RoadEntry _entry;

        private List<RoadSegment> _segments;

        public RoadEntry entry { get { return _entry; } }

        public event Action<RoadModel, RoadSegment> segmentUpdated;

        public List<RoadSegment> segments { get { return _segments; } }

        public RoadModel(RoadEntry entry)
        {
            _entry = entry;

            //Can be created in runtime, such a dirty method is only for testing
            var parent = GameObject.FindGameObjectWithTag(TAG);

            _segments = new List<RoadSegment>();
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                var transform = parent.transform.GetChild(i);
                var roadSegment = new RoadSegment(transform);

                _segments.Add(roadSegment);
            }
        }

        public void Next()
        {
            var target = _segments[0];
            var last = _segments[^1];
            var newPosition = last.position;

            target.SetPosition(new Vector3(newPosition.x, newPosition.y, newPosition.z + OFFSET));

            _segments.Remove(target);
            _segments.Add(target);

            segmentUpdated?.Invoke(this, target);
        }
    }
}