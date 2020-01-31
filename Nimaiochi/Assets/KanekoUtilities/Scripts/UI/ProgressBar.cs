using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        UGUIImage bar = null;

        float progress = 0.0f;
        public float Progress
        {
            get { return progress; }
            set
            {
                if (progress == value) return;

                progress = value;
                bar.Image.fillAmount = progress;
            }
        }
    }
}