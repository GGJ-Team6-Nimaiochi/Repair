using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public interface IUIParts
    {
        Color Color { get; set; }
        float Alpha { get; set; }
    }

    public abstract class UIParts : PoolMonoBehaviour, IUIParts
    {
        public abstract Color Color { get; set; }
        Color tempColor;
        public virtual float Alpha
        {
            get
            {
                return Color.a;
            }

            set
            {
                if (Color.a == value) return;
                tempColor = Color;
                tempColor.a = value;
                Color = tempColor;
            }
        }

        bool isSetDefaultAlpha = false;
        float defaultAlpha;
        public float DefaultAlpha
        {
            get
            {
                if (!isSetDefaultAlpha)
                {
                    isSetDefaultAlpha = true;
                    defaultAlpha = Alpha;
                }
                return defaultAlpha;

            }
        }
    }
}