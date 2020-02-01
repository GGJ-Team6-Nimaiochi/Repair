using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TitlePanel : Panel
{
    [SerializeField]
    AbstractUGUIText message = null;

    [SerializeField]
    Transform titleLogoTransform = null;

    [SerializeField]
    AnimationCurve rotationCurve = null;

    void Start()
    {
        SwipeGetter.Instance.onTouchStart.AddListener((_)=> Deactivate());
    }

    public override void Activate()
    {
        base.Activate();

        StartCoroutine(AlphaAnimation());
        StartCoroutine(TitleLogoAnimation());
    }

    IEnumerator AlphaAnimation()
    {
        var wait = new WaitForSeconds(3.0f);
        while(true)
        {
            yield return wait;

            yield return KKUtilities.FloatLerp(2.0f, (t) =>
            {
                message.Alpha = Mathf.Lerp(1.0f, 0.0f, Easing.Yoyo(Easing.InQuad(t)));
            });
        }
    }

    IEnumerator TitleLogoAnimation()
    {
        var wait = new WaitForSeconds(4.0f);
        while(true)
        {
            yield return wait;

            yield return KKUtilities.FloatLerp(3.0f, (t) =>
            {
                titleLogoTransform.SetRotationZ(Mathf.LerpUnclamped(0.0f, 10.0f, rotationCurve.Evaluate(t)));
            });
        }
    }
}
