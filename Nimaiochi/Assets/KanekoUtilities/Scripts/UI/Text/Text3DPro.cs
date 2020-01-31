using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KanekoUtilities
{
    [RequireComponent(typeof(TextMeshPro))]
    public class Text3DPro : AbstractTextMesh
    {
        TextMeshPro textMesh;
        public TextMeshPro TextMesh
        {
            get
            {
                if (textMesh == null)
                {
                    textMesh = GetComponent<TextMeshPro>();
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
                return (int)TextMesh.fontSize;
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
                return TextUtil.TextOptionToTextAnchor(TextMesh.alignment);
            }
            set
            {
                if (Alignment == value) return;
                 TextMesh.alignment = TextUtil.TextAnchorToTextOption(value);
            }
        }
    }
}