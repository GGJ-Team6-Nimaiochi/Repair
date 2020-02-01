using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class ConfigMenu : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup container = null;

        //歯車
        [SerializeField]
        Button viewConfigButton = null;

        [SerializeField]
        ConfigButton[] configButtons = null;

        bool isShow = false;

        UIAnimation buttonShowAnimation;

        void Start()
        {
            viewConfigButton.onClick.AddListener(() =>
            {
                isShow = !isShow;
                if (isShow) ShowMenu();
                else HideMenu();
            });

            for (int i = 0; i < configButtons.Length; i++)
            {
                configButtons[i].Init();
            }

            buttonShowAnimation = new UIAnimation((part, t) =>
            {
                part.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, Easing.OutBack(t));
                part.Alpha = Mathf.Lerp(0.0f, 1.0f, Easing.OutQuad(t));
            });
        }

        public void ShowMenu()
        {
            StopAllCoroutines();

            StartCoroutine(ShowMenuAnimation());
        }

        IEnumerator ShowMenuAnimation()
        {
            WaitForSeconds wait = new WaitForSeconds(0.1f);

            for (int i = 0; i < configButtons.Length; i++)
            {
                configButtons[i].transform.localScale = Vector3.zero;
                configButtons[i].gameObject.SetActive(false);
            }

            container.gameObject.SetActive(true);
            container.alpha = 1.0f;

            for (int i = 0; i < configButtons.Length; i++)
            {
                ConfigButton button = configButtons[i];
                button.gameObject.SetActive(true);
                StartCoroutine(buttonShowAnimation.GetAnimation(button, 0.3f));

                yield return wait;
            }
        }

        public void HideMenu()
        {
            StopAllCoroutines();

            StartCoroutine(KKUtilities.FloatLerp(0.3f, (t) =>
            {
                container.alpha = Mathf.Lerp(1.0f, 0.0f, t);
            }).OnCompleted(() =>
            {
                container.gameObject.SetActive(false);
            }));
        }
    }
}