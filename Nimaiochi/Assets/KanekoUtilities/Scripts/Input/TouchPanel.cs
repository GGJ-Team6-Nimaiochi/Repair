using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Image))]
    public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// 画面に指が触れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> OnTouchStart = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面に指が触れているとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> OnTouching = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面から指が離れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> OnTouchEnd = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面に指が触れているか
        /// </summary>
        public bool IsTouching { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsTouching = true;
            OnTouchStart.Invoke(TouchGetter.GetTouchPositon());
        }

        void Update()
        {
            if (!IsTouching) return;
            OnTouching.Invoke(TouchGetter.GetTouchPositon());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsTouching) return;
            
            IsTouching = false;
            OnTouchEnd.Invoke(TouchGetter.GetTouchPositon());
        }
    }
}
