using System;
using System.Collections;
using UnityEngine;

namespace KanekoUtilities
{
    public static class MonobehaviourExtentsions
    {
        /// <summary>
        /// duration秒後にactionを実行する
        /// </summary>
        public static Coroutine Delay(this MonoBehaviour mono, float duration, Action action, bool isScalable = true)
        {
            return mono.StartCoroutine(KKUtilities.Delay(duration, action, isScalable));
        }
        /// <summary>
        /// frameCountフレーム後にactionを実行する
        /// </summary>
        public static Coroutine DelayFrame(this MonoBehaviour mono, int frameCount, Action action)
        {
            return mono.StartCoroutine(KKUtilities.DelayFrame(frameCount, action));
        }
        
        /// <summary>
        /// 与えられたActionにduration秒かけて０→１になる値を毎フレーム渡す
        /// </summary>
        public static Coroutine FloatLerp(this MonoBehaviour mono, float duration, Action<float> action, bool isScalable = true)
        {
            return mono.StartCoroutine(KKUtilities.FloatLerp(duration, action, isScalable));
        }
        
        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static Coroutine While(this MonoBehaviour mono, Func<bool> update)
        {
            return mono.StartCoroutine(KKUtilities.While(update));
        }
        /// <summary>
        /// １フレームに１回actionを実行する(updateの戻り値は継続するか？)
        /// </summary>
        public static Coroutine DoWhile(this MonoBehaviour mono, Func<bool> update)
        {
            return mono.StartCoroutine(KKUtilities.DoWhile(update));
        }
        
        /// <summary>
        /// predicateが真を返すまで待機し、その後actionを実行する
        /// </summary>
        public static Coroutine WaitUntil(this MonoBehaviour mono, Func<bool> predicate, Action action)
        {
            return mono.StartCoroutine(KKUtilities.WaitUntil(predicate, action));
        }

        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static Coroutine WaitAction(this MonoBehaviour mono, MyUnityEvent action, Action callback = null)
        {
            return mono.StartCoroutine(KKUtilities.WaitAction(action, callback));
        }
        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static Coroutine WaitAction<T>(this MonoBehaviour mono, MyUnityEvent<T> action, Action callback = null)
        {
            return mono.StartCoroutine(KKUtilities.WaitAction(action, callback));
        }
        /// <summary>
        /// アクションが呼ばれるまで待機する
        /// </summary>
        public static Coroutine WaitAction<T1, T2>(this MonoBehaviour mono, MyUnityEvent<T1, T2> action, Action callback = null)
        {
            return mono.StartCoroutine(KKUtilities.WaitAction(action, callback));
        }
    }
}
