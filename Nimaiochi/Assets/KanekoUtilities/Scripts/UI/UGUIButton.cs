using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Button))]
    public class UGUIButton : UGUIParts
    {
        Button button;
        public Button Button
        {
            get
            {
                if(button == null)
                {
                    button = GetComponent<Button>();
                }

                return button;
            }
        }

        Dictionary<UGUIParts, Color> defaultColorDictionary = new Dictionary<UGUIParts, Color>();

        public bool Interactable
        {
            get
            {
                return Button.interactable;
            }
            set
            {
                Button.interactable = value;

                //if(Button.transition != Selectable.Transition.ColorTint) return;

                Color tempColor = value ? Button.colors.normalColor : Button.colors.disabledColor;
                foreach(var key in defaultColorDictionary.Keys)
                {
                    key.Color = defaultColorDictionary[key] * tempColor;
                }
            }
        }

        public UnityEvent OnClickEvent = new UnityEvent();

        public void AddListener(UnityAction action)
        {
            OnClickEvent.AddListener(action);
        }

        protected virtual void OnClick()
        {
            OnClickEvent.Invoke();
        }

        public override Color Color
        {
            get
            {
                return Button.targetGraphic.color;
            }
            set
            {
                if(Color == value) return;
                Button.image.color = value;
            }
        }

        void Awake()
        {
            UGUIParts[] childParts = GetComponentsInChildren<UGUIParts>();
            for(int i = 0 ; i < childParts.Length ; i++)
            {
                defaultColorDictionary.Add(childParts[i], childParts[i].Color);
            }

            Interactable = Button.interactable;

            button.onClick.AddListener(OnClick);
        }
    }
}