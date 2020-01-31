using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class NumberText : MonoBehaviour
    {
        AbstractUGUIText text;
        protected AbstractUGUIText Text
        {
            get
            {
                if (text != null) return text;
                text = GetComponent<AbstractUGUIText>();
                return text;
            }
        }

        protected int value;

        public virtual void SetValue(int value, bool isUpdate = false)
        {
            if (!isUpdate && this.value == value) return;
            this.value = value;
            Text.Text = value.ToString();
        }

        public virtual int GetValue()
        {
            return value;
        }
    }
}