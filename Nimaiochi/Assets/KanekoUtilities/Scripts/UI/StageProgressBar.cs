using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class StageProgressBar : ProgressBar
    {
        [SerializeField]
        Image nextStageCircle = null;
        [SerializeField]
        NumberText currentLevel = null;
        [SerializeField]
        NumberText nextLevel = null;
        [SerializeField]
        Color defaultCircleColor = Color.gray;
        [SerializeField]
        Color stageClearCircleColor = Color.yellow;

        float startValue;
        float stageLength;
        bool isEnd = false;

        public void UpdateProgressBar(float currentValue)
        {
            if (isEnd) return;
            currentValue = Mathf.Abs(currentValue - startValue);
            Progress = currentValue / stageLength;

            if (Progress >= 1.0f)
            {
                isEnd = true;
                nextStageCircle.color = stageClearCircleColor;
            }
        }

        public void Init(int currentLevel, float startValue, float endValue)
        {
            isEnd = false;
            this.startValue = startValue;
            stageLength = Mathf.Abs(endValue - startValue);

            Progress = 0.0f;
            nextStageCircle.color = defaultCircleColor;

            this.currentLevel.SetValue(currentLevel);
            nextLevel.SetValue(currentLevel + 1);
        }
    }
}