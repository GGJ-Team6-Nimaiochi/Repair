using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class ScoreText : NumberText
    {
        //何桁まで表示するか？
        [SerializeField]
        int maxDigit = 7;

        public override void SetValue(int value, bool isUpdate = false)
        {
            if (!isUpdate && this.value == value) return;
            this.value = value;
            Text.Text = value.ToString().PadLeft(maxDigit, '0');
        }
    }
}