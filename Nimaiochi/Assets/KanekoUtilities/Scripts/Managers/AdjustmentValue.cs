using System;
using UnityEngine;

namespace KanekoUtilities
{

    public interface IAdjustmentValue
    {
        AdjustmentValueName Name { get; }
        void Init();
        void LevelUp();
    }

    public class BaseAdjustmentValue<T> : IAdjustmentValue
    {
        [SerializeField]
        AdjustmentValueName name = 0;

        [SerializeField]
        protected T firstValue;
        [SerializeField]
        protected T limitValue;

        //レベルアップするたびにどれだけ変化するかの倍率
        [SerializeField]
        protected float magnification = 1.1f;

        protected enum MagnificationType { multiply, addition }

        [SerializeField]
        protected MagnificationType magnificationType = 0;

        public AdjustmentValueName Name { get { return name; } }
        public T CurrentValue { get; protected set; }
        public event Action<T> OnLevelUp;

        public virtual void Init()
        {
            CurrentValue = firstValue;
        }

        public virtual void LevelUp()
        {
            if (OnLevelUp != null) OnLevelUp.Invoke(CurrentValue);
        }
    }

    //トリガーが発生した時にレベルアップする調整要素
    [Serializable]
    public class AdjustmentValueByTrigger : BaseAdjustmentValue<int>
    {
        public override void LevelUp()
        {
            CurrentValue = (int)(CurrentValue * magnification);

            //上限値を超えていたら
            if (magnification > 1 == CurrentValue >= limitValue)
            {
                CurrentValue = limitValue;
            }

            base.LevelUp();
        }
    }

    //ゲームの時間によってレベルアップする調整要素
    [Serializable]
    public class AdjustmentValueByGameTime : BaseAdjustmentValue<float>
    {
        [SerializeField]
        bool autoStart = true;

        [SerializeField]
        float levelupIntervalTime = 1.0f;

        float elapsedTime = 0.0f;
        bool canLevelUp = false;

        public override void Init()
        {
            base.Init();
            canLevelUp = false;
            if (autoStart) Start();
        }

        public void Start()
        {
            canLevelUp = true;
        }

        public void Update()
        {
            if (!canLevelUp) return;

            elapsedTime += Time.deltaTime;

            if (elapsedTime > levelupIntervalTime)
            {
                elapsedTime = 0.0f;
                LevelUp();
            }
        }

        public override void LevelUp()
        {
            if (magnificationType == MagnificationType.multiply)
                CurrentValue *= magnification;
            else if (magnificationType == MagnificationType.addition)
                CurrentValue += magnification;

            //上限値を超えていたら
            if (limitValue > firstValue == CurrentValue >= limitValue)
            {
                Stop();
            }

            base.LevelUp();
        }

        void Stop()
        {
            canLevelUp = false;
            CurrentValue = limitValue;
        }
    }

    public class BaseAdjustmentValueByScore<T> : BaseAdjustmentValue<T>
    {
        [SerializeField]
        int intervalScore = 10;

        public void UpdateAdjustmentValue(int totalScore)
        {
            int levelUpNum = totalScore / intervalScore;

            CurrentValue = firstValue;

            for (int i = 0; i < levelUpNum; i++)
            {
                LevelUp();
            }
        }
    }
    [Serializable]
    public class IntAdjustmentValueByScore : BaseAdjustmentValueByScore<int>
    {
        public override void LevelUp()
        {
            if (magnificationType == MagnificationType.multiply)
                CurrentValue = (int)(CurrentValue * magnification);
            else if (magnificationType == MagnificationType.addition)
                CurrentValue += (int)magnification;

            //上限値を超えていたら
            if (limitValue > firstValue == CurrentValue >= limitValue)
            {
                CurrentValue = limitValue;
            }

            base.LevelUp();
        }
    }
    [Serializable]
    public class FloatAdjustmentValueByScore : BaseAdjustmentValueByScore<float>
    {
        public override void LevelUp()
        {
            if (magnificationType == MagnificationType.multiply)
                CurrentValue = CurrentValue * magnification;
            else if (magnificationType == MagnificationType.addition)
                CurrentValue += magnification;

            //上限値を超えていたら
            if (limitValue > firstValue == CurrentValue >= limitValue)
            {
                CurrentValue = limitValue;
            }

            base.LevelUp();
        }
    }
}