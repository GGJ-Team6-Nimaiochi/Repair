using System;
using UnityEngine;

namespace KanekoUtilities
{
    public interface IDialog
    {
        void Show();
        void Hide();
    }

    public class Dialog : Window, IDialog
    {
        [SerializeField]
        UGUIImage panel = null;

        [SerializeField]
        float showAnimationTime = 0.5f;
        [SerializeField]
        float hideAnimationTime = 0.2f;

        [HideInInspector]
        public string DialogName;

        UIAnimation showAnimation;
        UIAnimation hideAnimation = UIAnimationUtil.DefaultHideAnimation;

        public MyUnityEvent OnShowAnimationEnd = new MyUnityEvent();
        public MyUnityEvent OnHideAnimationEnd = new MyUnityEvent();

        protected virtual void Awake()
        {
            Container.SetActive(false);
            Vector3 startScale = Vector3.one * 0.5f;
            Vector3 endScale = Vector3.one;

            showAnimation = new UIAnimation((parts, t) =>
            {
                parts.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, Easing.OutBack(t, 3.0f));
            });
        }

        protected virtual void Start() { }

        public virtual void Show()
        {
            panel.gameObject.SetActive(true);
            Container.SetActive(true);
            Alpha = 1;
            StartCoroutine(KKUtilities.FloatLerp(showAnimationTime * 0.5f, (t) =>
            {
                panel.Alpha = Mathf.Lerp(0.0f, panel.DefaultAlpha, Easing.InQuad(t));
            }));
            
            StartCoroutine(showAnimation.GetAnimation(this, showAnimationTime).OnCompleted(() =>
            {
                OnShowAnimationEnd.Invoke();
            }));
        }
        public virtual void Hide()
        {
            StartCoroutine(KKUtilities.FloatLerp(showAnimationTime * 0.5f, (t) =>
            {
                panel.Alpha = Mathf.Lerp(panel.DefaultAlpha, 0.0f, Easing.OutQuad(t));
            }));

            StartCoroutine(hideAnimation.GetAnimation(this, hideAnimationTime).OnCompleted(() =>
            {
                panel.gameObject.SetActive(false);
                Container.SetActive(false);
                OnHideAnimationEnd.Invoke();
            }));
        }
    }
}