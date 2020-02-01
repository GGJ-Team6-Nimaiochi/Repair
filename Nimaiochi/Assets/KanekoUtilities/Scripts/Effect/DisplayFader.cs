using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    /// <summary>
    /// FadeIn,FadeOut,Flashをサポートする
    /// </summary>
    public class DisplayFader : SingletonMonobehaviour<DisplayFader>
    {
        [SerializeField]
        UGUIImage panel = null;

        Coroutine fadeCoroutine;

        /// <summary>
        /// パネルの色を設定する（Alphaは適用されない）
        /// </summary>
        public void SetPanelColor(Color color)
        {
            color.a = panel.Color.a;
            panel.Color = color;
        }

        IEnumerator Fade(float startAlpha, float endAlpha, float duration, Action endCallback)
        {
            panel.gameObject.SetActive(true);

            yield return this.FloatLerp(duration, (t) =>
            {
                panel.Alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            });

            fadeCoroutine = null;

            if (endCallback != null) endCallback.Invoke();
        }
        
        /// <summary>
        /// 次第にはっきりする
        /// </summary>
        public void FadeIn(float duration, Action endCallback = null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            endCallback += () =>
            {
                panel.gameObject.SetActive(false);
            };

            fadeCoroutine = StartCoroutine(Fade(1.0f, 0.0f, duration, endCallback));
        }

        /// <summary>
        /// 徐々に見えなくする
        /// </summary>
        public void FadeOut(float duration, Action endCallback = null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(Fade(0.0f, 1.0f, duration, endCallback));
        }

        /// <summary>
        /// 画面を一瞬白にする
        /// </summary>
        public void Flash()
        {
            Color defaultColor = panel.Color;
            SetPanelColor(Color.white);
            FadeIn(0.2f, ()=>
            {
                FadeOut(0.1f, () =>
                {
                    SetPanelColor(defaultColor);
                });
            });
        }
    }
}