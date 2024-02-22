using Game.Core.Events;
using UnityEngine;

namespace Game.InputManagement
{
    public class DesktopInputReader : BaseInputReader
    {
        private Vector3 _lastMousePosition;

        protected override void ReadInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePosition = Input.mousePosition;

                InvokeTap(TapInfo.CreateMouse(TouchPhase.Began));
                RegisterTaps();

                BeginSwipe();
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 delta = Input.mousePosition - _lastMousePosition;
                bool isMoved = delta.magnitude > 0;
                TouchPhase touchPhase = isMoved ? TouchPhase.Moved : TouchPhase.Stationary;

                InvokeTap(TapInfo.CreateMouse(touchPhase));

                if (isMoved)
                {
                    InvokeSwipe(SwipeInfo.CreateMouse(delta));
                    _lastMousePosition = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                InvokeTap(TapInfo.CreateMouse(TouchPhase.Ended));
                ClearDoubleTap();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                BeginSwipe();
                InvokeSwipe(SwipeInfo.CreateMouse(Vector2.down));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                BeginSwipe();
                InvokeSwipe(SwipeInfo.CreateMouse(Vector2.up));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                BeginSwipe();
                InvokeSwipe(SwipeInfo.CreateMouse(Vector2.right));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                BeginSwipe();
                InvokeSwipe(SwipeInfo.CreateMouse(Vector2.left));
            }
        }
    }
}