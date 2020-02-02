using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRenderer : MonoBehaviour
{
    [SerializeField]
    Renderer bookRend = null;
    [SerializeField]
    Renderer pageRend = null;

    public void SetTextrue(Texture2D frontEnd, Texture2D moveFront, Texture2D moveBack, Texture2D backEnd)
    {
        //bookRend.materials[0].mainTexture = frontEnd;
        //pageRend.materials[0].mainTexture = moveFront;
        //bookRend.materials[2].mainTexture = backEnd;
        //pageRend.materials[2].mainTexture = moveBack;
    }
}

