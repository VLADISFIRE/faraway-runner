using UnityEngine;

namespace Game.InputManagement
{
    public struct TapInfo
    {
        public TouchPhase touchPhase;
        public Vector2 position;
        public int pointerId;

        public static TapInfo Create(TouchPhase phase, Vector2 pos, int pointerId = -1)
        {
            return new TapInfo
            {
                touchPhase = phase,
                position = pos,
                pointerId = pointerId
            };
        }

        public static TapInfo CreateTouch(Touch touch)
        {
            return Create(touch.phase, touch.position, touch.fingerId);
        }

        public static TapInfo CreateMouse(TouchPhase phase)
        {
            return Create(phase, Input.mousePosition);
        }

        public override string ToString()
        {
            return $"Tap info - " +
                $"Phase: [{touchPhase}] " +
                $"Position: [{position}] " +
                $"PointerId: [{pointerId}]";
        }
    }
}