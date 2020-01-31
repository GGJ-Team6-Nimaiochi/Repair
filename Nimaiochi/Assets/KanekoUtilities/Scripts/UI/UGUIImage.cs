using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Image))]
    public class UGUIImage : UGUIParts
    {
        Image image;
        public Image Image
        {
            get
            {
                if (image == null)
                {
                    image = GetComponent<Image>();
                }
                return image;
            }
        }

        //よく使うものはプロパティにしておく
        public Sprite Sprite
        {
            get
            {
                return Image.sprite;
            }
            set
            {
                if (Sprite == value) return;
                Image.sprite = value;
            }
        }
        public override Color Color
        {
            get
            {
                return Image.color;
            }
            set
            {
                if (Image.color == value) return;
                Image.color = value;
            }
        }
    }
}
