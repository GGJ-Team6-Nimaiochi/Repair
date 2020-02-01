using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class TitlePanel : Panel
{
    [SerializeField]
    AbstractUGUIText message = null;

    public override void Activate()
    {
        base.Activate();

        Debug.Log("start animation");
        StartCoroutine(AlphaAnimation());
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
}
