using System;
using UnityEngine;
using UnityEngine.Events;

namespace KanekoUtilities
{
    public class OKDialog : Dialog
    {
        [SerializeField]
        UGUIButton okButton = null;
        [SerializeField]
        bool autoHide = true;
        
        public void Init(Action onOK)
        {
            okButton.AddListener(()=>
            {
                if(onOK != null) onOK();
                if (autoHide) DialogDisplayer.Instance.HideDialog();
            });
        }

        protected virtual void OnOK()
        {
            if (autoHide)
            {
                DialogDisplayer.Instance.HideDialog();
            }
        }
    }
}