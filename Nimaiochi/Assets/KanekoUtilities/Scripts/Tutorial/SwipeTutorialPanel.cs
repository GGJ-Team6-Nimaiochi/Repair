using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class SwipeTutorialPanel : TutorialPanel
    {
        [SerializeField]
        UGUIImage fingerImage = null;

        [SerializeField]
        RectTransform startPoint = null;
        [SerializeField]
        RectTransform endPoint = null;

        [SerializeField]
        float speed = 5.0f;

        [SerializeField]
        AnimationCurve curve = null;
        
        public override void Activate()
        {
            fingerImage.RectTransform.anchoredPosition = startPoint.anchoredPosition;
            base.Activate();
        }

        protected override IEnumerator TutorialAnimation()
        {
            while (true)
            {
                yield return KKUtilities.FloatLerp(speed, (t) =>
                {
                    fingerImage.RectTransform.anchoredPosition = Vector2.LerpUnclamped(startPoint.anchoredPosition, endPoint.anchoredPosition, curve.Evaluate(t));
                });
            }
        }
    }
}