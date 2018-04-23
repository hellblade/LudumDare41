using UnityEngine;

class InputHelper
{

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

    static int? jumpTouch = null;

#endif


    public static bool isJumpJustDown()
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                jumpTouch = touch.fingerId;
                return true;
            }
        }

        return false;
#else
        return Input.GetButtonDown("Jump");
#endif
    }

    public static bool isJumpDown()
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (!jumpTouch.HasValue)
            return false;

        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == jumpTouch)
            {
                var stillDown = touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended;

                if (!stillDown)
                {
                    jumpTouch = null;
                    return false;
                }

                return true;
            }
        }

        return false;
#else
        return Input.GetButton("Jump");
#endif
    }

}
