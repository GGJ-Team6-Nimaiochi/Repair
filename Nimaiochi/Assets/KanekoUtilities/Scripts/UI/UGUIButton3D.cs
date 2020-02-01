using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    /// <summary>
    /// 現状では綺麗にアルファがかからないのでアルファで消すとかはやならいほうがよい
    /// </summary>
    public class UGUIButton3D : UGUIButton
    {
        [SerializeField]
        UGUIImage buttonImage = null;

        [SerializeField]
        UGUIImage shadowImage = null;

        protected override void OnClick()
        {
            Button.interactable = false;
            Vector3 startPos = buttonImage.transform.position;
            Vector3 endPos = shadowImage.transform.position;

            StartCoroutine(KKUtilities.FloatLerp(0.15f, (t) =>
            {
                buttonImage.transform.position = Vector3.Lerp(startPos, endPos, Easing.Yoyo(t));
            }, false).OnCompleted(() =>
            {
                base.OnClick();
                Button.interactable = true;
                this.gameObject.SetActive(false);
            }));
        }
    }
}
