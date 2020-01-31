using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class Text3D : AbstractTextMesh
    {
        [SerializeField]
        TextMesh textMesh = null;
        public TextMesh TextMesh
        {
            get
            {
                if (textMesh == null)
                {
                    textMesh = GetComponent<TextMesh>();
                }
                return textMesh;
            }
        }

        public override string Text
        {
            get
            {
                return TextMesh.text;
            }
            set
            {
                if (Text == value) return;
                TextMesh.text = value;
            }
        }
        public override Color Color
        {
            get
            {
                return TextMesh.color;
            }
            set
            {
                if (Color == value) return;

                TextMesh.color = value;
            }
        }
        public override int FontSize
        {
            get
            {
                return TextMesh.fontSize;
            }

            set
            {
                if (FontSize == value) return;
                TextMesh.fontSize = value;
            }
        }
        public override TextAnchor Alignment
        {
            get
            {
                return TextMesh.anchor;
            }
            set
            {
                if (Alignment == value) return;
                
                TextMesh.anchor = value;
            }
        }
    }
}