using UnityEngine;
using UnityEngine.Events;

namespace KanekoUtilities
{
    public abstract class UGUIParts : UIParts
    {
        RectTransform rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = transform as RectTransform;
                }
                return rectTransform;
            }
        }
        //public float DefaultAlpha { get; set; }

        //protected virtual void Awake()
        //{
        //    DefaultAlpha = Alpha;
        //}

        ///// <summary>
        ///// 次第にはっきりする
        ///// </summary>
        //public void FadeIn(float duration, MonoBehaviour mono, UnityEvent callback = null)
        //{
        //    if (callback == null)
        //        mono.StartCoroutine(FadeInAnimation(this, duration));
        //    else
        //        mono.StartCoroutine(FadeInAnimation(this, duration).OnCompleted(() => callback.Invoke()));
        //}

        ///// <summary>
        ///// 徐々に見えなくする
        ///// </summary>
        //public void FadeOut(float duration, MonoBehaviour mono, UnityEvent callback = null)
        //{
        //    if (callback == null)
        //        mono.StartCoroutine(FadeOutAnimation(this, duration));
        //    else
        //        mono.StartCoroutine(FadeOutAnimation(this, duration).OnCompleted(() => callback.Invoke()));
        //}

        //public virtual MyCoroutine FadeInAnimation(UGUIParts parts, float duration)
        //{
        //    Color temp = parts.Color;
        //    return KKUtilities.FloatLerp(duration, (t) =>
        //    {
        //        temp.a = Mathf.Lerp(0.0f, DefaultAlpha, t);
        //        parts.Color = temp;
        //    });
        //}

        //public virtual MyCoroutine FadeOutAnimation(UGUIParts parts, float duration)
        //{
        //    Color temp = parts.Color;
        //    return KKUtilities.FloatLerp(duration, (t) =>
        //    {
        //        temp.a = Mathf.Lerp(DefaultAlpha, 0.0f, t);
        //        parts.Color = temp;
        //    });
        //}

        /////// <summary>
        /////// 次第にはっきりする
        /////// </summary>
        ////public void FadeIn(float duration, UnityEvent callback = null)
        ////{
        ////    FadeIn(duration, this, callback);
        ////}

        /////// <summary>
        /////// 徐々に見えなくする
        /////// </summary>
        ////public void FadeOut(float duration, UnityEvent callback = null)
        ////{
        ////    FadeOut(duration, this, callback);
        ////}
    }
}