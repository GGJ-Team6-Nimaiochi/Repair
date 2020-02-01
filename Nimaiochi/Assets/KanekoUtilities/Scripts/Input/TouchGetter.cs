using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class TouchGetter
    {
        static Vector3 touchPosition = Vector3.zero;
        static Vector3 zeroVec = Vector3.zero;

        public static TouchInfo GetTouch()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
            if (Input.GetMouseButton(0)) { return TouchInfo.Touching; }
            if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
            return TouchInfo.None;
#else
            if (Input.touchCount <= 0) return TouchInfo.None;
            TouchPhase phase = Input.GetTouch(0).phase;

            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    return TouchInfo.Began;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    return TouchInfo.Touching;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    return TouchInfo.Ended;
                default:
                    return TouchInfo.None;
            }
#endif
        }

        public static Vector3 GetTouchPositon()
        {
            if (GetTouch() == TouchInfo.None) return zeroVec;
#if UNITY_EDITOR
            touchPosition = Input.mousePosition;
#else
            touchPosition = Input.GetTouch(0).position;
#endif
            touchPosition.z = 0.0f;
            return touchPosition;
        }

        public static Vector3 GetTouchWorldPosition(Camera cam)
        {
            return cam.ScreenToWorldPoint(GetTouchPositon());
        }
    }

    public enum TouchInfo
    {
        None,       //画面に指が触れていない時
        Began,      //画面に指が触れた時
        Touching,   //画面に触れている時
        Ended,      //画面が指から離れた時(エラーを含む)
    }
}
