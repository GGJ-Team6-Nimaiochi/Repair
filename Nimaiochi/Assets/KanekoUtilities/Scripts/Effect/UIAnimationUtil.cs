using System;
using System.Collections;
using UnityEngine;

namespace KanekoUtilities
{
    public class UIAnimation
    {
        Action<UIParts, float> logic;

        public UIAnimation(Action<UIParts, float> logic)
        {
            this.logic = logic;
        }

        public MyCoroutine GetAnimation(UIParts parts, float duration)
        {
            return new MyCoroutine(KKUtilities.FloatLerp(duration, (t) => logic(parts, t)));
        }
    }

    public static class UIAnimationUtil
    {
        static UIAnimation defaultShowAnimation;
        public static UIAnimation DefaultShowAnimation
        {
            get
            {
                if (defaultShowAnimation == null)
                {
                    defaultShowAnimation = new UIAnimation((parts, t) =>
                    {
                        parts.Alpha = Mathf.Lerp(0.0f, parts.DefaultAlpha, Easing.OutQuad(t));
                    });
                }

                return defaultShowAnimation;
            }
        }

        static UIAnimation defaultHideAnimation;
        public static UIAnimation DefaultHideAnimation
        {
            get
            {
                if (defaultHideAnimation == null)
                {
                    defaultHideAnimation = new UIAnimation((parts, t) =>
                    {
                        parts.Alpha = Mathf.Lerp(parts.DefaultAlpha, 0.0f, Easing.Linear(t));
                    });
                }

                return defaultHideAnimation;
            }
        }

        public static UIAnimation AlphaControlAnimation(float startAlpha, float endAlpha, EaseType ease = EaseType.Linear)
        {
            return new UIAnimation((parts, t) =>
            {
                parts.Alpha = Mathf.Lerp(startAlpha, endAlpha, Easing.GetEase(t, ease));
            });
        }

        static UIAnimation popUPAnimation;
        public static UIAnimation PopUpAnimation
        {
            get
            {
                if (popUPAnimation == null)
                {

                    float temp;
                    Vector3 startScale = Vector3.one * 0.3f;
                    Vector3 endScale = Vector3.one;

                    popUPAnimation = new UIAnimation(((parts, t) =>
                    {
                        temp = Easing.OutQuad(t);
                        
                        parts.Alpha = Mathf.Lerp(0.0f, parts.DefaultAlpha, temp);

                        parts.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, temp);
                    }));
                }

                return popUPAnimation;
            }
        }
    }
}