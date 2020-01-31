using System;
using UnityEngine;

namespace KanekoUtilities
{
    public class ContinueButton : UGUIButton
    {
        [SerializeField]
        float timeLimit = 5.0f;

        [SerializeField]
        UGUIImage timeLimitGauge = null;

        [SerializeField]
        UGUIButton noThanksButton = null;
        
        Coroutine countDownCoroutine;

        public void StartCountDown(Action onEndCountDown, Action onNoThanks)
        {
            if (countDownCoroutine != null)
            {
                StopCoroutine(countDownCoroutine);
            }

            noThanksButton.OnClickEvent.RemoveAllListeners();
            noThanksButton.AddListener(() =>
            {
                if(onNoThanks != null) onNoThanks.Invoke();
            });

            countDownCoroutine = StartCoroutine(KKUtilities.FloatLerp(timeLimit, (t) =>
            {
                timeLimitGauge.Image.fillAmount = 1.0f - t;
            }).OnCompleted(() =>
            {
                if (onEndCountDown != null) onEndCountDown.Invoke();
                gameObject.SetActive(false);
                countDownCoroutine = null;
            }));
        }

        public void StopCountDown()
        {
            if (countDownCoroutine != null)
            {
                StopCoroutine(countDownCoroutine);
            }

            countDownCoroutine = null;

            gameObject.SetActive(false);
        }
    }
}