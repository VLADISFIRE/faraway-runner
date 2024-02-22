using Game.Core.Events;
using UnityEngine;

namespace Game.InputManagement
{
    public class MobileBaseInputReader : BaseInputReader
    {
        protected override void ReadInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                InvokeTap(TapInfo.CreateTouch(touch));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (Input.touchCount == 1)
                        {
                            RegisterTaps();
                        }

                        BeginSwipe();

                        break;
                    case TouchPhase.Moved:
                        var info = SwipeInfo.CreateTouch(touch);
                        InvokeSwipe(info);
                        break;
                    case TouchPhase.Ended:
                        ClearDoubleTap();
                        break;
                }
            }
        }
    }
}