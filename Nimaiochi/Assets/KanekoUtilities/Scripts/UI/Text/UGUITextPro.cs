using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KanekoUtilities
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UGUITextPro : AbstractUGUIText
    {
        TextMeshProUGUI message;
        public TextMeshProUGUI Message
        {
            get
            {
                if (message == null)
                {
                    message = GetComponent<TextMeshProUGUI>();
                }

                return message;
            }
        }

        //よく使うやつはプロパティを用意しておく
        public override string Text
        {
            get
            {
                return Message.text;
            }
            set
            {
                if (Message.text == value) return;
                Message.text = value;
            }
        }
        public override int FontSize
        {
            get
            {
                return (int)Message.fontSize;
            }
            set
            {
                if (FontSize == value) return;
                Message.fontSize = value;
            }
        }
        public override TextAnchor Alignment
        {
            get
            {
                return TextUtil.TextOptionToTextAnchor(Message.alignment);
            }
            set
            {
                if (Alignment == value) return;
                Message.alignment = TextUtil.TextAnchorToTextOption(value);
            }
        }
        public override Color Color
        {
            get
            {
                return Message.color;
            }
            set
            {
                if (Color == value) return;
                Message.color = value;
            }
        }
    }
}