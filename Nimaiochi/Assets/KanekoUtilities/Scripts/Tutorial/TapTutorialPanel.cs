using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class TapTutorialPanel : TutorialPanel
    {
        [SerializeField]
        UGUIImage fingerImage = null;

        [SerializeField]
        float speed = 3.0f;

        [SerializeField]
        AnimationCurve curve = null;

        protected override IEnumerator TutorialAnimation()
        {
            float maxScale = 1.2f;
            while(true)
            {
                yield return KKUtilities.FloatLerp(speed, (t) =>
                {
                    fingerImage.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * maxScale, curve.Evaluate(t));
                });
            }
        }
    }
}