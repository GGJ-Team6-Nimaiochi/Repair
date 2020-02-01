using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRenderer : MonoBehaviour
{
    [SerializeField]
    Renderer frontEndpaperRend = null;
    [SerializeField]
    Renderer backEndpaperRend = null;
    [SerializeField]
    Renderer moveFrontRend = null;
    [SerializeField]
    Renderer moveBackRend = null;

    public void SetTextrue(Texture2D frontEnd, Texture2D moveFront, Texture2D moveBack, Texture2D backEnd)
    {
        frontEndpaperRend.material.mainTexture = frontEnd;
        moveFrontRend.material.mainTexture = moveFront;
        moveBackRend.material.mainTexture = moveBack;
        backEndpaperRend.material.mainTexture = backEnd;
    }
}

