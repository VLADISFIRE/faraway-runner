using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CameraFollow
    {
        private const string TAG = "Camera";

        private readonly CinemachineVirtualCamera _cinemachine;

        public CameraFollow()
        {
            var cameraObj = GameObject.FindGameObjectWithTag(TAG);

            if (cameraObj.TryGetComponent(out CinemachineVirtualCamera cinemachine))
            {
                _cinemachine = cinemachine;
            }
        }

        public void SetTarget(GameObject target)
        {
            if (_cinemachine == null)
                return;

            _cinemachine.Follow = target.transform;
            _cinemachine.LookAt = target.transform;
        }
    }
}