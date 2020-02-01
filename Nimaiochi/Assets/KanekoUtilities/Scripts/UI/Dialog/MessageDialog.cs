using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class MessageDialog : Dialog
    {
        [SerializeField]
        AbstractUGUIText title = null;

        [SerializeField]
        AbstractUGUIText message = null;

        public void Init(string title, string message)
        {
            this.title.Text = title;
            this.message.Text = message;
        }
    }
}