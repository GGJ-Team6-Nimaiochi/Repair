using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class Window : UGUIParts
{
    [SerializeField]
    UGUIImage image = null;
    [SerializeField]
    CanvasGroup container = null;

    public GameObject Container { get { return container.gameObject; } }
    
    public CanvasGroup Group { get { return container; } }

    public override Color Color
    {
        get
        {
            return image.Color;
        }

        set
        {
            image.Color = value;
        }
    }
    public override float Alpha
    {
        get
        {
            return Group.alpha;
        }

        set
        {
            if (Alpha == value) return;
            Group.alpha = value;
        }
    }
}
