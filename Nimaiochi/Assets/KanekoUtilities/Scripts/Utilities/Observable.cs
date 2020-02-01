using System;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class ObservableParameter<T>
    {
        protected T value;

        Action<T> onUpdate;
        Action<T, T> onValueChanged;

        public ObservableParameter()
        {
            value = default(T);
        }
        public ObservableParameter(T defaultValue)
        {
            value = defaultValue;
        }

        public void Subscribe(Action<T> onUpdate)
        {
            onUpdate.SafeInvoke(value);
            this.onUpdate += onUpdate;
        }
        public void Subscribe(Action<T, T> onValueChanged)
        {
            onValueChanged.SafeInvoke(value, value);
            this.onValueChanged += onValueChanged;
        }

        public void SetValue(T value, bool isForcibly = false)
        {
            if (!isForcibly && value.Equals(this.value)) return;

            onValueChanged.SafeInvoke(this.value, value);
            this.value = value;
            onUpdate.SafeInvoke(value);
        }
        public T GetValue()
        {
            return value;
        }
    }
}