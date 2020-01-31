using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{    
    [RequireComponent(typeof(Text))]
    public class UGUITextUnity : AbstractUGUIText
    {
        Text message;
        public Text Message
        {
            get
            {
                if (message == null)
                {
                    message = GetComponent<Text>();
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
                return Message.fontSize;
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
                return Message.alignment;
            }
            set
            {
                if (Alignment == value) return;
                Message.alignment = value;
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