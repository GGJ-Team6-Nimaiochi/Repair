using UnityEngine;

namespace KanekoUtilities
{
    public enum EaseType
    {
        Linear,
        InQuad, InCubic, InQuart, InQuint, InExpo, InBack, InBounce,
        OutQuad, OutCubic, OutQuart, OutQuint, OutExpo, OutBack, OutBounce,
        InOutQuad, InOutCubic, InOutQuart, InOutQuint, InOutExpo, InOutBack, InOutBounce
    }
    public static class Easing
    {
        public static float Linear(float x)
        {
            return x;
        }

        public static float InQuad(float x)
        {
            return x * x;
        }
        public static float OutQuad(float x)
        {
            return 1.0f - InQuad(1 - x);
        }
        public static float InOutQuad(float x)
        {
            return x < 0.5f ?
                2.0f * InQuad(x) :
                1.0f - InQuad(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InCubic(float x)
        {
            return x * x * x;
        }
        public static float OutCubic(float x)
        {
            return 1.0f - InCubic(1.0f - x);
        }
        public static float InOutCubic(float x)
        {
            return x < 0.5f ?
                4.0f * x * x * x :
                1.0f - InCubic(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InQuart(float x)
        {
            return x * x * x * x;
        }
        public static float OutQuart(float x)
        {
            return 1.0f - InQuart(1.0f - x);
        }
        public static float InOutQuart(float x)
        {
            return x < 0.5f ?
                8.0f * x * x * x * x :
                1.0f - InQuart(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InQuint(float x)
        {
            return x * x * x * x * x;
        }
        public static float OutQuint(float x)
        {
            return 1.0f - InQuint(1.0f - x);
        }
        public static float InOutQuint(float x)
        {
            return x < 0.5f ?
                16.0f * x * x * x * x * x :
                1.0f - InQuint(-2.0f * x + 2.0f) * 0.5f;
        }

        public static float InExpo(float x)
        {
            return x == 0 ? 0 : Mathf.Pow(2.0f, 10.0f * x - 10.0f);
        }
        public static float OutExpo(float x)
        {
            return x == 1 ? 1 : 1 - Mathf.Pow(2.0f, -10.0f * x);
        }
        public static float InOutExpo(float x)
        {
            if (x == 0.0f) return 0.0f;
            if (x == 1.0f) return 1.0f;

            return x < 0.5f ?
                Mathf.Pow(2.0f, 20.0f * x - 10.0f) * 0.5f :
                (2.0f - Mathf.Pow(2.0f, -20.0f * x + 10.0f)) * 0.5f;
        }

        public static float InBack(float t, float s = 1.70158f)
        {
            return t * t * ((s + 1.0f) * t - s);
        }
        public static float OutBack(float t, float s = 1.70158f)
        {
            return 1.0f - InBack(1.0f - t, s);
        }
        public static float InOutBack(float t, float s = 1.70158f)
        {
            return t < 0.5f ? InBack(t * 2.0f, s) * 0.5f :
                OutBack(t * 2.0f - 1.0f, s) * 0.5f + 0.5f;
        }
        
        public static float InBounce(float t)
        {
            return 1.0f - OutBounce(1.0f - t);
        }

        public static float OutBounce(float t)
        {
            float a = 2.75f;
            float s = 7.5625f;

            if (t < (1.0f / a))
            {
                return s * t * t;
            }
            else if (t < (2.0f / a))
            {
                return s * (t -= (1.5f / a)) * t + 0.75f;
            }
            else if (t < (2.5f / a))
            {
                return s * (t -= (2.25f / a)) * t + 0.9375f;
            }
            else
            {
                return s * (t -= (2.625f / a)) * t + 0.984375f;
            }
        }

        public static float InOutBounce(float t)
        {
            return t < 0.5f ? InBounce(t * 2.0f) * 0.5f :
             OutBounce(t * 2.0f - 1.0f) * 0.5f + 0.5f;
        }

        public static float Yoyo(float t)
        {
            if (t < 0.5f) return t * 2.0f;
            else return 1.0f - ((t - 0.5f) * 2.0f);
        }

        public static float GetEase(float t, EaseType ease)
        {
            switch (ease)
            {
                case EaseType.Linear: return Linear(t);

                case EaseType.InQuad: return InQuad(t);
                case EaseType.InCubic: return InCubic(t);
                case EaseType.InQuart: return InQuart(t);
                case EaseType.InQuint: return InQuint(t);
                case EaseType.InExpo: return InExpo(t);
                case EaseType.InBack: return InBack(t);
                case EaseType.InBounce: return InBounce(t);

                case EaseType.OutQuad: return OutQuad(t);
                case EaseType.OutCubic: return OutCubic(t);
                case EaseType.OutQuart: return OutQuart(t);
                case EaseType.OutQuint: return OutQuint(t);
                case EaseType.OutExpo: return OutExpo(t);
                case EaseType.OutBack: return OutBack(t);
                case EaseType.OutBounce: return OutBounce(t);

                case EaseType.InOutQuad: return InOutQuad(t);
                case EaseType.InOutCubic: return InOutCubic(t);
                case EaseType.InOutQuart: return InOutQuart(t);
                case EaseType.InOutQuint: return InOutQuint(t);
                case EaseType.InOutExpo: return InOutExpo(t);
                case EaseType.InOutBack: return InOutBack(t);
                case EaseType.InOutBounce: return InOutBounce(t);
            }

            return t;
        }
    }
}