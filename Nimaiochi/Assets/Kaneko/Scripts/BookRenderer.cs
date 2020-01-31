using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRenderer : MonoBehaviour
{
    [SerializeField]
    Renderer leftPageRenderer = null;
    [SerializeField]
    Renderer rightPageRenderer = null;

    [SerializeField]
    Renderer movePageRenderer = null;
    [SerializeField]
    Renderer movePageBackRenderer = null;

    public void SetTextrue(Texture2D left, Texture2D move, Texture2D moveBack, Texture2D right)
    {
        leftPageRenderer.material.mainTexture = left;
        rightPageRenderer.material.mainTexture = right;
        movePageRenderer.material.mainTexture = move;
        movePageBackRenderer.material.mainTexture = moveBack;
    }
}
