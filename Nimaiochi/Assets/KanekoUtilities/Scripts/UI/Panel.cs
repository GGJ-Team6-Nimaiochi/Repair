using UnityEngine;

namespace KanekoUtilities
{
    //画面いっぱいに出るUI
    public class Panel : UGUIParts
    {
        [SerializeField]
        UGUIImage image = null;

        [SerializeField]
        protected CanvasGroup container;

        [SerializeField]
        bool activeOnAwake = false;
        [SerializeField]
        float showAnimationTime = 0.5f;
        [SerializeField]
        float hideAnimationTime = 0.2f;
        
        protected UIAnimation showAnimation;
        protected UIAnimation hideAnimation;

        protected virtual void Awake()
        {
            image.gameObject.SetActive(activeOnAwake);
            container.gameObject.SetActive(activeOnAwake);
            SetAnimation();
        }

        protected virtual void SetAnimation()
        {
            showAnimation = UIAnimationUtil.DefaultShowAnimation;
            hideAnimation = UIAnimationUtil.DefaultHideAnimation;
        }

        public virtual void Activate()
        {
            StopAllCoroutines();
            container.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            StartCoroutine(showAnimation.GetAnimation(image, showAnimationTime));
            StartCoroutine(showAnimation.GetAnimation(this, showAnimationTime));
        }

        public virtual void Deactivate()
        {
            StopAllCoroutines();

            StartCoroutine(hideAnimation.GetAnimation(image, hideAnimationTime).OnCompleted(()=>
            {
                image.gameObject.SetActive(false);
            }));
            StartCoroutine(hideAnimation.GetAnimation(this, hideAnimationTime).OnCompleted(() =>
            {
                container.gameObject.SetActive(false);
            }));
        }

        public override float Alpha
        {
            get
            {
                return container.alpha;
            }

            set
            {
                container.alpha = value;
            }
        }

        public override Color Color
        {
            get
            {
                return image.Color;
            }

            set
            {
                image.Color = value;
            }
        }

        //protected virtual void Awake()
        //{
        //    group = GetComponent<CanvasGroup>();
        //    Image.enabled = activeOnAwake;
        //    container = transform.GetChild(0).gameObject;
        //    container.SetActive(activeOnAwake);

        //    group.alpha = activeOnAwake ? 1.0f : 0.0f;
        //}

        //public virtual void Activate()
        //{
        //    Image.enabled = true;
        //    container.SetActive(true);
        //    group.interactable = true;
        //    group.blocksRaycasts = true;
        //    PlayAlphaControlAnimation(0.0f, 1.0f, activateDuration);
        //}

        //public virtual void Deactivate()
        //{
        //    Image.enabled = false;
        //    group.interactable = false;
        //    group.blocksRaycasts = false;
        //    PlayAlphaControlAnimation(1.0f, 0.0f, deactivateDuration, ()=> container.SetActive(false));
        //}

        //void PlayAlphaControlAnimation(float startAlpha, float endAlpha, float duration, Action callback = null)
        //{
        //    if (alphaControlCoroutine != null)
        //    {
        //        StopCoroutine(alphaControlCoroutine);
        //    }

        //    alphaControlCoroutine = StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
        //    {
        //        group.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
        //    }).OnCompleted(() =>
        //    {
        //        if (callback != null) callback();
        //        alphaControlCoroutine = null;
        //    }));
        //}
    }
}
