using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RoadSegment
    {
        private Transform _transform;
        private List<Transform> _sockets;
        public Vector3 position { get { return _transform.position; } }

        public List<Transform> sockets { get { return _sockets; } }

        public RoadSegment(Transform transform)
        {
            _transform = transform;

            PopulateSockets(transform);
        }

        public void SetPosition(Vector3 value)
        {
            _transform.position = value;
        }

        private void PopulateSockets(Transform transform)
        {
            var list = new List<Transform>();

            for (int j = 0; j < transform.childCount; j++)
            {
                var socket = transform.GetChild(j);
                list.Add(socket);
            }

            _sockets = list;
        }
    }
}