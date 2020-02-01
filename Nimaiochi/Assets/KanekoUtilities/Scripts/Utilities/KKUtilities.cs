using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace KanekoUtilities
{
    public class KKUtilities
    {
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static IEnumerator Delay(float duration, Action action, bool isScalable = true)
        {
            if (isScalable)
                yield return new WaitForSeconds(duration);
            else
                yield return new WaitForSecondsRealtime(duration);

            action.Invoke();
        }

        /// <summary>
        /// frameCountフレーム後にactionを実行する
        /// </summary>
        public static IEnumerator DelayFrame(int frameCount, Action action)
        {
            for (int i = 0; i < frameCount; i++)
            {
                yield return null;
            }

            action.Invoke();
        }

        /// <summary>
        /// 与えられたActionにduration秒かけて０→１になる値を毎フレーム渡す
        /// </summary>
        public static MyCoroutine FloatLerp(float duration, Action<float> action, bool isScalable = true)
        {
            if (isScalable)
                return new MyCoroutine(ScalableFloatLerp(duration, action));
            else
                return new MyCoroutine(UnscalableFloatLerp(duration, action));
        }
        static IEnumerator ScalableFloatLerp(float duration, Action<float> action)
        {
            float t = 0.0f;

            while (true)
            {
                t += Time.deltaTime;
                action.Invoke(t / duration);
                if (t > duration) break;
                yield return null;
            }

            action.Invoke(1.0f);
        }
        static IEnumerator UnscalableFloatLerp(float duration, Action<float> action)
        {
            float t = 0.0f;

            while (true)
            {
                t += Time.unscaledDeltaTime;
                action.Invoke(t / duration);
                if (t > duration) break;
                yield return null;
            }

            action.Invoke(1.0f);
        }

        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static IEnumerator While(Func<bool> update)
        {
            while (update.Invoke())
            {
                yield return null;
            }
        }

        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static IEnumerator DoWhile(Func<bool> update)
        {
            do
            {
                yield return null;
            }
            while (update.Invoke());
        }

        /// <summary>
        /// predicateが真を返すまで待機し、その後actionを実行する
        /// </summary>
        public static IEnumerator WaitUntil(Func<bool> predicate, Action action)
        {
            yield return new WaitUntil(predicate);

            action.Invoke();
        }

        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction(UnityEvent action, Action callback = null)
        {
            bool isCalled = false;
            UnityAction act = () => isCalled = true;

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
            if (callback != null) callback.Invoke();
        }
        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction<T>(MyUnityEvent<T> action, Action callback = null)
        {
            bool isCalled = false;
            UnityAction<T> act = ((arg) => isCalled = true);

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
            if (callback != null) callback.Invoke();
        }
        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static IEnumerator WaitAction<T1, T2>(MyUnityEvent<T1, T2> action, Action callback = null)
        {
            bool isCalled = false;
            UnityAction<T1, T2> act = ((arg1, arg2) => isCalled = true);

            action.AddListener(act);

            yield return new WaitUntil(() => isCalled);

            action.RemoveListener(act);
            if (callback != null) callback.Invoke();
        }

        /// <summary>
        /// 0~360の値で返す(-10なら350)
        /// </summary>
        public static float ClampRotation(float rotationValue)
        {
            if (rotationValue == 0.0f) return 0.0f;
            float clampValue = 0.0f;

            int temp = (int)(Mathf.Abs(rotationValue) / 360.0f);
            if (rotationValue > 0)
            {
                clampValue = rotationValue - (360.0f * temp);
            }
            else
            {
                clampValue = rotationValue + (360.0f * (temp + 1));
            }

            return clampValue;
        }

        static readonly string[] UnitTexts = { "k", "m", "b", "t", "q" };

        /// <summary>
        /// 桁の多い数字をstringに変換する
        /// 
        /// ex.
        /// 1000->1.00k
        /// 10000->10.0k
        /// </summary>
        public static string GetDigitText(long number)
        {
            //桁数を算出する
            int digit = GetDigitNum(number);

            if (digit < 4) return number.ToString();

            int temp = ((digit - 1) / 3);
            int unitTextIndex = temp - 1;

            if (unitTextIndex >= UnitTexts.Length) return "infinite";
            string unitText = UnitTexts[unitTextIndex];

            temp = (digit - 1) % 3;

            string result;
            string resultValueText = number.ToString();

            if (temp == 0)
                result = resultValueText.Substring(0, 1) + "." + resultValueText.Substring(1, 2);
            else if (temp == 1)
                result = resultValueText.Substring(0, 2) + "." + resultValueText.Substring(2, 1);
            else
                result = resultValueText.Substring(0, 3);

            return result + unitText;
        }

        /// <summary>
        /// 桁数を返す
        /// </summary>
        public static int GetDigitNum(long number)
        {
            return (int)Math.Log10(number) + 1;
        }

        public static long LongPow(long x, long y)
        {
            if (y == 0) return 1;
            if (y == 1) return x;

            long value = x;

            for (int i = 0; i < y; i++)
            {
                value *= x;
            }

            return value;
        }
    }

    public class MyUnityEvent : UnityEvent {}
    public class MyUnityEvent<T> : UnityEvent<T> { }
    public class MyUnityEvent<T1, T2> : UnityEvent<T1, T2> { }
}