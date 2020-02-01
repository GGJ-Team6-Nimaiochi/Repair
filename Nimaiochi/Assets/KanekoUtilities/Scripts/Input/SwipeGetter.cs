using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SwipeGetter : SingletonMonobehaviour<SwipeGetter>
    {
        [SerializeField]
        TouchPanel touchPanel = null;
        [SerializeField]
        float swipePowerBoder = 10.0f;

        /// <summary>
        /// タッチ操作を受け付けるか？
        /// </summary>
        public bool CanTouch;

        /// <summary>
        /// 指の移動量
        /// </summary>
        public Vector2 DeltaPosition { get; private set; }

        /// <summary>
        /// 画面がタップされたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTap = new MyUnityEvent<Vector2>();
        /// <summary>
        /// スワイプを検出したとき（移動量）
        /// </summary>
        public MyUnityEvent<Vector2> onSwipe = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面に指が触れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouchStart = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面に指が触れているとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouching = new MyUnityEvent<Vector2>();
        /// <summary>
        /// 画面から指が離れたとき（座標）
        /// </summary>
        public MyUnityEvent<Vector2> onTouchEnd = new MyUnityEvent<Vector2>();

        float screenSizeRate = 1.0f;

        Vector2 oldTouchPosition;
        Vector2 swipeValue;
        float swipePower = 0.0f;
        float touchTime = 0.0f;
        bool isStartTouch = false;

        const int bufferSize = 5;
        Vector2[] inputBuffer = new Vector2[bufferSize];
        int currentBufIndex = 0;

        protected override void Start()
        {
            base.Start();

            Camera mainCamera = Camera.main;
            float width = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 1.0f)).x * 2.0f;
            
            if (mainCamera.orthographic)
            {
                screenSizeRate = (width / mainCamera.orthographicSize) * (1080.0f / Screen.width);
            }
            else
            {
                screenSizeRate = Screen.width / 1080.0f;
            }

            touchPanel.OnTouchStart.AddListener(OnTouchStart);
            touchPanel.OnTouchEnd.AddListener(OnTouchEnd);
            touchPanel.OnTouching.AddListener(OnTouching);
        }

        void OnTouchStart(Vector2 touchPosition)
        {
            isStartTouch = CanTouch;
            if (!isStartTouch) return;

            DeltaPosition = Vector2.zero;
            swipePower = 0.0f;
            swipeValue = Vector2.zero;
            touchTime = 0.0f;
            oldTouchPosition = TouchGetter.GetTouchPositon();

            if (onTouchStart != null) onTouchStart.Invoke(touchPosition);
        }
        void OnTouching(Vector2 touchPosition)
        {
            //移動量の計算
            touchPosition = TouchGetter.GetTouchPositon();
            DeltaPosition = (touchPosition - oldTouchPosition) * screenSizeRate;
            oldTouchPosition = touchPosition;

            //スワイプ中にCanTouchがfalseになった
            if (isStartTouch && !CanTouch)
            {
                isStartTouch = false;
                if (onTouchEnd != null) onTouchEnd.Invoke(oldTouchPosition);
            }

            //操作受け中ではなかった
            if (!isStartTouch || !CanTouch) return;

            if (onTouching != null) onTouching.Invoke(touchPosition);

            inputBuffer[currentBufIndex] = DeltaPosition;
            currentBufIndex++;
            if (currentBufIndex >= bufferSize) currentBufIndex = 0;


            if (swipePower < swipePowerBoder)
            {
                touchTime += Time.deltaTime;
                swipeValue += DeltaPosition;
                swipePower = swipeValue.magnitude;
            }
            else
            {
                Vector2 sum = Vector2.zero;
                for (int i = 0; i < bufferSize; i++)
                {
                    sum += inputBuffer[i];
                }

                DeltaPosition = sum / bufferSize;
                if (onSwipe != null) onSwipe.Invoke(DeltaPosition);
            }
        }
        void OnTouchEnd(Vector2 touchPosition)
        {
            if (!isStartTouch || !CanTouch) return;
            if (swipePower < swipePowerBoder)
            {
                if (onTap != null) onTap.Invoke(touchPosition);
            }

            if (onTouchEnd != null) onTouchEnd.Invoke(touchPosition);

            DeltaPosition = Vector2.zero;
        }
    }
}
