using UnityEngine;

namespace Game.InputManagement
{
    public struct SwipeInfo
    {
        public Vector2 position;
        public Vector2 delta;

        public static SwipeInfo Create(Vector2 position, Vector2 delta)
        {
            return new SwipeInfo
            {
                position = position,
                delta = delta
            };
        }

        public static SwipeInfo CreateMouse(Vector2 delta)
        {
            return new SwipeInfo
            {
                position = Input.mousePosition,
                delta = delta
            };
        }

        public static SwipeInfo CreateTouch(Touch touch)
        {
            return new SwipeInfo
            {
                position = touch.position,
                delta = touch.deltaPosition
            };
        }
    }
}