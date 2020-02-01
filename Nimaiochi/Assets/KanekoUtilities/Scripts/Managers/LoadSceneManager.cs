using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KanekoUtilities
{
    public class LoadSceneManager : SingletonMonobehaviour<LoadSceneManager>
    {
        [SerializeField]
        Image panel = null;

        Coroutine fadeCoroutine = null;

        /// <summary>
        /// 普通のやつ
        /// </summary>
        public void LoadScene(string loadSceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            try
            {
                SceneManager.LoadScene(loadSceneName, mode);
            }
            catch
            {
                Debug.LogError(loadSceneName + " is not found");
            }
        }
        public AsyncOperation LoadSceneAsync(string loadSceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation asyncOperation = null;
            try
            {
                asyncOperation = SceneManager.LoadSceneAsync(loadSceneName, mode);
            }
            catch
            {
                Debug.LogError(loadSceneName + " is not found");
            }

            return asyncOperation;
        }

        /// <summary>
        /// フェード付きのやつ
        /// </summary>
        public void LoadScene(string loadSceneName, Color fadeColor, float duration = 1.0f)
        {
            FadeOut(duration, fadeColor, () =>
            {
                LoadScene(loadSceneName);
                FadeIn(duration, fadeColor);
            });
        }
        public void LoadSceneAsync(string loadSceneName, Color fadeColor, Action<float> loading, float duration = 1.0f)
        {
            FadeOut(duration, fadeColor, () =>
            {
                AsyncOperation asyncOperation = LoadSceneAsync(loadSceneName);
                bool isDone = false;
                this.While(() =>
                {
                    if(loading != null) loading.Invoke(asyncOperation.progress);
                    isDone = asyncOperation.isDone;
                    //ロードが終わったらフェードアウトする
                    if (isDone) FadeIn(duration, fadeColor);

                    return !isDone;
                });
            });
        }
        
        IEnumerator Fade(Color startColor, Color endColor, float duration, Action endCallback, bool finishedPanelActive)
        {
            panel.color = startColor;
            panel.gameObject.SetActive(true);

            yield return StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
                {
                    panel.color = Color.Lerp(startColor, endColor, t);
                })
            );

            fadeCoroutine = null;
            panel.gameObject.SetActive(finishedPanelActive);
            if (endCallback != null) endCallback.Invoke();
        }
        /// <summary>
        /// 次第にはっきりする
        /// </summary>
        void FadeIn(float duration, Color fadeColor, Action endCallback = null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(Fade(fadeColor, Color.clear, duration, endCallback, false));
        }
        /// <summary>
        /// 徐々に見えなくする
        /// </summary>
        void FadeOut(float duration, Color fadeColor, Action endCallback = null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(Fade(Color.clear, fadeColor, duration, endCallback, true));
        }
    }
}